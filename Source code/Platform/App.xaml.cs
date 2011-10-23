using System.Windows;

namespace Elysium.Platform
{
    public sealed partial class App
    {
        public App()
        {
            InitializeComponent();

            if (Interop.Windows.IsWindowsXP)
            {
                MessageBox.Show(Platform.Properties.Resources.XPError);
                Shutdown(0x47E);
            }
        }
    }
} ;