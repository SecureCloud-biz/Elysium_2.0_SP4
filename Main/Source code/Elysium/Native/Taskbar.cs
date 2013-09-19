using System;
using System.Runtime.InteropServices;
using System.Security;

using JetBrains.Annotations;

namespace Elysium.Native
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public static class Taskbar
    {
        [SecurityCritical]
        private static bool _isCacheValid;

        [SecuritySafeCritical]
        public static void Invalidate()
        {
            _isCacheValid = false;
        }

        // TODO: WM_SETTINGCHANGE
        [SecurityCritical]
        private static void InvalidateInternal()
        {
            _handle = Interop.FindWindow("Shell_TrayWnd", null);

            var data = new NativeMethods.APPBARDATA(_handle);
            Interop.SHAppBarMessage(NativeMethods.ShellMessages.ABM_GETTASKBARPOS, ref data);

            _position = (TaskbarPosition)data.uEdge;
            _left = data.rc.left;
            _top = data.rc.top;
            _right = data.rc.right;
            _bottom = data.rc.bottom;

            data = new NativeMethods.APPBARDATA(_handle);

            var state = (NativeMethods.TaskbarState)Interop.SHAppBarMessage(NativeMethods.ShellMessages.ABM_GETSTATE, ref data).ToInt32();

            _alwaysOnTop = state == NativeMethods.TaskbarState.ABS_ALWAYSONTOP || Windows.IsWindows7OrHigher;
            _autoHide = state == NativeMethods.TaskbarState.ABS_AUTOHIDE;

            _isCacheValid = true;
        }

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

        public static int Width
        {
            [SecuritySafeCritical]
            get
            {
                return Right - Left;
            }
        }

        public static int Height
        {
            [SecuritySafeCritical]
            get
            {
                return Bottom - Top;
            }
        }

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
}