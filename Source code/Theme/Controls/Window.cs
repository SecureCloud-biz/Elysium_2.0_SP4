using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Elysium.Theme.Controls
{
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

        static Window()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata(typeof(Window)));
        }

        public Window()
        {
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, (sender, e) => Close()));

            var isMainWindow = false;
            Application.Current.Dispatcher.Invoke(new Action(() => isMainWindow = Equals(Application.Current.MainWindow, this)));
            if (isMainWindow)
            {
                SetIsMainWindow(this, true);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _applicationBarHost = (Decorator)Template.FindName(ApplicationBarHost, this);
            if (_applicationBarHost == null)
                throw new NullReferenceException("Invalid template. " + ApplicationBarHost + " must be declared.");
            var applicationBar = GetApplicationBar(this);
            if (applicationBar != null)
            {
                _applicationBarHost.Child = applicationBar;
            }

            _caption = (FrameworkElement)Template.FindName(CaptionName, this);
            if (_caption == null)
                throw new NullReferenceException("Invalid template. " + CaptionName + " must be declared.");
            _caption.SizeChanged += (sender, e) =>
                                        {
                                            NonclientWidth = e.NewSize.Width;
                                            NonclientHeight = e.NewSize.Height;
                                        };
        }

        public static readonly DependencyProperty ProgressPercentProperty =
            DependencyProperty.Register("ProgressPercent", typeof(double), typeof(Window),
                                        new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.None));

        public double ProgressPercent
        {
            get { return (double)GetValue(ProgressPercentProperty); }
            set { SetValue(ProgressPercentProperty, value); }
        }

        public static readonly DependencyProperty NonclientWidthProperty =
            DependencyProperty.Register("NonclientWidth", typeof(double), typeof(Window),
                                        new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double NonclientWidth
        {
            get { return (double)GetValue(NonclientWidthProperty); }
            set { SetValue(NonclientWidthProperty, value); }
        }

        public static readonly DependencyProperty NonclientHeightProperty =
            DependencyProperty.Register("NonclientHeight", typeof(double), typeof(Window),
                                        new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double NonclientHeight
        {
            get { return (double)GetValue(NonclientHeightProperty); }
            set { SetValue(NonclientHeightProperty, value); }
        }

        private static readonly DependencyPropertyKey IsMainWindowPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly("IsMainWindow", typeof(bool), typeof(Window), new FrameworkPropertyMetadata(false));

        public static readonly DependencyProperty IsMainWindowProperty = IsMainWindowPropertyKey.DependencyProperty;

        public static bool GetIsMainWindow(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMainWindowProperty);
        }

        private static void SetIsMainWindow(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMainWindowPropertyKey, value);
        }

        public static readonly DependencyProperty ApplicationBarProperty =
            DependencyProperty.RegisterAttached("ApplicationBar", typeof(ApplicationBar), typeof(Window),
                                                new FrameworkPropertyMetadata((ApplicationBar)null, (OnApplicationBarChanged)));

        public static ApplicationBar GetApplicationBar(DependencyObject obj)
        {
            return (ApplicationBar)obj.GetValue(ApplicationBarProperty);
        }

        public static void SetApplicationBar(DependencyObject obj, ApplicationBar value)
        {
            obj.SetValue(ApplicationBarProperty, value);
        }

        private static void OnApplicationBarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (System.Windows.Window)d;
            if (e.OldValue == null)
            {
                instance.MouseRightButtonUp += OnApplicationBarOpening;
            }
            var window = d as Window;
            if (window != null && window._applicationBarHost != null)
            {
                var newApplicationBar = (ApplicationBar)e.NewValue;
                window._applicationBarHost.Child = newApplicationBar;
            }
        }

        private static void OnApplicationBarOpening(object sender, MouseButtonEventArgs e)
        {
            var applicationBar = GetApplicationBar((System.Windows.Window)sender);
            if (applicationBar != null)
            {
                applicationBar.IsOpen = true;
            }
        }
    }
} ;