using System;
using System.Windows;
using System.Windows.Media;

namespace Elysium.Theme
{
    public static class Parameters
    {
        static Parameters()
        {
            FontFamily = new FontFamily("Segoe UI");
            HeaderFontSize = 12.0 * (96.0 / 72.0);
            ContentFontSize = 10.0 * (96.0 / 72.0);
            TextFontSize = 9.0 * (96.0 / 72.0);

            DefaultThickness = new Thickness(1.0);
            SemiboldThickness = new Thickness(1.5);
            BoldThickness = new Thickness(2.0);

            DefaultStrokeThickness = 1.0;
            SemiboldStrokeThickness = 1.5;
            BoldStrokeThickness = 2.0;

            DefaultPadding = new Thickness(1.0);
            SemiboldPadding = new Thickness(2.0);
            BoldPadding = new Thickness(5.0);

            DefaultPaddingValue = 1.0;
            SemiboldPaddingValue = 2.0;
            BoldPaddingValue = 5.0;
        }

        #region Font

        public static FontFamily FontFamily { get; set; }

        public static double HeaderFontSize { get; set; }

        public static double ContentFontSize { get; set; }

        public static double TextFontSize { get; set; }

        #endregion

        #region Thickness

        public static Thickness DefaultThickness { get; set; }

        public static Thickness SemiboldThickness { get; set; }

        public static Thickness BoldThickness { get; set; }

        #endregion

        #region Stroke thickness

        public static double DefaultStrokeThickness { get; set; }

        public static double SemiboldStrokeThickness { get; set; }

        public static double BoldStrokeThickness { get; set; }

        #endregion

        #region Padding

        public static Thickness DefaultPadding { get; set; }

        public static Thickness SemiboldPadding { get; set; }

        public static Thickness BoldPadding { get; set; }

        public static double DefaultPaddingValue { get; set; }

        public static double SemiboldPaddingValue { get; set; }

        public static double BoldPaddingValue { get; set; }

        #endregion

        #region Animation

        public static readonly DependencyProperty DefaultDurationProperty =
            DependencyProperty.RegisterAttached("DefaultDuration", typeof(Duration), typeof(Parameters),
                                                new UIPropertyMetadata((new Duration(TimeSpan.Parse("00:00:00.0")))));

        public static Duration GetDefaultDuration(DependencyObject obj)
        {
            return (Duration)obj.GetValue(DefaultDurationProperty);
        }

        public static void SetDefaultDuration(DependencyObject obj, Duration value)
        {
            obj.SetValue(DefaultDurationProperty, value);
        }

        public static readonly DependencyProperty MinimumDurationProperty =
            DependencyProperty.RegisterAttached("MinimumDuration", typeof(Duration), typeof(Parameters),
                                                new UIPropertyMetadata(new Duration(TimeSpan.Parse("00:00:00.2"))));

        public static Duration GetMinimumDurationProperty(DependencyObject obj)
        {
            return (Duration)obj.GetValue(MinimumDurationProperty);
        }

        public static void SetMinimumDurationProperty(DependencyObject obj, Duration value)
        {
            obj.SetValue(MinimumDurationProperty, value);
        }

        public static readonly DependencyProperty OptimumDurationProperty =
            DependencyProperty.RegisterAttached("OptimumDuration", typeof(Duration), typeof(Parameters),
                                                new UIPropertyMetadata(new Duration(TimeSpan.Parse("00:00:00.5"))));

        public static Duration GetOptimumDurationProperty(DependencyObject obj)
        {
            return (Duration)obj.GetValue(OptimumDurationProperty);
        }

        public static void SetOptimumDurationProperty(DependencyObject obj, Duration value)
        {
            obj.SetValue(OptimumDurationProperty, value);
        }

        public static readonly DependencyProperty MaximumDurationProperty =
            DependencyProperty.RegisterAttached("MaximumDuration", typeof(Duration), typeof(Parameters),
                                                new UIPropertyMetadata(new Duration(TimeSpan.Parse("00:00:01.0"))));

        public static Duration GetMaximumDurationProperty(DependencyObject obj)
        {
            return (Duration)obj.GetValue(MaximumDurationProperty);
        }

        public static void SetMaximumDurationProperty(DependencyObject obj, Duration value)
        {
            obj.SetValue(MaximumDurationProperty, value);
        }

        #endregion

        #region CheckBox & RadioButton

        public static readonly DependencyProperty BulletDecoratorSizeProperty =
            DependencyProperty.RegisterAttached("BulletDecoratorSize", typeof(double), typeof(Parameters), new UIPropertyMetadata(14.0));

        public static double GetBulletDecoratorSize(DependencyObject obj)
        {
            return (double)obj.GetValue(BulletDecoratorSizeProperty);
        }

        public static void SetBulletDecoratorSize(DependencyObject obj, double value)
        {
            obj.SetValue(BulletDecoratorSizeProperty, value);
        }

        public static readonly DependencyProperty CheckBoxBulletSizeProperty =
            DependencyProperty.RegisterAttached("CheckBoxBulletSize", typeof(double), typeof(Parameters), new UIPropertyMetadata(10.0));

        public static double GetCheckBoxBulletSize(DependencyObject obj)
        {
            return (double)obj.GetValue(CheckBoxBulletSizeProperty);
        }

        public static void SetCheckBoxBulletSize(DependencyObject obj, double value)
        {
            obj.SetValue(CheckBoxBulletSizeProperty, value);
        }

        public static readonly DependencyProperty RadioButtonBulletSizeProperty =
            DependencyProperty.RegisterAttached("RadioButtonBulletSize", typeof(double), typeof(Parameters), new UIPropertyMetadata(8.0));

        public static double GetRadioButtonBulletSize(DependencyObject obj)
        {
            return (double)obj.GetValue(RadioButtonBulletSizeProperty);
        }

        public static void SetRadioButtonBulletSize(DependencyObject obj, double value)
        {
            obj.SetValue(RadioButtonBulletSizeProperty, value);
        }

        #endregion

        #region ComboBox

        public static readonly DependencyProperty ComboBoxButtonSizeProperty =
            DependencyProperty.RegisterAttached("ComboBoxButtonSize", typeof(double), typeof(Parameters), new UIPropertyMetadata(18.0));

        public static double GetComboBoxButtonSize(DependencyObject obj)
        {
            return (double)obj.GetValue(ComboBoxButtonSizeProperty);
        }

        public static void SetComboBoxButtonSize(DependencyObject obj, double value)
        {
            obj.SetValue(ComboBoxButtonSizeProperty, value);
        }

        public static readonly DependencyProperty ComboBoxArrowSizeProperty =
            DependencyProperty.RegisterAttached("ComboBoxArrowSize", typeof(double), typeof(Parameters), new UIPropertyMetadata(8.0));

        public static double GetComboBoxArrowSize(DependencyObject obj)
        {
            return (double)obj.GetValue(ComboBoxArrowSizeProperty);
        }

        public static void SetComboBoxArrowSize(DependencyObject obj, double value)
        {
            obj.SetValue(ComboBoxArrowSizeProperty, value);
        }

        public static readonly DependencyProperty ComboBoxArrowMarginProperty =
            DependencyProperty.RegisterAttached("ComboBoxArrowMargin", typeof(Thickness), typeof(Parameters), new UIPropertyMetadata(new Thickness(5, 0, 5, 0)));

        public static Thickness GetComboBoxArrowMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(ComboBoxArrowMarginProperty);
        }

        public static void SetComboBoxArrowMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(ComboBoxArrowMarginProperty, value);
        }

        #endregion

        #region Slider

        public static readonly DependencyProperty SliderTrackSizeProperty =
            DependencyProperty.RegisterAttached("SliderTrackSize", typeof(double), typeof(Parameters), new UIPropertyMetadata(4.0));

        public static double GetSliderTrackSize(DependencyObject obj)
        {
            return (double)obj.GetValue(SliderTrackSizeProperty);
        }

        public static void SetSliderTrackSize(DependencyObject obj, double value)
        {
            obj.SetValue(SliderTrackSizeProperty, value);
        }

        public static readonly DependencyProperty SliderThumbThicknessProperty =
            DependencyProperty.RegisterAttached("SliderThumbThickness", typeof(double), typeof(Parameters), new UIPropertyMetadata(6.0));

        public static double GetSliderThumbThickness(DependencyObject obj)
        {
            return (double)obj.GetValue(SliderThumbThicknessProperty);
        }

        public static void SetSliderThumbThickness(DependencyObject obj, double value)
        {
            obj.SetValue(SliderThumbThicknessProperty, value);
        }

        #endregion

        #region ProgressBar

        public static readonly DependencyProperty ProgressBarLoadingElementSizeProperty =
            DependencyProperty.RegisterAttached("ProgressBarLoadingElementSize", typeof(double), typeof(Parameters), new UIPropertyMetadata(4.0));

        public static double GetProgressBarLoadingElementSize(DependencyObject obj)
        {
            return (double)obj.GetValue(ProgressBarLoadingElementSizeProperty);
        }

        public static void SetProgressBarLoadingElementSize(DependencyObject obj, double value)
        {
            obj.SetValue(ProgressBarLoadingElementSizeProperty, value);
        }

        public static readonly DependencyProperty ProgressBarLoadingElementStartPositionProperty =
            DependencyProperty.RegisterAttached("ProgressBarLoadingElementStartPosition", typeof(double), typeof(Parameters), new UIPropertyMetadata(-5.0));

        public static double GetProgressBarLoadingElementStartPosition(DependencyObject obj)
        {
            return (double)obj.GetValue(ProgressBarLoadingElementStartPositionProperty);
        }

        public static void SetProgressBarLoadingElementStartPosition(DependencyObject obj, double value)
        {
            obj.SetValue(ProgressBarLoadingElementStartPositionProperty, value);
        }

        #endregion

        #region ScrollBar & ScrollViewer

        public static readonly DependencyProperty ScrollBarArrowSizeProperty =
            DependencyProperty.RegisterAttached("ScrollBarArrowSize", typeof(double), typeof(Parameters), new UIPropertyMetadata(6.0));

        public static double GetScrollBarArrowSize(DependencyObject obj)
        {
            return (double)obj.GetValue(ScrollBarArrowSizeProperty);
        }

        public static void SetScrollBarArrowSize(DependencyObject obj, double value)
        {
            obj.SetValue(ScrollBarArrowSizeProperty, value);
        }

        public static readonly DependencyProperty ScrollViewerShadowSizeProperty =
            DependencyProperty.RegisterAttached("ScrollViewerShadowSize", typeof(double), typeof(Parameters), new UIPropertyMetadata(5.0));

        public static double GetScrollViewerShadowSize(DependencyObject obj)
        {
            return (double)obj.GetValue(ScrollViewerShadowSizeProperty);
        }

        public static void SetScrollViewerShadowSize(DependencyObject obj, double value)
        {
            obj.SetValue(ScrollViewerShadowSizeProperty, value);
        }

        #endregion

        #region TabControl

        public static readonly DependencyProperty TabControlIndicatorThicknessProperty =
            DependencyProperty.RegisterAttached("TabControlIndicatorThickness", typeof(Thickness), typeof(Parameters), new UIPropertyMetadata(new Thickness()));

        public static Thickness GetTabControlIndicatorThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(TabControlIndicatorThicknessProperty);
        }

        public static void SetTabControlIndicatorThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(TabControlIndicatorThicknessProperty, value);
        }

        #endregion

        #region Menu

        public static readonly DependencyProperty SubmenuItemBulletSizeProperty =
            DependencyProperty.RegisterAttached("SubmenuItemBulletSize", typeof(double), typeof(Parameters), new UIPropertyMetadata(12.0));

        public static double GetSubmenuItemBulletSize(DependencyObject obj)
        {
            return (double)obj.GetValue(SubmenuItemBulletSizeProperty);
        }

        public static void SetSubmenuItemBulletSize(DependencyObject obj, double value)
        {
            obj.SetValue(SubmenuItemBulletSizeProperty, value);
        }

        public static readonly DependencyProperty SubmenuHeaderArrowSizeProperty =
            DependencyProperty.RegisterAttached("SubmenuHeaderArrowSize", typeof(double), typeof(Parameters), new UIPropertyMetadata(8.0));

        public static double GetSubmenuHeaderArrowSize(DependencyObject obj)
        {
            return (double)obj.GetValue(SubmenuHeaderArrowSizeProperty);
        }

        public static void SetSubmenuHeaderArrowSize(DependencyObject obj, double value)
        {
            obj.SetValue(SubmenuHeaderArrowSizeProperty, value);
        }

        public static readonly DependencyProperty SubmenuHeaderArrowMarginProperty =
            DependencyProperty.RegisterAttached("SubmenuHeaderArrowMargin", typeof(Thickness), typeof(Parameters),
                                                new UIPropertyMetadata(new Thickness(3, 0, 3, 0)));

        public static Thickness GetSubmenuHeaderArrowMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(SubmenuHeaderArrowMarginProperty);
        }

        public static void SetSubmenuHeaderArrowMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(SubmenuHeaderArrowMarginProperty, value);
        }

        #endregion
    }
} ;