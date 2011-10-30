using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;

namespace Elysium.Platform.Interop
{
    [SecurityCritical]
    internal static class Window
    {
        internal static void RemoveFromAeroPeek(IntPtr hwnd)
        {
            if (Windows.IsWindows7)
            {
                var value = Marshal.AllocHGlobal(sizeof(int));
                Marshal.WriteInt32(value, (int)WinAPI.Bool.True);
                WinAPI.DwmSetWindowAttribute(hwnd, WinAPI.DWMWindowAttribute.DWMWA_EXCLUDED_FROM_PEEK, value, sizeof(int));
                Marshal.FreeHGlobal(value);
            }
        }

        internal static void RemoveFromAltTab(IntPtr hwnd)
        {
            WinAPI.SetWindowLong(hwnd, WinAPI.GWL_EXSTYLE, new IntPtr(WinAPI.GetWindowLong(hwnd, WinAPI.GWL_EXSTYLE).ToInt32() | WinAPI.WS_EX_TOOLWINDOW));
        }

        internal static void RemoveFromFlip3D(IntPtr hwnd)
        {
            var value = Marshal.AllocHGlobal(sizeof(int));
            Marshal.WriteInt32(value, (int)WinAPI.DWMFlip3DWindowPolicy.DWMFLIP3D_EXCLUDEBELOW);
            WinAPI.DwmSetWindowAttribute(hwnd, WinAPI.DWMWindowAttribute.DWMWA_FLIP3D_POLICY, value, sizeof(int));
            Marshal.FreeHGlobal(value);
        }

        internal static void SetFullScreenAndBottomMost(IntPtr hwnd)
        {
            var left = Taskbar.Instance.Position == TaskbarPosition.Left ? Taskbar.Instance.AutoHide ? 1 : Taskbar.Instance.Width : 0;
            var top = Taskbar.Instance.Position == TaskbarPosition.Top ? Taskbar.Instance.AutoHide ? 1 : Taskbar.Instance.Height : 0;
            var width = SystemParameters.PrimaryScreenWidth -
                        (Taskbar.Instance.Position == TaskbarPosition.Right ? Taskbar.Instance.AutoHide ? 1 : Taskbar.Instance.Width : 0);
            var height = SystemParameters.PrimaryScreenHeight -
                         (Taskbar.Instance.Position == TaskbarPosition.Bottom ? Taskbar.Instance.AutoHide ? 1 : Taskbar.Instance.Height : 0);
            WinAPI.SetWindowPos(hwnd, WinAPI.HWND_BOTTOM, left, top, (int)width, (int)height, WinAPI.SWP_NOMOVE | WinAPI.SWP_NOACTIVATE);
        }
    }
} ;