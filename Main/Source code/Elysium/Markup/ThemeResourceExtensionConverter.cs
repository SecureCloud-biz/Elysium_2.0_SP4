using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace Elysium.Markup
{
    public class ThemeResourceExtensionConverter : TypeConverter
    {
        #region Overrides of TypeConverter

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(InstanceDescriptor))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            var themeResourceExtension = value as ThemeResourceExtension;
            if (themeResourceExtension == null)
            {
                throw new ArgumentException("value must be of type ThemeResourceExtension", "value");
            }
            return new InstanceDescriptor(typeof(ThemeResourceExtension).GetConstructor(new[] { typeof(object) }), new object[] { themeResourceExtension.ResourceKey });
        }

        #endregion
    }
}