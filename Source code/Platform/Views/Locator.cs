namespace Elysium.Platform.Views
{
    internal static class Locator
    {
        internal static Main Main
        {
            get { return _main ?? (_main = new Main { DataContext = ViewModels.Locator.Main }); }
        }

        private static Main _main;
    }
} ;