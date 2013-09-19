using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Elysium.Native
{
    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    internal static class SafeNativeMethods
    {
        #region Desktop and Monitors

        /// <summary>
        /// Retrieves a handle to the display monitor that has the largest area of intersection with the bounding rectangle of a specified window.
        /// </summary>
        /// <param name="hwnd">A handle to the window of interest.</param>
        /// <param name="dwFlags">Determines the function's return value if the window does not intersect any display monitor.</param>
        /// <returns>
        /// If the window intersects one or more display monitor rectangles, the return value is an HMONITOR handle to the display monitor that has the largest area of intersection with the window.
        /// If the window does not intersect a display monitor, the return value depends on the value of dwFlags.
        /// </returns>
        [DllImport(NativeMethods.User32, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.U4)] NativeMethods.MonitorDefaults dwFlags);

        /// <summary>
        /// Retrieves information about a display monitor.
        /// </summary>
        /// <param name="hMonitor">A handle to the display monitor of interest.</param>
        /// <param name="lpmi">
        /// A pointer to a <see cref="T:Elysium.Native.NativeMethods.MONITORINFO"/> structure that receives information about the specified display monitor.
        /// </param>
        /// <returns>TRUE if success; otherwise, FALSE.</returns>
        [DllImport(NativeMethods.User32, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, [In, Out] ref NativeMethods.MONITORINFO lpmi);

        /// <summary>
        /// The GetDeviceCaps function retrieves device-specific information for the specified device.
        /// </summary>
        /// <param name="hdc">A handle to the DC.</param>
        /// <param name="nIndex">The item to be returned.</param>
        /// <returns>The return value specifies the value of the desired item.</returns>
        [DllImport(NativeMethods.GDI32, ExactSpelling = true)]
        internal static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        #endregion

        #region Desktop Window Manager

        /// <summary>
        /// Obtains a value that indicates whether Desktop Window Manager (DWM) composition is enabled.
        /// Applications can listen for composition state changes by handling the <see cref="F:Elysium.Native.NativeMethods.WM_DWMCOMPOSITIONCHANGED"/> notification.
        /// </summary>
        /// <returns>TRUE if DWM composition is enabled; otherwise, FALSE.</returns>
        [DllImport(NativeMethods.DWMAPI, ExactSpelling = true, BestFitMapping = false, PreserveSig = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DwmIsCompositionEnabled();

        /// <summary>
        /// Retrieves the current composition timing information for a specified window.
        /// </summary>
        /// <param name="hwnd">The handle to the window for which the composition timing information should be retrieved.</param>
        /// <param name="pTimingInfo">A pointer to a DWM_TIMING_INFO structure that, when this function returns successfully, receives the current composition timing information for the window.</param>
        [DllImport(NativeMethods.DWMAPI, ExactSpelling = true, BestFitMapping = false, PreserveSig = false)]
        public static extern void DwmGetCompositionTimingInfo(IntPtr hwnd, [In, Out] ref NativeMethods.DWM_TIMING_INFO pTimingInfo);

        #endregion

        #region Window

        /// <summary>
        /// Retrieves the dimensions of the bounding rectangle of the specified window. The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="lpRect">A pointer to a <see cref="T:Elysium.Native.NativeMethods.RECT"/> structure that receives the screen coordinates of the upper-left and lower-right corners of the window.</param>
        /// <returns>TRUE if success; otherwise, FALSE.</returns>
        [DllImport(NativeMethods.User32, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, [In, Out] ref NativeMethods.RECT lpRect);

        /// <summary>
        /// Retrieves the show state and the restored, minimized, and maximized positions of the specified window.
        /// </summary>
        /// <param name="hwnd">A handle to the window.</param>
        /// <param name="lpwndpl">A pointer to the <see cref="T:Elysium.Native.NativeMethods.WINDOWPLACEMENT"/> structure that receives the show state and position information.</param>
        /// <returns>TRUE if success; otherwise, FALSE.</returns>
        [DllImport(NativeMethods.User32, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hwnd, ref NativeMethods.WINDOWPLACEMENT lpwndpl);

        /// <summary>
        /// Determines the visibility state of the specified window.
        /// </summary>
        /// <param name="hwnd">A handle to the window to be tested.</param>
        /// <returns>
        /// If the specified window, its parent window, its parent's parent window, and so forth, have the <see cref="F:Elysium.Native.NativeMethods.WindowStyles.WS_VISIBLE"/> style, the return value is TRUE;
        /// otherwise, the return value is FALSE.
        /// Because the return value specifies whether the window has the <see cref="F:Elysium.Native.NativeMethods.WindowStyles.WS_VISIBLE"/> style, it may be TRUE even if the window is totally obscured by other windows.
        /// </returns>
        [DllImport(NativeMethods.User32, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hwnd);

        #endregion

        #region Menu

        /// <summary>
        /// Displays a shortcut menu at the specified location and tracks the selection of items on the shortcut menu. The shortcut menu can appear anywhere on the screen.
        /// </summary>
        /// <param name="hmenu">
        /// A handle to the shortcut menu to be displayed.
        /// This handle can be obtained by calling the CreatePopupMenu function to create a new shortcut menu or by calling the GetSubMenu function to retrieve a handle to a submenu associated with an existing menu item.
        /// </param>
        /// <param name="fuFlags">Specifies function options.</param>
        /// <param name="x">The horizontal location of the shortcut menu, in screen coordinates.</param>
        /// <param name="y">The vertical location of the shortcut menu, in screen coordinates.</param>
        /// <param name="hwnd">
        /// A handle to the window that owns the shortcut menu. This window receives all messages from the menu.
        /// The window does not receive a WM_COMMAND message from the menu until the function returns. If you specify <see cref="F:Elysium.Native.NativeMethods.PopupMenuTracks.TPM_NONOTIFY"/> in the <paramref name="fuFlags"/> parameter,
        /// the function does not send messages to the window identified by hwnd. However, you must still pass a window handle in hwnd. It can be any window handle from your application.
        /// </param>
        /// <param name="lptpm">A pointer to a TPMPARAMS structure that specifies an area of the screen the menu should not overlap. This parameter can be IntPtr.Zero.</param>
        /// <returns>
        /// If you specify <see cref="F:Elysium.Native.NativeMethods.PopupMenuTracks.TPM_RETURNCMD"/> in the <paramref name="fuFlags"/> parameter, the return value is the menu-item identifier of the item that the user selected.
        /// If the user cancels the menu without making a selection, or if an error occurs, the return value is zero.
        /// If you do not specify <see cref="F:Elysium.Native.NativeMethods.PopupMenuTracks.TPM_RETURNCMD"/> in the <paramref name="fuFlags"/> parameter, the return value is nonzero if the function succeeds and zero if it fails.
        /// </returns>
        [DllImport(NativeMethods.User32, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern int TrackPopupMenuEx(IntPtr hmenu, [MarshalAs(UnmanagedType.U4)] NativeMethods.PopupMenuTracks fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);

        #endregion
    }
}