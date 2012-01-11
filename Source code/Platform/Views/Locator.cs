namespace Elysium.Platform.Views
{
    public static class Locator
    {
        public static Main Main
        {
            get { return _main ?? (_main = new Main { DataContext = ViewModels.Locator.Main }); }
        }

        private static Main _main;
    }
} ;