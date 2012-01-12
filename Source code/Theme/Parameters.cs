using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Media;

namespace Elysium.Theme
{
    public sealed class Parameters : DependencyObject
    {
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Parameters is singleton")]
        public static readonly Parameters Instance = new Parameters();

        private Parameters()
        {
        }

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
            get
            {
                var value = GetValue(TitleFontSizeProperty);
                Contract.Assume(value != null);
                return (double)value;
            }
            set { SetValue(TitleFontSizeProperty, value); }
        }

        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.Register("HeaderFontSize", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(16.0 * (96.0 / 72.0),
                                                                      FrameworkPropertyMetadataOptions.AffectsRender |
                                                                      FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double HeaderFontSize
        {
            get
            {
                var value = GetValue(HeaderFontSizeProperty);
                Contract.Assume(value != null);
                return (double)value;
            }
            set { SetValue(HeaderFontSizeProperty, value); }
        }

        public static readonly DependencyProperty ContentFontSizeProperty =
            DependencyProperty.Register("ContentFontSize", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(10.0 * (96.0 / 72.0),
                                                                      FrameworkPropertyMetadataOptions.AffectsRender |
                                                                      FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double ContentFontSize
        {
            get
            {
                var value = GetValue(ContentFontSizeProperty);
                Contract.Assume(value != null);
                return (double)value;
            }
            set { SetValue(ContentFontSizeProperty, value); }
        }

        public static readonly DependencyProperty TextFontSizeProperty =
            DependencyProperty.Register("TextFontSize", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(9.0 * (96.0 / 72.0),
                                                                      FrameworkPropertyMetadataOptions.AffectsRender |
                                                                      FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double TextFontSize
        {
            get
            {
                var value = GetValue(TextFontSizeProperty);
                Contract.Assume(value != null);
                return (double)value;
            }
            set { SetValue(TextFontSizeProperty, value); }
        }

        #endregion

        #region Thickness

        public static readonly DependencyProperty DefaultThicknessProperty =
            DependencyProperty.Register("DefaultThickness", typeof(Thickness), typeof(Parameters),
                                        new FrameworkPropertyMetadata(new Thickness(1.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Thickness DefaultThickness
        {
            get
            {
                var value = GetValue(DefaultThicknessProperty);
                Contract.Assume(value != null);
                return (Thickness)value;
            }
            set { SetValue(DefaultThicknessProperty, value); }
        }

        public static readonly DependencyProperty SemiBoldThicknessProperty =
            DependencyProperty.Register("SemiBoldThickness", typeof(Thickness), typeof(Parameters),
                                        new FrameworkPropertyMetadata(new Thickness(1.5), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Thickness SemiBoldThickness
        {
            get
            {
                var value = GetValue(SemiBoldThicknessProperty);
                Contract.Assume(value != null);
                return (Thickness)value;
            }
            set { SetValue(SemiBoldThicknessProperty, value); }
        }

        public static readonly DependencyProperty BoldThicknessProperty =
            DependencyProperty.Register("BoldThickness", typeof(Thickness), typeof(Parameters),
                                        new FrameworkPropertyMetadata(new Thickness(2.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Thickness BoldThickness
        {
            get
            {
                var value = GetValue(BoldThicknessProperty);
                Contract.Assume(value != null);
                return (Thickness)value;
            }
            set { SetValue(BoldThicknessProperty, value); }
        }

        public static readonly DependencyProperty DefaultThicknessValueProperty =
            DependencyProperty.Register("DefaultThicknessValue", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double DefaultThicknessValue
        {
            get
            {
                var value = GetValue(DefaultThicknessValueProperty);
                Contract.Assume(value != null);
                return (double)value;
            }
            set { SetValue(DefaultThicknessValueProperty, value); }
        }

        public static readonly DependencyProperty SemiBoldThicknessValueProperty =
            DependencyProperty.Register("SemiBoldThicknessValue", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(1.5, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double SemiBoldThicknessValue
        {
            get
            {
                var value = GetValue(SemiBoldThicknessValueProperty);
                Contract.Assume(value != null);
                return (double)value;
            }
            set { SetValue(SemiBoldThicknessValueProperty, value); }
        }

        public static readonly DependencyProperty BoldThicknessValueProperty =
            DependencyProperty.Register("BoldThicknessValue", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(2.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double BoldThicknessValue
        {
            get
            {
                var value = GetValue(BoldThicknessValueProperty);
                Contract.Assume(value != null);
                return (double)value;
            }
            set { SetValue(BoldThicknessValueProperty, value); }
        }

        #endregion

        #region Padding

        public static readonly DependencyProperty DefaultPaddingProperty =
            DependencyProperty.Register("DefaultPadding", typeof(Thickness), typeof(Parameters),
                                        new FrameworkPropertyMetadata(new Thickness(1.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Thickness DefaultPadding
        {
            get
            {
                var value = GetValue(DefaultPaddingProperty);
                Contract.Assume(value != null);
                return (Thickness)value;
            }
            set { SetValue(DefaultPaddingProperty, value); }
        }

        public static readonly DependencyProperty SemiBoldPaddingProperty =
            DependencyProperty.Register("SemiBoldPadding", typeof(Thickness), typeof(Parameters),
                                        new FrameworkPropertyMetadata(new Thickness(2.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Thickness SemiBoldPadding
        {
            get
            {
                var value = GetValue(SemiBoldPaddingProperty);
                Contract.Assume(value != null);
                return (Thickness)value;
            }
            set { SetValue(SemiBoldPaddingProperty, value); }
        }

        public static readonly DependencyProperty BoldPaddingProperty =
            DependencyProperty.Register("BoldPadding", typeof(Thickness), typeof(Parameters),
                                        new FrameworkPropertyMetadata(new Thickness(5.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Thickness BoldPadding
        {
            get
            {
                var value = GetValue(BoldPaddingProperty);
                Contract.Assume(value != null);
                return (Thickness)value;
            }
            set { SetValue(BoldPaddingProperty, value); }
        }

        public static readonly DependencyProperty DefaultPaddingValueProperty =
            DependencyProperty.Register("DefaultPaddingValue", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double DefaultPaddingValue
        {
            get
            {
                var value = GetValue(DefaultPaddingValueProperty);
                Contract.Assume(value != null);
                return (double)value;
            }
            set { SetValue(DefaultPaddingValueProperty, value); }
        }

        public static readonly DependencyProperty SemiBoldPaddingValueProperty =
            DependencyProperty.Register("SemiBoldPaddingValue", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(2.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double SemiBoldPaddingValue
        {
            get
            {
                var value = GetValue(SemiBoldPaddingValueProperty);
                Contract.Assume(value != null);
                return (double)value;
            }
            set { SetValue(SemiBoldPaddingValueProperty, value); }
        }

        public static readonly DependencyProperty BoldPaddingValueProperty =
            DependencyProperty.Register("BoldPaddingValue", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(5.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double BoldPaddingValue
        {
            get
            {
                var value = GetValue(BoldPaddingValueProperty);
                Contract.Assume(value != null);
                return (double)value;
            }
            set { SetValue(BoldPaddingValueProperty, value); }
        }

        #endregion

        #region Animation

        public static readonly DependencyProperty DefaultDurationProperty =
            DependencyProperty.RegisterAttached("DefaultDuration", typeof(Duration), typeof(Parameters),
                                                new FrameworkPropertyMetadata((new Duration(TimeSpan.FromSeconds(0.0))),
                                                                              FrameworkPropertyMetadataOptions.None));

        public static Duration GetDefaultDuration(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(DefaultDurationProperty);
            Contract.Assume(value != null);
            return (Duration)value;
        }

        public static void SetDefaultDuration(DependencyObject obj, Duration value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(DefaultDurationProperty, value);
        }

        public static readonly DependencyProperty MinimumDurationProperty =
            DependencyProperty.RegisterAttached("MinimumDuration", typeof(Duration), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(0.2)),
                                                                              FrameworkPropertyMetadataOptions.None));

        public static Duration GetMinimumDuration(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(MinimumDurationProperty);
            Contract.Assume(value != null);
            return (Duration)value;
        }

        public static void SetMinimumDuration(DependencyObject obj, Duration value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(MinimumDurationProperty, value);
        }

        public static readonly DependencyProperty OptimumDurationProperty =
            DependencyProperty.RegisterAttached("OptimumDuration", typeof(Duration), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(0.5)),
                                                                              FrameworkPropertyMetadataOptions.None));

        public static Duration GetOptimumDuration(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(OptimumDurationProperty);
            Contract.Assume(value != null);
            return (Duration)value;
        }

        public static void SetOptimumDuration(DependencyObject obj, Duration value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(OptimumDurationProperty, value);
        }

        public static readonly DependencyProperty MaximumDurationProperty =
            DependencyProperty.RegisterAttached("MaximumDuration", typeof(Duration), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(1.0)),
                                                                              FrameworkPropertyMetadataOptions.None));

        public static Duration GetMaximumDuration(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(MaximumDurationProperty);
            Contract.Assume(value != null);
            return (Duration)value;
        }

        public static void SetMaximumDuration(DependencyObject obj, Duration value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(MaximumDurationProperty, value);
        }

        #endregion

        #region Buttons

        public static readonly DependencyProperty CommandButtonMaskProperty =
            DependencyProperty.RegisterAttached("CommandButtonMask", typeof(Brush), typeof(Parameters),
                                                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                    FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        public static Brush GetCommandButtonMask(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return (Brush)obj.GetValue(CommandButtonMaskProperty);
        }

        public static void SetCommandButtonMask(DependencyObject obj, Brush value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(CommandButtonMaskProperty, value);
        }

        #endregion

        #region CheckBox & RadioButton

        public static readonly DependencyProperty BulletDecoratorSizeProperty =
            DependencyProperty.RegisterAttached("BulletDecoratorSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(14.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static double GetBulletDecoratorSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(BulletDecoratorSizeProperty);
            Contract.Assume(value != null);
            return (double)value;
        }

        public static void SetBulletDecoratorSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(BulletDecoratorSizeProperty, value);
        }

        public static readonly DependencyProperty CheckBoxBulletSizeProperty =
            DependencyProperty.RegisterAttached("CheckBoxBulletSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static double GetCheckBoxBulletSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(CheckBoxBulletSizeProperty);
            Contract.Assume(value != null);
            return (double)value;
        }

        public static void SetCheckBoxBulletSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(CheckBoxBulletSizeProperty, value);
        }

        public static readonly DependencyProperty RadioButtonBulletSizeProperty =
            DependencyProperty.RegisterAttached("RadioButtonBulletSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(8.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static double GetRadioButtonBulletSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(RadioButtonBulletSizeProperty);
            Contract.Assume(value != null);
            return (double)value;
        }

        public static void SetRadioButtonBulletSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(RadioButtonBulletSizeProperty, value);
        }

        #endregion

        #region ComboBox

        public static readonly DependencyProperty ComboBoxButtonSizeProperty =
            DependencyProperty.RegisterAttached("ComboBoxButtonSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(18.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static double GetComboBoxButtonSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(ComboBoxButtonSizeProperty);
            Contract.Assume(value != null);
            return (double)value;
        }

        public static void SetComboBoxButtonSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(ComboBoxButtonSizeProperty, value);
        }

        public static readonly DependencyProperty ComboBoxArrowSizeProperty =
            DependencyProperty.RegisterAttached("ComboBoxArrowSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(8.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static double GetComboBoxArrowSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(ComboBoxArrowSizeProperty);
            Contract.Assume(value != null);
            return (double)value;
        }

        public static void SetComboBoxArrowSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(ComboBoxArrowSizeProperty, value);
        }

        public static readonly DependencyProperty ComboBoxArrowMarginProperty =
            DependencyProperty.RegisterAttached("ComboBoxArrowMargin", typeof(Thickness), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Thickness(5, 0, 5, 0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static Thickness GetComboBoxArrowMargin(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(ComboBoxArrowMarginProperty);
            Contract.Assume(value != null);
            return (Thickness)value;
        }

        public static void SetComboBoxArrowMargin(DependencyObject obj, Thickness value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(ComboBoxArrowMarginProperty, value);
        }

        #endregion

        #region Slider

        public static readonly DependencyProperty SliderTrackSizeProperty =
            DependencyProperty.RegisterAttached("SliderTrackSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(4.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static double GetSliderTrackSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(SliderTrackSizeProperty);
            Contract.Assume(value != null);
            return (double)value;
        }

        public static void SetSliderTrackSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(SliderTrackSizeProperty, value);
        }

        public static readonly DependencyProperty SliderThumbThicknessProperty =
            DependencyProperty.RegisterAttached("SliderThumbThickness", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(6.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static double GetSliderThumbThickness(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(SliderThumbThicknessProperty);
            Contract.Assume(value != null);
            return (double)value;
        }

        public static void SetSliderThumbThickness(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(SliderThumbThicknessProperty, value);
        }

        #endregion

        #region ProgressBar

        public static readonly DependencyProperty ProgressBarLoadingElementSizeProperty =
            DependencyProperty.RegisterAttached("ProgressBarLoadingElementSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(4.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static double GetProgressBarLoadingElementSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(ProgressBarLoadingElementSizeProperty);
            Contract.Assume(value != null);
            return (double)value;
        }

        public static void SetProgressBarLoadingElementSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(ProgressBarLoadingElementSizeProperty, value);
        }

        public static readonly DependencyProperty CircularProgressBarThicknessProperty =
            DependencyProperty.RegisterAttached("CircularProgressBarThickness", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(3.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static double GetCircularProgressBarThickness(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(CircularProgressBarThicknessProperty);
            Contract.Assume(value != null);
            return (double)value;
        }

        public static void SetCircularProgressBarThickness(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(CircularProgressBarThicknessProperty, value);
        }

        #endregion

        #region ScrollBar

        public static readonly DependencyProperty ScrollBarArrowSizeProperty =
            DependencyProperty.RegisterAttached("ScrollBarArrowSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(6.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static double GetScrollBarArrowSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(ScrollBarArrowSizeProperty);
            Contract.Assume(value != null);
            return (double)value;
        }

        public static void SetScrollBarArrowSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(ScrollBarArrowSizeProperty, value);
        }

        #endregion

        #region TabControl

        public static readonly DependencyProperty TabControlIndicatorBrushProperty =
            DependencyProperty.RegisterAttached("TabControlIndicatorBrush", typeof(Brush), typeof(Parameters),
                                                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                    FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        public static Brush GetTabControlIndicatorBrush(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return (Brush)obj.GetValue(TabControlIndicatorBrushProperty);
        }

        public static void SetTabControlIndicatorBrush(DependencyObject obj, Brush value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(TabControlIndicatorBrushProperty, value);
        }

        public static readonly DependencyProperty TabControlIndicatorThicknessProperty =
            DependencyProperty.RegisterAttached("TabControlIndicatorThickness", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(2.0, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                   FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static Thickness GetTabControlIndicatorThickness(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(TabControlIndicatorThicknessProperty);
            Contract.Assume(value != null);
            return (Thickness)value;
        }

        public static void SetTabControlIndicatorThickness(DependencyObject obj, Thickness value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(TabControlIndicatorThicknessProperty, value);
        }

        public static readonly DependencyProperty TabItemHeaderStyleProperty =
            DependencyProperty.RegisterAttached("TabItemHeaderStyle", typeof(Style), typeof(Parameters),
                                                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public static Style GetTabItemHeaderStyle(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return (Style)obj.GetValue(TabItemHeaderStyleProperty);
        }

        public static void SetTabItemHeaderStyle(DependencyObject obj, Style value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(TabItemHeaderStyleProperty, value);
        }

        #endregion

        #region Menu

        public static readonly DependencyProperty SubmenuItemBulletSizeProperty =
            DependencyProperty.RegisterAttached("SubmenuItemBulletSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(12.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static double GetSubmenuItemBulletSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(SubmenuItemBulletSizeProperty);
            Contract.Assume(value != null);
            return (double)value;
        }

        public static void SetSubmenuItemBulletSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(SubmenuItemBulletSizeProperty, value);
        }

        public static readonly DependencyProperty SubmenuHeaderArrowSizeProperty =
            DependencyProperty.RegisterAttached("SubmenuHeaderArrowSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(8.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static double GetSubmenuHeaderArrowSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(SubmenuHeaderArrowSizeProperty);
            Contract.Assume(value != null);
            return (double)value;
        }

        public static void SetSubmenuHeaderArrowSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(SubmenuHeaderArrowSizeProperty, value);
        }

        public static readonly DependencyProperty SubmenuHeaderArrowMarginProperty =
            DependencyProperty.RegisterAttached("SubmenuHeaderArrowMargin", typeof(Thickness), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Thickness(3, 0, 3, 0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static Thickness GetSubmenuHeaderArrowMargin(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(SubmenuHeaderArrowMarginProperty);
            Contract.Assume(value != null);
            return (Thickness)value;
        }

        public static void SetSubmenuHeaderArrowMargin(DependencyObject obj, Thickness value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(SubmenuHeaderArrowMarginProperty, value);
        }

        #endregion
    }
} ;