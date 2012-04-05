using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
                Application.Current.ApplyTheme(null, Application.Current.Resources[(string)item.Header + "Brush"] as SolidColorBrush, null);
            }
        }
    }
} ;