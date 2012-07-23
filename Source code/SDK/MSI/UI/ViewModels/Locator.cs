using GalaSoft.MvvmLight;

namespace Elysium.SDK.MSI.UI.ViewModels
{
    public static class Locator
    {
        public static MainViewModel MainViewModel
        {
            get
            {
                if (ViewModelBase.IsInDesignModeStatic)
                {
                    return _mainViewModel ?? new MainViewModel();
                }
                return _mainViewModel;
            }
            set { _mainViewModel = value; }
        }

        private static MainViewModel _mainViewModel;
    }
} ;