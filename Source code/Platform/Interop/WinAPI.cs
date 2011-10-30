using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Elysium.Platform.Interop
{
    [SecurityCritical]
    internal static class WinAPI
    {
// ReSharper disable InconsistentNaming
        internal const int GWL_EXSTYLE = -20;
        internal const int WS_EX_TOOLWINDOW = 0x00000080;

        internal const uint SWP_NOACTIVATE = 0x0010;
        internal const uint SWP_NOMOVE = 0x0002;
        internal const uint SWP_NOSIZE = 0x0001;

        internal static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        internal const uint ABM_GETTASKBARPOS = 0x00000005;
        internal const uint ABM_GETSTATE = 0x00000004;

        internal const uint ABS_AUTOHIDE = 0x0000001;
        internal const uint ABS_ALWAYSONTOP = 0x0000002;

        internal enum Bool
        {
            False = 0,
            True
        }

        [ComVisible(true)]
        internal enum DWMWindowAttribute : uint
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
        internal enum DWMFlip3DWindowPolicy
        {
            DWMFLIP3D_DEFAULT,
            DWMFLIP3D_EXCLUDEBELOW,
            DWMFLIP3D_EXCLUDEABOVE,
            DWMFLIP3D_LAST
        }

        internal enum ABE : uint
        {
            Left = 0,
            Top,
            Right,
            Bottom
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            internal int left;
            internal int top;
            internal int right;
            internal int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct APPBARDATA
        {
            internal uint cbSize;
            internal IntPtr hWnd;
            internal uint uCallbackMessage;
            internal ABE uEdge;
            internal RECT rc;

            [MarshalAs(UnmanagedType.SysInt)] internal IntPtr lParam;
        }

// ReSharper restore InconsistentNaming

#if X86
        private const string GetWindowLongEntryPoint = "GetWindowLong";
#else
        private const string GetWindowLongEntryPoint = "GetWindowLongPtr";
#endif

        [DllImport("user32.dll", SetLastError = true, EntryPoint = GetWindowLongEntryPoint)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

#if X86
        private const string SetWindowLongEntryPoint = "SetWindowLong";
#else
        private const string SetWindowLongEntryPoint = "SetWindowLongPtr";
#endif

        [DllImport("user32.dll", SetLastError = true, EntryPoint = SetWindowLongEntryPoint)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        internal static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, [MarshalAs(UnmanagedType.SysInt)] IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        [DllImport("dwmapi.dll", SetLastError = false, PreserveSig = true)]
        [return: MarshalAs(UnmanagedType.Error)]
        internal static extern int DwmSetWindowAttribute(IntPtr hwnd,
                                                         DWMWindowAttribute dwmAttribute,
                                                         IntPtr pvAttribute,
                                                         uint cbAttribute);


        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("shell32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.SysUInt)]
        internal static extern UIntPtr SHAppBarMessage(uint dwMessage, [In, Out] ref APPBARDATA pData);
    }
} ;