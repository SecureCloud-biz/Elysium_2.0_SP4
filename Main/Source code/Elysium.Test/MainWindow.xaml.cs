using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

using Elysium.Markup;
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

        private void LightClick(object sender, RoutedEventArgs e)
        {
            Application.Current.ThemeResources.Source = ThemeResources.Light;
        }

        private void DarkClick(object sender, RoutedEventArgs e)
        {
            Application.Current.ThemeResources.Source = ThemeResources.Dark;
        }

        private void AccentClick(object sender, RoutedEventArgs e)
        {
            var item = e.Source as MenuItem;
            if (item != null)
            {
                var accentBrush = (SolidColorBrush)((Rectangle)item.Icon).Fill;
                Application.Current.ThemeResources[ThemeResource.AccentColor] = accentBrush.Color;
                Application.Current.ThemeResources[ThemeResource.AccentBrush] = accentBrush.AsFrozen();
            }
        }

        private static readonly Color SemitrasparentWhite = Color.FromArgb(0x1F, 0xFF, 0xFF, 0xFF);
        private static readonly SolidColorBrush SemitrasparentWhiteBrush = new SolidColorBrush(SemitrasparentWhite).AsFrozen();

        private void WhiteClick(object sender, RoutedEventArgs e)
        {
            Application.Current.ThemeResources[ThemeResource.ContrastColor] = Colors.White;
            Application.Current.ThemeResources[ThemeResource.ContrastBrush] = Brushes.White;
            Application.Current.ThemeResources[ThemeResource.SemitransparentContrastColor] = SemitrasparentWhite;
            Application.Current.ThemeResources[ThemeResource.SemitransparentContrastBrush] = SemitrasparentWhiteBrush;
        }

        private static readonly Color SemitrasparentBlack = Color.FromArgb(0x1F, 0x00, 0x00, 0x00);
        private static readonly SolidColorBrush SemitrasparentBlackBrush = new SolidColorBrush(SemitrasparentBlack).AsFrozen();

        private void BlackClick(object sender, RoutedEventArgs e)
        {
            Application.Current.ThemeResources[ThemeResource.ContrastColor] = Colors.Black;
            Application.Current.ThemeResources[ThemeResource.ContrastBrush] = Brushes.Black;
            Application.Current.ThemeResources[ThemeResource.SemitransparentContrastColor] = SemitrasparentBlack;
            Application.Current.ThemeResources[ThemeResource.SemitransparentContrastBrush] = SemitrasparentBlackBrush;
        }

#if NETFX4
        private void NotificationClick(object sender, RoutedEventArgs e)
#elif NETFX45
        private async void NotificationClick(object sender, RoutedEventArgs e)
#endif
        {
#if NETFX4
            NotificationManager.BeginTryPush("Message", "The quick brown fox jumps over the lazy dog");
#elif NETFX45
            await NotificationManager.TryPushAsync("Message", "The quick brown fox jumps over the lazy dog");
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