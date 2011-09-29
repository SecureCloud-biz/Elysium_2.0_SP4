using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Elysium.Theme.Converters
{
    public class SolidColorBrushToColorConverter : IValueConverter
    {
        public static readonly SolidColorBrushToColorConverter Instance = new SolidColorBrushToColorConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((SolidColorBrush)value).Color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush((Color)value);
        }
    }
}