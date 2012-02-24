using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Interop;

using JetBrains.Annotations;

namespace Elysium.NativeExtensions
{
    [PublicAPI]
    public static class Window
    {
        [PublicAPI]
        [SecuritySafeCritical]
        public static void RemoveFromAeroPeek(WindowInteropHelper wnd)
        {
            RemoveFromAeroPeek(wnd.Handle);
        }

        [PublicAPI]
        [SecurityCritical]
        public static void RemoveFromAeroPeek(IntPtr hwnd)
        {
            if (Windows.IsWindows7)
            {
                var value = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
                Marshal.WriteInt32(value, (int)Interop.Bool.True);
                var result = Interop.DwmSetWindowAttribute(hwnd, Interop.DWMWindowAttribute.DWMWA_EXCLUDED_FROM_PEEK, value, Marshal.SizeOf(typeof(int)));
                if (result != 0)
                {
                    throw Marshal.GetExceptionForHR(result);
                }
                Marshal.FreeHGlobal(value);
            }
        }

        [PublicAPI]
        [SecuritySafeCritical]
        public static void RemoveFromAltTab(WindowInteropHelper wnd)
        {
            RemoveFromAltTab(wnd.Handle);
        }

        [PublicAPI]
        [SecurityCritical]
        public static void RemoveFromAltTab(IntPtr hwnd)
        {
            var windowInfo = Interop.GetWindowLong(hwnd, Interop.GWL_EXSTYLE).ToInt32();
            if (windowInfo == 0)
            {
                Trace.TraceError("Return value of GetWindowLong must be non-null");
            }
            Interop.SetLastError(0);
            var result = Interop.SetWindowLong(hwnd, Interop.GWL_EXSTYLE, new IntPtr(windowInfo | Interop.WS_EX_TOOLWINDOW));
            if (result == IntPtr.Zero && Marshal.GetLastWin32Error() != 0)
            {
                throw new Win32Exception();
            }

            if (!Interop.SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, Interop.SWP_NOMOVE | Interop.SWP_NOSIZE | Interop.SWP_NOZORDER | Interop.SWP_FRAMECHANGED))
            {
                throw new Win32Exception();
            }
        }

        [PublicAPI]
        [SecuritySafeCritical]
        public static void RemoveFromFlip3D(WindowInteropHelper wnd)
        {
            RemoveFromFlip3D(wnd.Handle);
        }

        [PublicAPI]
        [SecurityCritical]
        public static void RemoveFromFlip3D(IntPtr hwnd)
        {
            if (Windows.IsWindowsVista || Windows.IsWindows7)
            {
                var value = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
                Marshal.WriteInt32(value, (int)Interop.DWMFlip3DWindowPolicy.DWMFLIP3D_EXCLUDEBELOW);
                var result = Interop.DwmSetWindowAttribute(hwnd, Interop.DWMWindowAttribute.DWMWA_FLIP3D_POLICY, value, Marshal.SizeOf(typeof(int)));
                if (result != 0)
                {
                    throw Marshal.GetExceptionForHR(result);
                }
                Marshal.FreeHGlobal(value);
            }
        }

        [PublicAPI]
        [SecuritySafeCritical]
        public static void SetFullScreenAndBottomMost(WindowInteropHelper wnd)
        {
            SetFullScreenAndBottomMost(wnd.Handle);
        }

        [PublicAPI]
        [SecurityCritical]
        public static void SetFullScreenAndBottomMost(IntPtr hwnd)
        {
            var windowInfo = Interop.GetWindowLong(hwnd, Interop.GWL_EXSTYLE).ToInt32();
            if (windowInfo == 0)
            {
                Trace.TraceError("Return value of GetWindowLong must be non-null");
            }
            Interop.SetLastError(0);
            var result = Interop.SetWindowLong(hwnd, Interop.GWL_EXSTYLE, new IntPtr(windowInfo | Interop.WS_EX_NOACTIVATE));
            if (result == IntPtr.Zero && Marshal.GetLastWin32Error() != 0)
            {
                throw new Win32Exception();
            }

            var pvParam = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Interop.RECT)));
            if (!Interop.SystemParametersInfo(Interop.SPI_GETWORKAREA, 0, pvParam, 0))
            {
                throw new Win32Exception();
            }
            var primaryScreenWorkArea = (Interop.RECT)Marshal.PtrToStructure(pvParam, typeof(Interop.RECT));
            Marshal.FreeHGlobal(pvParam);

            Taskbar.Invalidate();
            var left = Taskbar.Position == TaskbarPosition.Left && Taskbar.AutoHide ? 1 : 0;
            var top = Taskbar.Position == TaskbarPosition.Top && Taskbar.AutoHide ? 1 : 0;
            var width = primaryScreenWorkArea.right - primaryScreenWorkArea.left - (Taskbar.Position == TaskbarPosition.Right && Taskbar.AutoHide ? 1 : 0);
            var height = primaryScreenWorkArea.bottom - primaryScreenWorkArea.top - (Taskbar.Position == TaskbarPosition.Bottom && Taskbar.AutoHide ? 1 : 0);

            if (!Interop.SetWindowPos(hwnd, Interop.HWND_BOTTOM, left, top, width, height, Interop.SWP_NOMOVE | Interop.SWP_NOACTIVATE))
            {
                throw new Win32Exception();
            }
        }
    }
} ;