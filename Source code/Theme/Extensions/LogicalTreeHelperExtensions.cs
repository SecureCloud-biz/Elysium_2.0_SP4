using System.Windows;

namespace Elysium.Theme.Extensions
{
    public class LogicalTreeHelperExtensions
    {
        public static T FindParent<T>(DependencyObject current)
            where T : DependencyObject
        {
            var currentParent = LogicalTreeHelper.GetParent(current);
            var parent = currentParent as T;
            return parent ?? FindParent<T>(currentParent);
        }
    }
} ;