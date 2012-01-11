using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Elysium.Theme.Converters
{
    public sealed class PercentToAngleConverter : IValueConverter
    {
        public static readonly PercentToAngleConverter Instance = new PercentToAngleConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value * 360.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value * (1.0 / 360.0);
        }
    }
} ;