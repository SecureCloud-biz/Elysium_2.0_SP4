using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace Elysium.Theme.WPF.Controls
{
    [TemplatePart(Name = TitleName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = CaptionName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = MinimizeName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = MaximizeRestoreName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = CloseName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = GripName, Type = typeof(ResizeGrip))]
    public class Window : System.Windows.Window
    {
        private const string TitleName = "PART_Title";
        private const string CaptionName = "PART_Caption";
        private const string MinimizeName = "PART_Minimize";
        private const string MaximizeRestoreName = "PART_MaximizeRestore";
        private const string CloseName = "PART_Close";
        private const string GripName = "PART_Grip";

        private FrameworkElement _caption;

        static Window()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata(typeof(Window)));
        }

        public Window()
        {
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, (sender, e) => Close()));
            SetBinding(TitleVisibilityProperty,
                       new Binding { Source = Application.Current.MainWindow.GetType() == GetType(), Converter = new BooleanToVisibilityConverter() });
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _caption = GetTemplateChild(CaptionName) as FrameworkElement;
            if (_caption != null)
                _caption.SizeChanged += (sender, e) =>
                                            {
                                                NonclientWidth = e.NewSize.Width;
                                                NonclientHeight = e.NewSize.Height;
                                            };
        }

        public static readonly DependencyProperty NonclientWidthProperty =
            DependencyProperty.Register("NonclientWidth", typeof(double), typeof(Window), new UIPropertyMetadata(0.0));

        public double NonclientWidth
        {
            get { return (double)GetValue(NonclientWidthProperty); }
            set { SetValue(NonclientWidthProperty, value); }
        }

        public static readonly DependencyProperty NonclientHeightProperty =
            DependencyProperty.Register("NonclientHeight", typeof(double), typeof(Window), new UIPropertyMetadata(0.0));

        public double NonclientHeight
        {
            get { return (double)GetValue(NonclientHeightProperty); }
            set { SetValue(NonclientHeightProperty, value); }
        }

        public static readonly DependencyProperty TitleVisibilityProperty =
            DependencyProperty.Register("TitleVisibility", typeof(Visibility), typeof(Window), new UIPropertyMetadata(Visibility.Visible));

        public Visibility TitleVisibility
        {
            get { return (Visibility)GetValue(TitleVisibilityProperty); }
            set { SetValue(TitleVisibilityProperty, value); }
        }
    }
} ;