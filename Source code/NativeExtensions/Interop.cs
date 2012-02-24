using System;
using System.Runtime.InteropServices;
using System.Security;

using JetBrains.Annotations;

namespace Elysium.NativeExtensions
{
    [SecurityCritical]
    public static class Interop
    {
// ReSharper disable InconsistentNaming

        #region Constants

        [PublicAPI]
        [MarshalAs(UnmanagedType.SysUInt)]
        public const int ABS_ALWAYSONTOP = 0x0000002;

        [PublicAPI]
        [MarshalAs(UnmanagedType.SysUInt)]
        public const int ABS_AUTOHIDE = 0x0000001;

        [PublicAPI]
        [MarshalAs(UnmanagedType.U4)]
        public const int ABM_GETSTATE = 0x00000004;

        [PublicAPI]
        [MarshalAs(UnmanagedType.U4)]
        public const int ABM_GETTASKBARPOS = 0x00000005;

        [PublicAPI]
        public const int GWL_EXSTYLE = -20;

        [PublicAPI]
        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        [PublicAPI]
        public const int SPI_GETWORKAREA = 48;

        [PublicAPI]
        [MarshalAs(UnmanagedType.U4)]
        public const int SWP_FRAMECHANGED = 0x0020;

        [PublicAPI]
        [MarshalAs(UnmanagedType.U4)]
        public const int SWP_NOACTIVATE = 0x0010;

        [PublicAPI]
        [MarshalAs(UnmanagedType.U4)]
        public const int SWP_NOMOVE = 0x0002;

        [PublicAPI]
        [MarshalAs(UnmanagedType.U4)]
        public const int SWP_NOSIZE = 0x0001;

        [PublicAPI]
        [MarshalAs(UnmanagedType.U4)]
        public const int SWP_NOZORDER = 0x0004;

        [PublicAPI]
        public const int WM_WINDOWPOSCHANGING = 0x0046;

        [PublicAPI]
        public const int WS_EX_NOACTIVATE = 0x08000000;

        [PublicAPI]
        public const int WS_EX_TOOLWINDOW = 0x00000080;

        #endregion

        #region Enumerations

        [PublicAPI]
        public enum ABE
        {
            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            Left = 0,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            Top,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            Right,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            Bottom
        }

        [PublicAPI]
        public enum Bool
        {
            [PublicAPI]
            False = 0,

            [PublicAPI]
            True
        }

        [PublicAPI]
        [Flags]
        public enum DesktopFlags
        {
            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DESKTOP_READOBJECTS = 0x0001,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DESKTOP_CREATEWINDOW = 0x0002,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DESKTOP_CREATEMENU = 0x0004,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DESKTOP_HOOKCONTROL = 0x0008,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DESKTOP_JOURNALRECORD = 0x0010,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DESKTOP_JOURNALPLAYBACK = 0x0020,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DESKTOP_ENUMERATE = 0x0040,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DESKTOP_WRITEOBJECTS = 0x0080,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DESKTOP_SWITCHDESKTOP = 0x0100,
        }

        [PublicAPI]
        [ComVisible(true)]
        public enum DWMFlip3DWindowPolicy
        {
            [PublicAPI]
            DWMFLIP3D_DEFAULT,

            [PublicAPI]
            DWMFLIP3D_EXCLUDEBELOW,

            [PublicAPI]
            DWMFLIP3D_EXCLUDEABOVE,

            [PublicAPI]
            DWMFLIP3D_LAST
        }

        [PublicAPI]
        [ComVisible(true)]
        public enum DWMWindowAttribute
        {
            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DWMWA_NCRENDERING_ENABLED = 1,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DWMWA_NCRENDERING_POLICY,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DWMWA_TRANSITIONS_FORCEDISABLED,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DWMWA_ALLOW_NCPAINT,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DWMWA_CAPTION_BUTTON_BOUNDS,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DWMWA_NONCLIENT_RTL_LAYOUT,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DWMWA_FORCE_ICONIC_REPRESENTATION,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DWMWA_FLIP3D_POLICY,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DWMWA_EXTENDED_FRAME_BOUNDS,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DWMWA_HAS_ICONIC_BITMAP,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DWMWA_DISALLOW_PEEK,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DWMWA_EXCLUDED_FROM_PEEK,

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            DWMWA_LAST
        }

        #endregion

        #region Structures

        [PublicAPI]
        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {
            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;

            [PublicAPI]
            public IntPtr hWnd;

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            public int uCallbackMessage;

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            public ABE uEdge;

            [PublicAPI]
            public RECT rc;

            [PublicAPI]
            [MarshalAs(UnmanagedType.SysInt)]
            public IntPtr lParam;
        }

        [PublicAPI]
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            [PublicAPI]
            public int left;

            [PublicAPI]
            public int top;

            [PublicAPI]
            public int right;

            [PublicAPI]
            public int bottom;
        }

        [PublicAPI]
        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            [PublicAPI]
            public IntPtr hWnd;

            [PublicAPI]
            public IntPtr hWndInsertAfter;

            [PublicAPI]
            public int x;

            [PublicAPI]
            public int y;

            [PublicAPI]
            public int cx;

            [PublicAPI]
            public int cy;

            [PublicAPI]
            [MarshalAs(UnmanagedType.U4)]
            public int uFlags;
        }

        #endregion

        #region Functions

        [PublicAPI]
        [SecurityCritical]
        [DllImport("user32.dll", EntryPoint = "CreateDesktop", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseDesktop(IntPtr hDesktop);

        [PublicAPI]
        [SecurityCritical]
        [DllImport("user32.dll", EntryPoint = "CreateDesktop", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CreateDesktop(string lpszDesktop,
                                                  string lpszDevice,
                                                  IntPtr pDevmode,
                                                  [MarshalAs(UnmanagedType.U4)] int dwFlags,
                                                  [MarshalAs(UnmanagedType.U4)] DesktopFlags dwDesiredAccess,
                                                  IntPtr lpSecurityAttributes);

        [PublicAPI]
        [SecurityCritical]
        [DllImport("dwmapi.dll", EntryPoint = "DwmSetWindowAttribute", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = false, PreserveSig = true)]
        [return: MarshalAs(UnmanagedType.Error)]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd,
                                                       [MarshalAs(UnmanagedType.U4)] DWMWindowAttribute dwmAttribute,
                                                       IntPtr pvAttribute,
                                                       [MarshalAs(UnmanagedType.U4)] int cbAttribute);

        [PublicAPI]
        [SecurityCritical]
        [DllImport("user32.dll", EntryPoint = "FindWindow", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [PublicAPI]
        [SecurityCritical]
        [DllImport("kernel32.dll", EntryPoint = "GetCurrentThreadId", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = false)]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern int GetCurrentThreadId();

        [PublicAPI]
        [SecurityCritical]
        [DllImport("user32.dll", EntryPoint = "GetThreadDesktop", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetThreadDesktop([MarshalAs(UnmanagedType.U4)] int dwThreadId);

#if X86
        private const string GetWindowLongEntryPoint = "GetWindowLong";
#else
        private const string GetWindowLongEntryPoint = "GetWindowLongPtr";
#endif

        [PublicAPI]
        [SecurityCritical]
        [DllImport("user32.dll", EntryPoint = GetWindowLongEntryPoint, ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        [PublicAPI]
        [SecurityCritical]
        [DllImport("imm32.dll", EntryPoint = "ImmDisableIME", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ImmDisableIME([MarshalAs(UnmanagedType.U4)] int idThread);

        [PublicAPI]
        [SecurityCritical]
        [DllImport("user32.dll", EntryPoint = "SetThreadDesktop", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetThreadDesktop(IntPtr hDesktop);

#if X86
        private const string SetWindowLongEntryPoint = "SetWindowLong";
#else
        private const string SetWindowLongEntryPoint = "SetWindowLongPtr";
#endif

        [PublicAPI]
        [SecurityCritical]
        [DllImport("kernel32.dll", EntryPoint = "SetLastError", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void SetLastError(int dwErrorCode);

        [PublicAPI]
        [SecurityCritical]
        [DllImport("user32.dll", EntryPoint = SetWindowLongEntryPoint, ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, [MarshalAs(UnmanagedType.SysInt)] IntPtr dwNewLong);

        [PublicAPI]
        [SecurityCritical]
        [DllImport("user32.dll", EntryPoint = "SetWindowPos", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, [MarshalAs(UnmanagedType.U4)] int uFlags);

        [PublicAPI]
        [SecurityCritical]
        [DllImport("shell32.dll", EntryPoint = "SHAppBarMessage", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = false)]
        [return: MarshalAs(UnmanagedType.SysUInt)]
        public static extern IntPtr SHAppBarMessage([MarshalAs(UnmanagedType.U4)] int dwMessage, [In] [Out] ref APPBARDATA pData);

        [PublicAPI]
        [SecurityCritical]
        [DllImport("user32.dll", EntryPoint = "SwitchDesktop", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SwitchDesktop(IntPtr hDesktop);

        [PublicAPI]
        [SecurityCritical]
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SystemParametersInfo([MarshalAs(UnmanagedType.U4)] int uiAction,
                                                       [MarshalAs(UnmanagedType.U4)] int uiParam,
                                                       [In] [Out] IntPtr pvParam,
                                                       [MarshalAs(UnmanagedType.U4)] int fWinIni);

        #endregion

// ReSharper restore InconsistentNaming
    }
} ;