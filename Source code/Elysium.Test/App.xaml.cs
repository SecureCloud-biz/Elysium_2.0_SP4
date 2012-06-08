using System.Windows.Media;

namespace Elysium.Test
{
    public partial class App
    {
        private void StartupHandler(object sender, System.Windows.StartupEventArgs e)
        {
            this.ApplyTheme(Theme.Light, AccentBrushes.Viridian, Brushes.White);
        }
    }
} ;