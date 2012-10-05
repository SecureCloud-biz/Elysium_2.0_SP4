using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

using Elysium.SDK.MSI.UI.Enumerations;

namespace Elysium.SDK.MSI.UI.Converters
{
    [ValueConversion(typeof(Screen), typeof(Visibility))]
    public class ScreenToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Screen) || !(parameter is string))
            {
                return DependencyProperty.UnsetValue;
            }

            return Enum.IsDefined(typeof(Screen), parameter) && (Screen)value == (Screen)Enum.Parse(typeof(Screen), (string)parameter)
                       ? Visibility.Visible
                       : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
