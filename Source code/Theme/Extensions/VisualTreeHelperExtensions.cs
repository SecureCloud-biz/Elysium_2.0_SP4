using System.Windows;
using System.Windows.Media;

namespace Elysium.Theme.Extensions
{
    public class VisualTreeHelperExtensions
    {
        public static T FindParent<T>(DependencyObject reference)
            where T : DependencyObject
        {
            var currentParent = VisualTreeHelper.GetParent(reference);
            var parent = currentParent as T;
            return parent ?? (currentParent != null ? FindParent<T>(currentParent) : null);
        }
    }
} ;