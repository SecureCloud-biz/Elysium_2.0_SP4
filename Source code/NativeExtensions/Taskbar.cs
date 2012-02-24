using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;

using JetBrains.Annotations;

namespace Elysium.NativeExtensions
{
    [PublicAPI]
    public static class Taskbar
    {
        [SecurityCritical]
        private static bool _isCacheValid;

        [PublicAPI]
        [SecuritySafeCritical]
        public static void Invalidate()
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
            // see http://msdn.microsoft.com/en-us/library/bb787947(v=vs.85).aspx for Windows 7
            _alwaysOnTop = (state & Interop.ABS_ALWAYSONTOP) == Interop.ABS_ALWAYSONTOP || Windows.IsWindows7;
            _autoHide = (state & Interop.ABS_AUTOHIDE) == Interop.ABS_AUTOHIDE;

            _isCacheValid = true;
        }

        [PublicAPI]
        public static IntPtr Handle
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
        public static TaskbarPosition Position
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
        public static int Left
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
        public static int Top
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
        public static int Right
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
        public static int Bottom
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
        public static int Width
        {
            [SecuritySafeCritical]
            get { return Right - Left; }
        }

        [PublicAPI]
        public static int Height
        {
            [SecuritySafeCritical]
            get { return Bottom - Top; }
        }

        [PublicAPI]
        public static bool AlwaysOnTop
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
        public static bool AutoHide
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
} ;