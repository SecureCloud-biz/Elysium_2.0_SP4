using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Elysium.Theme.Converters
{
    [ValueConversion(typeof(double), typeof(GridLength))]
    public sealed class DoubleToGridLengthConverter : IValueConverter
    {
        public static readonly DoubleToGridLengthConverter Instance = new DoubleToGridLengthConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var unitType = parameter as string;

            switch (unitType)
            {
                case "Auto":
                    return new GridLength(0.0, GridUnitType.Auto);
                case "*":
                    return new GridLength((double)value, GridUnitType.Star);
                default:
                    return new GridLength((double)value, GridUnitType.Pixel);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var unitType = parameter as string;

            var length = (GridLength)value;
            switch (unitType)
            {
                case "Auto":
                    return length.IsAuto ? length.Value : DependencyProperty.UnsetValue;
                case "*":
                    return length.IsStar ? length.Value : DependencyProperty.UnsetValue;
                default:
                    return length.IsAbsolute ? length.Value : DependencyProperty.UnsetValue;
            }
        }
    }
} ;