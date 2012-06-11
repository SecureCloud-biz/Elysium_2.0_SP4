using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;

using Elysium.Extensions;

using JetBrains.Annotations;

using Microsoft.Windows.Shell;

namespace Elysium.Controls
{
    [PublicAPI]
    [TemplatePart(Name = LayoutRootName, Type = typeof(Panel))]
    [TemplatePart(Name = CaptionName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = TitleName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ProgressBarName, Type = typeof(LinearProgressBar))]
    [TemplatePart(Name = MinimizeName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = MaximizeRestoreName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = CloseName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ApplicationBarHost, Type = typeof(Decorator))]
    [TemplatePart(Name = GripName, Type = typeof(ResizeGrip))]
    public class Window : System.Windows.Window
    {
        private const string LayoutRootName = "PART_LayoutRoot";
        private const string TitleName = "PART_Title";
        private const string CaptionName = "PART_Caption";
        private const string ProgressBarName = "PART_ProgressBar";
        private const string MinimizeName = "PART_Minimize";
        private const string MaximizeRestoreName = "PART_MaximizeRestore";
        private const string CloseName = "PART_Close";
        private const string ApplicationBarHost = "PART_ApplicationBarHost";
        private const string GripName = "PART_Grip";

        private FrameworkElement _caption;
        private Decorator _applicationBarHost;

        private WindowChrome _chrome;

        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static Window()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata(typeof(Window)));
        }

        public Window()
        {
            _chrome = new WindowChrome
                          {
                              CaptionHeight = SystemParameters2.Current.WindowCaptionHeight,
                              CornerRadius = new CornerRadius(0d),
                              GlassFrameThickness = new Thickness(0d),
                              NonClientFrameEdges = NonClientFrameEdges.None,
                              ResizeBorderThickness = Parameters.GetWindowResizeBorderThickness(this),
                              UseAeroCaptionButtons = false
                          };
            _chrome.Freeze();
            if (WindowChrome.GetWindowChrome(this) == null)
            {
                WindowChrome.SetWindowChrome(this, _chrome);
            }

            var resizeBorderThicknessPropertyDescriptor =
                DependencyPropertyDescriptor.FromProperty(Parameters.WindowResizeBorderThicknessProperty, typeof(Window));
            resizeBorderThicknessPropertyDescriptor.AddValueChanged(this, delegate
            {
                if (WindowChrome.GetWindowChrome(this) == _chrome)
                {
                    _chrome = new WindowChrome
                    {
                        CaptionHeight = _chrome.CaptionHeight,
                        CornerRadius = _chrome.CornerRadius,
                        GlassFrameThickness = _chrome.GlassFrameThickness,
                        NonClientFrameEdges = _chrome.NonClientFrameEdges,
                        ResizeBorderThickness = Parameters.GetWindowResizeBorderThickness(this),
                        UseAeroCaptionButtons = _chrome.UseAeroCaptionButtons
                    };
                    _chrome.Freeze();
                    WindowChrome.SetWindowChrome(this, _chrome);
                }
            });

            CommandBindings.Add(new CommandBinding(WindowCommands.Minimize, (sender, e) => WindowState = WindowState.Minimized));
            CommandBindings.Add(new CommandBinding(WindowCommands.Maximize, (sender, e) => WindowState = WindowState.Maximized));
            CommandBindings.Add(new CommandBinding(WindowCommands.Restore, (sender, e) => WindowState = WindowState.Normal));
            CommandBindings.Add(new CommandBinding(WindowCommands.Close, (sender, e) => Close()));
        }

        [PublicAPI]
        public static readonly DependencyProperty IsMainWindowProperty =
            DependencyProperty.RegisterAttached("IsMainWindow", typeof(bool), typeof(Window),
                                                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, OnIsMainWindowChanged));

        [PublicAPI]
        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Window))]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static bool GetIsMainWindow([NotNull] System.Windows.Window obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return BooleanBoxingHelper.Unbox(obj.GetValue(IsMainWindowProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetIsMainWindow([NotNull] System.Windows.Window obj, bool value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(IsMainWindowProperty, BooleanBoxingHelper.Box(value));
        }

        private static void OnIsMainWindowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (System.Windows.Window)d;
            if (instance != null && e.NewValue == BooleanBoxingHelper.TrueBox)
            {
                Action setMainWindow =
                    () =>
                    {
                        foreach (var window in Application.Current.Windows.AsParallel().Cast<Window>().Where(window => window != instance))
                        {
                            SetIsMainWindow(window, false);
                        }
                    };
                Application.Current.Dispatcher.BeginInvoke(setMainWindow, DispatcherPriority.Render);
            }
        }

        [PublicAPI]
        public static readonly DependencyProperty ProgressProperty =
            DependencyProperty.Register("Progress", typeof(double), typeof(Window),
                                        new FrameworkPropertyMetadata(100d, FrameworkPropertyMetadataOptions.None, OnProgressChanged));

        [PublicAPI]
        [Category("Appearance")]
        [Description("The current magnitude of the progress bar that placed in the top of window.")]
        public double Progress
        {
            get { return BoxingHelper<double>.Unbox(GetValue(ProgressProperty)); }
            set { SetValue(ProgressProperty, value); }
        }

        private static void OnProgressChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var instance = (Window)obj;
            instance.OnProgressChanged((double)e.OldValue, (double)e.NewValue);
        }

        [PublicAPI]
// ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnProgressChanged(double oldProgress, double newProgress)
// ReSharper restore VirtualMemberNeverOverriden.Global
        {
        }

        [PublicAPI]
        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(Window),
                                        new FrameworkPropertyMetadata(BooleanBoxingHelper.FalseBox, FrameworkPropertyMetadataOptions.None, OnIsBusyChanged));

        [PublicAPI]
        [Category("Appearance")]
        [Description("Indicates busy state of window.")]
        public bool IsBusy
        {
            get { return BooleanBoxingHelper.Unbox(GetValue(IsBusyProperty)); }
            set { SetValue(IsBusyProperty, BooleanBoxingHelper.Box(value)); }
        }

        private static void OnIsBusyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var instance = (Window)obj;
            instance.OnIsBusyChanged(BooleanBoxingHelper.Unbox(e.OldValue), BooleanBoxingHelper.Unbox(e.NewValue));
        }

        [PublicAPI]
// ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnIsBusyChanged(bool oldIsBusy, bool newIsBusy)
// ReSharper restore VirtualMemberNeverOverriden.Global
        {
        }

        [PublicAPI]
        public static readonly DependencyProperty HasDropShadowProperty =
            DependencyProperty.Register("HasDropShadow", typeof(bool), typeof(Window),
                                        new FrameworkPropertyMetadata(BooleanBoxingHelper.FalseBox, FrameworkPropertyMetadataOptions.None,
                                                                      OnHasDropShadowChanged, CoerceHasDropShadow));

        [PublicAPI]
        [Category("Appearance")]
        [Description("Indicates has window drop shadow or not.")]
        public bool HasDropShadow
        {
            get { return BooleanBoxingHelper.Unbox(GetValue(HasDropShadowProperty)); }
            set { SetValue(HasDropShadowProperty, BooleanBoxingHelper.Box(value)); }
        }

        private static void OnHasDropShadowChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var instance = (Window)obj;
            instance.OnHasDropShadowChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        [PublicAPI]
// ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnHasDropShadowChanged(bool oldHasDropShadow, bool newHasDropShadow)
// ReSharper restore VirtualMemberNeverOverriden.Global
        {
            if (WindowChrome.GetWindowChrome(this) == _chrome)
            {
                _chrome = new WindowChrome
                              {
                                  CaptionHeight = _chrome.CaptionHeight,
                                  CornerRadius = _chrome.CornerRadius,
                                  GlassFrameThickness = !newHasDropShadow ? new Thickness(0d) : new Thickness(0d, 0d, 0d, 1d),
                                  NonClientFrameEdges = _chrome.NonClientFrameEdges,
                                  ResizeBorderThickness = _chrome.ResizeBorderThickness,
                                  UseAeroCaptionButtons = _chrome.UseAeroCaptionButtons
                              };
                _chrome.Freeze();
                WindowChrome.SetWindowChrome(this, _chrome);
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private static object CoerceHasDropShadow(DependencyObject obj, object basevalue)
        {
            ValidationHelper.NotNull(obj, () => obj);
            try
            {
                // NOTE: Ignore Code Contracts warnings
                return BooleanBoxingHelper.Unbox(basevalue) && Environment.OSVersion.Version.Major >= 6;
            }
            catch
            {
                return basevalue;
            }
        }

        [PublicAPI]
        public static readonly DependencyProperty ApplicationBarProperty =
            DependencyProperty.RegisterAttached("ApplicationBar", typeof(ApplicationBar), typeof(Window),
                                                new FrameworkPropertyMetadata(null, (OnApplicationBarChanged)));

        [PublicAPI]
        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Window))]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static ApplicationBar GetApplicationBar([NotNull] System.Windows.Window obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return (ApplicationBar)obj.GetValue(ApplicationBarProperty);
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetApplicationBar([NotNull] System.Windows.Window obj, ApplicationBar value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(ApplicationBarProperty, value);
        }

        private static void OnApplicationBarChanged([NotNull] DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ValidationHelper.NotNull(obj, () => obj);
            var instance = obj as System.Windows.Window;
            if (instance != null && e.OldValue == null)
            {
                instance.MouseRightButtonUp += OnApplicationBarOpening;
            }
            var window = obj as Window;
            if (window != null && window._applicationBarHost != null)
            {
                var newApplicationBar = (ApplicationBar)e.NewValue;
                window._applicationBarHost.Child = newApplicationBar;
            }
        }

        private static void OnApplicationBarOpening(object sender, MouseButtonEventArgs e)
        {
            var window = sender as System.Windows.Window;
            var source = e.OriginalSource as UIElement;
            if (window != null && source != null && !ApplicationBar.GetPreventsOpen(source))
            {
                var applicationBar = GetApplicationBar(window);
                if (applicationBar != null)
                {
                    applicationBar.IsOpen = true;
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (Template != null)
            {
                _applicationBarHost = Template.FindName(ApplicationBarHost, this) as Decorator;
                if (_applicationBarHost == null)
                {
                    Trace.TraceWarning(ApplicationBarHost + " not found.");
                }
                else
                {
                    var applicationBar = GetApplicationBar(this);
                    if (applicationBar != null)
                    {
                        _applicationBarHost.Child = applicationBar;
                    }
                }

                // NOTE: Lack of contracts: FindName is pure method
                Contract.Assume(Template != null);
                _caption = Template.FindName(CaptionName, this) as FrameworkElement;
                if (_caption == null)
                {
                    Trace.TraceError(CaptionName + " not found.");
                }
                else
                {
                    _caption.SizeChanged += (sender, e) =>
                    {
                        if (WindowChrome.GetWindowChrome(this) == _chrome && e.HeightChanged)
                        {
                            _chrome = new WindowChrome
                                          {
                                              CaptionHeight = e.NewSize.Height,
                                              CornerRadius = _chrome.CornerRadius,
                                              GlassFrameThickness = _chrome.GlassFrameThickness,
                                              NonClientFrameEdges = _chrome.NonClientFrameEdges,
                                              ResizeBorderThickness = _chrome.ResizeBorderThickness,
                                              UseAeroCaptionButtons = _chrome.UseAeroCaptionButtons
                                          };
                            _chrome.Freeze();
                            WindowChrome.SetWindowChrome(this, _chrome);
                        }
                    };
                }
            }
        }
    }
} ;