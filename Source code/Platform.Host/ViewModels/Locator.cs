namespace Elysium.Platform.ViewModels
{
    internal static class Locator
    {
        public static MainViewModel Main
        {
            get { return _main ?? (_main = new MainViewModel()); }
        }

        private static MainViewModel _main;

        public static AccessRequestViewModel AccessRequest
        {
            get { return _accessRequest ?? (_accessRequest = new AccessRequestViewModel()); }
        }

        private static AccessRequestViewModel _accessRequest;
    }
} ;