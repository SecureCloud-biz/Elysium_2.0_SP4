using System.Windows.Media;

namespace Elysium.Test
{
    public partial class App
    {
        private void StartupHandler(object sender, System.Windows.StartupEventArgs e)
        {
            var accentBrush = new SolidColorBrush(AccentColors.Viridian);
            accentBrush.Freeze();
            var contrastBrush = new SolidColorBrush(Colors.White);
            contrastBrush.Freeze();
            this.ApplyTheme(Theme.Light, accentBrush, contrastBrush);
        }
    }
} ;