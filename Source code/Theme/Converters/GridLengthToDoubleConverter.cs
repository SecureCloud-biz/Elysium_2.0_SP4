using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Elysium.Theme.Converters
{
    [ValueConversion(typeof(GridLength), typeof(double))]
    public sealed class GridLengthToDoubleConverter : IValueConverter
    {
        public static readonly GridLengthToDoubleConverter Instance = new GridLengthToDoubleConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DoubleToGridLengthConverter.Instance.ConvertBack(value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DoubleToGridLengthConverter.Instance.Convert(value, targetType, parameter, culture);
        }
    }
} ;