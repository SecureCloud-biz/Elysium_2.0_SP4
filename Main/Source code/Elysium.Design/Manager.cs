using System.Windows;
using System.Windows.Media;

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
            if (identifier.DeclaringType.IsAssignableFrom(typeof(FrameworkElement)) && identifier.Name == "Resources")
            {
                var control = (FrameworkElement)item.View.PlatformObject;
                var resources = (ResourceDictionary)value;

                var theme = Parameters.Manager.GetTheme(control);
                var accentBrush = Parameters.Manager.GetAccentBrush(control);
                var contrastBrush = Parameters.Manager.GetContrastBrush(control);

                if (theme == null && accentBrush == null && contrastBrush == null)
                {
                    Elysium.Manager.RemoveCore(resources);
                }
                else
                {
                    Theme controlTheme;
                    SolidColorBrush controlAcentBrush;
                    SolidColorBrush controlContrastBrush;

                    Elysium.Manager.ApplyCore(
                        resources,
                        theme ?? (control.TryGetTheme(out controlTheme) ? (Theme?)controlTheme : Elysium.Manager.DefaultTheme),
                        accentBrush ?? (control.TryGetAccentBrush(out controlAcentBrush) ? controlAcentBrush : Elysium.Manager.DefaultAccentBrush),
                        contrastBrush ?? (control.TryGetContrastBrush(out controlContrastBrush) ? controlContrastBrush : Elysium.Manager.DefaultContrastBrush));
                }
                return resources;
            }

            return base.TranslatePropertyValue(item, identifier, value);
        }
    }
}