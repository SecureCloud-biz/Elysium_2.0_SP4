using System.Windows;
using System.Windows.Media;

using JetBrains.Annotations;

namespace Elysium.Theme.Extensions
{
    [PublicAPI]
    public static class VisualTreeHelperExtensions
    {
        [PublicAPI]
        public static T FindParent<T>(DependencyObject reference)
            where T : DependencyObject
        {
            var currentParent = VisualTreeHelper.GetParent(reference);
            var parent = currentParent as T;
            return parent ?? (currentParent != null ? FindParent<T>(currentParent) : null);
        }
    }
} ;