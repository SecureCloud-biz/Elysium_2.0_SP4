using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

#if NETFX4
using Elysium.Extensions;
#endif
using Elysium.SDK.MSI.UI.Enumerations;
using Elysium.SDK.MSI.UI.Models;
using Elysium.SDK.MSI.UI.Native;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Microsoft.Tools.WindowsInstallerXml.Bootstrapper;
using ErrorEventArgs = Microsoft.Tools.WindowsInstallerXml.Bootstrapper.ErrorEventArgs;

namespace Elysium.SDK.MSI.UI.ViewModels
{
    using Resources = Properties.Resources;

    public class MainViewModel : ViewModelBase
    {
        private bool _downgrade;

        // ReSharper disable InconsistentNaming
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1311:StaticReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "Suppression is OK here.")]
        private static readonly object _lock = new object();

        // ReSharper restore InconsistentNaming

        public MainViewModel()
        {
            if (!IsInDesignMode)
            {
                PropertyChanged += OnPropertyChanged;

                App.Current.ResolveSource += ResolveSource;
                App.Current.DetectBegin += DetectBegin;
                App.Current.DetectMsiFeature += DetectMsiFeature;
                App.Current.DetectRelatedBundle += DetectRelatedBundle;
                App.Current.DetectPackageComplete += DetectPackageComplete;
                App.Current.DetectComplete += DetectComplete;
                App.Current.PlanPackageBegin += PlanPackageBegin;
                App.Current.PlanMsiFeature += PlanMsiFeature;
                App.Current.PlanComplete += PlanComplete;
                App.Current.Progress += ApplyProgress;
                App.Current.CacheAcquireProgress += CacheAcquireProgress;
                App.Current.CacheComplete += CacheComplete;
                App.Current.ExecuteMsiMessage += ExecuteMsiMessage;
                App.Current.ExecuteProgress += ExecuteProgress;
                App.Current.ApplyComplete += ApplyComplete;
                App.Current.Error += Error;
                App.Current.Shutdown += Shutdown;
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "State")
            {
                switch (State)
                {
                    case InstallationState.Initializing:
                        CurrentScreen = Screen.Initializing;
                        break;
                    case InstallationState.Layout:
                        CurrentScreen = Screen.Layout;
                        break;
                    case InstallationState.DetectedAbsent:
                        if (!(CurrentScreen == Screen.Primary || CurrentScreen == Screen.Features))
                        {
                            CurrentScreen = Screen.Primary;
                        }
                        break;
                    case InstallationState.DetectedPresent:
                        CurrentScreen = Screen.Features;
                        break;
                    case InstallationState.DetectedNewer:
                        CurrentScreen = Screen.Downgrade;
                        break;
                    case InstallationState.Applying:
                        CurrentScreen = Screen.Progress;
                        break;
                    case InstallationState.Successful:
                        CurrentScreen = Screen.Final;
                        break;
                    case InstallationState.Failed:
                        CurrentScreen = Canceled ? Screen.Canceled : Screen.Fail;
                        break;
                    case InstallationState.Help:
                        CurrentScreen = Screen.Help;
                        break;
                    case InstallationState.RebootRequired:
                        CurrentScreen = Screen.Reboot;
                        break;
                }

                RaisePropertyChanged("ShowBack");
                RaisePropertyChanged("ShowNext");
                RaisePropertyChanged("ShowInstall");
                RaisePropertyChanged("ShowModify");
                RaisePropertyChanged("ShowRepair");
                RaisePropertyChanged("ShowUninstall");
                RaisePropertyChanged("ShowDownload");
            }
        }

        public Screen CurrentScreen
        {
            get { return IsInDesignMode ? Screen.Primary : _currentScreen; }

            private set
            {
                if (_currentScreen != value)
                {
                    _currentScreen = value;
                    RaisePropertyChanged("CurrentScreen");
                    RaisePropertyChanged("ShowBack");
                    RaisePropertyChanged("ShowNext");
                    RaisePropertyChanged("ShowInstall");
                    RaisePropertyChanged("ShowModify");
                    RaisePropertyChanged("ShowRepair");
                    RaisePropertyChanged("ShowUninstall");
                    RaisePropertyChanged("ShowDownload");
                    RaisePropertyChanged("ShowCancel");
                    RaisePropertyChanged("ShowFinish");
                }
            }
        }

        private Screen _currentScreen;

        public string InstallFolder
        {
            get
            {
                var installFolder = (Environment.Is64BitOperatingSystem
                                         ? Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
                                         : Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)) +
                                    @"\" + Resources.SDKName + @"\" + Resources.NETFrameworkName + @"\" + Resources.SDKVersion + @"\";
                if (IsInDesignMode)
                {
                    return installFolder;
                }
                return App.Current.Engine.StringVariables.Contains("InstallFolder")
                           ? App.Current.Engine.FormatString("[InstallFolder]")
                           : installFolder;
            }

            set
            {
                App.Current.Engine.StringVariables["InstallFolder"] = value;
                RaisePropertyChanged("InstallFolder");
            }
        }

        public string LayoutFolder
        {
            get
            {
                if (IsInDesignMode)
                {
                    return InstallFolder + @"\Setup\";
                }
                return App.Current.Engine.StringVariables.Contains("WixBundleLayoutDirectory")
                           ? App.Current.Engine.StringVariables["WixBundleLayoutDirectory"]
                           : InstallFolder + @"\Setup\";
            }

            set
            {
                App.Current.Engine.StringVariables["WixBundleLayoutDirectory"] = value;
                RaisePropertyChanged("LayoutFolder");
            }
        }

        public ICommand Browse
        {
            get
            {
                if (IsInDesignMode)
                {
                    return null;
                }
                if (_browse == null)
                {
                    Action browse = delegate
                    {
                        var browserDialog = new System.Windows.Forms.FolderBrowserDialog
                        {
                            RootFolder = Environment.SpecialFolder.MyComputer,
                            SelectedPath =
                                App.Current.Command.Action == LaunchAction.Layout ? InstallFolder : LayoutFolder
                        };

                        var result = browserDialog.ShowDialog();

                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            if (App.Current.Command.Action == LaunchAction.Layout)
                            {
                                LayoutFolder = browserDialog.SelectedPath;
                            }
                            else
                            {
                                InstallFolder = browserDialog.SelectedPath;
                            }
                        }
                    };
                    return _browse = new RelayCommand(browse, () => App.Current.Command.Display == Display.Full);
                }
                return _browse;
            }
        }

        private ICommand _browse;

        public ICommand License
        {
            get
            {
                if (IsInDesignMode)
                {
                    return null;
                }
                return _command ?? (_command = new RelayCommand(delegate
                {
                    var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    if (location != null)
                    {
                        var path = Path.Combine(location, "License.rtf");
                        new Process { StartInfo = { FileName = path, UseShellExecute = true, Verb = "open" } }.Start();
                    }
                }));
            }
        }

        private ICommand _command;

        public bool Agreement
        {
            get { return IsInDesignMode || _agreement; }
            set
            {
                if (_agreement != value)
                {
                    _agreement = value;
                    RaisePropertyChanged("Agreement");
                    RaisePropertyChanged("Next");
                }
            }
        }

        private bool _agreement;

        private bool _isProcessingFeatures;
        private bool _isComputedFeaturesSelected;

        public ObservableDictionary<Feature, bool> Features
        {
            get
            {
                if (IsInDesignMode)
                {
                    return _features ?? (_features = new ObservableDictionary<Feature, bool>
                                                         {
                                                             { new Feature("Elysium", false), true },
                                                             { new Feature(Resources.Notifications, true), false },
                                                             { new Feature(Resources.Documentation_en, true), true },
                                                             { new Feature(Resources.Documentation_ru, true), false },
                                                             { new Feature(Resources.Test, true), false }
                                                         });
                }
                if (_features == null)
                {
                    _features = new ObservableDictionary<Feature, bool>();
                    _features.PropertyChanged += (sender, e) =>
                    {
                        if (e.PropertyName == "Item[]" && !_isProcessingFeatures)
                        {
                            _isProcessingFeatures = true;
                            _isComputedFeaturesSelected = true;
                            var @checked =
                                _features.All<KeyValuePair<Feature, bool>>(feature => !feature.Key.AllowAbsent || feature.Value);
                            var @unchecked =
                                _features.All<KeyValuePair<Feature, bool>>(feature => !feature.Key.AllowAbsent || !feature.Value);
                            IsAllFeaturesSelected = @checked ? true : @unchecked ? false : (bool?)null;
                            _isProcessingFeatures = false;
                        }
                    };
                }
                return _features;
            }
        }

        private ObservableDictionary<Feature, bool> _features;

        public bool? IsAllFeaturesSelected
        {
            get { return IsInDesignMode ? null : _isAllFeaturesSelected; }
            set
            {
                lock (_lock)
                {
                    if (_isComputedFeaturesSelected)
                    {
                        _isComputedFeaturesSelected = false;
                    }
                    else
                    {
                        value = !_isAllFeaturesSelected.HasValue || !_isAllFeaturesSelected.Value;
                    }
                    if (_isAllFeaturesSelected != value)
                    {
                        if (value.HasValue && !_isProcessingFeatures)
                        {
                            App.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate
                            {
                                _isProcessingFeatures = true;
                                var features = new LinkedList<ObservableKeyValuePair<Feature, bool>>(Features);
                                foreach (var feature in features.Where(feature => feature.Key.AllowAbsent))
                                {
                                    Features[feature.Key] = value.Value;
                                }
                                _isProcessingFeatures = false;
                            });
                        }
                        _isAllFeaturesSelected = value;
                        RaisePropertyChanged("IsAllFeaturesSelected");
                    }
                }
            }
        }

        private bool? _isAllFeaturesSelected;

        public int Progress
        {
            get { return IsInDesignMode ? 75 : _progress; }
            private set
            {
                _progress = value;
                RaisePropertyChanged("Progress");
            }
        }

        private int _progress;

        public string Message
        {
            get { return IsInDesignMode ? "Message" : _message; }
            private set
            {
                if (_message != value)
                {
                    _message = value;
                    RaisePropertyChanged("Message");
                }
            }
        }

        private string _message = string.Empty;

        public ICommand Back
        {
            get
            {
                if (IsInDesignMode)
                {
                    return null;
                }
                return _back ?? (_back = new RelayCommand(() => CurrentScreen = Screen.Primary));
            }
        }

        private ICommand _back;

        public bool ShowBack
        {
            get
            {
                if (IsInDesignMode)
                {
                    return true;
                }
                return CurrentScreen == Screen.Features && State == InstallationState.DetectedAbsent;
            }
        }

        public ICommand Next
        {
            get
            {
                if (IsInDesignMode)
                {
                    return null;
                }
                return _next ?? (_next = new RelayCommand(() => CurrentScreen = Screen.Features, () => Agreement));
            }
        }

        private ICommand _next;

        public bool ShowNext
        {
            get
            {
                if (IsInDesignMode)
                {
                    return true;
                }
                return CurrentScreen == Screen.Primary && State == InstallationState.DetectedAbsent;
            }
        }

        public ICommand Install
        {
            get
            {
                if (IsInDesignMode)
                {
                    return null;
                }
                return _install ?? (_install = new RelayCommand(delegate
                {
                    Action = LaunchAction.Install;
                    Plan(LaunchAction.Install);
                }));
            }
        }

        private ICommand _install;

        public bool ShowInstall
        {
            get
            {
                if (IsInDesignMode)
                {
                    return true;
                }
                return CurrentScreen == Screen.Features && State == InstallationState.DetectedAbsent;
            }
        }

        public ICommand Modify
        {
            get
            {
                if (IsInDesignMode)
                {
                    return null;
                }
                return _modify ?? (_modify = new RelayCommand(delegate
                {
                    Action = LaunchAction.Modify;
                    Plan(LaunchAction.Modify);
                }));
            }
        }

        private ICommand _modify;

        public bool ShowModify
        {
            get
            {
                if (IsInDesignMode)
                {
                    return true;
                }
                return CurrentScreen == Screen.Features && State == InstallationState.DetectedPresent;
            }
        }

        public ICommand Repair
        {
            get
            {
                if (IsInDesignMode)
                {
                    return null;
                }
                return _repair ?? (_repair = new RelayCommand(delegate
                {
                    Action = LaunchAction.Repair;
                    Plan(LaunchAction.Repair);
                }));
            }
        }

        private ICommand _repair;

        public bool ShowRepair
        {
            get
            {
                if (IsInDesignMode)
                {
                    return true;
                }
                return CurrentScreen == Screen.Features && State == InstallationState.DetectedPresent;
            }
        }

        public ICommand Uninstall
        {
            get
            {
                if (IsInDesignMode)
                {
                    return null;
                }
                return _uninstall ?? (_uninstall = new RelayCommand(delegate
                {
                    Action = LaunchAction.Uninstall;
                    Plan(LaunchAction.Uninstall);
                }));
            }
        }

        private ICommand _uninstall;

        public bool ShowUninstall
        {
            get
            {
                if (IsInDesignMode)
                {
                    return true;
                }
                return CurrentScreen == Screen.Features && State == InstallationState.DetectedPresent;
            }
        }

        public ICommand Download
        {
            get
            {
                if (IsInDesignMode)
                {
                    return null;
                }
                return _download ?? (_download = new RelayCommand(delegate
                {
                    Action = LaunchAction.Layout;
                    Plan(LaunchAction.Layout);
                }));
            }
        }

        private ICommand _download;

        public bool ShowDownload
        {
            get
            {
                if (IsInDesignMode)
                {
                    return true;
                }
                return CurrentScreen == Screen.Layout && State == InstallationState.Layout;
            }
        }

        public ICommand Cancel
        {
            get
            {
                if (IsInDesignMode)
                {
                    return null;
                }
                return _cancel ?? (_cancel = new RelayCommand(delegate
                {
                    lock (_lock)
                    {
                        Canceled = MessageBoxResult.Yes == MessageBox.Show(Resources.CancellationWarning,
                                                                           string.Format("{0} for {1}", Resources.SDKName, Resources.NETFrameworkName),
                                                                           MessageBoxButton.YesNo, MessageBoxImage.Error);
                    }
                }));
            }
        }

        private ICommand _cancel;

        public bool ShowCancel
        {
            get
            {
                if (IsInDesignMode)
                {
                    return true;
                }
                return CurrentScreen == Screen.Progress;
            }
        }

        private bool Canceled
        {
            get { return _canceled; }
            set
            {
                if (_canceled != value)
                {
                    _canceled = value;
                    RaisePropertyChanged("Canceled");
                }
            }
        }

        private bool _canceled;

        public ICommand Finish
        {
            get
            {
                if (IsInDesignMode)
                {
                    return null;
                }
                return _finish ?? (_finish = new RelayCommand(() => App.Current.Dispatcher.InvokeShutdown()));
            }
        }

        private ICommand _finish;

        public bool ShowFinish
        {
            get
            {
                if (IsInDesignMode)
                {
                    return true;
                }
                return CurrentScreen == Screen.Final || CurrentScreen == Screen.Canceled || CurrentScreen == Screen.Fail || CurrentScreen == Screen.Reboot;
            }
        }

        public bool IsReboot
        {
            get { return IsInDesignMode || _isReboot; }

            set
            {
                _isReboot = value;
                RaisePropertyChanged("IsReboot");
            }
        }

        private bool _isReboot;

        private LaunchAction Action
        {
            get { return _action; }
            set
            {
                if (_action != value)
                {
                    _action = value;
                    RaisePropertyChanged("Action");
                }
            }
        }

        private LaunchAction _action = LaunchAction.Unknown;

        private InstallationState State
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    RaisePropertyChanged("State");
                }
            }
        }

        private InstallationState _state;

        private InstallationState PreApplyState
        {
            get { return _preApplyState; }
            set
            {
                if (_preApplyState != value)
                {
                    _preApplyState = value;
                    RaisePropertyChanged("PreApplyState");
                }
            }
        }

        private InstallationState _preApplyState;

        public void Refresh()
        {
            Canceled = false;
            App.Current.Engine.Detect();
        }

        private void ResolveSource(object sender, ResolveSourceEventArgs e)
        {
            e.Result = !string.IsNullOrEmpty(e.DownloadSource) ? Result.Download : Result.Ok;
        }

        private void DetectBegin(object sender, DetectBeginEventArgs e)
        {
            State = InstallationState.Initializing;
        }

        private void DetectMsiFeature(object sender, DetectMsiFeatureEventArgs e)
        {
            if (e.PackageId.Equals("Elysium.SDK." + (App.Current.Dispatcher.Thread.CurrentCulture.LCID == 1049 ? "ru" : "en"), StringComparison.Ordinal))
            {
                App.Current.Dispatcher.Invoke(
                    DispatcherPriority.Render,
                    (Action)(() => Features.Add(new Feature(e.FeatureId, !e.FeatureId.Equals("Elysium", StringComparison.Ordinal)),
                                                e.FeatureId.Equals("Elysium", StringComparison.Ordinal) || e.State == FeatureState.Local)));
            }
        }

        private void DetectRelatedBundle(object sender, DetectRelatedBundleEventArgs e)
        {
            if (e.Operation == RelatedOperation.Downgrade)
            {
                _downgrade = true;
            }
        }

        private void DetectPackageComplete(object sender, DetectPackageCompleteEventArgs e)
        {
            if (e.PackageId.Equals("Elysium.SDK." + (App.Current.Dispatcher.Thread.CurrentCulture.LCID == 1049 ? "ru" : "en"), StringComparison.Ordinal))
            {
                State = e.State == PackageState.Present ? InstallationState.DetectedPresent : InstallationState.DetectedAbsent;
            }
        }

        private void DetectComplete(object sender, DetectCompleteEventArgs e)
        {
            if (App.Current.Command.Action == LaunchAction.Uninstall)
            {
                App.Current.Engine.Log(LogLevel.Verbose, "Invoking automatic plan for uninstall");
                App.Current.Dispatcher.Invoke(() => Plan(LaunchAction.Uninstall));
            }
            else if (HResult.Succeeded(e.Status))
            {
                if (_downgrade)
                {
                    State = InstallationState.DetectedNewer;
                }

                switch (App.Current.Command.Action)
                {
                    case LaunchAction.Layout:
                        LayoutFolder = string.IsNullOrEmpty(App.Current.Command.LayoutDirectory)
                                           ? InstallFolder + @"\Setup\"
                                           : App.Current.Command.LayoutDirectory;
                        if (App.Current.Command.Display == Display.Full)
                        {
                            State = InstallationState.Layout;
                        }
                        break;
                    case LaunchAction.Help:
                        State = InstallationState.Help;
                        break;
                    default:
                        if (App.Current.Command.Display != Display.Full)
                        {
                            App.Current.Engine.Log(LogLevel.Verbose, "Invoking automatic plan for non-interactive mode.");
                            App.Current.Dispatcher.Invoke(DispatcherPriority.Send, (Action)(() => Plan(App.Current.Command.Action)));
                        }
                        break;
                }
            }
            else
            {
                State = InstallationState.Failed;
            }
        }

        public void ParseCommandLine()
        {
            var args = App.Current.Command.GetCommandLineArgs();
            var lang = args.FirstOrDefault(arg => arg.StartsWith("/lang:", StringComparison.InvariantCultureIgnoreCase));
            if (!string.IsNullOrWhiteSpace(lang))
            {
                // ReSharper disable PossibleNullReferenceException
                var clsid = lang.Split(new[] { ':' })[1];
                // ReSharper restore PossibleNullReferenceException
                Dispatcher.CurrentDispatcher.Thread.CurrentUICulture = CultureInfo.GetCultureInfo(int.Parse(clsid));
            }
        }

        private void Plan(LaunchAction action)
        {
            Canceled = false;
            App.Current.Engine.Plan(action);
        }

        private void PlanPackageBegin(object sender, PlanPackageBeginEventArgs e)
        {
            if (App.Current.Engine.StringVariables.Contains("MbaNetfxPackageId") &&
                e.PackageId.Equals(App.Current.Engine.StringVariables["MbaNetfxPackageId"], StringComparison.Ordinal))
            {
                e.State = RequestState.None;
            }
            else if (e.PackageId.Equals("Elysium.SDK." + (App.Current.Dispatcher.Thread.CurrentCulture.LCID != 1049 ? "ru" : "en"),
                                        StringComparison.Ordinal))
            {
                e.State = RequestState.None;
            }
        }

        private void PlanMsiFeature(object sender, PlanMsiFeatureEventArgs e)
        {
            if (e.PackageId.Equals("Elysium.SDK." + (App.Current.Dispatcher.Thread.CurrentCulture.LCID == 1049 ? "ru" : "en"), StringComparison.Ordinal))
            {
                var @checked = Features.First<KeyValuePair<Feature, bool>>(feature => feature.Key.Name == e.FeatureId).Value;
                var action = Action == LaunchAction.Unknown ? App.Current.Command.Action : Action;
                switch (action)
                {
                    case LaunchAction.Install:
                    case LaunchAction.Modify:
                    case LaunchAction.Repair:
                        e.State = @checked ? FeatureState.Local : FeatureState.Absent;
                        break;
                    case LaunchAction.Uninstall:
                        e.State = @checked ? FeatureState.Absent : FeatureState.Local;
                        break;
                }
            }
        }

        private void PlanComplete(object sender, PlanCompleteEventArgs e)
        {
            if (HResult.Succeeded(e.Status))
            {
                PreApplyState = State;
                State = InstallationState.Applying;
                App.Current.Apply();
            }
            else
            {
                State = InstallationState.Failed;
            }
        }

        private void ApplyProgress(object sender, ProgressEventArgs e)
        {
            lock (_lock)
            {
                e.Result = Canceled ? Result.Cancel : Result.Ok;
            }
        }

        private int _acquireProgress;
        private int _applyProgress;

        private void CacheAcquireProgress(object sender, CacheAcquireProgressEventArgs e)
        {
            lock (_lock)
            {
                _acquireProgress = e.OverallPercentage;
                Progress = (_acquireProgress + _applyProgress) / (App.Current.Command.Action == LaunchAction.Layout ? 1 : 2);
                e.Result = Canceled ? Result.Cancel : Result.Ok;
            }
        }

        private void CacheComplete(object sender, CacheCompleteEventArgs cacheCompleteEventArgs)
        {
            lock (_lock)
            {
                _acquireProgress = 100;
                Progress = (_acquireProgress + _applyProgress) / (App.Current.Command.Action == LaunchAction.Layout ? 1 : 2);
            }
        }

        private void ExecuteMsiMessage(object sender, ExecuteMsiMessageEventArgs e)
        {
            lock (_lock)
            {
                if (e.MessageType == InstallMessage.ActionStart && e.Data != null && e.Data.Count > 1 && !string.IsNullOrWhiteSpace(e.Data[1]))
                {
                    Message = e.Data[1];
                }
                e.Result = Canceled ? Result.Cancel : Result.Ok;
            }
        }

        private void ExecuteProgress(object sender, ExecuteProgressEventArgs e)
        {
            lock (_lock)
            {
                _applyProgress = e.OverallPercentage;
                Progress = (_acquireProgress + _applyProgress) / 2;

                if (App.Current.Command.Display == Display.Embedded)
                {
                    App.Current.Engine.SendEmbeddedProgress(e.ProgressPercentage, Progress);
                }

                e.Result = Canceled ? Result.Cancel : Result.Ok;
            }
        }

        private void ApplyComplete(object sender, ApplyCompleteEventArgs e)
        {
            App.Current.Result = e.Status;

            if (App.Current.Command.Display != Display.Full)
            {
                if (App.Current.Command.Display == Display.Passive)
                {
                    App.Current.Engine.Log(LogLevel.Verbose, "Automatically closing the window for non-interactive install");
                    App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() => App.Current.Finish()));
                }
                else
                {
                    App.Current.Dispatcher.InvokeShutdown();
                }
            }

            if (State != PreApplyState)
            {
                State = e.Restart != ApplyRestart.None
                            ? InstallationState.RebootRequired
                            : HResult.Succeeded(e.Status) ? InstallationState.Successful : InstallationState.Failed;
            }
        }

        private void Error(object sender, ErrorEventArgs e)
        {
            lock (_lock)
            {
                if (!Canceled)
                {
                    if (State == InstallationState.Applying && e.ErrorCode == (int)SystemErrorCodes.ERROR_CANCELLED)
                    {
                        State = PreApplyState;
                    }
                    else
                    {
                        Message = e.ErrorMessage;

                        if (App.Current.Command.Display == Display.Full)
                        {
                            var buttons = System.Windows.Forms.MessageBoxButtons.OK;
                            switch (e.UIHint & 0xF)
                            {
                                case 0:
                                    buttons = System.Windows.Forms.MessageBoxButtons.OK;
                                    break;
                                case 1:
                                    buttons = System.Windows.Forms.MessageBoxButtons.OKCancel;
                                    break;
                                case 2:
                                    buttons = System.Windows.Forms.MessageBoxButtons.AbortRetryIgnore;
                                    break;
                                case 3:
                                    buttons = System.Windows.Forms.MessageBoxButtons.YesNoCancel;
                                    break;
                                case 4:
                                    buttons = System.Windows.Forms.MessageBoxButtons.YesNo;
                                    break;
                            }

                            var result = System.Windows.Forms.MessageBox.Show(e.ErrorMessage, string.Format("{0} for {1}", Resources.SDKName, Resources.NETFrameworkName),
                                                                  buttons, System.Windows.Forms.MessageBoxIcon.Error);

                            if ((int)buttons == (e.UIHint & 0xF))
                            {
                                e.Result = (Result)result;
                            }
                        }
                    }
                }
                else
                {
                    e.Result = Result.Cancel;
                }
            }
        }

        private void Shutdown(object sender, ShutdownEventArgs e)
        {
            e.Result = IsReboot ? Result.Restart : Result.Close;
        }
    }
}