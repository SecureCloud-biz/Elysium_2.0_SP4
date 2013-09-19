using System.Security;
using System.Security.Permissions;

namespace Elysium.Extensions
{
    internal static class SecurityHelper
    {
        [SecuritySafeCritical]
        internal static void DemandUnmanagedCode()
        {
            if (_unmanagedCodePermission == null)
            {
                _unmanagedCodePermission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
            }
            _unmanagedCodePermission.Demand();
        }

        private static SecurityPermission _unmanagedCodePermission;

        [SecuritySafeCritical]
        internal static void DemandUIWindowPermission()
        {
            if (_allWindowsUIPermission == null)
            {
                _allWindowsUIPermission = new UIPermission(UIPermissionWindow.AllWindows);
            }
            _allWindowsUIPermission.Demand();
        }

        private static UIPermission _allWindowsUIPermission = null; 
    }
}