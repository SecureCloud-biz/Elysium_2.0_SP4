namespace Elysium.Theme.Test
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LightClick(object sender, System.Windows.RoutedEventArgs e)
        {
            ThemeManager.Instance.Light(ThemeManager.Instance.Accent);
        }

        private void DarkClick(object sender, System.Windows.RoutedEventArgs e)
        {
            ThemeManager.Instance.Dark(ThemeManager.Instance.Accent);
        }
    }
} ;