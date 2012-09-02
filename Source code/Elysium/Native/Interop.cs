using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;

using JetBrains.Annotations;

namespace Elysium.Native
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter", Justification = "Suppression is OK here.")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore", Justification = "Suppression is OK here.")]
    [SecurityCritical]
    internal static class Interop
    {
// ReSharper disable InconsistentNaming

        #region Constants

        [MarshalAs(UnmanagedType.SysUInt)]
        internal const int ABS_ALWAYSONTOP = 0x0000002;

        [MarshalAs(UnmanagedType.SysUInt)]
        internal const int ABS_AUTOHIDE = 0x0000001;

        [MarshalAs(UnmanagedType.U4)]
        internal const int ABM_GETSTATE = 0x00000004;

        [MarshalAs(UnmanagedType.U4)]
        internal const int ABM_GETTASKBARPOS = 0x00000005;

        internal const int WM_GETMINMAXINFO = 0x0024;

        [MarshalAs(UnmanagedType.U4)]
        internal const int MONITOR_DEFAULTTONEAREST = 0x00000002;

        #endregion

        #region Enumerations

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        internal enum ABE
        {
            [MarshalAs(UnmanagedType.U4)]
            Left = 0,

            [MarshalAs(UnmanagedType.U4)]
            Top,

            [MarshalAs(UnmanagedType.U4)]
            Right,

            [MarshalAs(UnmanagedType.U4)]
            Bottom
        }

        #endregion

        #region Structures

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        [StructLayout(LayoutKind.Sequential)]
        internal struct MONITORINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;

            public RECT rcMonitor;

            public RECT rcWork;

            [MarshalAs(UnmanagedType.U4)]
            public int dwFlags;
        }

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        [StructLayout(LayoutKind.Sequential)]
        internal struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        [StructLayout(LayoutKind.Sequential)]
        internal struct APPBARDATA
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;

            public IntPtr hWnd;

            [MarshalAs(UnmanagedType.U4)]
            public int uCallbackMessage;

            [MarshalAs(UnmanagedType.U4)]
            public ABE uEdge;

            public RECT rc;

            [MarshalAs(UnmanagedType.SysInt)]
            public IntPtr lParam;
        }

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        [StructLayout(LayoutKind.Sequential)]
        internal struct POINT
        {
            public int x;
            public int y;
        }

        #endregion

        #region Functions

        [SecurityCritical]
        [DllImport("user32.dll", EntryPoint = "FindWindow", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [SecurityCritical]
        [DllImport("user32.dll", EntryPoint = "GetMonitorInfo", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, [Out] IntPtr lpmi);
        
        [SecurityCritical]
        [DllImport("user32.dll", EntryPoint = "MonitorFromWindow", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr MonitorFromWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.U4)] int dwFlags);

        [SecurityCritical]
        [DllImport("shell32.dll", EntryPoint = "SHAppBarMessage", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = false)]
        [return: MarshalAs(UnmanagedType.SysUInt)]
        internal static extern IntPtr SHAppBarMessage([MarshalAs(UnmanagedType.U4)] int dwMessage, [In] [Out] ref APPBARDATA pData);

        #endregion

// ReSharper restore InconsistentNaming
    }
}