using System.Windows;
using System.Windows.Media;

using Elysium.Controls.Primitives;
using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium.Parameters
{
    [PublicAPI]
    public static class CommandButtons
    {
        [PublicAPI]
        public static readonly DependencyProperty MaskProperty =
            DependencyProperty.RegisterAttached("Mask", typeof(SolidColorBrush), typeof(CommandButtons),
                                                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                    FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(CommandButtonBase))]
        public static SolidColorBrush GetMask(CommandButtonBase obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            return (SolidColorBrush)obj.GetValue(MaskProperty);
        }

        [PublicAPI]
        public static void SetMask(CommandButtonBase obj, SolidColorBrush value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(MaskProperty, value);
        }
    }
}
