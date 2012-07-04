using System.Windows;

using JetBrains.Annotations;

namespace Elysium.Extensions
{
    [PublicAPI]
    public static class LogicalTreeHelperExtensions
    {
        [PublicAPI]
        public static T FindParent<T>(DependencyObject current)
            where T : DependencyObject
        {
            var currentParent = LogicalTreeHelper.GetParent(current);
            var parent = currentParent as T;
            return parent ?? (currentParent != null ? FindParent<T>(currentParent) : null);
        }
    }
} ;