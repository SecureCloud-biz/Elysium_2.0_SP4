namespace Elysium.Platform.Views
{
    internal static class Locator
    {
        public static MainView Main
        {
            get { return _main ?? (_main = new MainView()); }
        }

        private static MainView _main;

        public static AccessRequestView AccessRequest
        {
            get { return _accessRequest ?? (_accessRequest = new AccessRequestView()); }
        }

        private static AccessRequestView _accessRequest;
    }
} ;