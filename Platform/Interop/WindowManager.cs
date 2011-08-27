using System;
using System.Runtime.InteropServices;

namespace Elysium.Platform.Interop
{
    public static class WindowManager
    {
        public static void RemoveFromAeroPeek(IntPtr hwnd)
        {
            if (Environment.OSVersion.Version.Major == 6 &&
                Environment.OSVersion.Version.Minor == 1)
            {
                var value = Marshal.AllocHGlobal(sizeof(int));
                Marshal.WriteInt32(value, (int)WinAPI.Bool.True);
                WinAPI.DwmSetWindowAttribute(hwnd, WinAPI.DWMWindowAttribute.DWMWA_EXCLUDED_FROM_PEEK, value, sizeof(int));
            }
        }

        public static void RemoveFromAltTab(IntPtr hwnd)
        {
            if (!Environment.Is64BitProcess)
                WinAPI.SetWindowLong(hwnd, WinAPI.GWL_EXSTYLE, WinAPI.GetWindowLong(hwnd, WinAPI.GWL_EXSTYLE) | WinAPI.WS_EX_TOOLWINDOW);
            else
                WinAPI.SetWindowLongPtr(hwnd, WinAPI.GWL_EXSTYLE, WinAPI.GetWindowLongPtr(hwnd, WinAPI.GWL_EXSTYLE) | WinAPI.WS_EX_TOOLWINDOW);
        }

        public static void RemoveFromFlip3D(IntPtr hwnd)
        {
            var value = Marshal.AllocHGlobal(sizeof(int));
            Marshal.WriteInt32(value, (int)WinAPI.DWMFlip3DWindowPolicy.DWMFLIP3D_EXCLUDEBELOW);
            WinAPI.DwmSetWindowAttribute(hwnd, WinAPI.DWMWindowAttribute.DWMWA_FLIP3D_POLICY, value, sizeof(int));
        }

        public static void SetBottomMost(IntPtr hwnd)
        {
            WinAPI.SetWindowPos(hwnd, WinAPI.HWND_BOTTOM, 0, 0, 0, 0, WinAPI.SWP_NOSIZE | WinAPI.SWP_NOMOVE | WinAPI.SWP_NOACTIVATE);
        }
    }
} ;