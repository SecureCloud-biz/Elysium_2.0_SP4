using System;
using System.Windows;
using System.Windows.Media;

namespace Elysium.Theme
{
    public class Parameters : DependencyObject
    {
        public static readonly Parameters Instance = new Parameters();

        #region Font

        public static readonly DependencyProperty FontFamilyProperty =
            DependencyProperty.Register("FontFamily", typeof(FontFamily), typeof(Parameters),
                                        new FrameworkPropertyMetadata(new FontFamily("Segoe UI"),
                                                                      FrameworkPropertyMetadataOptions.AffectsRender |
                                                                      FrameworkPropertyMetadataOptions.AffectsMeasure));

        public FontFamily FontFamily
        {
            get { return (FontFamily)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public static readonly DependencyProperty TitleFontSizeProperty =
            DependencyProperty.Register("TitleFontSize", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(12.0 * (96.0 / 72.0),
                                                                      FrameworkPropertyMetadataOptions.AffectsRender |
                                                                      FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double TitleFontSize
        {
            get { return (double)GetValue(TitleFontSizeProperty); }
            set { SetValue(TitleFontSizeProperty, value); }
        }

        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.Register("HeaderFontSize", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(16.0 * (96.0 / 72.0),
                                                                      FrameworkPropertyMetadataOptions.AffectsRender |
                                                                      FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double HeaderFontSize
        {
            get { return (double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }

        public static readonly DependencyProperty ContentFontSizeProperty =
            DependencyProperty.Register("ContentFontSize", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(10.0 * (96.0 / 72.0),
                                                                      FrameworkPropertyMetadataOptions.AffectsRender |
                                                                      FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double ContentFontSize
        {
            get { return (double)GetValue(ContentFontSizeProperty); }
            set { SetValue(ContentFontSizeProperty, value); }
        }

        public static readonly DependencyProperty TextFontSizeProperty =
            DependencyProperty.Register("TextFontSize", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(9.0 * (96.0 / 72.0),
                                                                      FrameworkPropertyMetadataOptions.AffectsRender |
                                                                      FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double TextFontSize
        {
            get { return (double)GetValue(TextFontSizeProperty); }
            set { SetValue(TextFontSizeProperty, value); }
        }

        #endregion

        #region Thickness

        public static readonly DependencyProperty DefaultThicknessProperty =
            DependencyProperty.Register("DefaultThickness", typeof(Thickness), typeof(Parameters),
                                        new FrameworkPropertyMetadata(new Thickness(1.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Thickness DefaultThickness
        {
            get { return (Thickness)GetValue(DefaultThicknessProperty); }
            set { SetValue(DefaultThicknessProperty, value); }
        }

        public static readonly DependencyProperty SemiboldThicknessProperty =
            DependencyProperty.Register("SemiboldThickness", typeof(Thickness), typeof(Parameters),
                                        new FrameworkPropertyMetadata(new Thickness(1.5), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Thickness SemiboldThickness
        {
            get { return (Thickness)GetValue(SemiboldThicknessProperty); }
            set { SetValue(SemiboldThicknessProperty, value); }
        }

        public static readonly DependencyProperty BoldThicknessProperty =
            DependencyProperty.Register("BoldThickness", typeof(Thickness), typeof(Parameters),
                                        new FrameworkPropertyMetadata(new Thickness(2.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Thickness BoldThickness
        {
            get { return (Thickness)GetValue(BoldThicknessProperty); }
            set { SetValue(BoldThicknessProperty, value); }
        }

        public static readonly DependencyProperty DefaultThicknessValueProperty =
            DependencyProperty.Register("DefaultThicknessValue", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double DefaultThicknessValue
        {
            get { return (double)GetValue(DefaultThicknessValueProperty); }
            set { SetValue(DefaultThicknessValueProperty, value); }
        }

        public static readonly DependencyProperty SemiboldThicknessValueProperty =
            DependencyProperty.Register("SemiboldThicknessValue", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(1.5, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double SemiboldThicknessValue
        {
            get { return (double)GetValue(SemiboldThicknessValueProperty); }
            set { SetValue(SemiboldThicknessValueProperty, value); }
        }

        public static readonly DependencyProperty BoldThicknessValueProperty =
            DependencyProperty.Register("BoldThicknessValue", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(2.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double BoldThicknessValue
        {
            get { return (double)GetValue(BoldThicknessValueProperty); }
            set { SetValue(BoldThicknessValueProperty, value); }
        }

        #endregion

        #region Padding

        public static readonly DependencyProperty DefaultPaddingProperty =
            DependencyProperty.Register("DefaultPadding", typeof(Thickness), typeof(Parameters),
                                        new FrameworkPropertyMetadata(new Thickness(1.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Thickness DefaultPadding
        {
            get { return (Thickness)GetValue(DefaultPaddingProperty); }
            set { SetValue(DefaultPaddingProperty, value); }
        }

        public static readonly DependencyProperty SemiboldPaddingProperty =
            DependencyProperty.Register("SemiboldPadding", typeof(Thickness), typeof(Parameters),
                                        new FrameworkPropertyMetadata(new Thickness(2.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Thickness SemiboldPadding
        {
            get { return (Thickness)GetValue(SemiboldPaddingProperty); }
            set { SetValue(SemiboldPaddingProperty, value); }
        }

        public static readonly DependencyProperty BoldPaddingProperty =
            DependencyProperty.Register("BoldPadding", typeof(Thickness), typeof(Parameters),
                                        new FrameworkPropertyMetadata(new Thickness(5.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Thickness BoldPadding
        {
            get { return (Thickness)GetValue(BoldPaddingProperty); }
            set { SetValue(BoldPaddingProperty, value); }
        }

        public static readonly DependencyProperty DefaultPaddingValueProperty =
            DependencyProperty.Register("DefaultPaddingValue", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double DefaultPaddingValue
        {
            get { return (double)GetValue(DefaultPaddingValueProperty); }
            set { SetValue(DefaultPaddingValueProperty, value); }
        }

        public static readonly DependencyProperty SemiboldPaddingValueProperty =
            DependencyProperty.Register("SemiboldPaddingValue", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(2.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double SemiboldPaddingValue
        {
            get { return (double)GetValue(SemiboldPaddingValueProperty); }
            set { SetValue(SemiboldPaddingValueProperty, value); }
        }

        public static readonly DependencyProperty BoldPaddingValueProperty =
            DependencyProperty.Register("BoldPaddingValue", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(5.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double BoldPaddingValue
        {
            get { return (double)GetValue(BoldPaddingValueProperty); }
            set { SetValue(BoldPaddingValueProperty, value); }
        }

        #endregion

        #region Animation

        public static readonly DependencyProperty DefaultDurationProperty =
            DependencyProperty.RegisterAttached("DefaultDuration", typeof(Duration), typeof(Parameters),
                                                new FrameworkPropertyMetadata((new Duration(TimeSpan.Parse("00:00:00.0"))),
                                                                              FrameworkPropertyMetadataOptions.None));

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
                                                new FrameworkPropertyMetadata(new Duration(TimeSpan.Parse("00:00:00.2")),
                                                                              FrameworkPropertyMetadataOptions.None));

        public static Duration GetMinimumDuration(DependencyObject obj)
        {
            return (Duration)obj.GetValue(MinimumDurationProperty);
        }

        public static void SetMinimumDuration(DependencyObject obj, Duration value)
        {
            obj.SetValue(MinimumDurationProperty, value);
        }

        public static readonly DependencyProperty OptimumDurationProperty =
            DependencyProperty.RegisterAttached("OptimumDuration", typeof(Duration), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Duration(TimeSpan.Parse("00:00:00.5")),
                                                                              FrameworkPropertyMetadataOptions.None));

        public static Duration GetOptimumDuration(DependencyObject obj)
        {
            return (Duration)obj.GetValue(OptimumDurationProperty);
        }

        public static void SetOptimumDuration(DependencyObject obj, Duration value)
        {
            obj.SetValue(OptimumDurationProperty, value);
        }

        public static readonly DependencyProperty MaximumDurationProperty =
            DependencyProperty.RegisterAttached("MaximumDuration", typeof(Duration), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Duration(TimeSpan.Parse("00:00:01.0")),
                                                                              FrameworkPropertyMetadataOptions.None));

        public static Duration GetMaximumDuration(DependencyObject obj)
        {
            return (Duration)obj.GetValue(MaximumDurationProperty);
        }

        public static void SetMaximumDuration(DependencyObject obj, Duration value)
        {
            obj.SetValue(MaximumDurationProperty, value);
        }

        #endregion

        #region Buttons

        public static readonly DependencyProperty CommandButtonMaskProperty =
            DependencyProperty.RegisterAttached("CommandButtonMask", typeof(Brush), typeof(Parameters),
                                                new FrameworkPropertyMetadata((Brush)null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                           FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        public static Brush GetCommandButtonMask(DependencyObject obj)
        {
            return (Brush)obj.GetValue(CommandButtonMaskProperty);
        }

        public static void SetCommandButtonMask(DependencyObject obj, Brush value)
        {
            obj.SetValue(CommandButtonMaskProperty, value);
        }

        #endregion

        #region CheckBox & RadioButton

        public static readonly DependencyProperty BulletDecoratorSizeProperty =
            DependencyProperty.RegisterAttached("BulletDecoratorSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(14.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static double GetBulletDecoratorSize(DependencyObject obj)
        {
            return (double)obj.GetValue(BulletDecoratorSizeProperty);
        }

        public static void SetBulletDecoratorSize(DependencyObject obj, double value)
        {
            obj.SetValue(BulletDecoratorSizeProperty, value);
        }

        public static readonly DependencyProperty CheckBoxBulletSizeProperty =
            DependencyProperty.RegisterAttached("CheckBoxBulletSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static double GetCheckBoxBulletSize(DependencyObject obj)
        {
            return (double)obj.GetValue(CheckBoxBulletSizeProperty);
        }

        public static void SetCheckBoxBulletSize(DependencyObject obj, double value)
        {
            obj.SetValue(CheckBoxBulletSizeProperty, value);
        }

        public static readonly DependencyProperty RadioButtonBulletSizeProperty =
            DependencyProperty.RegisterAttached("RadioButtonBulletSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(8.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

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
            DependencyProperty.RegisterAttached("ComboBoxButtonSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(18.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static double GetComboBoxButtonSize(DependencyObject obj)
        {
            return (double)obj.GetValue(ComboBoxButtonSizeProperty);
        }

        public static void SetComboBoxButtonSize(DependencyObject obj, double value)
        {
            obj.SetValue(ComboBoxButtonSizeProperty, value);
        }

        public static readonly DependencyProperty ComboBoxArrowSizeProperty =
            DependencyProperty.RegisterAttached("ComboBoxArrowSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(8.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static double GetComboBoxArrowSize(DependencyObject obj)
        {
            return (double)obj.GetValue(ComboBoxArrowSizeProperty);
        }

        public static void SetComboBoxArrowSize(DependencyObject obj, double value)
        {
            obj.SetValue(ComboBoxArrowSizeProperty, value);
        }

        public static readonly DependencyProperty ComboBoxArrowMarginProperty =
            DependencyProperty.RegisterAttached("ComboBoxArrowMargin", typeof(Thickness), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Thickness(5, 0, 5, 0), FrameworkPropertyMetadataOptions.AffectsMeasure));

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
            DependencyProperty.RegisterAttached("SliderTrackSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(4.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static double GetSliderTrackSize(DependencyObject obj)
        {
            return (double)obj.GetValue(SliderTrackSizeProperty);
        }

        public static void SetSliderTrackSize(DependencyObject obj, double value)
        {
            obj.SetValue(SliderTrackSizeProperty, value);
        }

        public static readonly DependencyProperty SliderThumbThicknessProperty =
            DependencyProperty.RegisterAttached("SliderThumbThickness", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(6.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

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
            DependencyProperty.RegisterAttached("ProgressBarLoadingElementSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(4.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static double GetProgressBarLoadingElementSize(DependencyObject obj)
        {
            return (double)obj.GetValue(ProgressBarLoadingElementSizeProperty);
        }

        public static void SetProgressBarLoadingElementSize(DependencyObject obj, double value)
        {
            obj.SetValue(ProgressBarLoadingElementSizeProperty, value);
        }

        public static readonly DependencyProperty CircularProgressBarThicknessProperty =
            DependencyProperty.RegisterAttached("CircularProgressBarThickness", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(3.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static double GetCircularProgressBarThickness(DependencyObject obj)
        {
            return (double)obj.GetValue(CircularProgressBarThicknessProperty);
        }

        public static void SetCircularProgressBarThickness(DependencyObject obj, double value)
        {
            obj.SetValue(CircularProgressBarThicknessProperty, value);
        }

        #endregion

        #region ScrollBar

        public static readonly DependencyProperty ScrollBarArrowSizeProperty =
            DependencyProperty.RegisterAttached("ScrollBarArrowSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(6.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static double GetScrollBarArrowSize(DependencyObject obj)
        {
            return (double)obj.GetValue(ScrollBarArrowSizeProperty);
        }

        public static void SetScrollBarArrowSize(DependencyObject obj, double value)
        {
            obj.SetValue(ScrollBarArrowSizeProperty, value);
        }

        #endregion

        #region TabControl

        public static readonly DependencyProperty TabControlIndicatorBrushProperty =
            DependencyProperty.RegisterAttached("TabControlIndicatorBrush", typeof(Brush), typeof(Parameters),
                                                new FrameworkPropertyMetadata((Brush)null,
                                                                              FrameworkPropertyMetadataOptions.AffectsRender |
                                                                              FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        public static Brush GetTabControlIndicatorBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(TabControlIndicatorBrushProperty);
        }

        public static void SetTabControlIndicatorBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(TabControlIndicatorBrushProperty, value);
        }

        public static readonly DependencyProperty TabControlIndicatorThicknessProperty =
            DependencyProperty.RegisterAttached("TabControlIndicatorThickness", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(2.0,
                                                                              FrameworkPropertyMetadataOptions.AffectsRender |
                                                                              FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static Thickness GetTabControlIndicatorThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(TabControlIndicatorThicknessProperty);
        }

        public static void SetTabControlIndicatorThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(TabControlIndicatorThicknessProperty, value);
        }

        public static readonly DependencyProperty TabItemHeaderStyleProperty =
            DependencyProperty.RegisterAttached("TabItemHeaderStyle", typeof(Style), typeof(Parameters),
                                                new FrameworkPropertyMetadata((Style)null, FrameworkPropertyMetadataOptions.AffectsRender));

        public static Style GetTabItemHeaderStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(TabItemHeaderStyleProperty);
        }

        public static void SetTabItemHeaderStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(TabItemHeaderStyleProperty, value);
        }

        #endregion

        #region Menu

        public static readonly DependencyProperty SubmenuItemBulletSizeProperty =
            DependencyProperty.RegisterAttached("SubmenuItemBulletSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(12.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static double GetSubmenuItemBulletSize(DependencyObject obj)
        {
            return (double)obj.GetValue(SubmenuItemBulletSizeProperty);
        }

        public static void SetSubmenuItemBulletSize(DependencyObject obj, double value)
        {
            obj.SetValue(SubmenuItemBulletSizeProperty, value);
        }

        public static readonly DependencyProperty SubmenuHeaderArrowSizeProperty =
            DependencyProperty.RegisterAttached("SubmenuHeaderArrowSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(8.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

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
                                                new FrameworkPropertyMetadata(new Thickness(3, 0, 3, 0), FrameworkPropertyMetadataOptions.AffectsMeasure));

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