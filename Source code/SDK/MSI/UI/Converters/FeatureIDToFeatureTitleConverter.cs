using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Elysium.SDK.MSI.UI.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class FeatureIDToFeatureTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
            {
                return DependencyProperty.UnsetValue;
            }

            if ((string)value == "Notifications")
            {
                return Properties.Resources.Notifications;
            }
            if ((string)value == "Documentation.en")
            {
                return Properties.Resources.Documentation_en;
            }
            if ((string)value == "Documentation.ru")
            {
                return Properties.Resources.Documentation_ru;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
            {
                return null;
            }
            if ((string)value == Properties.Resources.Notifications)
            {
                return "Notifications";
            }
            if ((string)value == Properties.Resources.Documentation_en)
            {
                return "Documentation.en";
            }
            if ((string)value == Properties.Resources.Documentation_ru)
            {
                return "Documentation.ru";
            }
            return value;
        }
    }
} ;