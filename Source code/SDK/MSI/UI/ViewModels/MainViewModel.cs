using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using Elysium.SDK.MSI.UI.Enumerations;
using Elysium.SDK.MSI.UI.Models;
using Elysium.SDK.MSI.UI.Native;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Microsoft.Tools.WindowsInstallerXml.Bootstrapper;

using WinForms = System.Windows.Forms;

namespace Elysium.SDK.MSI.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
// ReSharper disable InconsistentNaming
        private static readonly object _lock = new Object();
// ReSharper restore InconsistentNaming

        public MainViewModel()
        {
            if (!IsInDesignMode)
            {
                PropertyChanged += OnPropertyChanged;

                App.Current.ResolveSource += ResolveSource;
                App.Current.DetectBegin += DetectBegin;
                App.Current.DetectMsiFeature += DetectMsiFeature;
                App.Current.DetectPackageComplete += DetectPackageComplete;
                App.Current.DetectComplete += DetectComplete;
                App.Current.PlanPackageBegin += PlanPackageBegin;
                App.Current.PlanMsiFeature += PlanMsiFeature;
                App.Current.PlanComplete += PlanComplete;
                App.Current.Progress += Progress;
                App.Current.CacheAcquireProgress += CacheAcquireProgress;
                App.Current.CacheComplete += CacheComplete;
                App.Current.ExecuteMsiMessage += ExecuteMsiMessage;
                App.Current.ExecuteProgress += ExecuteProgress;
                App.Current.ApplyComplete += ApplyComplete;
                App.Current.Error += Error;
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == GetPropertyName(() => State))
            {
                switch (State)
                {
                    case InstallationState.Initializing:
                        CurrentScreen = Screen.Initializing;
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
                    case InstallationState.Applying:
                        CurrentScreen = Screen.Progress;
                        break;
                    case InstallationState.Successful:
                        CurrentScreen = Screen.Final;
                        break;
                    case InstallationState.Failed:
                        CurrentScreen = Canceled ? Screen.Canceled : Screen.Fail;
                        break;
                }

                RaisePropertyChanged(() => ShowBack);
                RaisePropertyChanged(() => ShowNext);
                RaisePropertyChanged(() => ShowInstall);
                RaisePropertyChanged(() => ShowModify);
                RaisePropertyChanged(() => ShowRepair);
                RaisePropertyChanged(() => ShowUninstall);
            }
        }

        public Screen CurrentScreen
        {
            get
            {
                if (IsInDesignMode)
                {
                    return Screen.Primary;
                }
                return _currentScreen;
            }
            private set
            {
                if (_currentScreen != value)
                {
                    _currentScreen = value;
                    RaisePropertyChanged(() => CurrentScreen);
                    RaisePropertyChanged(() => ShowBack);
                    RaisePropertyChanged(() => ShowNext);
                    RaisePropertyChanged(() => ShowInstall);
                    RaisePropertyChanged(() => ShowCancel);
                    RaisePropertyChanged(() => ShowFinish);
                }
            }
        }

        private Screen _currentScreen;

        public string DestinationFolder
        {
            get
            {
                var defaultDestinationFolder = (Environment.Is64BitOperatingSystem && !Environment.Is64BitProcess
                                                    ? Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)
                                                    : Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)) + @"\Elysium SDK\.NET Framework 4\1.5\";
                if (IsInDesignMode)
                {
                    return defaultDestinationFolder;
                }
                return App.Current.Engine.StringVariables.Contains("WixBundleLayoutDirectory")
                           ? App.Current.Engine.StringVariables["WixBundleLayoutDirectory"]
                           : defaultDestinationFolder;
            }
            set
            {
                App.Current.Engine.StringVariables["WixBundleLayoutDirectory"] = value;
                RaisePropertyChanged(() => DestinationFolder);
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
                return _browse ?? (_browse = new RelayCommand(
                                                 () => App.Current.Dispatcher.Invoke(DispatcherPriority.Render, (Action)(() =>
                                                 {
                                                     var browserDialog = new WinForms.FolderBrowserDialog
                                                                             {
                                                                                 RootFolder = Environment.SpecialFolder.MyComputer,
                                                                                 SelectedPath = DestinationFolder
                                                                             };

                                                     var result = browserDialog.ShowDialog();

                                                     if (result == WinForms.DialogResult.OK)
                                                     {
                                                         DestinationFolder = browserDialog.SelectedPath;
                                                     }
                                                 })),
                                                 () => App.Current.Command.Display == Display.Full));
            }
        }

        private ICommand _browse;

        public bool Agreement
        {
            get
            {
                if (IsInDesignMode)
                {
                    return true;
                }
                return _agreement;
            }
            set
            {
                if (_agreement != value)
                {
                    _agreement = value;
                    RaisePropertyChanged(() => Agreement);
                    RaisePropertyChanged(() => Next);
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
                                                             { new Feature(Properties.Resources.Notifications, true), false },
                                                             { new Feature(Properties.Resources.Documentation_en, true), true },
                                                             { new Feature(Properties.Resources.Documentation_ru, true), false },
                                                             { new Feature(Properties.Resources.Test, true), false }
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
                            var @checked = _features.All<KeyValuePair<Feature, bool>>(feature => !feature.Key.AllowAbsent || feature.Value);
                            var @unchecked = _features.All<KeyValuePair<Feature, bool>>(feature => !feature.Key.AllowAbsent || !feature.Value);
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
            get
            {
                if (IsInDesignMode)
                {
                    return null;
                }
                return _isAllFeaturesSelected;
            }
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
                            App.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                            {
                                _isProcessingFeatures = true;
                                var features = new LinkedList<ObservableKeyValuePair<Feature, bool>>(Features);
                                foreach (var feature in features.Where(feature => feature.Key.AllowAbsent))
                                {
                                    Features[feature.Key] = value.Value;
                                }
                                _isProcessingFeatures = false;
                            }));
                        }
                        _isAllFeaturesSelected = value;
                        RaisePropertyChanged(() => IsAllFeaturesSelected);
                    }
                }
            }
        }

        private bool? _isAllFeaturesSelected;

        public double AcquiringProgress
        {
            get
            {
                if (IsInDesignMode)
                {
                    return 100;
                }
                return _acquiringProgress;
            }
            private set
            {
                _acquiringProgress = value;
                RaisePropertyChanged(() => AcquiringProgress);
            }
        }

        private double _acquiringProgress;

        public string AcquiringMember
        {
            get
            {
                if (IsInDesignMode)
                {
                    return "Acquiring member";
                }
                return _acquiringMember;
            }
            private set
            {
                if (_acquiringMember != value)
                {
                    _acquiringMember = value;
                    RaisePropertyChanged(() => AcquiringMember);
                }
            }
        }

        private string _acquiringMember;

        public double ApplyingProgress
        {
            get
            {
                if (IsInDesignMode)
                {
                    return 75;
                }
                return _applyingProgress;
            }
            private set
            {
                _applyingProgress = value;
                RaisePropertyChanged(() => ApplyingProgress);
            }
        }

        private double _applyingProgress;

        public string ApplyingMember
        {
            get
            {
                if (IsInDesignMode)
                {
                    return "Applying member";
                }
                return _applyingMember;
            }
            private set
            {
                if (_applyingMember != value)
                {
                    _applyingMember = value;
                    RaisePropertyChanged(() => ApplyingMember);
                }
            }
        }

        private string _applyingMember;

        public string Message
        {
            get
            {
                if (IsInDesignMode)
                {
                    return "Message";
                }
                return _message;
            }
            private set
            {
                if (_message != value)
                {
                    _message = value;
                    RaisePropertyChanged(() => Message);
                }
            }
        }

        private string _message;

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
                return State == InstallationState.DetectedAbsent && CurrentScreen == Screen.Features;
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
                return State == InstallationState.DetectedAbsent && CurrentScreen == Screen.Primary;
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
                return _install ?? (_install = new RelayCommand(() =>
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
                return State == InstallationState.DetectedAbsent && CurrentScreen == Screen.Features;
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
                return _modify ?? (_modify = new RelayCommand(() =>
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
                return State == InstallationState.DetectedPresent;
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
                return _repair ?? (_repair = new RelayCommand(() =>
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
                //if (IsInDesignMode)
                //{
                //    return true;
                //}
                //return State == InstallationState.DetectedPresent;
                //BUG in Windows Installer XML: http://sourceforge.net/tracker/?func=detail&aid=3538732&group_id=105970&atid=642714
                return false;
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
                return _uninstall ?? (_uninstall = new RelayCommand(() =>
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
                return State == InstallationState.DetectedPresent;
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
                return _cancel ?? (_cancel = new RelayCommand(() =>
                {
                    lock (_lock)
                    {
                        Canceled = MessageBoxResult.Yes ==
                                   MessageBox.Show("Are you sure you want to cancel?", "Elysium SDK for .NET Framework 4", MessageBoxButton.YesNo, MessageBoxImage.Error);
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
                    RaisePropertyChanged(() => Canceled);
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
                return CurrentScreen == Screen.Final || CurrentScreen == Screen.Canceled || CurrentScreen == Screen.Fail;
            }
        }

        private LaunchAction Action
        {
            get { return _action; }
            set
            {
                if (_action != value)
                {
                    _action = value;
                    RaisePropertyChanged(() => Action);
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
                    RaisePropertyChanged(() => State);
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
                    RaisePropertyChanged(() => PreApplyState);
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
            if (e.PackageId.Equals("Elysium.SDK", StringComparison.Ordinal))
            {
                App.Current.Dispatcher.Invoke(DispatcherPriority.Render, (Action)(() =>
                {
// ReSharper disable ConvertToLambdaExpression
                    Features.Add(new Feature(e.FeatureId, !e.FeatureId.Equals("Elysium", StringComparison.Ordinal)), e.FeatureId.Equals("Elysium", StringComparison.Ordinal) || e.State == FeatureState.Local);
// ReSharper restore ConvertToLambdaExpression
                }));
            }
        }

        private void DetectPackageComplete(object sender, DetectPackageCompleteEventArgs e)
        {
            if (e.PackageId.Equals("Elysium.SDK", StringComparison.Ordinal))
            {
                State = e.State == PackageState.Present ? InstallationState.DetectedPresent : InstallationState.DetectedAbsent;
            }
        }

        private void DetectComplete(object sender, DetectCompleteEventArgs e)
        {
            if (HResult.Succeeded(e.Status))
            {
                if (App.Current.Command.Action == LaunchAction.Layout)
                {
                    if (String.IsNullOrEmpty(App.Current.Command.LayoutDirectory))
                    {
                        DestinationFolder = (Environment.Is64BitOperatingSystem && !Environment.Is64BitProcess
                                                 ? Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)
                                                 : Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)) + @"\Elysium SDK\.NET Framework 4\1.5\";

                        if (App.Current.Command.Display == Display.Full)
                        {
                            App.Current.Dispatcher.Invoke(DispatcherPriority.Send, (Action)(() => Plan(App.Current.Command.Action)));
                        }
                    }
                    else
                    {
                        DestinationFolder = App.Current.Command.LayoutDirectory;

                        App.Current.Dispatcher.Invoke(DispatcherPriority.Send, (Action)(() => Plan(App.Current.Command.Action)));
                    }
                }
                else if (App.Current.Command.Display != Display.Full)
                {
                    App.Current.Engine.Log(LogLevel.Verbose, "Invoking automatic plan for non-interactive mode.");
                    App.Current.Dispatcher.Invoke(DispatcherPriority.Send, (Action)(() => Plan(App.Current.Command.Action)));
                }
            }
            else
            {
                State = InstallationState.Failed;
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
            else if (e.PackageId.Equals("Elysium", StringComparison.Ordinal))
            {
                var action = Action == LaunchAction.Unknown ? App.Current.Command.Action : Action;
                switch (action)
                {
                    case LaunchAction.Modify:
                        e.State = RequestState.Present;
                        break;
                    case LaunchAction.Repair:
                        e.State = RequestState.Repair;
                        break;
                }
            }
        }

        private void PlanMsiFeature(object sender, PlanMsiFeatureEventArgs e)
        {
            if (e.PackageId.Equals("Elysium.SDK", StringComparison.Ordinal))
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

        private void Progress(object sender, ProgressEventArgs e)
        {
            lock (_lock)
            {
                e.Result = Canceled ? Result.Cancel : Result.Ok;
            }
        }

        private void CacheAcquireProgress(object sender, CacheAcquireProgressEventArgs e)
        {
            lock (_lock)
            {
                AcquiringMember = e.PackageOrContainerId.Equals("Elysium.SDK", StringComparison.Ordinal) ? "Elysium SDK for .NET Framework 4" : e.PackageOrContainerId;
                AcquiringProgress = e.OverallPercentage;
                e.Result = Canceled ? Result.Cancel : Result.Ok;
            }
        }

        private void CacheComplete(object sender, CacheCompleteEventArgs cacheCompleteEventArgs)
        {
            lock (_lock)
            {
                AcquiringProgress = 100d;
            }
        }

        private void ExecuteMsiMessage(object sender, ExecuteMsiMessageEventArgs e)
        {
            lock (_lock)
            {
                Message = e.Message;
                ApplyingMember = e.PackageId.Equals("Elysium.SDK", StringComparison.Ordinal) ? "Elysium SDK for .NET Framework 4" : e.PackageId;
                e.Result = Canceled ? Result.Cancel : Result.Ok;
            }
        }

        private void ExecuteProgress(object sender, ExecuteProgressEventArgs e)
        {
            lock (_lock)
            {
                ApplyingProgress = e.OverallPercentage;

                if (App.Current.Command.Display == Display.Embedded)
                {
                    App.Current.Engine.SendEmbeddedProgress(e.ProgressPercentage, (int)ApplyingProgress);
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
                State = HResult.Succeeded(e.Status) ? InstallationState.Successful : InstallationState.Failed;
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
                            var buttons = WinForms.MessageBoxButtons.OK;
                            switch (e.UIHint & 0xF)
                            {
                                case 0:
                                    buttons = WinForms.MessageBoxButtons.OK;
                                    break;
                                case 1:
                                    buttons = WinForms.MessageBoxButtons.OKCancel;
                                    break;
                                case 2:
                                    buttons = WinForms.MessageBoxButtons.AbortRetryIgnore;
                                    break;
                                case 3:
                                    buttons = WinForms.MessageBoxButtons.YesNoCancel;
                                    break;
                                case 4:
                                    buttons = WinForms.MessageBoxButtons.YesNo;
                                    break;
                            }

                            var result = WinForms.MessageBox.Show(e.ErrorMessage, @"Elysium SDK for .NET Framework 4", buttons, WinForms.MessageBoxIcon.Error);

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
    }
} ;