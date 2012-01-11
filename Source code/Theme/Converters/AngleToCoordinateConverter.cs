using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Elysium.Theme.Converters
{
    public sealed class AngleToCoordinateConverter : IMultiValueConverter
    {
        public static readonly AngleToCoordinateConverter Instance = new AngleToCoordinateConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 4)
            {
                return DependencyProperty.UnsetValue;
            }

            var fallbackValue = values[0];
            var angle = values[1] as double?;
            var areaWidth = values[2] as double?;
            var areaHeight = values[3] as double?;

            if (angle == null || angle < 0.0 || areaWidth == null || areaHeight == null)
            {
                return fallbackValue;
            }

            var width = values.Length > 3 ? values[4] as double? : null;
            var height = values.Length > 3 ? values[5] as double? : null;
            var radiusXCoordinate = values.Length > 6 ? values[5] as double? : areaWidth / 2;
            var radiusYCoordinate = values.Length > 7 ? values[6] as double? : areaHeight / 2;

            var length = Math.Max(width ?? 0.0, height ?? 0.0);
            var radius = Math.Min(areaWidth.Value / 2, areaHeight.Value / 2) - length;

            try
            {
                switch (parameter as string)
                {
                    case "X":
                    case "x":
                        var x = (radiusXCoordinate ?? 0.0) + radius * Math.Cos(angle.Value * Math.PI / 180);
                        return x;
                    case "Y":
                    case "y":
                        var y = (radiusYCoordinate ?? 0.0) + radius * Math.Sin(angle.Value * Math.PI / 180);
                        return y;
                    default:
                        return fallbackValue;
                }
            }
            catch
            {
                return fallbackValue;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
} ;