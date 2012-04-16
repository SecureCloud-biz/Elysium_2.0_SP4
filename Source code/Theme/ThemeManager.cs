using System;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium
{
    [PublicAPI]
    public static class ThemeManager
    {
        [PublicAPI]
        public static readonly DependencyProperty ThemeProperty =
            DependencyProperty.RegisterAttached("Theme", typeof(Theme?), typeof(ThemeManager),
                                                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, OnThemeChanged));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static Theme? GetTheme([NotNull] FrameworkElement obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return (Theme?)obj.GetValue(ThemeProperty);
        }

        [PublicAPI]
        public static void SetTheme([NotNull] FrameworkElement obj, Theme? value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(ThemeProperty, value);
        }

        private static void OnThemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FrameworkElement;
            if (control != null)
            {
                control.ApplyTheme((Theme?)e.NewValue, null, null);
            }
        }

        [PublicAPI]
        public static readonly DependencyProperty AccentBrushProperty =
            DependencyProperty.RegisterAttached("AccentBrush", typeof(SolidColorBrush), typeof(ThemeManager),
                                                new FrameworkPropertyMetadata(null, OnAccentBrushChanged));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static SolidColorBrush GetAccentBrush([NotNull] FrameworkElement obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return (SolidColorBrush)obj.GetValue(AccentBrushProperty);
        }

        [PublicAPI]
        public static void SetAccentBrush([NotNull] FrameworkElement obj, SolidColorBrush value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(AccentBrushProperty, value);
        }

        private static void OnAccentBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FrameworkElement;
            if (control != null)
            {
                control.ApplyTheme(null, (SolidColorBrush)e.NewValue, null);
            }
        }

        [PublicAPI]
        public static readonly DependencyProperty ContrastBrushProperty =
            DependencyProperty.RegisterAttached("ContrastBrush", typeof(SolidColorBrush), typeof(ThemeManager),
                                                new FrameworkPropertyMetadata(null, OnContrastBrushChanged));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static SolidColorBrush GetContrastBrush([NotNull] FrameworkElement obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return (SolidColorBrush)obj.GetValue(ContrastBrushProperty);
        }

        [PublicAPI]
        public static void SetContrastBrush([NotNull] FrameworkElement obj, SolidColorBrush value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(ContrastBrushProperty, value);
        }

        private static void OnContrastBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FrameworkElement;
            if (control != null)
            {
                control.ApplyTheme(null, null, (SolidColorBrush)e.NewValue);
            }
        }

        private delegate void ApplyThemeToApplicationDelegate(Application application, Theme? theme, SolidColorBrush accentBrush, SolidColorBrush contrastBrush);

        [PublicAPI]
        public static void ApplyTheme(this Application application, Theme? theme, SolidColorBrush accentBrush, SolidColorBrush contrastBrush)
        {
            ValidationHelper.NotNull(application, () => application);

            application.Dispatcher.Invoke(new ApplyThemeToApplicationDelegate(ApplyThemeInternal), DispatcherPriority.Render,
                                          application, theme, accentBrush, contrastBrush);
        }

        [SecuritySafeCritical]
        private static void ApplyThemeInternal(Application application, Theme? theme, SolidColorBrush accentBrush, SolidColorBrush contrastBrush)
        {
            // Resource dictionaries paths
            var lightColorsUri = new Uri("/Elysium;component/Themes/LightBrushes.xaml", UriKind.Relative);
            var darkColorsUri = new Uri("/Elysium;component/Themes/DarkBrushes.xaml", UriKind.Relative);

            // Resource dictionaries
            var lightBrushesDictionary = new ResourceDictionary { Source = lightColorsUri };
            var darkBrushesDictionary = new ResourceDictionary { Source = darkColorsUri };

            if (theme == Theme.Light)
            {
                // Add LightBrushes.xaml, if not included
                if (application.Resources.MergedDictionaries.All(dictionary => dictionary.Source != lightColorsUri))
                {
                    application.Resources.MergedDictionaries.Add(lightBrushesDictionary);
                }

                // Remove DarkBrushes.xaml, if included
                var darkColorsDictionaries = application.Resources.MergedDictionaries.Where(dictionary => dictionary.Source == darkColorsUri).ToList();
                foreach (var dictionary in darkColorsDictionaries)
                {
                    application.Resources.MergedDictionaries.Remove(dictionary);
                }
            }
            if (theme == Theme.Dark)
            {
                // Add DarkBrushes.xaml, if not included
                if (application.Resources.MergedDictionaries.All(dictionary => dictionary.Source != darkColorsUri))
                {
                    application.Resources.MergedDictionaries.Add(darkBrushesDictionary);
                }

                // Remove LightBrushes.xaml, if included
                var lightColorsDictionaries = application.Resources.MergedDictionaries.Where(dictionary => dictionary.Source == lightColorsUri).ToList();
                foreach (var dictionary in lightColorsDictionaries)
                {
                    application.Resources.MergedDictionaries.Remove(dictionary);
                }
            }

            // Bug in WPF 4: http://connect.microsoft.com/VisualStudio/feedback/details/555322/global-wpf-styles-are-not-shown-when-using-2-levels-of-references
            if (application.Resources.Keys.Count == 0)
            {
                application.Resources.Add(typeof(Window), new Style(typeof(Window)));
            }

            if (accentBrush != null)
            {
                if (application.Resources.Contains("AccentBrush"))
                {
                    // Set AccentBrush value, if key exist
                    application.Resources["AccentBrush"] = accentBrush;
                }
                else
                {
                    // Add AccentBrush key and value, if key doesn't exist
                    application.Resources.Add("AccentBrush", accentBrush);
                }
            }

            if (contrastBrush != null)
            {
                if (application.Resources.Contains("ContrastBrush"))
                {
                    // Set ContrastBrush value, if key exist
                    application.Resources["ContrastBrush"] = contrastBrush;
                }
                else
                {
                    // Add ContrastBrush key and value, if key doesn't exist
                    application.Resources.Add("ContrastBrush", contrastBrush);
                }
            }

            // Add Generic.xaml, if not included
            var genericDictionaryUri = new Uri("/Elysium;component/Themes/Generic.xaml", UriKind.Relative);
            if (application.Resources.MergedDictionaries.All(dictionary => dictionary.Source != genericDictionaryUri))
            {
                application.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = genericDictionaryUri });
            }

            OnThemeChanged();
        }

        private delegate void ApplyThemeToControlDelegate(FrameworkElement control, Theme? theme, SolidColorBrush accentBrush, SolidColorBrush contrastBrush);

        [PublicAPI]
        public static void ApplyTheme(this FrameworkElement control, Theme? theme, SolidColorBrush accentBrush, SolidColorBrush contrastBrush)
        {
            ValidationHelper.NotNull(control, () => control);

            control.Dispatcher.Invoke(new ApplyThemeToControlDelegate(ApplyThemeInternal), DispatcherPriority.Render,
                                      control, theme, accentBrush, contrastBrush);
        }

        [SecuritySafeCritical]
        private static void ApplyThemeInternal(this FrameworkElement control, Theme? theme, SolidColorBrush accentBrush, SolidColorBrush contrastBrush)
        {
            // Resource dictionaries paths
            var lightColorsUri = new Uri("/Elysium;component/Themes/LightBrushes.xaml", UriKind.Relative);
            var darkColorsUri = new Uri("/Elysium;component/Themes/DarkBrushes.xaml", UriKind.Relative);

            // Resource dictionaries
            var lightBrushesDictionary = new ResourceDictionary { Source = lightColorsUri };
            var darkBrushesDictionary = new ResourceDictionary { Source = darkColorsUri };

            if (theme == Theme.Light)
            {
                // Add LightBrushes.xaml, if not included
                if (control.Resources.MergedDictionaries.All(dictionary => dictionary.Source != lightColorsUri))
                {
                    control.Resources.MergedDictionaries.Add(lightBrushesDictionary);
                }

                // Remove DarkBrushes.xaml, if included
                var darkColorsDictionaries = control.Resources.MergedDictionaries.Where(dictionary => dictionary.Source == darkColorsUri).ToList();
                foreach (var dictionary in darkColorsDictionaries)
                {
                    control.Resources.MergedDictionaries.Remove(dictionary);
                }
            }
            if (theme == Theme.Dark)
            {
                // Add DarkBrushes.xaml, if not included
                if (control.Resources.MergedDictionaries.All(dictionary => dictionary.Source != darkColorsUri))
                {
                    control.Resources.MergedDictionaries.Add(darkBrushesDictionary);
                }

                // Remove LightBrushes.xaml, if included
                var lightColorsDictionaries = control.Resources.MergedDictionaries.Where(dictionary => dictionary.Source == lightColorsUri).ToList();
                foreach (var dictionary in lightColorsDictionaries)
                {
                    control.Resources.MergedDictionaries.Remove(dictionary);
                }
            }

            // Bug in WPF 4: http://connect.microsoft.com/VisualStudio/feedback/details/555322/global-wpf-styles-are-not-shown-when-using-2-levels-of-references
            if (control.Resources.Keys.Count == 0)
            {
                control.Resources.Add(typeof(Window), new Style(typeof(Window)));
            }

            if (accentBrush != null)
            {
                if (control.Resources.Contains("AccentBrush"))
                {
                    // Set AccentBrush value, if key exist
                    control.Resources["AccentBrush"] = accentBrush;
                }
                else
                {
                    // Add AccentBrush key and value, if key doesn't exist
                    control.Resources.Add("AccentBrush", accentBrush);
                }
            }

            if (contrastBrush != null)
            {
                if (control.Resources.Contains("ContrastBrush"))
                {
                    // Set ContrastBrush value, if key exist
                    control.Resources["ContrastBrush"] = contrastBrush;
                }
                else
                {
                    // Add ContrastBrush key and value, if key doesn't exist
                    control.Resources.Add("ContrastBrush", contrastBrush);
                }
            }

            // Add Generic.xaml, if not included
            var genericDictionaryUri = new Uri("/Elysium;component/Themes/Generic.xaml", UriKind.Relative);
            if (control.Resources.MergedDictionaries.All(dictionary => dictionary.Source != genericDictionaryUri))
            {
                control.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = genericDictionaryUri });
            }

            OnThemeChanged();
        }

        [SecurityCritical]
        private static void OnThemeChanged()
        {
            var systemColors = typeof(SystemColors);
            var invalidateColors = systemColors.GetMethod("InvalidateCache", BindingFlags.Static | BindingFlags.NonPublic);
            if (invalidateColors != null)
            {
                invalidateColors.Invoke(null, null);
            }

            var systemParameters = typeof(SystemParameters);
            var invalidateParameters = systemParameters.GetMethod("InvalidateCache", BindingFlags.Static | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
            if (invalidateParameters != null)
            {
                invalidateParameters.Invoke(null, null);
            }

            var presentationFramework = Assembly.GetAssembly(typeof(Window));
            if (presentationFramework != null)
            {
                var systemResources = presentationFramework.GetType("System.Windows.SystemResources");

                if (systemResources != null)
                {
                    var onThemeChanged = systemResources.GetMethod("OnThemeChanged", BindingFlags.Static | BindingFlags.NonPublic);
                    if (onThemeChanged != null)
                    {
                        onThemeChanged.Invoke(null, null);
                    }

                    var invalidateResources = systemResources.GetMethod("InvalidateResources", BindingFlags.Static | BindingFlags.NonPublic);
                    if (invalidateResources != null)
                    {
                        invalidateResources.Invoke(null, new object[] { false });
                    }
                }
            }
        }
    }
} ;