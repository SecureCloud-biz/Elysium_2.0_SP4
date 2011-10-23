using System;
using System.Runtime.InteropServices;

namespace Elysium.Platform.Interop
{
    public enum TaskbarPosition
    {
        Left,
        Top,
        Right,
        Bottom,
    }

    public sealed class Taskbar
    {
        public static readonly Taskbar Instance = new Taskbar();

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

        public IntPtr Handle { get; private set; }

        public TaskbarPosition Position { get; private set; }

        public int Left { get; private set; }

        public int Top { get; private set; }

        public int Right { get; private set; }

        public int Bottom { get; private set; }

        public int Width
        {
            get { return Right - Left; }
        }

        public int Height
        {
            get { return Bottom - Top; }
        }

        public bool AlwaysOnTop { get; private set; }

        public bool AutoHide { get; private set; }
    }
} ;