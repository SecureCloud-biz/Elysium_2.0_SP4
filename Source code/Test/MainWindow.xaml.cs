﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

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
    }
} ;