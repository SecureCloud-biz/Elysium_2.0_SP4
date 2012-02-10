using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

using Elysium.Theme.Controls;
using Elysium.Theme.Controls.Primitives;
using Elysium.Theme.Extensions;

using JetBrains.Annotations;

namespace Elysium.Theme
{
    [PublicAPI]
    public sealed class Parameters : DependencyObject
    {
        [PublicAPI]
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Parameters is singleton")]
        public static readonly Parameters Instance = new Parameters();

        private Parameters()
        {
        }

        #region Font

        [PublicAPI]
        public static readonly DependencyProperty FontFamilyProperty =
            DependencyProperty.Register("FontFamily", typeof(FontFamily), typeof(Parameters),
                                        new FrameworkPropertyMetadata(new FontFamily("Segoe UI"),
                                                                      FrameworkPropertyMetadataOptions.AffectsRender |
                                                                      FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        public FontFamily FontFamily
        {
            get { return (FontFamily)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        [PublicAPI]
        public static readonly DependencyProperty TitleFontSizeProperty =
            DependencyProperty.Register("TitleFontSize", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(12.0 * (96.0 / 72.0),
                                                                      FrameworkPropertyMetadataOptions.AffectsRender |
                                                                      FrameworkPropertyMetadataOptions.AffectsMeasure), IsFontSizeValid);

        [PublicAPI]
        public double TitleFontSize
        {
            get
            {
                Contract.Ensures(!double.IsNaN(Contract.Result<double>()) && double.IsInfinity(Contract.Result<double>()) && Contract.Result<double>() > 0.0);
                var unboxedValue = BoxingHelper<double>.Unbox(GetValue(TitleFontSizeProperty));
                Contract.Assume(!double.IsNaN(unboxedValue) && double.IsInfinity(unboxedValue) && unboxedValue > 0.0);
                return unboxedValue;
            }
            set
            {
                if (!(value > 0.0) || double.IsNaN(value) || double.IsInfinity(value))
                {
                    throw new ArgumentException("Font size must be greater than zero.");
                }
                Contract.EndContractBlock();
                SetValue(TitleFontSizeProperty, value);
            }
        }

        [PublicAPI]
        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.Register("HeaderFontSize", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(16.0 * (96.0 / 72.0),
                                                                      FrameworkPropertyMetadataOptions.AffectsRender |
                                                                      FrameworkPropertyMetadataOptions.AffectsMeasure), IsFontSizeValid);

        [PublicAPI]
        public double HeaderFontSize
        {
            get
            {
                Contract.Ensures(!double.IsNaN(Contract.Result<double>()) && double.IsInfinity(Contract.Result<double>()) && Contract.Result<double>() > 0.0);
                var unboxedValue = BoxingHelper<double>.Unbox(GetValue(HeaderFontSizeProperty));
                Contract.Assume(!double.IsNaN(unboxedValue) && double.IsInfinity(unboxedValue) && unboxedValue > 0.0);
                return unboxedValue;
            }
            set
            {
                if (!(value > 0.0) || double.IsNaN(value) || double.IsInfinity(value))
                {
                    throw new ArgumentException("Font size must be greater than zero.");
                }
                Contract.EndContractBlock();
                SetValue(HeaderFontSizeProperty, value);
            }
        }

        [PublicAPI]
        public static readonly DependencyProperty ContentFontSizeProperty =
            DependencyProperty.Register("ContentFontSize", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(10.0 * (96.0 / 72.0),
                                                                      FrameworkPropertyMetadataOptions.AffectsRender |
                                                                      FrameworkPropertyMetadataOptions.AffectsMeasure), IsFontSizeValid);

        [PublicAPI]
        public double ContentFontSize
        {
            get
            {
                Contract.Ensures(!double.IsNaN(Contract.Result<double>()) && double.IsInfinity(Contract.Result<double>()) && Contract.Result<double>() > 0.0);
                var unboxedValue = BoxingHelper<double>.Unbox(GetValue(ContentFontSizeProperty));
                Contract.Assume(!double.IsNaN(unboxedValue) && double.IsInfinity(unboxedValue) && unboxedValue > 0.0);
                return unboxedValue;
            }
            set
            {
                if (!(value > 0.0) || double.IsNaN(value) || double.IsInfinity(value))
                {
                    throw new ArgumentException("Font size must be greater than zero.");
                }
                Contract.EndContractBlock();
                SetValue(ContentFontSizeProperty, value);
            }
        }

        [PublicAPI]
        public static readonly DependencyProperty TextFontSizeProperty =
            DependencyProperty.Register("TextFontSize", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(9.0 * (96.0 / 72.0),
                                                                      FrameworkPropertyMetadataOptions.AffectsRender |
                                                                      FrameworkPropertyMetadataOptions.AffectsMeasure), IsFontSizeValid);

        [PublicAPI]
        public double TextFontSize
        {
            get
            {
                Contract.Ensures(!double.IsNaN(Contract.Result<double>()) && double.IsInfinity(Contract.Result<double>()) && Contract.Result<double>() > 0.0);
                var unboxedValue = BoxingHelper<double>.Unbox(GetValue(TextFontSizeProperty));
                Contract.Assume(!double.IsNaN(unboxedValue) && double.IsInfinity(unboxedValue) && unboxedValue > 0.0);
                return unboxedValue;
            }
            set
            {
                if (!(value > 0.0) || double.IsNaN(value) || double.IsInfinity(value))
                {
                    throw new ArgumentException("Font size must be greater than zero.");
                }
                Contract.EndContractBlock();
                SetValue(TextFontSizeProperty, value);
            }
        }

        private static bool IsFontSizeValid(object value)
        {
            var unboxedValue = BoxingHelper<double>.Unbox(value);
            return !(double.IsNaN(unboxedValue)) && !(double.IsInfinity(unboxedValue)) && unboxedValue > 0.0;
        }

        #endregion

        #region Thickness

        [PublicAPI]
        public static readonly DependencyProperty DefaultThicknessProperty =
            DependencyProperty.Register("DefaultThickness", typeof(Thickness), typeof(Parameters),
                                        new FrameworkPropertyMetadata(new Thickness(1.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        public Thickness DefaultThickness
        {
            get { return BoxingHelper<Thickness>.Unbox(GetValue(DefaultThicknessProperty)); }
            set { SetValue(DefaultThicknessProperty, value); }
        }

        [PublicAPI]
        public static readonly DependencyProperty SemiBoldThicknessProperty =
            DependencyProperty.Register("SemiBoldThickness", typeof(Thickness), typeof(Parameters),
                                        new FrameworkPropertyMetadata(new Thickness(1.5), FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        public Thickness SemiBoldThickness
        {
            get { return BoxingHelper<Thickness>.Unbox(GetValue(SemiBoldThicknessProperty)); }
            set { SetValue(SemiBoldThicknessProperty, value); }
        }

        [PublicAPI]
        public static readonly DependencyProperty BoldThicknessProperty =
            DependencyProperty.Register("BoldThickness", typeof(Thickness), typeof(Parameters),
                                        new FrameworkPropertyMetadata(new Thickness(2.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        public Thickness BoldThickness
        {
            get { return BoxingHelper<Thickness>.Unbox(GetValue(BoldThicknessProperty)); }
            set { SetValue(BoldThicknessProperty, value); }
        }

        [PublicAPI]
        public static readonly DependencyProperty DefaultThicknessValueProperty =
            DependencyProperty.Register("DefaultThicknessValue", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        public double DefaultThicknessValue
        {
            get { return BoxingHelper<double>.Unbox(GetValue(DefaultThicknessValueProperty)); }
            set { SetValue(DefaultThicknessValueProperty, value); }
        }

        [PublicAPI]
        public static readonly DependencyProperty SemiBoldThicknessValueProperty =
            DependencyProperty.Register("SemiBoldThicknessValue", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(1.5, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        public double SemiBoldThicknessValue
        {
            get { return BoxingHelper<double>.Unbox(GetValue(SemiBoldThicknessValueProperty)); }
            set { SetValue(SemiBoldThicknessValueProperty, value); }
        }

        [PublicAPI]
        public static readonly DependencyProperty BoldThicknessValueProperty =
            DependencyProperty.Register("BoldThicknessValue", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(2.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        public double BoldThicknessValue
        {
            get { return BoxingHelper<double>.Unbox(GetValue(BoldThicknessValueProperty)); }
            set { SetValue(BoldThicknessValueProperty, value); }
        }

        #endregion

        #region Padding

        [PublicAPI]
        public static readonly DependencyProperty DefaultPaddingProperty =
            DependencyProperty.Register("DefaultPadding", typeof(Thickness), typeof(Parameters),
                                        new FrameworkPropertyMetadata(new Thickness(1.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        public Thickness DefaultPadding
        {
            get { return BoxingHelper<Thickness>.Unbox(GetValue(DefaultPaddingProperty)); }
            set { SetValue(DefaultPaddingProperty, value); }
        }

        [PublicAPI]
        public static readonly DependencyProperty SemiBoldPaddingProperty =
            DependencyProperty.Register("SemiBoldPadding", typeof(Thickness), typeof(Parameters),
                                        new FrameworkPropertyMetadata(new Thickness(2.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        public Thickness SemiBoldPadding
        {
            get { return BoxingHelper<Thickness>.Unbox(GetValue(SemiBoldPaddingProperty)); }
            set { SetValue(SemiBoldPaddingProperty, value); }
        }

        [PublicAPI]
        public static readonly DependencyProperty BoldPaddingProperty =
            DependencyProperty.Register("BoldPadding", typeof(Thickness), typeof(Parameters),
                                        new FrameworkPropertyMetadata(new Thickness(5.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        public Thickness BoldPadding
        {
            get { return BoxingHelper<Thickness>.Unbox(GetValue(BoldPaddingProperty)); }
            set { SetValue(BoldPaddingProperty, value); }
        }

        [PublicAPI]
        public static readonly DependencyProperty DefaultPaddingValueProperty =
            DependencyProperty.Register("DefaultPaddingValue", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        public double DefaultPaddingValue
        {
            get { return BoxingHelper<double>.Unbox(GetValue(DefaultPaddingValueProperty)); }
            set { SetValue(DefaultPaddingValueProperty, value); }
        }

        [PublicAPI]
        public static readonly DependencyProperty SemiBoldPaddingValueProperty =
            DependencyProperty.Register("SemiBoldPaddingValue", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(2.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        public double SemiBoldPaddingValue
        {
            get { return BoxingHelper<double>.Unbox(GetValue(SemiBoldPaddingValueProperty)); }
            set { SetValue(SemiBoldPaddingValueProperty, value); }
        }

        [PublicAPI]
        public static readonly DependencyProperty BoldPaddingValueProperty =
            DependencyProperty.Register("BoldPaddingValue", typeof(double), typeof(Parameters),
                                        new FrameworkPropertyMetadata(5.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        public double BoldPaddingValue
        {
            get { return BoxingHelper<double>.Unbox(GetValue(BoldPaddingValueProperty)); }
            set { SetValue(BoldPaddingValueProperty, value); }
        }

        #endregion

        #region Animation

        [PublicAPI]
        public static readonly DependencyProperty DefaultDurationProperty =
            DependencyProperty.RegisterAttached("DefaultDuration", typeof(Duration), typeof(Parameters),
                                                new FrameworkPropertyMetadata((new Duration(TimeSpan.FromSeconds(0.0))),
                                                                              FrameworkPropertyMetadataOptions.None));

        [PublicAPI]
        public static Duration GetDefaultDuration(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<Duration>.Unbox(obj.GetValue(DefaultDurationProperty));
        }

        [PublicAPI]
        public static void SetDefaultDuration(DependencyObject obj, Duration value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(DefaultDurationProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty MinimumDurationProperty =
            DependencyProperty.RegisterAttached("MinimumDuration", typeof(Duration), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(0.2)),
                                                                              FrameworkPropertyMetadataOptions.None));

        [PublicAPI]
        public static Duration GetMinimumDuration(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<Duration>.Unbox(obj.GetValue(MinimumDurationProperty));
        }

        [PublicAPI]
        public static void SetMinimumDuration(DependencyObject obj, Duration value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(MinimumDurationProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty OptimumDurationProperty =
            DependencyProperty.RegisterAttached("OptimumDuration", typeof(Duration), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(0.5)),
                                                                              FrameworkPropertyMetadataOptions.None));

        [PublicAPI]
        public static Duration GetOptimumDuration(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<Duration>.Unbox(obj.GetValue(OptimumDurationProperty));
        }

        [PublicAPI]
        public static void SetOptimumDuration(DependencyObject obj, Duration value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(OptimumDurationProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty MaximumDurationProperty =
            DependencyProperty.RegisterAttached("MaximumDuration", typeof(Duration), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(1.0)),
                                                                              FrameworkPropertyMetadataOptions.None));

        [PublicAPI]
        public static Duration GetMaximumDuration(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<Duration>.Unbox(obj.GetValue(MaximumDurationProperty));
        }

        [PublicAPI]
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

        [PublicAPI]
        public static readonly DependencyProperty CommandButtonMaskProperty =
            DependencyProperty.RegisterAttached("CommandButtonMask", typeof(Brush), typeof(Parameters),
                                                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                    FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(CommandButton))]
        public static Brush GetCommandButtonMask(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return (Brush)obj.GetValue(CommandButtonMaskProperty);
        }

        [PublicAPI]
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

        [PublicAPI]
        public static readonly DependencyProperty BulletDecoratorSizeProperty =
            DependencyProperty.RegisterAttached("BulletDecoratorSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(14.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(CheckBox))]
        [AttachedPropertyBrowsableForType(typeof(RadioButton))]
        public static double GetBulletDecoratorSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(BulletDecoratorSizeProperty));
        }

        [PublicAPI]
        public static void SetBulletDecoratorSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(BulletDecoratorSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty CheckBoxBulletSizeProperty =
            DependencyProperty.RegisterAttached("CheckBoxBulletSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(CheckBox))]
        public static double GetCheckBoxBulletSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(CheckBoxBulletSizeProperty));
        }

        [PublicAPI]
        public static void SetCheckBoxBulletSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(CheckBoxBulletSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty RadioButtonBulletSizeProperty =
            DependencyProperty.RegisterAttached("RadioButtonBulletSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(8.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(RadioButton))]
        public static double GetRadioButtonBulletSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(RadioButtonBulletSizeProperty));
        }

        [PublicAPI]
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

        #region ToggleSwitch

        [PublicAPI]
        public static readonly DependencyProperty ToggleSwitchTrackSizeProperty =
            DependencyProperty.RegisterAttached("ToggleSwitchTrackSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(50.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ToggleSwitch))]
        public static double GetToggleSwitchTrackSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(ToggleSwitchTrackSizeProperty));
        }

        [PublicAPI]
        public static void SetToggleSwitchTrackSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(ToggleSwitchTrackSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty ToggleSwitchThumbThicknessProperty =
            DependencyProperty.RegisterAttached("ToggleSwitchThumbThickness", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(12.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ToggleSwitch))]
        public static double GetToggleSwitchThumbThickness(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(ToggleSwitchThumbThicknessProperty));
        }

        [PublicAPI]
        public static void SetToggleSwitchThumbThickness(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(ToggleSwitchThumbThicknessProperty, value);
        }

        #endregion

        #region ComboBox

        [PublicAPI]
        public static readonly DependencyProperty ComboBoxButtonSizeProperty =
            DependencyProperty.RegisterAttached("ComboBoxButtonSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(18.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static double GetComboBoxButtonSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(ComboBoxButtonSizeProperty));
        }

        [PublicAPI]
        public static void SetComboBoxButtonSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(ComboBoxButtonSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty ComboBoxArrowSizeProperty =
            DependencyProperty.RegisterAttached("ComboBoxArrowSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(8.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static double GetComboBoxArrowSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(ComboBoxArrowSizeProperty));
        }

        [PublicAPI]
        public static void SetComboBoxArrowSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(ComboBoxArrowSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty ComboBoxArrowMarginProperty =
            DependencyProperty.RegisterAttached("ComboBoxArrowMargin", typeof(Thickness), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Thickness(5, 0, 5, 0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static Thickness GetComboBoxArrowMargin(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(ComboBoxArrowMarginProperty));
        }

        [PublicAPI]
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

        [PublicAPI]
        public static readonly DependencyProperty SliderTrackSizeProperty =
            DependencyProperty.RegisterAttached("SliderTrackSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(4.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(Slider))]
        public static double GetSliderTrackSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(SliderTrackSizeProperty));
        }

        [PublicAPI]
        public static void SetSliderTrackSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(SliderTrackSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty SliderThumbThicknessProperty =
            DependencyProperty.RegisterAttached("SliderThumbThickness", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(6.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(Slider))]
        public static double GetSliderThumbThickness(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(SliderThumbThicknessProperty));
        }

        [PublicAPI]
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

        [PublicAPI]
        public static readonly DependencyProperty ProgressBarBusyElementSizeProperty =
            DependencyProperty.RegisterAttached("ProgressBarBusyElementSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(4.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ProgressBarBase))]
        public static double GetProgressBarBusyElementSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(ProgressBarBusyElementSizeProperty));
        }

        [PublicAPI]
        public static void SetProgressBarBusyElementSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(ProgressBarBusyElementSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty CircularProgressBarThicknessProperty =
            DependencyProperty.RegisterAttached("CircularProgressBarThickness", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(3.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(CircularProgressBar))]
        public static double GetCircularProgressBarThickness(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(CircularProgressBarThicknessProperty));
        }

        [PublicAPI]
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

        [PublicAPI]
        public static readonly DependencyProperty ScrollBarArrowSizeProperty =
            DependencyProperty.RegisterAttached("ScrollBarArrowSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(6.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ScrollBar))]
        public static double GetScrollBarArrowSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(ScrollBarArrowSizeProperty));
        }

        [PublicAPI]
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

        [PublicAPI]
        public static readonly DependencyProperty TabControlIndicatorBrushProperty =
            DependencyProperty.RegisterAttached("TabControlIndicatorBrush", typeof(Brush), typeof(Parameters),
                                                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                    FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(TabControl))]
        public static Brush GetTabControlIndicatorBrush(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return (Brush)obj.GetValue(TabControlIndicatorBrushProperty);
        }

        [PublicAPI]
        public static void SetTabControlIndicatorBrush(DependencyObject obj, Brush value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(TabControlIndicatorBrushProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty TabControlIndicatorThicknessProperty =
            DependencyProperty.RegisterAttached("TabControlIndicatorThickness", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(2.0, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                   FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(TabControl))]
        public static Thickness GetTabControlIndicatorThickness(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(TabControlIndicatorThicknessProperty));
        }

        [PublicAPI]
        public static void SetTabControlIndicatorThickness(DependencyObject obj, Thickness value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(TabControlIndicatorThicknessProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty TabItemHeaderStyleProperty =
            DependencyProperty.RegisterAttached("TabItemHeaderStyle", typeof(Style), typeof(Parameters),
                                                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(TabItem))]
        public static Style GetTabItemHeaderStyle(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return (Style)obj.GetValue(TabItemHeaderStyleProperty);
        }

        [PublicAPI]
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

        [PublicAPI]
        public static readonly DependencyProperty SubmenuItemBulletSizeProperty =
            DependencyProperty.RegisterAttached("SubmenuItemBulletSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(12.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(MenuItem))]
        public static double GetSubmenuItemBulletSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(SubmenuItemBulletSizeProperty));
        }

        [PublicAPI]
        public static void SetSubmenuItemBulletSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(SubmenuItemBulletSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty SubmenuHeaderArrowSizeProperty =
            DependencyProperty.RegisterAttached("SubmenuHeaderArrowSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(8.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(MenuItem))]
        public static double GetSubmenuHeaderArrowSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(SubmenuHeaderArrowSizeProperty));
        }

        [PublicAPI]
        public static void SetSubmenuHeaderArrowSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(SubmenuHeaderArrowSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty SubmenuHeaderArrowMarginProperty =
            DependencyProperty.RegisterAttached("SubmenuHeaderArrowMargin", typeof(Thickness), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Thickness(3, 0, 3, 0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(MenuItem))]
        public static Thickness GetSubmenuHeaderArrowMargin(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(SubmenuHeaderArrowMarginProperty));
        }

        [PublicAPI]
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