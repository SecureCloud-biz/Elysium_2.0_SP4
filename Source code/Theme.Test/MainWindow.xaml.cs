namespace Elysium.Theme.Test
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ThemeManager.Instance.Theme == Theme.Light)
            {
                ThemeManager.Instance.Dark(ThemeManager.Instance.Accent);
            }
            else
            {
                ThemeManager.Instance.Light(ThemeManager.Instance.Accent);
            }
        }
    }
} ;