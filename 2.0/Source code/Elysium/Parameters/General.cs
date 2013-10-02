using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium.Parameters
{
    [PublicAPI]
    public static class General
    {
        #region Font size

        [PublicAPI]
        public static readonly DependencyProperty TitleFontSizeProperty =
            DependencyProperty.RegisterAttached("TitleFontSize", typeof(double), typeof(General),
                                                new FrameworkPropertyMetadata(12d * (96d / 72d),
                                                                              FrameworkPropertyMetadataOptions.AffectsMeasure |
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.AffectsRender |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures", Justification = "Can't be proven.")]
        public static double GetTitleFontSize(DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(TitleFontSizeProperty));
        }

        [PublicAPI]
        public static void SetTitleFontSize(DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(TitleFontSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.RegisterAttached("HeaderFontSize", typeof(double), typeof(General),
                                                new FrameworkPropertyMetadata(16d * (96d / 72d),
                                                                              FrameworkPropertyMetadataOptions.AffectsMeasure |
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.AffectsRender |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures", Justification = "Can't be proven.")]
        public static double GetHeaderFontSize(DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(HeaderFontSizeProperty));
        }

        [PublicAPI]
        public static void SetHeaderFontSize(DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(HeaderFontSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty ContentFontSizeProperty =
            DependencyProperty.RegisterAttached("ContentFontSize", typeof(double), typeof(General),
                                                new FrameworkPropertyMetadata(10d * (96d / 72d),
                                                                              FrameworkPropertyMetadataOptions.AffectsMeasure |
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.AffectsRender |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures", Justification = "Can't be proven.")]
        public static double GetContentFontSize(DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(ContentFontSizeProperty));
        }

        [PublicAPI]
        public static void SetContentFontSize(DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(ContentFontSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty TextFontSizeProperty =
            DependencyProperty.RegisterAttached("TextFontSize", typeof(double), typeof(General),
                                                new FrameworkPropertyMetadata(9d * (96d / 72d),
                                                                              FrameworkPropertyMetadataOptions.AffectsMeasure |
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.AffectsRender |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures", Justification = "Can't be proven.")]
        public static double GetTextFontSize(DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(TextFontSizeProperty));
        }

        [PublicAPI]
        public static void SetTextFontSize(DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(TextFontSizeProperty, value);
        }

        #endregion

        #region Thickness

        [PublicAPI]
        public static readonly DependencyProperty DefaultThicknessProperty =
            DependencyProperty.RegisterAttached("DefaultThickness", typeof(Thickness), typeof(General),
                                                new FrameworkPropertyMetadata(new Thickness(1d),
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, ThicknessUtil.CoerceNonNegative));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures", Justification = "Can't be proven.")]
        public static Thickness GetDefaultThickness(DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            ThicknessUtil.EnsureNonNegative();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(DefaultThicknessProperty));
        }

        [PublicAPI]
        public static void SetDefaultThickness(DependencyObject obj, Thickness value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(DefaultThicknessProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty SemiBoldThicknessProperty =
            DependencyProperty.RegisterAttached("SemiBoldThickness", typeof(Thickness), typeof(General),
                                                new FrameworkPropertyMetadata(new Thickness(1.5d),
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, ThicknessUtil.CoerceNonNegative));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures", Justification = "Can't be proven.")]
        public static Thickness GetSemiBoldThickness(DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            ThicknessUtil.EnsureNonNegative();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(SemiBoldThicknessProperty));
        }

        [PublicAPI]
        public static void SetSemiBoldThickness(DependencyObject obj, Thickness value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(SemiBoldThicknessProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty BoldThicknessProperty =
            DependencyProperty.RegisterAttached("BoldThickness", typeof(Thickness), typeof(General),
                                                new FrameworkPropertyMetadata(new Thickness(2d),
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, ThicknessUtil.CoerceNonNegative));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures", Justification = "Can't be proven.")]
        public static Thickness GetBoldThickness(DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            ThicknessUtil.EnsureNonNegative();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(BoldThicknessProperty));
        }

        [PublicAPI]
        public static void SetBoldThickness(DependencyObject obj, Thickness value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(BoldThicknessProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty DefaultThicknessValueProperty =
            DependencyProperty.RegisterAttached("DefaultThicknessValue", typeof(double), typeof(General),
                                                new FrameworkPropertyMetadata(1d,
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures", Justification = "Can't be proven.")]
        public static double GetDefaultThicknessValue(DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(DefaultThicknessValueProperty));
        }

        [PublicAPI]
        public static void SetDefaultThicknessValue(DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(DefaultThicknessValueProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty SemiBoldThicknessValueProperty =
            DependencyProperty.RegisterAttached("SemiBoldThicknessValue", typeof(double), typeof(General),
                                                new FrameworkPropertyMetadata(1.5d,
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures", Justification = "Can't be proven.")]
        public static double GetSemiBoldThicknessValue(DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(SemiBoldThicknessValueProperty));
        }

        [PublicAPI]
        public static void SetSemiBoldThicknessValue(DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(SemiBoldThicknessValueProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty BoldThicknessValueProperty =
            DependencyProperty.RegisterAttached("BoldThicknessValue", typeof(double), typeof(General),
                                                new FrameworkPropertyMetadata(2d,
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures", Justification = "Can't be proven.")]
        public static double GetBoldThicknessValue(DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(BoldThicknessValueProperty));
        }

        [PublicAPI]
        public static void SetBoldThicknessValue(DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(BoldThicknessValueProperty, value);
        }

        #endregion

        #region Padding

        [PublicAPI]
        public static readonly DependencyProperty DefaultPaddingProperty =
            DependencyProperty.RegisterAttached("DefaultPadding", typeof(Thickness), typeof(General),
                                                new FrameworkPropertyMetadata(new Thickness(1d),
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, ThicknessUtil.CoerceNonNegative));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures", Justification = "Can't be proven.")]
        public static Thickness GetDefaultPadding(DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            ThicknessUtil.EnsureNonNegative();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(DefaultPaddingProperty));
        }

        [PublicAPI]
        public static void SetDefaultPadding(DependencyObject obj, Thickness value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(DefaultPaddingProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty SemiBoldPaddingProperty =
            DependencyProperty.RegisterAttached("SemiBoldPadding", typeof(Thickness), typeof(General),
                                                new FrameworkPropertyMetadata(new Thickness(2d),
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, ThicknessUtil.CoerceNonNegative));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures", Justification = "Can't be proven.")]
        public static Thickness GetSemiBoldPadding(DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            ThicknessUtil.EnsureNonNegative();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(SemiBoldPaddingProperty));
        }

        [PublicAPI]
        public static void SetSemiBoldPadding(DependencyObject obj, Thickness value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(SemiBoldPaddingProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty BoldPaddingProperty =
            DependencyProperty.RegisterAttached("BoldPadding", typeof(Thickness), typeof(General),
                                                new FrameworkPropertyMetadata(new Thickness(6d),
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, ThicknessUtil.CoerceNonNegative));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures", Justification = "Can't be proven.")]
        public static Thickness GetBoldPadding(DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            ThicknessUtil.EnsureNonNegative();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(BoldPaddingProperty));
        }

        [PublicAPI]
        public static void SetBoldPadding(DependencyObject obj, Thickness value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(BoldPaddingProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty DefaultPaddingValueProperty =
            DependencyProperty.RegisterAttached("DefaultPaddingValue", typeof(double), typeof(General),
                                                new FrameworkPropertyMetadata(1d,
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures", Justification = "Can't be proven.")]
        public static double GetDefaultPaddingValue(DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(DefaultPaddingValueProperty));
        }

        [PublicAPI]
        public static void SetDefaultPaddingValue(DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(DefaultPaddingValueProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty SemiBoldPaddingValueProperty =
            DependencyProperty.RegisterAttached("SemiBoldPaddingValue", typeof(double), typeof(General),
                                                new FrameworkPropertyMetadata(2d,
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures", Justification = "Can't be proven.")]
        public static double GetSemiBoldPaddingValue(DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(SemiBoldPaddingValueProperty));
        }

        [PublicAPI]
        public static void SetSemiBoldPaddingValue(DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(SemiBoldPaddingValueProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty BoldPaddingValueProperty =
            DependencyProperty.RegisterAttached("BoldPaddingValue", typeof(double), typeof(General),
                                                new FrameworkPropertyMetadata(6d,
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Ensures", Justification = "Can't be proven.")]
        public static double GetBoldPaddingValue(DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(BoldPaddingValueProperty));
        }

        [PublicAPI]
        public static void SetBoldPaddingValue(DependencyObject obj, double value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(BoldPaddingValueProperty, value);
        }

        #endregion

        #region Animation

        [PublicAPI]
        public static readonly DependencyProperty DefaultDurationProperty =
            DependencyProperty.RegisterAttached("DefaultDuration", typeof(Duration), typeof(General),
                                                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(0d)), FrameworkPropertyMetadataOptions.Inherits));

        [PublicAPI]
        public static Duration GetDefaultDuration(DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            return BoxingHelper<Duration>.Unbox(obj.GetValue(DefaultDurationProperty));
        }

        [PublicAPI]
        public static void SetDefaultDuration(DependencyObject obj, Duration value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(DefaultDurationProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty MinimumDurationProperty =
            DependencyProperty.RegisterAttached("MinimumDuration", typeof(Duration), typeof(General),
                                                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(0.2d)),
                                                                              FrameworkPropertyMetadataOptions.Inherits));

        [PublicAPI]
        public static Duration GetMinimumDuration(DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            return BoxingHelper<Duration>.Unbox(obj.GetValue(MinimumDurationProperty));
        }

        [PublicAPI]
        public static void SetMinimumDuration(DependencyObject obj, Duration value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(MinimumDurationProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty OptimumDurationProperty =
            DependencyProperty.RegisterAttached("OptimumDuration", typeof(Duration), typeof(General),
                                                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(0.5d)),
                                                                              FrameworkPropertyMetadataOptions.Inherits));

        [PublicAPI]
        public static Duration GetOptimumDuration(DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            return BoxingHelper<Duration>.Unbox(obj.GetValue(OptimumDurationProperty));
        }

        [PublicAPI]
        public static void SetOptimumDuration(DependencyObject obj, Duration value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(OptimumDurationProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty MaximumDurationProperty =
            DependencyProperty.RegisterAttached("MaximumDuration", typeof(Duration), typeof(General),
                                                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(1.0d)),
                                                                              FrameworkPropertyMetadataOptions.Inherits));

        [PublicAPI]
        public static Duration GetMaximumDuration(DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            return BoxingHelper<Duration>.Unbox(obj.GetValue(MaximumDurationProperty));
        }

        [PublicAPI]
        public static void SetMaximumDuration(DependencyObject obj, Duration value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(MaximumDurationProperty, value);
        }

        #endregion

        #region System Resources

        [PublicAPI]
        public static readonly DependencyProperty ShadowBrushProperty =
            DependencyProperty.RegisterAttached("ShadowBrush", typeof(SolidColorBrush), typeof(General),
                                                new FrameworkPropertyMetadata(null,
                                                                              FrameworkPropertyMetadataOptions.AffectsRender |
                                                                              FrameworkPropertyMetadataOptions.Inherits));

        [PublicAPI]
        public static SolidColorBrush GetShadowBrush(DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            return (SolidColorBrush)obj.GetValue(ShadowBrushProperty);
        }

        [PublicAPI]
        public static void SetShadowBrush(DependencyObject obj, SolidColorBrush value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(ShadowBrushProperty, value);
        }

        #endregion
    }
}
