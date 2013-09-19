using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Windows;

using Elysium.Extensions;
using Elysium.Markup;

using JetBrains.Annotations;

namespace Elysium
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ThemeDictionaryConverter : TypeConverter
    {
        internal static ThemeDictionaryConverter Instance
        {
            get { return _instance.Value; }
        }

        private static Lazy<ThemeDictionaryConverter> _instance = new Lazy<ThemeDictionaryConverter>(() => new ThemeDictionaryConverter());

        internal static System.Windows.ResourceDictionary Convert(ThemeDictionaryBase value)
        {
            return (System.Windows.ResourceDictionary)Instance.ConvertTo(value, typeof(System.Windows.ResourceDictionary));
        }

        #region Overrides of TypeConverter

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(System.Windows.ResourceDictionary) || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            ValidationHelper.NotNull(value, "value");
            ValidationHelper.OfType(value, "value", typeof(ThemeDictionaryBase));
            Util.EnsureNotNull<object>();
            Util.EnsureOfType<object>(typeof(System.Windows.ResourceDictionary));
            if (destinationType != typeof(System.Windows.ResourceDictionary))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            var themeDictionaryBase = (ThemeDictionaryBase)value;
            var result = new System.Windows.ResourceDictionary { Source = Manager.ResourcesUri };
            System.Windows.ResourceDictionary resources;
            switch (themeDictionaryBase.Source)
            {
                case ThemeResources.Light:
                    resources = new System.Windows.ResourceDictionary { Source = Manager.LightBrushesDictionaryUri };
                    result.SafeSet(resources);
                    break;
                case ThemeResources.LightGray:
                    resources = new System.Windows.ResourceDictionary { Source = Manager.LightGrayBrushesDictionaryUri };
                    result.SafeSet(resources);
                    break;
                case ThemeResources.DarkGray:
                    resources = new System.Windows.ResourceDictionary { Source = Manager.DarkGrayBrushesDictionaryUri };
                    result.SafeSet(resources);
                    break;
                case ThemeResources.Dark:
                    resources = new System.Windows.ResourceDictionary { Source = Manager.DarkBrushesDictionaryUri };
                    result.SafeSet(resources);
                    break;
                case ThemeResources.Inherited:
                    var themeDictionary = themeDictionaryBase as ThemeDictionary;
                    if (themeDictionary == null)
                    {
                        throw new InvalidOperationException("Dictionary must be of type ThemeDictionary");
                    }
                    if (themeDictionary.Control == null || !themeDictionary.Control.IsAlive)
                    {
                        throw new InvalidOperationException("ThemeDictionary must be assigned with control before to be converted to System.Windows.ResourceDictionary");
                    }
                    var control = (FrameworkElement)themeDictionary.Control.Target;
                    foreach (var resourceKey in themeDictionary.Keys)
                    {
                        result.SafeSet(resourceKey, control.FindResource(resourceKey));
                    }
                    break;
            }
            result.SafeSet<ThemeResource, object>(themeDictionaryBase);
            return result;
        }

        #endregion
    }
}