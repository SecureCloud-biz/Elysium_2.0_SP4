using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium.Native
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore", Justification = "We must use original style.")]
    internal static class Interop
    {
// ReSharper disable InconsistentNaming

        //#region Constants

        //#region Messages

        //internal const int WM_GETMINMAXINFO = 0x0024;

        //#endregion

        //#endregion

        //#region Structures

        //[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        //[StructLayout(LayoutKind.Sequential)]
        //internal struct WINDOWINFO
        //{
        //    [MarshalAs(UnmanagedType.U4)]
        //    public int cbSize;

        //    public RECT rcWindow;

        //    public RECT rcClient;

        //    [MarshalAs(UnmanagedType.U4)]
        //    public int dwStyle;

        //    [MarshalAs(UnmanagedType.U4)]
        //    public int dwExStyle;

        //    [MarshalAs(UnmanagedType.U4)]
        //    public int dwWindowStatus;

        //    [MarshalAs(UnmanagedType.U4)]
        //    public int cxWindowBorders;

        //    [MarshalAs(UnmanagedType.U4)]
        //    public int cyWindowBorders;

        //    [MarshalAs(UnmanagedType.U2)]
        //    public short atomWindowType;

        //    [MarshalAs(UnmanagedType.U2)]
        //    public short wCreatorVersion;
        //}

        //[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        //[StructLayout(LayoutKind.Sequential)]
        //internal struct MINMAXINFO
        //{
        //    public POINT ptReserved;
        //    public POINT ptMaxSize;
        //    public POINT ptMaxPosition;
        //    public POINT ptMinTrackSize;
        //    public POINT ptMaxTrackSize;
        //}

        //#endregion

        //#region Functions

        //[SecurityCritical]
        //[DllImport("kernel32.dll", EntryPoint = "CloseHandle", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass", Justification = "This native method is a part of interop wrapper")]
        //private static extern bool _CloseHandle(IntPtr hObject);

        //[SecurityCritical]
        //internal static void CloseHandle(IntPtr hObject)
        //{
        //    var result = _CloseHandle(hObject);
        //    if (!result)
        //    {
        //        throw new Win32Exception(Marshal.GetLastWin32Error());
        //    }
        //}

        //[SecurityCritical]
        //[DllImport("user32.dll", EntryPoint = "GetWindowInfo", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass", Justification = "This native method is a part of interop wrapper")]
        //private static extern bool _GetWindowInfo(IntPtr hwnd, [In] [Out] ref WINDOWINFO pwi);

        //[SecurityCritical]
        //internal static void GetWindowInfo(IntPtr hwnd, out WINDOWINFO pwi)
        //{
        //    pwi = new WINDOWINFO { cbSize = Marshal.SizeOf(typeof(WINDOWINFO)) };
        //    var result = _GetWindowInfo(hwnd, ref pwi);
        //    if (!result)
        //    {
        //        throw new Win32Exception(Marshal.GetLastWin32Error());
        //    }
        //}

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
        [SecuritySafeCritical]
        public static IntPtr MonitorFromWindow(IntPtr hwnd, NativeMethods.MonitorDefaults dwFlags)
        {
            Contract.Ensures(Contract.Result<IntPtr>() != IntPtr.Zero);

            var handle = SafeNativeMethods.MonitorFromWindow(hwnd, dwFlags);
            ThrowLastError(handle == IntPtr.Zero);
            return handle;
        }

        /// <summary>
        /// Retrieves information about a display monitor.
        /// </summary>
        /// <param name="hMonitor">A handle to the display monitor of interest.</param>
        /// <param name="mi">
        /// A  <see cref="T:Elysium.Native.NativeMethods.MONITORINFO"/>  structure that receives information about the specified display monitor.
        /// </param>
        [SecuritySafeCritical]
        public static void GetMonitorInfo(IntPtr hMonitor, out NativeMethods.MONITORINFO mi)
        {
            mi = new NativeMethods.MONITORINFO();
            ThrowLastError(SafeNativeMethods.GetMonitorInfo(hMonitor, ref mi));
        }
        
        /// <summary>
        /// The GetDeviceCaps function retrieves device-specific information for the specified device.
        /// </summary>
        /// <param name="hdc">A handle to the DC.</param>
        /// <param name="nIndex">The item to be returned.</param>
        /// <returns>The return value specifies the value of the desired item.</returns>
        [SecuritySafeCritical]
        public static int GetDeviceCaps(IntPtr hdc, int nIndex)
        {
            return SafeNativeMethods.GetDeviceCaps(hdc, nIndex);
        }

        [SecurityCritical]
        internal static IntPtr SHAppBarMessage(NativeMethods.ShellMessages dwMessage, ref NativeMethods.APPBARDATA pData)
        {
            Contract.Ensures(dwMessage != NativeMethods.ShellMessages.ABM_GETTASKBARPOS || Contract.Result<IntPtr>() != IntPtr.Zero);

            var result = UnsafeNativeMethods.SHAppBarMessage(dwMessage, ref pData);
            ThrowInvalidOperationException(dwMessage == NativeMethods.ShellMessages.ABM_GETTASKBARPOS && result == IntPtr.Zero);
            return result;
        }

        #endregion

        #region Desktop Window Manager

        /// <summary>
        /// Obtains a value that indicates whether Desktop Window Manager (DWM) composition is enabled.
        /// Applications can listen for composition state changes by handling the <see cref="F:Elysium.Native.NativeMethods.WM_DWMCOMPOSITIONCHANGED"/> notification.
        /// </summary>
        /// <returns>TRUE if DWM composition is enabled; otherwise, FALSE.</returns>
        [SecuritySafeCritical]
        public static bool IsDesktopCompositionEnabled()
        {
            if (_isDWMAPINotFound)
            {
                return false;
            }
            if (!Windows.IsWindowsVistaOrHigher)
            {
                _isDWMAPINotFound = true;
                return false;
            }
            try
            {
                return SafeNativeMethods.DwmIsCompositionEnabled();
            }
            catch (DllNotFoundException)
            {
                _isDWMAPINotFound = true;
                return false;
            }
        }

        private static bool _isDWMAPINotFound;

        /// <summary>
        /// Retrieves the current composition timing information.
        /// </summary>
        /// <param name="hwnd">The handle to the window for which the composition timing information should be retrieved.</param>
        /// <returns>A pointer to a DWM_TIMING_INFO structure that, when this function returns successfully, receives the current composition timing information for the window. </returns>
        [SecuritySafeCritical]
        public static NativeMethods.DWM_TIMING_INFO DwmGetCompositionTimingInfo(IntPtr hwnd)
        {
            if (!Windows.IsWindowsVistaOrHigher)
            {
                return null;
            }

            var dti = new NativeMethods.DWM_TIMING_INFO();
            SafeNativeMethods.DwmGetCompositionTimingInfo(hwnd, ref dti);

            return dti;
        }

        #endregion

        #region Graphics
        
        /// <summary>
        /// The GetDC function retrieves a handle to a device context (DC) for the client area of a specified window or for the entire screen.
        /// You can use the returned handle in subsequent GDI functions to draw in the DC. The device context is an opaque data structure, whose values are used internally by GDI.
        /// </summary>
        /// <param name="hwnd">A handle to the window whose DC is to be retrieved. If this value is IntPtr.Zero, GetDC retrieves the DC for the entire screen.</param>
        /// <returns>The return value is a handle to the DC for the specified window's client area.</returns>
        [SecurityCritical]
        public static IntPtr GetDC(IntPtr hwnd)
        {
            Contract.Ensures(Contract.Result<IntPtr>() != IntPtr.Zero);

            var handle = UnsafeNativeMethods.GetDC(hwnd);
            ThrowInvalidOperationException(handle == IntPtr.Zero);
            return handle;
        }

        [SecurityCritical]
        public static void ReleaseDC(IntPtr hWnd, IntPtr hDC)
        {
            ThrowInvalidOperationException(UnsafeNativeMethods.ReleaseDC(hWnd, hDC));
        }

        /// <summary>
        /// Creates a rectangular region.
        /// </summary>
        /// <param name="nLeftRect">Specifies the x-coordinate of the upper-left corner of the region in logical units.</param>
        /// <param name="nTopRect">Specifies the y-coordinate of the upper-left corner of the region in logical units.</param>
        /// <param name="nRightRect">Specifies the x-coordinate of the lower-right corner of the region in logical units.</param>
        /// <param name="nBottomRect">Specifies the y-coordinate of the lower-right corner of the region in logical units.</param>
        /// <returns>
        /// The handle to the region.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.Win32Exception">When Win32 error occurs.</exception>
        [SecurityCritical]
        public static IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect)
        {
            var handle = UnsafeNativeMethods.CreateRectRgn(nLeftRect, nTopRect, nRightRect, nBottomRect);
            ThrowLastError(handle == IntPtr.Zero);
            return handle;
        }

        /// <summary>
        /// Creates a rectangular region with rounded corners.
        /// </summary>
        /// <param name="nLeftRect">Specifies the x-coordinate of the upper-left corner of the region in device units.</param>
        /// <param name="nTopRect">Specifies the y-coordinate of the upper-left corner of the region in device units.</param>
        /// <param name="nRightRect">Specifies the x-coordinate of the lower-right corner of the region in device units.</param>
        /// <param name="nBottomRect">Specifies the y-coordinate of the lower-right corner of the region in device units.</param>
        /// <param name="nWidthEllipse">Specifies the width of the ellipse used to create the rounded corners in device units.</param>
        /// <param name="nHeightEllipse">Specifies the height of the ellipse used to create the rounded corners in device units.</param>
        /// <returns>
        /// The handle to the region.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.Win32Exception">When Win32 error occurs.</exception>
        [SecurityCritical]
        public static IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse)
        {
            var handle = UnsafeNativeMethods.CreateRoundRectRgn(nLeftRect, nTopRect, nRightRect, nBottomRect, nWidthEllipse, nHeightEllipse);
            ThrowLastError(handle == IntPtr.Zero);
            return handle;
        }

        /// <summary>
        /// Creates a rectangular region with or without rounded corners.
        /// </summary>
        /// <param name="region">Specifies the coordinates of the region in device units.</param>
        /// <param name="radius">Specifies the radius of the circle used to create the rounded corners in device units. Can be zero.</param>
        /// <returns>
        /// The handle to the region.
        /// </returns>
        [SecurityCritical]
        public static IntPtr CreateRoundRectRgn(Rect region, double radius)
        {
            SecurityHelper.DemandUnmanagedCode();
            
            // Round outwards.
            if (DoubleUtil.AreClose(0, radius))
            {
                return CreateRectRgn((int)Math.Floor(region.Left), (int)Math.Floor(region.Top), (int)Math.Ceiling(region.Right), (int)Math.Ceiling(region.Bottom));
            }

            var roundedRadius = (int)Math.Ceiling(radius);

            // RoundedRect HRGNs require an additional pixel of padding on the bottom right to look correct.
            return CreateRoundRectRgn((int)Math.Floor(region.Left), (int)Math.Floor(region.Top), (int)Math.Ceiling(region.Right) + 1, (int)Math.Ceiling(region.Bottom) + 1,
                roundedRadius, roundedRadius);
        }

        /// <summary>
        /// Creates a rectangular region. 
        /// </summary>
        /// <param name="lprc">Pointer to a RECT structure that contains the coordinates of the upper-left and lower-right corners of the rectangle that defines the region in logical units. </param>
        /// <returns>
        /// The handle to the region.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.Win32Exception">When Win32 error occurs.</exception>
        [SecurityCritical]
        public static IntPtr CreateRectRgnIndirect([In] ref NativeMethods.RECT lprc)
        {
            var handle = UnsafeNativeMethods.CreateRectRgnIndirect(ref lprc);
            ThrowLastError(handle == IntPtr.Zero);
            return handle;
        }

        /// <summary>
        /// Combines two regions and stores the result in a third region. The two regions are combined according to the specified mode. 
        /// </summary>
        /// <param name="hrgnDest">A handle to a new region with dimensions defined by combining two other regions. (This region must exist before CombineRgn is called.) </param>
        /// <param name="hrgnSrc1">A handle to the first of two regions to be combined.</param>
        /// <param name="hrgnSrc2">A handle to the second of two regions to be combined.</param>
        /// <param name="fnCombineMode">A mode indicating how the two regions will be combined. </param>
        /// <returns>The return value specifies the type of the resulting region. </returns>
        /// <exception cref="T:System.ComponentModel.Win32Exception">When Win32 error occurs.</exception>
        [SecurityCritical]
        public static NativeMethods.RegionCombinationResult CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1, IntPtr hrgnSrc2, NativeMethods.RegionCombinations fnCombineMode)
        {
            var result = UnsafeNativeMethods.CombineRgn(hrgnDest, hrgnSrc1, hrgnSrc2, fnCombineMode);
            ThrowLastError(result == NativeMethods.RegionCombinationResult.ERROR);
            return result;
        }

        /// <summary>
        /// Creates region and combines it with first region.
        /// </summary>
        /// <param name="hrgnSource">A handle to the first of two regions to be combined.</param>
        /// <param name="region">Specifies the coordinates of the second of two regions to be combined in device units.</param>
        /// <param name="radius">Specifies the radius of the circle used to create the rounded corners of the second of two regions to be combined in device units. Can be zero.</param>
        [SecurityCritical]
        public static void CreateAndCombineRoundRectRgn(IntPtr hrgnSource, Rect region, double radius)
        {
            SecurityHelper.DemandUnmanagedCode();

            var hrgn = IntPtr.Zero;
            try
            {
                hrgn = CreateRoundRectRgn(region, radius);
                CombineRgn(hrgnSource, hrgnSource, hrgn, NativeMethods.RegionCombinations.RGN_OR);
            }
            catch
            {
                DeleteObject(hrgn);
                throw;
            }
        }

        /// <summary>
        /// The SetWindowRgn function sets the window region of a window. 
        /// The window region determines the area within the window where the system permits drawing. 
        /// The system does not display any portion of a window that lies outside of the window region 
        /// </summary>
        /// <param name="hWnd">A handle to the window whose window region is to be set.</param>
        /// <param name="hRgn">
        /// A handle to a region. The function sets the window region of the window to this region.
        /// If hRgn is IntPtr.Zero, the function sets the window region to NULL.
        /// </param>
        /// <param name="bRedraw">
        /// Specifies whether the system redraws the window after setting the window region. If bRedraw is TRUE, the system does so; otherwise, it does not.
        /// Typically, you set bRedraw to TRUE if the window is visible.
        /// </param>
        /// <exception cref="T:System.InvalidOperationException">When Win32 error occurs.</exception>
        [SecurityCritical]
        public static void SetWindowRgn(IntPtr hWnd, ref IntPtr hRgn, bool bRedraw)
        {
            ThrowInvalidOperationException(!UnsafeNativeMethods.SetWindowRgn(hWnd, hRgn, bRedraw));
            hRgn = IntPtr.Zero;
        }

        /// <summary>
        /// The DeleteObject function deletes a logical pen, brush, font, bitmap, region, 
        /// or palette, freeing all system resources associated with the object. After the object 
        /// is deleted, the specified handle is no longer valid. 
        /// </summary>
        /// <param name="hObject">A handle to a logical pen, brush, font, bitmap, region, or palette. </param>
        /// <exception cref="T:System.ComponentModel.Win32Exception">When Win32 error occurs.</exception>
        [SecurityCritical]
        public static void DeleteObject(IntPtr hObject)
        {
            ThrowLastError(!UnsafeNativeMethods.DeleteObject(hObject));
        }

        #endregion

        #region Window

        /// <summary>
        /// Retrieves a handle to the top-level window whose class name and window name match the specified strings.
        /// This function does not search child windows. This function does not perform a case-sensitive search.
        /// To search child windows, beginning with a specified child window, use the FindWindowEx function.
        /// </summary>
        /// <param name="lpClassName">
        /// The class name or a class atom created by a previous call to the RegisterClass or RegisterClassEx function. The atom must be in the low-order word of lpClassName; the high-order word must be zero.
        /// If lpClassName points to a string, it specifies the window class name. The class name can be any name registered with RegisterClass or RegisterClassEx, or any of the predefined control-class names.
        /// If lpClassName is NULL, it finds any window whose title matches the lpWindowName parameter.
        /// </param>
        /// <param name="lpWindowName">
        /// The window name (the window's title). If this parameter is NULL, all window names match.
        /// </param>
        /// <returns>
        /// A handle to the window that has the specified class name and window name.
        /// </returns>
        [SecurityCritical]
        internal static IntPtr FindWindow(string lpClassName, string lpWindowName)
        {
            Contract.Ensures(Contract.Result<IntPtr>() != IntPtr.Zero);

            var handle = UnsafeNativeMethods.FindWindow(lpClassName, lpWindowName);
            ThrowLastError(handle == IntPtr.Zero);
            return handle;
        }

        /// <summary>
        /// The PostMessage function places (posts) a message in the message queue 
        /// associated with the thread that created the specified window and returns 
        /// without waiting for the thread to process the message. 
        /// </summary>
        /// <param name="hWnd">Handle to the window whose window procedure is to receive the message.</param>
        /// <param name="msg">Specifies the message to be posted.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <exception cref="T:System.ComponentModel.Win32Exception">When Win32 error occurs.</exception>
        [SecurityCritical]
        public static void PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            ThrowLastError(!UnsafeNativeMethods.PostMessage(hWnd, msg, wParam, lParam));
        }

        /// <summary>
        /// Retrieves information about the specified window. The function also retrieves the value at a specified offset into the extra window memory.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">
        /// The zero-based offset to the value to be retrieved. Valid values are in the range zero through the number of bytes of extra window memory, minus four (32-bit) or eight (64-bit).
        /// To retrieve any other value, specify one of the <see cref="T:Elysium.Native.NativeMethods.WindowInfo"/> values.
        /// </param>
        /// <returns>
        /// Requested value.
        /// If <see cref="M:Elysium.Native.Interop.SetWindowLongPtr(System.IntPtr,System.Int32,System.IntPtr)"/> has not been called previously, GetWindowLongPtr throws exception for values in the extra window or class memory.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.Win32Exception">When Win32 error occurs.</exception>
        [SecurityCritical]
        public static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
        {
            Contract.Ensures(Contract.Result<IntPtr>() != IntPtr.Zero);

            var result = new IntPtr(Environment.Is64BitProcess ? UnsafeNativeMethods.GetWindowLongPtr(hWnd, nIndex) : UnsafeNativeMethods.GetWindowLong(hWnd, nIndex));
            ThrowLastError(result == IntPtr.Zero);
            return result;
        }

        /// <summary>
        /// Changes an attribute of the specified window. The function also sets a value at the specified offset in the extra window memory.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window and, indirectly, the class to which the window belongs.
        /// The SetWindowLongPtr function fails if the process that owns the window specified by the hWnd parameter is at a higher process privilege in the UIPI hierarchy than the process the calling thread resides in.
        /// Windows XP/2000:   The SetWindowLongPtr function fails if the window specified by the hWnd parameter does not belong to the same process as the calling thread.
        /// </param>
        /// <param name="nIndex">The zero-based offset to the value to be set. Valid values are in the range zero through the number of bytes of extra window memory, minus four (32-bit) or eight (64-bit).
        /// To set any other value, specify one of the <see cref="T:Elysium.Native.NativeMethods.WindowInfo"/> values.</param>
        /// <param name="dwNewLong">The replacement value.</param>
        /// <returns>
        /// Previous value of the specified offset.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.Win32Exception">When Win32 error occurs.</exception>
        [SecurityCritical]
        public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            Contract.Ensures(Contract.Result<IntPtr>() != IntPtr.Zero);

            UnsafeNativeMethods.SetLastError(0);
            var result = new IntPtr(Environment.Is64BitProcess ? UnsafeNativeMethods.SetWindowLongPtr(hWnd, nIndex, dwNewLong.ToInt64()) : UnsafeNativeMethods.SetWindowLong(hWnd, nIndex, dwNewLong.ToInt32()));
            ThrowLastError(result == IntPtr.Zero);
            return result;
        }

        /// <summary>
        /// Removes specified style and/or add new style to the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window/</param>
        /// <param name="removeStyle">Removed style.</param>
        /// <param name="addStyle">New style.</param>
        /// <returns>TRUE if success; otherwise, FALSE.</returns>
        [SecurityCritical]
        public static bool ModifyStyle(IntPtr hWnd, NativeMethods.WindowStyles? removeStyle, NativeMethods.WindowStyles? addStyle)
        {
            SecurityHelper.DemandUnmanagedCode();
            SecurityHelper.DemandUIWindowPermission();

            var dwStyle = (NativeMethods.WindowStyles)GetWindowLongPtr(hWnd, (int)NativeMethods.WindowInfo.GWL_STYLE).ToInt32();
            var dwNewStyle = (dwStyle & ~removeStyle ?? 0) | addStyle ?? 0;
            if (dwStyle == dwNewStyle)
            {
                return false;
            }
            SetWindowLongPtr(hWnd, (int)NativeMethods.WindowInfo.GWL_STYLE, new IntPtr((int)dwNewStyle));
            return true;
        }

        /// <summary>
        /// Changes the size, position, and Z order of a child, pop-up, or top-level window. 
        /// These windows are ordered according to their appearance on the screen. 
        /// The topmost window receives the highest rank and is the first window in the Z order.
        /// </summary>
        /// <param name="hWnd">A handle to the window</param>
        /// <param name="hWndInsertAfter">A handle to the window to precede the positioned window in the Z order</param>
        /// <param name="x">The new position of the left side of the window, in client coordinates</param>
        /// <param name="y">The new position of the top of the window, in client coordinates</param>
        /// <param name="cx">The new width of the window, in pixels</param>
        /// <param name="cy">The new height of the window, in pixels</param>
        /// <param name="uFlags">The window sizing and positioning flags.</param>
        /// <exception cref="T:System.ComponentModel.Win32Exception">When Win32 error occurs.</exception>
        [SecurityCritical]
        public static void SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, NativeMethods.WindowPosition uFlags)
        {
            ThrowLastError(!UnsafeNativeMethods.SetWindowPos(hWnd, hWndInsertAfter, x, y, cx, cy, uFlags));
        }

        /// <summary>
        /// Retrieves the dimensions of the bounding rectangle of the specified window. The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="rect">A <see cref="T:Elysium.Native.NativeMethods.RECT"/> structure that receives the screen coordinates of the upper-left and lower-right corners of the window.</param>
        /// <exception cref="T:System.ComponentModel.Win32Exception">When Win32 error occurs.</exception>
        [SecuritySafeCritical]
        public static void GetWindowRect(IntPtr hWnd, out NativeMethods.RECT rect)
        {
            rect = new NativeMethods.RECT();
            ThrowLastError(!SafeNativeMethods.GetWindowRect(hWnd, ref rect));
        }

        /// <summary>
        /// Retrieves window state of the specificed window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <returns>Current window state.</returns>
        /// <exception cref="T:System.ComponentModel.Win32Exception">When Win32 error occurs.</exception>
        [SecuritySafeCritical]
        public static WindowState GetHwndState(IntPtr hWnd)
        {
            SecurityHelper.DemandUnmanagedCode();
            SecurityHelper.DemandUIWindowPermission();

            var placement = new NativeMethods.WINDOWPLACEMENT();
            ThrowLastError(!SafeNativeMethods.GetWindowPlacement(hWnd, ref placement));
            switch (placement.showCmd)
            {
                case NativeMethods.WindowState.SW_SHOWMINIMIZED: return WindowState.Minimized;
                case NativeMethods.WindowState.SW_SHOWMAXIMIZED: return WindowState.Maximized;
            }
            return WindowState.Normal;
        }

        /// <summary>
        /// Determines the visibility state of the specified window.
        /// </summary>
        /// <param name="hwnd">A handle to the window to be tested.</param>
        /// <returns>
        /// If the specified window, its parent window, its parent's parent window, and so forth, have the <see cref="F:Elysium.Native.NativeMethods.WindowStyles.WS_VISIBLE"/> style, the return value is TRUE;
        /// otherwise, the return value is FALSE.
        /// Because the return value specifies whether the window has the <see cref="F:Elysium.Native.NativeMethods.WindowStyles.WS_VISIBLE"/> style, it may be TRUE even if the window is totally obscured by other windows.
        /// </returns>
        [SecuritySafeCritical]
        public static bool IsWindowVisible(IntPtr hwnd)
        {
            return SafeNativeMethods.IsWindowVisible(hwnd);
        }

        #endregion

        #region Hooks

        /// <summary>
        /// Installs an application-defined hook procedure into a hook chain. You would install a hook procedure to monitor the system for certain types of events.
        /// These events are associated either with a specific thread or with all threads in the same desktop as the calling thread.
        /// </summary>
        /// <param name="hookType">The type of hook procedure to be installed.</param>
        /// <param name="lpfn">
        /// A pointer to the hook procedure. If the dwThreadId parameter is zero or specifies the identifier of a thread created by a different process, the lpfn parameter must point to a hook procedure in a DLL.
        /// Otherwise, lpfn can point to a hook procedure in the code associated with the current process.
        /// </param>
        /// <param name="hMod">
        /// A handle to the DLL containing the hook procedure pointed to by the lpfn parameter.
        /// The hMod parameter must be set to NULL if the dwThreadId parameter specifies a thread created by the current process and if the hook procedure is within the code associated with the current process.
        /// </param>
        /// <param name="dwThreadId">
        /// The identifier of the thread with which the hook procedure is to be associated.
        /// If this parameter is zero, the hook procedure is associated with all existing threads running in the same desktop as the calling thread.
        /// </param>
        /// <returns>
        /// The handle to the hook procedure.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.Win32Exception">When Win32 error occurs.</exception>
        [SecurityCritical]
        public static IntPtr SetWindowsHookEx(NativeMethods.HookType hookType, NativeMethods.HookProc lpfn, IntPtr hMod, int dwThreadId)
        {
            Contract.Ensures(Contract.Result<IntPtr>() != IntPtr.Zero);

            var handle = UnsafeNativeMethods.SetWindowsHookEx(hookType, lpfn, hMod, dwThreadId);
            ThrowLastError(handle == IntPtr.Zero);
            return handle;
        }

        /// <summary>
        /// Removes a hook procedure installed in a hook chain
        /// by the <see cref="M:Elysium.Native.UnsafeNativeMethods.SetWindowsHookEx(Elysium.Native.NativeMethods.HookType,Elysium.Native.NativeMethods.HookProc,System.IntPtr,System.Int32)"/> function.
        /// </summary>
        /// <param name="hhk">
        /// A handle to the hook to be removed. This parameter is a hook handle obtained
        /// by a previous call to <see cref="M:Elysium.Native.UnsafeNativeMethods.SetWindowsHookEx(Elysium.Native.NativeMethods.HookType,Elysium.Native.NativeMethods.HookProc,System.IntPtr,System.Int32)"/>.
        /// </param>
        /// <exception cref="T:System.ComponentModel.Win32Exception">When Win32 error occurs.</exception>
        [SecurityCritical]
        public static void UnhookWindowsHookEx(IntPtr hhk)
        {
            ThrowLastError(!UnsafeNativeMethods.UnhookWindowsHookEx(hhk));
        }

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
        /// If the user cancels the menu without making a selection the return value is zero.
        /// In other cases, the return value must be ignored.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.Win32Exception">When Win32 error occurs.</exception>
        [SecuritySafeCritical]
        public static int TrackPopupMenuEx(IntPtr hmenu, NativeMethods.PopupMenuTracks fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm)
        {
            Contract.Ensures(Contract.Result<int>() != 0);

            var result = SafeNativeMethods.TrackPopupMenuEx(hmenu, fuFlags, x, y, hwnd, lptpm);
            ThrowLastError(result == 0, true);
            return result;
        }

        /// <summary>
        /// Enables, disables, or grays the specified menu item.
        /// </summary>
        /// <param name="hMenu">A handle to the menu.</param>
        /// <param name="uIDEnableItem">The menu item to be enabled, disabled, or grayed, as determined by the uEnable parameter. This parameter specifies an item in a menu bar, menu, or submenu.</param>
        /// <param name="uEnable">Controls the interpretation of the uIDEnableItem parameter and indicate whether the menu item is enabled, disabled, or grayed.</param>
        /// <returns> The return value specifies the previous state of the menu item (it is either MF_DISABLED, MF_ENABLED, or MF_GRAYED).</returns>
        /// <exception cref="T:System.InvalidOperationException">When Win32 error occurs.</exception>
        [SecurityCritical]
        public static int EnableMenuItem(IntPtr hMenu, int uIDEnableItem, NativeMethods.MenuItemState uEnable)
        {
            Contract.Ensures(Contract.Result<int>() != -1);

            var result = UnsafeNativeMethods.EnableMenuItem(hMenu, uIDEnableItem, uEnable);
            ThrowInvalidOperationException(result == -1);
            return result;
        }

        #endregion

        #region Helpers

        [SecuritySafeCritical]
        private static void ThrowInvalidOperationException(bool @throw)
        {
            if (@throw)
            {
                throw new InvalidOperationException();
            }
        }

        [SecuritySafeCritical]
        private static void ThrowLastError(bool @throw, bool checkWin32Error = false)
        {
            if (@throw)
            {
                if (checkWin32Error)
                {
                    var lastWin32Error = Marshal.GetLastWin32Error();
                    if (lastWin32Error != 0)
                    {
                        throw new Win32Exception(lastWin32Error);
                    }
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        #endregion

        // ReSharper restore InconsistentNaming
    }
}