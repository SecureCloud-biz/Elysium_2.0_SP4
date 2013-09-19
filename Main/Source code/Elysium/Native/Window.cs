using System;
using System.Diagnostics.CodeAnalysis;
using System.Security;

using JetBrains.Annotations;

namespace Elysium.Native
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Window
    {
        [SecurityCritical]
        private static bool _isCacheValid;

        [SecuritySafeCritical]
        public Window(IntPtr hwnd)
        {
            _handle = hwnd;
        }

        [SecuritySafeCritical]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This method must be called only after class initialization.")]
        public void Invalidate()
        {
            _isCacheValid = false;
        }

        [SecurityCritical]
        private void InvalidateInternal()
        {
            Interop.WINDOWINFO windowInfo;
            Interop.GetWindowInfo(_handle, out windowInfo);

            _bounds = windowInfo.rcWindow;
            _clientArea = windowInfo.rcClient;
            _nonClientBorderWidth = windowInfo.cxWindowBorders;
            _nonClientBorderHeight = windowInfo.cyWindowBorders;
            _windowStyle = windowInfo.dwStyle;
            _windowExStyle = windowInfo.dwExStyle;

            _isCacheValid = true;
        }

        public IntPtr Handle
        {
            [SecurityCritical]
            get { return _handle; }
        }

        private readonly IntPtr _handle;

        public int WindowStyle
        {
            [SecuritySafeCritical]
            get
            {
                if (!_isCacheValid)
                {
                    InvalidateInternal();
                }
                return _windowStyle;
            }
        }

        private int _windowStyle;

        public int WindowExStyle
        {
            [SecuritySafeCritical]
            get
            {
                if (!_isCacheValid)
                {
                    InvalidateInternal();
                }
                return _windowExStyle;
            }
        }

        private int _windowExStyle;

        public Interop.RECT Bounds
        {
            [SecuritySafeCritical]
            get
            {
                if (!_isCacheValid)
                {
                    InvalidateInternal();
                }
                return _bounds;
            }
        }

        private Interop.RECT _bounds;

        public Interop.RECT ClientArea
        {
            [SecuritySafeCritical]
            get
            {
                if (!_isCacheValid)
                {
                    InvalidateInternal();
                }
                return _clientArea;
            }
        }

        private Interop.RECT _clientArea;

        public int NonClientBorderWidth
        {
            [SecuritySafeCritical]
            get
            {
                if (!_isCacheValid)
                {
                    InvalidateInternal();
                }
                return _nonClientBorderWidth;
            }
        }

        private int _nonClientBorderWidth;

        public int NonClientBorderHeight
        {
            [SecuritySafeCritical]
            get
            {
                if (!_isCacheValid)
                {
                    InvalidateInternal();
                }
                return _nonClientBorderHeight;
            }
        }

        private int _nonClientBorderHeight;
    }
}