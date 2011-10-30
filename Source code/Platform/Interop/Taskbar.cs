using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Elysium.Platform.Interop
{
    internal enum TaskbarPosition
    {
        Left,
        Top,
        Right,
        Bottom,
    }


    internal sealed class Taskbar
    {
        internal static readonly Taskbar Instance = new Taskbar();
        
        [SecurityCritical]
        private Taskbar()
        {
            Handle = WinAPI.FindWindow("Shell_TrayWnd", null);

            var data = new WinAPI.APPBARDATA { cbSize = (uint)Marshal.SizeOf(typeof(WinAPI.APPBARDATA)), hWnd = Handle };
            var result = WinAPI.SHAppBarMessage(WinAPI.ABM_GETTASKBARPOS, ref data);
            if (result == UIntPtr.Zero)
                throw new InvalidOperationException();

            Position = (TaskbarPosition)data.uEdge;
            Left = data.rc.left;
            Top = data.rc.top;
            Right = data.rc.right;
            Bottom = data.rc.bottom;

            data = new WinAPI.APPBARDATA { cbSize = (uint)Marshal.SizeOf(typeof(WinAPI.APPBARDATA)), hWnd = Handle };
            result = WinAPI.SHAppBarMessage(WinAPI.ABM_GETSTATE, ref data);

            var state = result.ToUInt32();
            // see http://msdn.microsoft.com/en-us/library/bb787947(v=vs.85).aspx for Windows 7
            AlwaysOnTop = (state & WinAPI.ABS_ALWAYSONTOP) == WinAPI.ABS_ALWAYSONTOP || Windows.IsWindows7;
            AutoHide = (state & WinAPI.ABS_AUTOHIDE) == WinAPI.ABS_AUTOHIDE;
        }

        internal IntPtr Handle { get; private set; }

        internal TaskbarPosition Position { get; private set; }

        internal int Left { get; private set; }

        internal int Top { get; private set; }

        internal int Right { get; private set; }

        internal int Bottom { get; private set; }

        internal int Width
        {
            get { return Right - Left; }
        }

        internal int Height
        {
            get { return Bottom - Top; }
        }

        internal bool AlwaysOnTop { get; private set; }

        internal bool AutoHide { get; private set; }
    }
} ;