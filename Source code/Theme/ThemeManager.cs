using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows;
using System.Windows.Media;

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
        public static Theme? GetTheme(FrameworkElement obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return (Theme?)obj.GetValue(ThemeProperty);
        }

        [PublicAPI]
        public static void SetTheme(FrameworkElement obj, Theme? value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
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
        public static SolidColorBrush GetAccentBrush(FrameworkElement obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return (SolidColorBrush)obj.GetValue(AccentBrushProperty);
        }

        [PublicAPI]
        public static void SetAccentBrush(FrameworkElement obj, SolidColorBrush value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
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
        public static SolidColorBrush GetContrastBrush(FrameworkElement obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return (SolidColorBrush)obj.GetValue(ContrastBrushProperty);
        }

        [PublicAPI]
        public static void SetContrastBrush(FrameworkElement obj, SolidColorBrush value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
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

        [PublicAPI]
        public static void ApplyTheme(this Application application, Theme? theme,
                                      SolidColorBrush accentBrush, SolidColorBrush contrastBrush)
        {
            // Resource dictionaries paths
            var accentColorsUri = new Uri("/Elysium;component/Themes/AccentColors.xaml", UriKind.Relative);
            var lightColorsUri = new Uri("/Elysium;component/Themes/LightColors.xaml", UriKind.Relative);
            var darkColorsUri = new Uri("/Elysium;component/Themes/DarkColors.xaml", UriKind.Relative);

            // Resource dictionaries
            var accentColorsDictionary = new ResourceDictionary { Source = accentColorsUri };
            var lightColorsDictionary = new ResourceDictionary { Source = lightColorsUri };
            var darkColorsDictionary = new ResourceDictionary { Source = darkColorsUri };

            // Add AccentColors.xaml, if not included
            if (application.Resources.MergedDictionaries.All(dictionary => dictionary.Source != accentColorsUri))
            {
                application.Resources.MergedDictionaries.Add(accentColorsDictionary);
            }

            if (theme == Theme.Light)
            {
                // Add LightColors.xaml, if not included
                if (application.Resources.MergedDictionaries.All(dictionary => dictionary.Source != lightColorsUri))
                {
                    application.Resources.MergedDictionaries.Add(lightColorsDictionary);
                }

                // Remove DarkColors.xaml, if included
                var darkColorsDictionaries = application.Resources.MergedDictionaries.Where(dictionary => dictionary.Source == darkColorsUri).ToList();
                foreach (var dictionary in darkColorsDictionaries)
                {
                    application.Resources.MergedDictionaries.Remove(dictionary);
                }
            }
            if (theme == Theme.Dark)
            {
                // Add DarkColors.xaml, if not included
                if (application.Resources.MergedDictionaries.All(dictionary => dictionary.Source != darkColorsUri))
                {
                    application.Resources.MergedDictionaries.Add(darkColorsDictionary);
                }

                // Remove LightColors.xaml, if included
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
        }

        [PublicAPI]
        public static void ApplyTheme(this FrameworkElement control, Theme? theme,
                                      SolidColorBrush accentBrush, SolidColorBrush contrastBrush)
        {
            // Resource dictionaries paths
            var accentColorsUri = new Uri("/Elysium;component/Themes/AccentColors.xaml", UriKind.Relative);
            var lightColorsUri = new Uri("/Elysium;component/Themes/LightColors.xaml", UriKind.Relative);
            var darkColorsUri = new Uri("/Elysium;component/Themes/DarkColors.xaml", UriKind.Relative);

            // Resource dictionaries
            var accentColorsDictionary = new ResourceDictionary { Source = accentColorsUri };
            var lightColorsDictionary = new ResourceDictionary { Source = lightColorsUri };
            var darkColorsDictionary = new ResourceDictionary { Source = darkColorsUri };

            // Add AccentColors.xaml, if not included
            if (control.Resources.MergedDictionaries.All(dictionary => dictionary.Source != accentColorsUri))
            {
                control.Resources.MergedDictionaries.Add(accentColorsDictionary);
            }

            if (theme == Theme.Light)
            {
                // Add LightColors.xaml, if not included
                if (control.Resources.MergedDictionaries.All(dictionary => dictionary.Source != lightColorsUri))
                {
                    control.Resources.MergedDictionaries.Add(lightColorsDictionary);
                }

                // Remove DarkColors.xaml, if included
                var darkColorsDictionaries = control.Resources.MergedDictionaries.Where(dictionary => dictionary.Source == darkColorsUri).ToList();
                foreach (var dictionary in darkColorsDictionaries)
                {
                    control.Resources.MergedDictionaries.Remove(dictionary);
                }
            }
            if (theme == Theme.Dark)
            {
                // Add DarkColors.xaml, if not included
                if (control.Resources.MergedDictionaries.All(dictionary => dictionary.Source != darkColorsUri))
                {
                    control.Resources.MergedDictionaries.Add(darkColorsDictionary);
                }

                // Remove LightColors.xaml, if included
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
        }
    }
} ;