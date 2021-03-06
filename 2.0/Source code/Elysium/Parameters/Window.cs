﻿using System.Diagnostics.CodeAnalysis;
using System.Windows;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium.Parameters
{
    [PublicAPI]
    public static class Window
    {
        [PublicAPI]
        public static readonly DependencyProperty ResizeBorderThicknessProperty =
            DependencyProperty.RegisterAttached("ResizeBorderThickness", typeof(Thickness), typeof(Window),
                                                new FrameworkPropertyMetadata(new Thickness(3d), FrameworkPropertyMetadataOptions.AffectsArrange, null, ThicknessUtil.CoerceNonNegative));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(Controls.Window))]
        [SuppressMessage("Microsoft.Contracts", "Ensures", Justification = "Can't be proven.")]
        public static Thickness GetResizeBorderThickness(Controls.Window obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            ThicknessUtil.EnsureNonNegative();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(ResizeBorderThicknessProperty));
        }

        [PublicAPI]
        public static void SetResizeBorderThickness(Controls.Window obj, Thickness value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(ResizeBorderThicknessProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty MinimizeButtonToolTipProperty = DependencyProperty.RegisterAttached("MinimizeButtonToolTip", typeof(object), typeof(Window),
                                                                                                                      new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(Controls.Window))]
        public static void SetMinimizeButtonToolTip(Controls.Window obj, object value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(MinimizeButtonToolTipProperty, value);
        }

        [PublicAPI]
        public static object GetMinimizeButtonToolTip(Controls.Window obj)
        {
            return obj.GetValue(MinimizeButtonToolTipProperty);
        }

        [PublicAPI]
        public static readonly DependencyProperty MaximizeButtonToolTipProperty = DependencyProperty.RegisterAttached("MaximizeButtonToolTip", typeof(object), typeof(Window),
                                                                                                                      new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(Controls.Window))]
        public static void SetMaximizeButtonToolTip(Controls.Window obj, object value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(MaximizeButtonToolTipProperty, value);
        }

        [PublicAPI]
        public static object GetMaximizeButtonToolTip(Controls.Window obj)
        {
            return obj.GetValue(MaximizeButtonToolTipProperty);
        }

        [PublicAPI]
        public static readonly DependencyProperty RestoreButtonToolTipProperty = DependencyProperty.RegisterAttached("RestoreButtonToolTip", typeof(object), typeof(Window),
                                                                                                                      new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(Controls.Window))]
        public static void SetRestoreButtonToolTip(Controls.Window obj, object value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(RestoreButtonToolTipProperty, value);
        }

        [PublicAPI]
        public static object GetRestoreButtonToolTip(Controls.Window obj)
        {
            return obj.GetValue(RestoreButtonToolTipProperty);
        }

        [PublicAPI]
        public static readonly DependencyProperty CloseButtonToolTipProperty = DependencyProperty.RegisterAttached("CloseButtonToolTip", typeof(object), typeof(Window),
                                                                                                                      new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(Controls.Window))]
        public static void SetCloseButtonToolTip(Controls.Window obj, object value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(CloseButtonToolTipProperty, value);
        }

        [PublicAPI]
        public static object GetCloseButtonToolTip(Controls.Window obj)
        {
            return obj.GetValue(CloseButtonToolTipProperty);
        }
    }
}