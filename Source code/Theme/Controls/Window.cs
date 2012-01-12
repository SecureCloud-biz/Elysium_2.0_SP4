using System;
using System.Diagnostics.Contracts;
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static Window()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata(typeof(Window)));
        }

        public Window()
        {
            Contract.Assume(CommandBindings != null);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, (sender, e) => Close()));

            Contract.Assume(Application.Current != null);
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

            if (Template != null)
            {
                _applicationBarHost = (Decorator)Template.FindName(ApplicationBarHost, this);
                if (_applicationBarHost != null)
                {
                    var applicationBar = GetApplicationBar(this);
                    if (applicationBar != null)
                    {
                        _applicationBarHost.Child = applicationBar;
                    }
                }

                Contract.Assume(Template != null);
                _caption = (FrameworkElement)Template.FindName(CaptionName, this);
                if (_caption != null)
                {
                    _caption.SizeChanged += (sender, e) =>
                                                {
                                                    NonClientWidth = e.NewSize.Width;
                                                    NonClientHeight = e.NewSize.Height;
                                                };
                }
            }
        }

        public static readonly DependencyProperty ProgressPercentProperty =
            DependencyProperty.Register("ProgressPercent", typeof(double), typeof(Window),
                                        new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.None));

        public double ProgressPercent
        {
            get
            {
                var value = GetValue(ProgressPercentProperty);
                Contract.Assume(value != null);
                return (double)value;
            }
            set { SetValue(ProgressPercentProperty, value); }
        }

        public static readonly DependencyProperty NonClientWidthProperty =
            DependencyProperty.Register("NonClientWidth", typeof(double), typeof(Window),
                                        new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double NonClientWidth
        {
            get
            {
                var value = GetValue(NonClientWidthProperty);
                Contract.Assume(value != null);
                return (double)value;
            }
            set { SetValue(NonClientWidthProperty, value); }
        }

        public static readonly DependencyProperty NonClientHeightProperty =
            DependencyProperty.Register("NonClientHeight", typeof(double), typeof(Window),
                                        new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double NonClientHeight
        {
            get
            {
                var value = GetValue(NonClientHeightProperty);
                Contract.Assume(value != null);
                return (double)value;
            }
            set { SetValue(NonClientHeightProperty, value); }
        }

        private static readonly DependencyPropertyKey IsMainWindowPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly("IsMainWindow", typeof(bool), typeof(Window), new FrameworkPropertyMetadata(false));

        public static readonly DependencyProperty IsMainWindowProperty = IsMainWindowPropertyKey.DependencyProperty;

        public static bool GetIsMainWindow(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(IsMainWindowProperty);
            Contract.Assume(value != null);
            return (bool)value;
        }

        private static void SetIsMainWindow(DependencyObject obj, bool value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(IsMainWindowPropertyKey, value);
        }

        public static readonly DependencyProperty ApplicationBarProperty =
            DependencyProperty.RegisterAttached("ApplicationBar", typeof(ApplicationBar), typeof(Window),
                                                new FrameworkPropertyMetadata(null, (OnApplicationBarChanged)));

        public static ApplicationBar GetApplicationBar(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(ApplicationBarProperty);
            Contract.Assume(value != null);
            return (ApplicationBar)value;
        }

        public static void SetApplicationBar(DependencyObject obj, ApplicationBar value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(ApplicationBarProperty, value);
        }

        private static void OnApplicationBarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();
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
            var window = sender as System.Windows.Window;
            if (window == null)
            {
                return;
            }
            var applicationBar = GetApplicationBar(window);
            if (applicationBar != null)
            {
                applicationBar.IsOpen = true;
            }
        }
    }
} ;