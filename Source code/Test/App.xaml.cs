using System.Windows.Media;

namespace Elysium.Test
{
    public partial class App
    {
        public App()
        {
            var accentBrush = new SolidColorBrush(AccentColors.Blue);
            accentBrush.Freeze();
            var contrastBrush = new SolidColorBrush(Colors.White);
            contrastBrush.Freeze();
            this.ApplyTheme(Theme.Light, accentBrush, contrastBrush);
        }
    }
} ;