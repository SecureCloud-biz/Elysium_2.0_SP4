using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

using Elysium.Notifications;

namespace Elysium.Test
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LightClick(object sender, RoutedEventArgs e)
        {
            Application.Current.ApplyTheme(Theme.Light, null, null);
        }

        private void DarkClick(object sender, RoutedEventArgs e)
        {
            Application.Current.ApplyTheme(Theme.Dark, null, null);
        }

        private void AccentClick(object sender, RoutedEventArgs e)
        {
            var item = e.Source as MenuItem;
            if (item != null)
            {
                var accentBrush = (SolidColorBrush)((Rectangle)item.Icon).Fill;
                Application.Current.ApplyTheme(null, accentBrush, null);
            }
        }

        private void NotificationClick(object sender, RoutedEventArgs e)
        {
            NotificationManager.PushAsync("Message", "The quick brown fox jumps over the lazy dog");
        }
    }
} ;