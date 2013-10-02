using System.Diagnostics.CodeAnalysis;
using System.Windows;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium.Parameters
{
    [PublicAPI]
    public static class Slider
    {
        [PublicAPI]
        public static readonly DependencyProperty TrackSizeProperty =
            DependencyProperty.RegisterAttached("TrackSize", typeof(double), typeof(Slider),
                                                new FrameworkPropertyMetadata(4d, FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.Slider))]
        [SuppressMessage("Microsoft.Contracts", "Ensures", Justification = "Can't be proven.")]
        public static double GetTrackSize(System.Windows.Controls.Slider obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(TrackSizeProperty));
        }

        [PublicAPI]
        public static void SetTrackSize(System.Windows.Controls.Slider obj, double value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(TrackSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty ThumbThicknessProperty =
            DependencyProperty.RegisterAttached("ThumbThickness", typeof(double), typeof(Slider),
                                                new FrameworkPropertyMetadata(6d, FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.Slider))]
        [SuppressMessage("Microsoft.Contracts", "Ensures", Justification = "Can't be proven.")]
        public static double GetThumbThickness(System.Windows.Controls.Slider obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(ThumbThicknessProperty));
        }

        [PublicAPI]
        public static void SetThumbThickness(System.Windows.Controls.Slider obj, double value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(ThumbThicknessProperty, value);
        }
    }
}
