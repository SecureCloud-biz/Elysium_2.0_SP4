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
            if ((string)value == "Documentation.En")
            {
                return Properties.Resources.Documentation_en;
            }
            if ((string)value == "Documentation.Ru")
            {
                return Properties.Resources.Documentation_ru;
            }
            if ((string)value == "Test")
            {
                return Properties.Resources.Test;
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
                return "Documentation.En";
            }
            if ((string)value == Properties.Resources.Documentation_ru)
            {
                return "Documentation.Ru";
            }
            if ((string)value == Properties.Resources.Test)
            {
                return "Test";
            }
            return value;
        }
    }
}