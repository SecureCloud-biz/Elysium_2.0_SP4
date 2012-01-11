using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Elysium.Theme.Converters
{
    [ValueConversion(typeof(Thickness), typeof(double))]
    public sealed class ThicknessToDoubleConverter : IValueConverter
    {
        public static readonly ThicknessToDoubleConverter Instance = new ThicknessToDoubleConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DoubleToThicknessConverter.Instance.ConvertBack(value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DoubleToThicknessConverter.Instance.Convert(value, targetType, parameter, culture);
        }
    }
} ;