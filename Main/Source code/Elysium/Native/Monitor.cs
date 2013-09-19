using System;
using System.Diagnostics.CodeAnalysis;
using System.Security;
using System.Security.Permissions;

using JetBrains.Annotations;

namespace Elysium.Native
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    internal sealed class Monitor
    {
        [SecurityCritical]
        private static bool _isCacheValid;

        [SecuritySafeCritical]
        public Monitor(IntPtr hwnd)
        {
            _handle = Interop.MonitorFromWindow(hwnd, NativeMethods.MonitorDefaults.MONITOR_DEFAULTTONEAREST);
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
            NativeMethods.MONITORINFO monitorInfo;
            Interop.GetMonitorInfo(_handle, out monitorInfo);

            _bounds = monitorInfo.rcMonitor;
            _workArea = monitorInfo.rcWork;

            _isCacheValid = true;
        }

        public IntPtr Handle
        {
            [SecurityCritical]
            get { return _handle; }
        }

        private readonly IntPtr _handle;

        public NativeMethods.RECT Bounds
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

        private NativeMethods.RECT _bounds;

        public NativeMethods.RECT WorkArea
        {
            [SecuritySafeCritical]
            get
            {
                if (!_isCacheValid)
                {
                    InvalidateInternal();
                }
                return _workArea;
            }
        }

        private NativeMethods.RECT _workArea;
    }
}