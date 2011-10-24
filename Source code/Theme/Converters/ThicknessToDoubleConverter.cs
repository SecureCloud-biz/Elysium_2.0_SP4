using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Elysium.Theme.WPF.Converters
{
    [ValueConversion(typeof(Thickness), typeof(double))]
    public class ThicknessToDoubleConverter : IValueConverter
    {
        public static readonly ThicknessToDoubleConverter Instance = new ThicknessToDoubleConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var thickness = (Thickness)value;
            return (thickness.Left + thickness.Top + thickness.Bottom + thickness.Right) / 4;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
} ;