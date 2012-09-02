using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;

using JetBrains.Annotations;

namespace Elysium.Native
{
    [PublicAPI]
    internal static class Taskbar
    {
        [SecurityCritical]
        private static bool _isCacheValid;

        [PublicAPI]
        [SecuritySafeCritical]
        internal static void Invalidate()
        {
            _isCacheValid = false;
        }

        [SecurityCritical]
        private static void InvalidateInternal()
        {
            _handle = Interop.FindWindow("Shell_TrayWnd", null);
            if (_handle == IntPtr.Zero)
            {
                throw new Win32Exception();
            }

            var data = new Interop.APPBARDATA { cbSize = Marshal.SizeOf(typeof(Interop.APPBARDATA)), hWnd = _handle };
            var result = Interop.SHAppBarMessage(Interop.ABM_GETTASKBARPOS, ref data);
            if (result == IntPtr.Zero)
            {
                throw new InvalidOperationException();
            }

            _position = (TaskbarPosition)data.uEdge;
            _left = data.rc.left;
            _top = data.rc.top;
            _right = data.rc.right;
            _bottom = data.rc.bottom;

            data = new Interop.APPBARDATA { cbSize = Marshal.SizeOf(typeof(Interop.APPBARDATA)), hWnd = _handle };
            result = Interop.SHAppBarMessage(Interop.ABM_GETSTATE, ref data);

            var state = result.ToInt32();

            // See http://msdn.microsoft.com/en-us/library/bb787947(v=vs.85).aspx
            _alwaysOnTop = (state & Interop.ABS_ALWAYSONTOP) == Interop.ABS_ALWAYSONTOP || Windows.IsWindows7;
            _autoHide = (state & Interop.ABS_AUTOHIDE) == Interop.ABS_AUTOHIDE;

            _isCacheValid = true;
        }

        [PublicAPI]
        internal static IntPtr Handle
        {
            [SecurityCritical]
            get
            {
                if (!_isCacheValid)
                {
                    InvalidateInternal();
                }
                return _handle;
            }
        }

        private static IntPtr _handle;

        [PublicAPI]
        internal static TaskbarPosition Position
        {
            [SecuritySafeCritical]
            get
            {
                if (!_isCacheValid)
                {
                    InvalidateInternal();
                }
                return _position;
            }
        }

        private static TaskbarPosition _position;

        [PublicAPI]
        internal static int Left
        {
            [SecuritySafeCritical]
            get
            {
                if (!_isCacheValid)
                {
                    InvalidateInternal();
                }
                return _left;
            }
        }

        private static int _left;

        [PublicAPI]
        internal static int Top
        {
            [SecuritySafeCritical]
            get
            {
                if (!_isCacheValid)
                {
                    InvalidateInternal();
                }
                return _top;
            }
        }

        private static int _top;

        [PublicAPI]
        internal static int Right
        {
            [SecuritySafeCritical]
            get
            {
                if (!_isCacheValid)
                {
                    InvalidateInternal();
                }
                return _right;
            }
        }

        private static int _right;

        [PublicAPI]
        internal static int Bottom
        {
            [SecuritySafeCritical]
            get
            {
                if (!_isCacheValid)
                {
                    InvalidateInternal();
                }
                return _bottom;
            }
        }

        private static int _bottom;

        [PublicAPI]
        internal static int Width
        {
            [SecuritySafeCritical]
            get { return Right - Left; }
        }

        [PublicAPI]
        internal static int Height
        {
            [SecuritySafeCritical]
            get { return Bottom - Top; }
        }

        [PublicAPI]
        internal static bool AlwaysOnTop
        {
            [SecuritySafeCritical]
            get
            {
                if (!_isCacheValid)
                {
                    InvalidateInternal();
                }
                return _alwaysOnTop;
            }
        }

        private static bool _alwaysOnTop;

        [PublicAPI]
        internal static bool AutoHide
        {
            [SecuritySafeCritical]
            get
            {
                if (!_isCacheValid)
                {
                    InvalidateInternal();
                }
                return _autoHide;
            }
        }

        private static bool _autoHide;
    }
}