using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Elysium.SDK.MSI.UI.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool ? ((bool)value ? Visibility.Visible : Visibility.Hidden) : DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Visibility ? ((Visibility)value == Visibility.Visible) : DependencyProperty.UnsetValue;
        }
    }
}
