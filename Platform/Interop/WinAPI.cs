using System;
using System.Runtime.InteropServices;

namespace Elysium.Platform.Interop
{
    public static class WinAPI
    {
// ReSharper disable InconsistentNaming
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_TOOLWINDOW = 0x00000080;

        public const uint SWP_NOACTIVATE = 0x0010;
        public const uint SWP_NOMOVE = 0x0002;
        public const uint SWP_NOSIZE = 0x0001;

        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        public const uint ABM_GETTASKBARPOS = 0x00000005;
        public const uint ABM_GETSTATE = 0x00000004;

        public const uint ABS_AUTOHIDE = 0x0000001;
        public const uint ABS_ALWAYSONTOP = 0x0000002;

        public enum Bool
        {
            False = 0,
            True
        }

        [ComVisible(true)]
        public enum DWMWindowAttribute : uint
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

        public enum ABE : uint
        {
            Left = 0,
            Top,
            Right,
            Bottom
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {
            public uint cbSize;
            public IntPtr hWnd;
            public uint uCallbackMessage;
            public ABE uEdge;
            public RECT rc;

            [MarshalAs(UnmanagedType.SysInt)]
            public IntPtr lParam;
        }

// ReSharper restore InconsistentNaming

#if X86
        private const string GetWindowLongEntryPoint = "GetWindowLong";
#else
        private const string GetWindowLongEntryPoint = "GetWindowLongPtr";
#endif

        [DllImport("user32.dll", SetLastError = true, EntryPoint = GetWindowLongEntryPoint)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

#if X86
        private const string SetWindowLongEntryPoint = "SetWindowLong";
#else
        private const string SetWindowLongEntryPoint = "SetWindowLongPtr";
#endif

        [DllImport("user32.dll", SetLastError = true, EntryPoint = SetWindowLongEntryPoint)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, [MarshalAs(UnmanagedType.SysInt)] IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        [DllImport("dwmapi.dll", SetLastError = false, PreserveSig = true)]
        [return: MarshalAs(UnmanagedType.Error)]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd,
                                                       DWMWindowAttribute dwmAttribute,
                                                       IntPtr pvAttribute,
                                                       uint cbAttribute);


        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("shell32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.SysUInt)]
        public static extern UIntPtr SHAppBarMessage(uint dwMessage, [In, Out] ref APPBARDATA pData);
    }
} ;