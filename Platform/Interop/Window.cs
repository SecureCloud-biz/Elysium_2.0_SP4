using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace Elysium.Platform.Interop
{
    public static class Window
    {
        public static void RemoveFromAeroPeek(IntPtr hwnd)
        {
            if (Windows.IsWindows7)
            {
                var value = Marshal.AllocHGlobal(sizeof(int));
                Marshal.WriteInt32(value, (int)WinAPI.Bool.True);
                WinAPI.DwmSetWindowAttribute(hwnd, WinAPI.DWMWindowAttribute.DWMWA_EXCLUDED_FROM_PEEK, value, sizeof(int));
                Marshal.FreeHGlobal(value);
            }
        }

        public static void RemoveFromAltTab(IntPtr hwnd)
        {
            WinAPI.SetWindowLong(hwnd, WinAPI.GWL_EXSTYLE, new IntPtr(WinAPI.GetWindowLong(hwnd, WinAPI.GWL_EXSTYLE).ToInt32() | WinAPI.WS_EX_TOOLWINDOW));
        }

        public static void RemoveFromFlip3D(IntPtr hwnd)
        {
            var value = Marshal.AllocHGlobal(sizeof(int));
            Marshal.WriteInt32(value, (int)WinAPI.DWMFlip3DWindowPolicy.DWMFLIP3D_EXCLUDEBELOW);
            WinAPI.DwmSetWindowAttribute(hwnd, WinAPI.DWMWindowAttribute.DWMWA_FLIP3D_POLICY, value, sizeof(int));
            Marshal.FreeHGlobal(value);
        }

        public static void SetFullScreenAndBottomMost(IntPtr hwnd)
        {
            var left = Taskbar.Instance.Position == TaskbarPosition.Left ? Taskbar.Instance.Width : 0;
            var top = Taskbar.Instance.Position == TaskbarPosition.Top ? Taskbar.Instance.Height : 0;
            var width = SystemParameters.PrimaryScreenWidth - (Taskbar.Instance.Position == TaskbarPosition.Right ? Taskbar.Instance.Width : 0);
            var height = SystemParameters.PrimaryScreenHeight - (Taskbar.Instance.Position == TaskbarPosition.Bottom ? Taskbar.Instance.Height : 0);
            WinAPI.SetWindowPos(hwnd, WinAPI.HWND_BOTTOM, left, top, (int)width, (int)height, WinAPI.SWP_NOMOVE | WinAPI.SWP_NOACTIVATE);
        }
    }
} ;