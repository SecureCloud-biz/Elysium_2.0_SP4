using System;
using System.Windows;

using Elysium.Markup;
using Elysium.Parameters;

using Microsoft.Windows.Design.Metadata;
using Microsoft.Windows.Design.Model;

namespace Elysium.Design
{
    public class Manager : DesignModeValueProvider
    {
        public Manager()
        {
            Properties.Add(typeof(FrameworkElement), "Resources");
        }

        public override object TranslatePropertyValue(ModelItem item, PropertyIdentifier identifier, object value)
        {
            if (item != null && identifier.DeclaringType.IsAssignableFrom(typeof(FrameworkElement)) && identifier.Name == "Resources")
            {
                var control = (FrameworkElement)item.View.PlatformObject;
                var resources = (System.Windows.ResourceDictionary)value;

                var themeResources = General.GetThemeResources(control);

                if (themeResources == null)
                {
                    Elysium.Manager.RemoveCore(resources);
                }
                else
                {
                    if (themeResources.Source == ThemeResources.Inherited)
                    {
                        themeResources.Control = new WeakReference(control);
                    }
                    Elysium.Manager.ApplyCore(resources, themeResources);
                }
                return resources;
            }

            return base.TranslatePropertyValue(item, identifier, value);
        }
    }
}