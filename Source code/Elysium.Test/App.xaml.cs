using System.Windows.Media;

namespace Elysium.Test
{
    public sealed partial class App
    {
        private void StartupHandler(object sender, System.Windows.StartupEventArgs e)
        {
            this.ApplyTheme(Theme.Dark, AccentBrushes.Blue, Brushes.White);
        }
    }
} ;