using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

using Elysium.Notifications;

namespace Elysium.Test
{
    public sealed partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private static readonly string Windows = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        private static readonly string SegoeUI = Windows + @"\Fonts\SegoeUI.ttf";
        private static readonly string Verdana = Windows + @"\Fonts\Verdana.ttf";

        private void ThemeGlyphInitialized(object sender, EventArgs e)
        {
            ThemeGlyph.FontUri = new Uri(File.Exists(SegoeUI) ? SegoeUI : Verdana);
        }

        private void AccentGlyphInitialized(object sender, EventArgs e)
        {
            AccentGlyph.FontUri = new Uri(File.Exists(SegoeUI) ? SegoeUI : Verdana);
        }

        private void ContrastGlyphInitialized(object sender, EventArgs e)
        {
            ContrastGlyph.FontUri = new Uri(File.Exists(SegoeUI) ? SegoeUI : Verdana);
        }

        private static readonly ResourceDictionary LightBrushes = new ResourceDictionary { Source = new Uri("/Elysium;component/Themes/LightBrushes.xaml", UriKind.Relative) };

        private void LightClick(object sender, RoutedEventArgs e)
        {
            foreach (var brushKey in LightBrushes.Keys)
            {
                SafeSet(Application.Current.Resources, brushKey, LightBrushes[brushKey]);
            }
        }

        private static readonly ResourceDictionary DarkBrushes = new ResourceDictionary { Source = new Uri("/Elysium;component/Themes/DarkBrushes.xaml", UriKind.Relative) };

        private void DarkClick(object sender, RoutedEventArgs e)
        {
            foreach (var brushKey in DarkBrushes.Keys)
            {
                SafeSet(Application.Current.Resources, brushKey, DarkBrushes[brushKey]);
            }
        }

        private void AccentClick(object sender, RoutedEventArgs e)
        {
            var item = e.Source as MenuItem;
            if (item != null)
            {
                var accentBrush = (SolidColorBrush)((Rectangle)item.Icon).Fill;
                SafeSet(Application.Current.Resources, Elysium.Resources.AccentBrushKey, accentBrush);
            }
        }

        private static readonly SolidColorBrush WhiteSemitransparentContrastBrush = new SolidColorBrush(Color.FromArgb(0x1F, 0xFF, 0xFF, 0xFF));

        private void WhiteClick(object sender, RoutedEventArgs e)
        {
            SafeSet(Application.Current.Resources, Elysium.Resources.ContrastBrushKey, Brushes.White);
            SafeSet(Application.Current.Resources, Elysium.Resources.SemitransparentContrastBrushKey, WhiteSemitransparentContrastBrush);
        }
        
        private static readonly SolidColorBrush BlackSemitransparentContrastBrush = new SolidColorBrush(Color.FromArgb(0x1F, 0x00, 0x00, 0x00));

        private void BlackClick(object sender, RoutedEventArgs e)
        {
            SafeSet(Application.Current.Resources, Elysium.Resources.ContrastBrushKey, Brushes.Black);
            SafeSet(Application.Current.Resources, Elysium.Resources.SemitransparentContrastBrushKey, BlackSemitransparentContrastBrush);
        }

        private static void SafeSet(ResourceDictionary resources, object key, object value)
        {
            if (!resources.Contains(key))
            {
                resources.Add(key, value);
            }
            else
            {
                resources[key] = value;
            }
        }

        private void NotificationClick(object sender, RoutedEventArgs e)
        {
#if NETFX4
            NotificationManager.BeginTryPush("Message", "The quick brown fox jumps over the lazy dog");
#elif NETFX45
            NotificationManager.TryPushAsync("Message", "The quick brown fox jumps over the lazy dog");
#endif
        }

        private void DonateClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=KNYYZ7RM6LBCG");
        }

        private void LicenseClick(object sender, RoutedEventArgs e)
        {
            Process.Start("http://elysium.asvishnyakov.com/License.cshtml#header");
        }

        private void AuthorsClick(object sender, RoutedEventArgs e)
        {
            Process.Start("http://elysium.codeplex.com/team/view");
        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {
            Process.Start("http://elysium.asvishnyakov.com/Documentation.cshtml#header");
        }
    }
}