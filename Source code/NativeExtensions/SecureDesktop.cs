using System;
using System.ComponentModel;
using System.Security;
using System.Threading;

using JetBrains.Annotations;

namespace Elysium.NativeExtensions
{
    [PublicAPI]
    public static class SecureDesktop
    {
        [PublicAPI]
        [SecurityCritical]
        public static void Show(System.Windows.Window dialog)
        {
            var currentThreadID = Interop.GetCurrentThreadId();
            var currentDesktop = Interop.GetThreadDesktop(currentThreadID);
            if (currentDesktop == IntPtr.Zero)
            {
                throw new Win32Exception();
            }
            try
            {
                var secureDesktopName = Guid.NewGuid().ToString();
                var secureDesktop = Interop.CreateDesktop(secureDesktopName, null, IntPtr.Zero, 0,
                                                          Interop.DesktopFlags.DESKTOP_CREATEWINDOW | Interop.DesktopFlags.DESKTOP_READOBJECTS |
                                                          Interop.DesktopFlags.DESKTOP_WRITEOBJECTS | Interop.DesktopFlags.DESKTOP_SWITCHDESKTOP, IntPtr.Zero);
                if (secureDesktop == IntPtr.Zero)
                {
                    throw new Win32Exception();
                }
                try
                {

                    try
                    {
                        var secureThread = new Thread(Show)
                                               {
                                                   CurrentCulture = Thread.CurrentThread.CurrentCulture,
                                                   CurrentUICulture = Thread.CurrentThread.CurrentUICulture,
                                                   Priority = ThreadPriority.Highest
                                               };
                        secureThread.SetApartmentState(ApartmentState.STA);
                        secureThread.Start(new object[] { secureDesktop, dialog });
                        secureThread.Join();
                    }
                    finally
                    {
                        if (!Interop.SwitchDesktop(currentDesktop) ||
                            !Interop.SetThreadDesktop(currentDesktop))
                        {
                            throw new Win32Exception();
                        }
                    }
                }
                finally
                {
                    if (!Interop.CloseDesktop(secureDesktop))
                    {
                        throw new Win32Exception();
                    }
                }
            }
            finally
            {
                if (!Interop.CloseDesktop(currentDesktop))
                {
                    throw new Win32Exception();
                }
            }
        }

        [SecurityCritical]
        private static void Show(object parameter)
        {
            var parameters = parameter as object[];
            if (!(parameters != null && parameters[0] is IntPtr && parameters[1] != null && parameters[1] is System.Windows.Window))
            {
                throw new ArgumentException("Invalid parameter.", "parameter");
            }

            var secureDesktop = (IntPtr)parameters[0];
            if (!Interop.SetThreadDesktop(secureDesktop))
            {
                throw new Win32Exception();
            }

            if (Interop.GetThreadDesktop(Interop.GetCurrentThreadId()) != secureDesktop)
            {
                throw new Win32Exception();
            }

            if ((Windows.IsWindowsVista || Windows.IsWindows7) && !Interop.ImmDisableIME(0))
            {
                throw new InvalidOperationException();
            }

            if (!Interop.SwitchDesktop(secureDesktop))
            {
                throw new Win32Exception();
            }

            var dialog = (System.Windows.Window)parameters[1];
            dialog.Dispatcher.Invoke((Action)(() => dialog.ShowDialog()));
        }
    }
} ;