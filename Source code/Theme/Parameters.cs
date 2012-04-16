using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

using Elysium.Controls;
using Elysium.Controls.Primitives;
using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium
{
    [PublicAPI]
    public static class Parameters
    {
        #region Font size

        [PublicAPI]
        public static readonly DependencyProperty TitleFontSizeProperty =
            DependencyProperty.RegisterAttached("TitleFontSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(12d * (96d / 72d),
                                                                              FrameworkPropertyMetadataOptions.AffectsMeasure |
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.AffectsRender |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, CoerceSize));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        public static double GetTitleFontSize([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(TitleFontSizeProperty));
        }

        [PublicAPI]
        public static void SetTitleFontSize([NotNull] DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(TitleFontSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.RegisterAttached("HeaderFontSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(16d * (96d / 72d),
                                                                              FrameworkPropertyMetadataOptions.AffectsMeasure |
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.AffectsRender |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, CoerceSize));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        public static double GetHeaderFontSize([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(HeaderFontSizeProperty));
        }

        [PublicAPI]
        public static void SetHeaderFontSize([NotNull] DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(HeaderFontSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty ContentFontSizeProperty =
            DependencyProperty.RegisterAttached("ContentFontSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(10d * (96d / 72d),
                                                                              FrameworkPropertyMetadataOptions.AffectsMeasure |
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.AffectsRender |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, CoerceSize));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        public static double GetContentFontSize([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(ContentFontSizeProperty));
        }

        [PublicAPI]
        public static void SetContentFontSize([NotNull] DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(ContentFontSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty TextFontSizeProperty =
            DependencyProperty.RegisterAttached("TextFontSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(9d * (96d / 72d),
                                                                              FrameworkPropertyMetadataOptions.AffectsMeasure |
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.AffectsRender |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, CoerceSize));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        public static double GetTextFontSize([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(TextFontSizeProperty));
        }

        [PublicAPI]
        public static void SetTextFontSize([NotNull] DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(TextFontSizeProperty, value);
        }

        #endregion

        #region Thickness

        [PublicAPI]
        public static readonly DependencyProperty DefaultThicknessProperty =
            DependencyProperty.RegisterAttached("DefaultThickness", typeof(Thickness), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Thickness(1d),
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, CoerceThickness));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        public static Thickness GetDefaultThickness([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureThickness();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(DefaultThicknessProperty));
        }

        [PublicAPI]
        public static void SetDefaultThickness([NotNull] DependencyObject obj, Thickness value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(DefaultThicknessProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty SemiBoldThicknessProperty =
            DependencyProperty.RegisterAttached("SemiBoldThickness", typeof(Thickness), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Thickness(1.5d),
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, CoerceThickness));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        public static Thickness GetSemiBoldThickness([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureThickness();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(SemiBoldThicknessProperty));
        }

        [PublicAPI]
        public static void SetSemiBoldThickness([NotNull] DependencyObject obj, Thickness value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(SemiBoldThicknessProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty BoldThicknessProperty =
            DependencyProperty.RegisterAttached("BoldThickness", typeof(Thickness), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Thickness(2d),
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, CoerceThickness));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        public static Thickness GetBoldThickness([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureThickness();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(BoldThicknessProperty));
        }

        [PublicAPI]
        public static void SetBoldThickness([NotNull] DependencyObject obj, Thickness value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(BoldThicknessProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty DefaultThicknessValueProperty =
            DependencyProperty.RegisterAttached("DefaultThicknessValue", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(1d,
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, CoerceSize));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        public static double GetDefaultThicknessValue([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(DefaultThicknessValueProperty));
        }

        [PublicAPI]
        public static void SetDefaultThicknessValue([NotNull] DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(DefaultThicknessValueProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty SemiBoldThicknessValueProperty =
            DependencyProperty.RegisterAttached("SemiBoldThicknessValue", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(1.5d,
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, CoerceSize));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        public static double GetSemiBoldThicknessValue([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(SemiBoldThicknessValueProperty));
        }

        [PublicAPI]
        public static void SetSemiBoldThicknessValue([NotNull] DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(SemiBoldThicknessValueProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty BoldThicknessValueProperty =
            DependencyProperty.RegisterAttached("BoldThicknessValue", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(2d,
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, CoerceSize));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        public static double GetBoldThicknessValue([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(BoldThicknessValueProperty));
        }

        [PublicAPI]
        public static void SetBoldThicknessValue([NotNull] DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(BoldThicknessValueProperty, value);
        }

        #endregion

        #region Padding

        [PublicAPI]
        public static readonly DependencyProperty DefaultPaddingProperty =
            DependencyProperty.RegisterAttached("DefaultPadding", typeof(Thickness), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Thickness(1d),
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, CoerceThickness));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        public static Thickness GetDefaultPadding([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureThickness();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(DefaultPaddingProperty));
        }

        [PublicAPI]
        public static void SetDefaultPadding([NotNull] DependencyObject obj, Thickness value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(DefaultPaddingProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty SemiBoldPaddingProperty =
            DependencyProperty.RegisterAttached("SemiBoldPadding", typeof(Thickness), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Thickness(2d),
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, CoerceThickness));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        public static Thickness GetSemiBoldPadding([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureThickness();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(SemiBoldPaddingProperty));
        }

        [PublicAPI]
        public static void SetSemiBoldPadding([NotNull] DependencyObject obj, Thickness value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(SemiBoldPaddingProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty BoldPaddingProperty =
            DependencyProperty.RegisterAttached("BoldPadding", typeof(Thickness), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Thickness(5d),
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, CoerceThickness));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        public static Thickness GetBoldPadding([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureThickness();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(BoldPaddingProperty));
        }

        [PublicAPI]
        public static void SetBoldPadding([NotNull] DependencyObject obj, Thickness value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(BoldPaddingProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty DefaultPaddingValueProperty =
            DependencyProperty.RegisterAttached("DefaultPaddingValue", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(1d,
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, CoerceSize));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        public static double GetDefaultPaddingValue([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(DefaultPaddingValueProperty));
        }

        [PublicAPI]
        public static void SetDefaultPaddingValue([NotNull] DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(DefaultPaddingValueProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty SemiBoldPaddingValueProperty =
            DependencyProperty.RegisterAttached("SemiBoldPaddingValue", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(2d,
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, CoerceSize));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        public static double GetSemiBoldPaddingValue([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(SemiBoldPaddingValueProperty));
        }

        [PublicAPI]
        public static void SetSemiBoldPaddingValue([NotNull] DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(SemiBoldPaddingValueProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty BoldPaddingValueProperty =
            DependencyProperty.RegisterAttached("BoldPaddingValue", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(5d,
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, CoerceSize));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        public static double GetBoldPaddingValue([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(BoldPaddingValueProperty));
        }

        [PublicAPI]
        public static void SetBoldPaddingValue([NotNull] DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(BoldPaddingValueProperty, value);
        }

        #endregion

        #region Animation

        [PublicAPI]
        public static readonly DependencyProperty DefaultDurationProperty =
            DependencyProperty.RegisterAttached("DefaultDuration", typeof(Duration), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(0d)), FrameworkPropertyMetadataOptions.Inherits));

        [PublicAPI]
        public static Duration GetDefaultDuration([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return BoxingHelper<Duration>.Unbox(obj.GetValue(DefaultDurationProperty));
        }

        [PublicAPI]
        public static void SetDefaultDuration([NotNull] DependencyObject obj, Duration value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(DefaultDurationProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty MinimumDurationProperty =
            DependencyProperty.RegisterAttached("MinimumDuration", typeof(Duration), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(0.2d)),
                                                                              FrameworkPropertyMetadataOptions.Inherits));

        [PublicAPI]
        public static Duration GetMinimumDuration([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return BoxingHelper<Duration>.Unbox(obj.GetValue(MinimumDurationProperty));
        }

        [PublicAPI]
        public static void SetMinimumDuration([NotNull] DependencyObject obj, Duration value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(MinimumDurationProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty OptimumDurationProperty =
            DependencyProperty.RegisterAttached("OptimumDuration", typeof(Duration), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(0.5d)),
                                                                              FrameworkPropertyMetadataOptions.Inherits));

        [PublicAPI]
        public static Duration GetOptimumDuration([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return BoxingHelper<Duration>.Unbox(obj.GetValue(OptimumDurationProperty));
        }

        [PublicAPI]
        public static void SetOptimumDuration([NotNull] DependencyObject obj, Duration value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(OptimumDurationProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty MaximumDurationProperty =
            DependencyProperty.RegisterAttached("MaximumDuration", typeof(Duration), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(1.0d)),
                                                                              FrameworkPropertyMetadataOptions.Inherits));

        [PublicAPI]
        public static Duration GetMaximumDuration([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return BoxingHelper<Duration>.Unbox(obj.GetValue(MaximumDurationProperty));
        }

        [PublicAPI]
        public static void SetMaximumDuration([NotNull] DependencyObject obj, Duration value)
        {
            ValidationHelper.NotNull(obj, () => obj);
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
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Brush GetCommandButtonMask([NotNull] CommandButtonBase obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return (Brush)obj.GetValue(CommandButtonMaskProperty);
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetCommandButtonMask([NotNull] CommandButtonBase obj, Brush value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(CommandButtonMaskProperty, value);
        }

        #endregion

        #region CheckBox & RadioButton

        [PublicAPI]
        public static readonly DependencyProperty BulletDecoratorSizeProperty =
            DependencyProperty.RegisterAttached("BulletDecoratorSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(16d, FrameworkPropertyMetadataOptions.AffectsMeasure, null, CoerceSize));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(CheckBox))]
        [AttachedPropertyBrowsableForType(typeof(RadioButton))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        public static double GetBulletDecoratorSize([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            ValidationHelper.OfTypes(obj, () => obj, typeof(CheckBox), typeof(RadioButton), "CheckBox", "RadioButton");
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(BulletDecoratorSizeProperty));
        }

        [PublicAPI]
        public static void SetBulletDecoratorSize([NotNull] DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            ValidationHelper.OfTypes(obj, () => obj, typeof(CheckBox), typeof(RadioButton), "CheckBox", "RadioButton");
            obj.SetValue(BulletDecoratorSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty BulletSizeProperty =
            DependencyProperty.RegisterAttached("BulletSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(8d, FrameworkPropertyMetadataOptions.AffectsMeasure, null, CoerceSize));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(CheckBox))]
        [AttachedPropertyBrowsableForType(typeof(RadioButton))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        public static double GetBulletSize([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            ValidationHelper.OfTypes(obj, () => obj, typeof(CheckBox), typeof(RadioButton), "CheckBox", "RadioButton");
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(BulletSizeProperty));
        }

        [PublicAPI]
        public static void SetBulletSize([NotNull] DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            ValidationHelper.OfTypes(obj, () => obj, typeof(CheckBox), typeof(RadioButton), "CheckBox", "RadioButton");
            obj.SetValue(BulletSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty CheckSizeProperty =
            DependencyProperty.RegisterAttached("CheckSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(10d, FrameworkPropertyMetadataOptions.AffectsMeasure, null, CoerceSize));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(CheckBox))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetCheckSize([NotNull] CheckBox obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(CheckSizeProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetCheckSize([NotNull] CheckBox obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(CheckSizeProperty, value);
        }

        #endregion

        #region ToggleSwitch

        [PublicAPI]
        public static readonly DependencyProperty ToggleSwitchTrackSizeProperty =
            DependencyProperty.RegisterAttached("ToggleSwitchTrackSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(48d, FrameworkPropertyMetadataOptions.AffectsMeasure, null, CoerceSize));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ToggleSwitch))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetToggleSwitchTrackSize([NotNull] ToggleSwitch obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(ToggleSwitchTrackSizeProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetToggleSwitchTrackSize([NotNull] ToggleSwitch obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(ToggleSwitchTrackSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty ToggleSwitchThumbThicknessProperty =
            DependencyProperty.RegisterAttached("ToggleSwitchThumbThickness", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(10d, FrameworkPropertyMetadataOptions.AffectsMeasure, null, CoerceSize));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ToggleSwitch))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetToggleSwitchThumbThickness([NotNull] ToggleSwitch obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(ToggleSwitchThumbThicknessProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetToggleSwitchThumbThickness([NotNull] ToggleSwitch obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(ToggleSwitchThumbThicknessProperty, value);
        }

        #endregion

        #region ComboBox

        [PublicAPI]
        public static readonly DependencyProperty ComboBoxButtonSizeProperty =
            DependencyProperty.RegisterAttached("ComboBoxButtonSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(18d, FrameworkPropertyMetadataOptions.AffectsMeasure, null, CoerceSize));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetComboBoxButtonSize([NotNull] ComboBox obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(ComboBoxButtonSizeProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetComboBoxButtonSize([NotNull] ComboBox obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(ComboBoxButtonSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty ComboBoxArrowSizeProperty =
            DependencyProperty.RegisterAttached("ComboBoxArrowSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(8d, FrameworkPropertyMetadataOptions.AffectsMeasure, null, CoerceSize));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetComboBoxArrowSize([NotNull] ComboBox obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(ComboBoxArrowSizeProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetComboBoxArrowSize([NotNull] ComboBox obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(ComboBoxArrowSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty ComboBoxArrowMarginProperty =
            DependencyProperty.RegisterAttached("ComboBoxArrowMargin", typeof(Thickness), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Thickness(5d, 0d, 5d, 0d), FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Thickness GetComboBoxArrowMargin([NotNull] ComboBox obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureThickness();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(ComboBoxArrowMarginProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetComboBoxArrowMargin([NotNull] ComboBox obj, Thickness value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(ComboBoxArrowMarginProperty, value);
        }

        #endregion

        #region Slider

        [PublicAPI]
        public static readonly DependencyProperty SliderTrackSizeProperty =
            DependencyProperty.RegisterAttached("SliderTrackSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(4d, FrameworkPropertyMetadataOptions.AffectsMeasure, null, CoerceSize));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(Slider))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetSliderTrackSize([NotNull] Slider obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(SliderTrackSizeProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetSliderTrackSize([NotNull] Slider obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(SliderTrackSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty SliderThumbThicknessProperty =
            DependencyProperty.RegisterAttached("SliderThumbThickness", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(6d, FrameworkPropertyMetadataOptions.AffectsMeasure, null, CoerceSize));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(Slider))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetSliderThumbThickness([NotNull] Slider obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(SliderThumbThicknessProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetSliderThumbThickness([NotNull] Slider obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(SliderThumbThicknessProperty, value);
        }

        #endregion

        #region ProgressBar

        [PublicAPI]
        public static readonly DependencyProperty ProgressBarBusyElementSizeProperty =
            DependencyProperty.RegisterAttached("ProgressBarBusyElementSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(4d, FrameworkPropertyMetadataOptions.AffectsMeasure, null, CoerceSize));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ProgressBarBase))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetProgressBarBusyElementSize([NotNull] ProgressBarBase obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(ProgressBarBusyElementSizeProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetProgressBarBusyElementSize([NotNull] ProgressBarBase obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(ProgressBarBusyElementSizeProperty, value);
        }

        #endregion

        #region ScrollBar

        [PublicAPI]
        public static readonly DependencyProperty ScrollBarArrowSizeProperty =
            DependencyProperty.RegisterAttached("ScrollBarArrowSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(6d, FrameworkPropertyMetadataOptions.AffectsMeasure, null, CoerceSize));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ScrollBar))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetScrollBarArrowSize([NotNull] ScrollBar obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(ScrollBarArrowSizeProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetScrollBarArrowSize([NotNull] ScrollBar obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
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
        [AttachedPropertyBrowsableForType(typeof(TabItem))]
        public static Brush GetTabControlIndicatorBrush([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            ValidationHelper.OfTypes(obj, () => obj, typeof(TabControl), typeof(TabItem), "TabControl", "TabItem");
            return (Brush)obj.GetValue(TabControlIndicatorBrushProperty);
        }

        [PublicAPI]
        public static void SetTabControlIndicatorBrush([NotNull] DependencyObject obj, Brush value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            ValidationHelper.OfTypes(obj, () => obj, typeof(TabControl), typeof(TabItem), "TabControl", "TabItem");
            obj.SetValue(TabControlIndicatorBrushProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty TabControlIndicatorThicknessProperty =
            DependencyProperty.RegisterAttached("TabControlIndicatorThickness", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(2d, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                  FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                                              null, CoerceSize));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(TabControl))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetTabControlIndicatorThickness([NotNull] TabControl obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(TabControlIndicatorThicknessProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetTabControlIndicatorThickness([NotNull] TabControl obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(TabControlIndicatorThicknessProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty TabItemHeaderStyleProperty =
            DependencyProperty.RegisterAttached("TabItemHeaderStyle", typeof(Style), typeof(Parameters),
                                                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(TabItem))]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Style GetTabItemHeaderStyle([NotNull] TabItem obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return (Style)obj.GetValue(TabItemHeaderStyleProperty);
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetTabItemHeaderStyle([NotNull] TabItem obj, Style value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(TabItemHeaderStyleProperty, value);
        }

        #endregion

        #region Menu

        [PublicAPI]
        public static readonly DependencyProperty SubmenuItemBulletSizeProperty =
            DependencyProperty.RegisterAttached("SubmenuItemBulletSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(12d, FrameworkPropertyMetadataOptions.AffectsMeasure, null, CoerceSize));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(MenuItem))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetSubmenuItemBulletSize([NotNull] MenuItem obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(SubmenuItemBulletSizeProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetSubmenuItemBulletSize([NotNull] MenuItem obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(SubmenuItemBulletSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty SubmenuHeaderArrowSizeProperty =
            DependencyProperty.RegisterAttached("SubmenuHeaderArrowSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(8d, FrameworkPropertyMetadataOptions.AffectsMeasure, null, CoerceSize));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(MenuItem))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetSubmenuHeaderArrowSize([NotNull] MenuItem obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(SubmenuHeaderArrowSizeProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetSubmenuHeaderArrowSize([NotNull] MenuItem obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(SubmenuHeaderArrowSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty SubmenuHeaderArrowMarginProperty =
            DependencyProperty.RegisterAttached("SubmenuHeaderArrowMargin", typeof(Thickness), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Thickness(3d, 0d, 3d, 0d), FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                                              null, CoerceThickness));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(MenuItem))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Thickness GetSubmenuHeaderArrowMargin([NotNull] MenuItem obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureThickness();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(SubmenuHeaderArrowMarginProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetSubmenuHeaderArrowMargin([NotNull] MenuItem obj, Thickness value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(SubmenuHeaderArrowMarginProperty, value);
        }

        #endregion

        #region Window

        [PublicAPI]
        public static readonly DependencyProperty WindowResizeBorderThicknessProperty =
            DependencyProperty.RegisterAttached("WindowResizeBorderThickness", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(3d, FrameworkPropertyMetadataOptions.AffectsArrange, null, CoerceSize));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(Controls.Window))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetWindowResizeBorderThickness([NotNull] Controls.Window obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            EnsureSize();
            return BoxingHelper<double>.Unbox(obj.GetValue(WindowResizeBorderThicknessProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetWindowResizeBorderThickness([NotNull] Controls.Window obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(WindowResizeBorderThicknessProperty, value);
        }

        #endregion

        #region Coerce methods

        private static object CoerceSize(DependencyObject obj, object basevalue)
        {
            ValidationHelper.NotNull(obj, () => obj);
            var value = BoxingHelper<double>.Unbox(basevalue);
            return IsValidSize(value) ? value : 0d;
        }


        private static object CoerceThickness(DependencyObject obj, object basevalue)
        {
            ValidationHelper.NotNull(obj, () => obj);
            var value = BoxingHelper<Thickness>.Unbox(basevalue);
            if (!IsValidSize(value.Left))
            {
                value.Left = 0d;
            }
            if (!IsValidSize(value.Top))
            {
                value.Top = 0d;
            }
            if (!IsValidSize(value.Right))
            {
                value.Right = 0d;
            }
            if (!IsValidSize(value.Bottom))
            {
                value.Bottom = 0d;
            }
            return value;
        }

        #endregion

        #region Contracts

        [DebuggerHidden]
        [ContractAbbreviator]
        private static void EnsureSize()
        {
            Contract.Ensures(IsValidSize(Contract.Result<double>()));
        }

        [DebuggerHidden]
        [ContractAbbreviator]
        private static void EnsureThickness()
        {
            Contract.Ensures(IsValidSize(Contract.Result<Thickness>().Left));
            Contract.Ensures(IsValidSize(Contract.Result<Thickness>().Top));
            Contract.Ensures(IsValidSize(Contract.Result<Thickness>().Right));
            Contract.Ensures(IsValidSize(Contract.Result<Thickness>().Bottom));
        }

        #endregion

        #region Helpers

        private static bool IsValidSize(double size)
        {
            return !double.IsNaN(size) && !double.IsInfinity(size) && size > 0d;
        }

        #endregion
    }
} ;