using System;
using System.Runtime.InteropServices;

namespace Elysium.Platform.Interop
{
    internal static class WinAPI
    {

// ReSharper disable InconsistentNaming

        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_TOOLWINDOW = 0x00000080;

        public const uint SWP_NOACTIVATE = 0x0010;
        public const uint SWP_NOMOVE = 0x0002;
        public const uint SWP_NOSIZE = 0x0001;

        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        public enum Bool
        {
            False = 0,
            True
        }

        [ComVisible(true)]
        public enum DWMWindowAttribute
        {
            DWMWA_NCRENDERING_ENABLED = 1,
            DWMWA_NCRENDERING_POLICY,
            DWMWA_TRANSITIONS_FORCEDISABLED,
            DWMWA_ALLOW_NCPAINT,
            DWMWA_CAPTION_BUTTON_BOUNDS,
            DWMWA_NONCLIENT_RTL_LAYOUT,
            DWMWA_FORCE_ICONIC_REPRESENTATION,
            DWMWA_FLIP3D_POLICY,
            DWMWA_EXTENDED_FRAME_BOUNDS,
            DWMWA_HAS_ICONIC_BITMAP,
            DWMWA_DISALLOW_PEEK,
            DWMWA_EXCLUDED_FROM_PEEK,
            DWMWA_LAST
        }

        [ComVisible(true)]
        public enum DWMFlip3DWindowPolicy
        {
            DWMFLIP3D_DEFAULT,
            DWMFLIP3D_EXCLUDEBELOW,
            DWMFLIP3D_EXCLUDEABOVE,
            DWMFLIP3D_LAST 
        }

// ReSharper restore InconsistentNaming
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.I8)]
        public static extern Int64 GetWindowLongPtr(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, [MarshalAs(UnmanagedType.I4)] int dwNewLong);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.I8)]
        public static extern Int64 SetWindowLongPtr(IntPtr hWnd, int nIndex, [MarshalAs(UnmanagedType.I8)] Int64 dwNewLong);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);
        
        [DllImport("dwmapi.dll", PreserveSig = true)]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, DWMWindowAttribute dwmAttribute, IntPtr pvAttribute, uint cbAttribute);
    }
} ;