using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Navigation;

namespace Elysium.Native
{
    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    internal static class UnsafeNativeMethods
    {
        #region General

        /// <summary>
        /// Sets the last-error code for the calling thread.
        /// </summary>
        /// <param name="dwErrCode">The last-error code for the thread.</param>
        [DllImport(NativeMethods.Kernel, ExactSpelling = true)]
        public static extern void SetLastError([MarshalAs(UnmanagedType.U4)] int dwErrCode);

        #endregion

        #region Desktop and Monitors

        [DllImport(NativeMethods.Shell32, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.SysUInt)]
        public static extern IntPtr SHAppBarMessage([MarshalAs(UnmanagedType.U4)] NativeMethods.ShellMessages dwMessage, ref NativeMethods.APPBARDATA pData);

        #endregion

        #region Desktop Window Manager

        /// <summary>
        /// Extends the window frame into the client area.
        /// </summary>
        /// <param name="hWnd">The handle to the window in which the frame will be extended into the client area.</param>
        /// <param name="pMargins">A pointer to a MARGINS structure that describes the margins to use when extending the frame into the client area.</param>
        [DllImport(NativeMethods.DWMAPI, ExactSpelling = true, BestFitMapping = false, PreserveSig = false)]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, [In] ref NativeMethods.MARGINS pMargins);

        /// <summary>
        /// Default window procedure for Desktop Window Manager (DWM) hit testing within the non-client area.
        /// </summary>
        /// <param name="hwnd">Handle to the window procedure that received the message.</param>
        /// <param name="msg">Specifies the message.</param>
        /// <param name="wParam">Specifies additional message information. The content of this parameter depends on the value of the msg parameter. </param>
        /// <param name="lParam">Specifies additional message information. The content of this parameter depends on the value of the msg parameter. </param>
        /// <param name="plResult">Pointer to an LRESULT value that, when this method returns, receives the result of the hit test.</param>
        /// <returns>TRUE if DwmDefWindowProc handled the message; otherwise, FALSE. </returns>
        [DllImport(NativeMethods.DWMAPI, ExactSpelling = true, BestFitMapping = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DwmDefWindowProc(IntPtr hwnd, [MarshalAs(UnmanagedType.U4)] int msg, [MarshalAs(UnmanagedType.SysUInt)] IntPtr wParam, [MarshalAs(UnmanagedType.SysInt)] IntPtr lParam, [MarshalAs(UnmanagedType.SysInt)] [In, Out] ref IntPtr plResult);

        #endregion

        #region Graphics
        
        /// <summary>
        /// The GetDC function retrieves a handle to a device context (DC) for the client area of a specified window or for the entire screen.
        /// You can use the returned handle in subsequent GDI functions to draw in the DC. The device context is an opaque data structure, whose values are used internally by GDI.
        /// </summary>
        /// <param name="hwnd">A handle to the window whose DC is to be retrieved. If this value is IntPtr.Zero, GetDC retrieves the DC for the entire screen.</param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the DC for the specified window's client area.
        /// If the function fails, the return value is IntPtr.Zero.</returns>
        [DllImport(NativeMethods.User32, ExactSpelling = true)]
        internal static extern IntPtr GetDC(IntPtr hwnd);

        /// <summary>
        /// The ReleaseDC function releases a device context (DC), freeing it for use by other applications. The effect of the ReleaseDC function depends on the type of DC.
        /// It frees only common and window DCs. It has no effect on class or private DCs.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose DC is to be released.</param>
        /// <param name="hDC">A handle to the DC to be released.</param>
        /// <returns>TRUE if success; otherwise, FALSE.</returns>
        [DllImport(NativeMethods.User32, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

        /// <summary>
        /// Creates a rectangular region.
        /// </summary>
        /// <param name="nLeftRect">Specifies the x-coordinate of the upper-left corner of the region in logical units.</param>
        /// <param name="nTopRect">Specifies the y-coordinate of the upper-left corner of the region in logical units.</param>
        /// <param name="nRightRect">Specifies the x-coordinate of the lower-right corner of the region in logical units.</param>
        /// <param name="nBottomRect">Specifies the y-coordinate of the lower-right corner of the region in logical units.</param>
        /// <returns>
        /// If the function succeeds, the return value is the handle to the region.
        /// If the function fails, the return value is IntPtr.Zero.
        /// </returns>
        [DllImport(NativeMethods.GDI32, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

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
        /// If the function succeeds, the return value is the handle to the region.
        /// If the function fails, the return value is IntPtr.Zero.
        /// </returns>
        [DllImport(NativeMethods.GDI32, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        /// <summary>
        /// Creates a rectangular region. 
        /// </summary>
        /// <param name="lprc">Pointer to a RECT structure that contains the coordinates of the upper-left and lower-right corners of the rectangle that defines the region in logical units. </param>
        /// <returns>
        /// If the function succeeds, the return value is the handle to the region.
        /// If the function fails, the return value is IntPtr.Zero.
        /// </returns>
        [DllImport(NativeMethods.GDI32, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateRectRgnIndirect([In] ref NativeMethods.RECT lprc);

        /// <summary>
        /// Combines two regions and stores the result in a third region. The two regions are combined according to the specified mode. 
        /// </summary>
        /// <param name="hrgnDest">A handle to a new region with dimensions defined by combining two other regions. (This region must exist before CombineRgn is called.) </param>
        /// <param name="hrgnSrc1">A handle to the first of two regions to be combined.</param>
        /// <param name="hrgnSrc2">A handle to the second of two regions to be combined.</param>
        /// <param name="fnCombineMode">A mode indicating how the two regions will be combined. </param>
        /// <returns>The return value specifies the type of the resulting region. </returns>
        [DllImport(NativeMethods.GDI32, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern NativeMethods.RegionCombinationResult CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1, IntPtr hrgnSrc2, [MarshalAs(UnmanagedType.I4)] NativeMethods.RegionCombinations fnCombineMode);

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
        /// <returns>TRUE if success; otherwise, FALSE.</returns>
        [DllImport(NativeMethods.User32, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowRgn(IntPtr hWnd, IntPtr hRgn, [MarshalAs(UnmanagedType.Bool)] bool bRedraw);

        /// <summary>
        /// The DeleteObject function deletes a logical pen, brush, font, bitmap, region, 
        /// or palette, freeing all system resources associated with the object. After the object 
        /// is deleted, the specified handle is no longer valid. 
        /// </summary>
        /// <param name="hObject">A handle to a logical pen, brush, font, bitmap, region, or palette. </param>
        /// <returns>TRUE if succeeds; if the specified handle is not valid or is currently selected into a DC, the return value is FALSE.</returns>
        [DllImport(NativeMethods.GDI32, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject(IntPtr hObject);

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
        /// If the function succeeds, the return value is a handle to the window that has the specified class name and window name.
        /// If the function fails, the return value is IntrPtr.Zero.
        /// </returns>
        [DllImport(NativeMethods.User32, ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// The DefWindowProc function calls the default window procedure to provide default processing for any window messages that an application does not process. This function ensures that every message is processed. DefWindowProc is called with the same parameters received by the window procedure. 
        /// </summary>
        /// <param name="hWnd">Handle to the window procedure that received the message. </param>
        /// <param name="Msg">Specifies the message. </param>
        /// <param name="wParam">Specifies additional message information. The content of this parameter depends on the value of the Msg parameter. </param>
        /// <param name="lParam">Specifies additional message information. The content of this parameter depends on the value of the Msg parameter. </param>
        /// <returns>The return value is the result of the message processing and depends on the message.</returns>
        [DllImport(NativeMethods.User32, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, [MarshalAs(UnmanagedType.U4)] int Msg, [MarshalAs(UnmanagedType.SysUInt)] IntPtr wParam, [MarshalAs(UnmanagedType.SysInt)] IntPtr lParam);

        /// <summary>
        /// Sends the specified message to a window or windows. It calls the window procedure for the specified window and does not return until the window procedure has processed the message. 
        /// To send a message and return immediately, use the SendMessageCallback or SendNotifyMessage function.
        /// To post a message to a thread's message queue and return immediately, use the <see cref="M:Elysium.Native.UnsafeNativeMethods.PostMessage(System.IntPtr,System.Int32,System.IntPtr,System.IntPtr)"/> or PostThreadMessage function.
        /// </summary>
        /// <param name="hWnd">
        /// Handle to the window whose window procedure will receive the message.
        /// If this parameter is <see cref="F:Elysium.Native.NativeMethods.HWND_BROADCAST"/>, the message is sent to all top-level windows in the system, including disabled or invisible unowned windows, overlapped windows, and pop-up windows;
        /// but the message is not sent to child windows.
        /// Message sending is subject to UIPI. The thread of a process can send messages only to message queues of threads in processes of lesser or equal integrity level.
        /// </param>
        /// <param name="Msg">The message to be sent</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <returns>The return value specifies the result of the message processing; it depends on the message sent</returns>
        [DllImport(NativeMethods.User32, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        public static extern IntPtr SendMessage(IntPtr hWnd, [MarshalAs(UnmanagedType.U4)] int Msg, [MarshalAs(UnmanagedType.SysUInt)] IntPtr wParam, [MarshalAs(UnmanagedType.SysInt)] IntPtr lParam);

        /// <summary>
        /// The PostMessage function places (posts) a message in the message queue 
        /// associated with the thread that created the specified window and returns 
        /// without waiting for the thread to process the message. 
        /// </summary>
        /// <param name="hWnd">Handle to the window whose window procedure is to receive the message.</param>
        /// <param name="Msg">Specifies the message to be posted.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <returns>TRUE if success; otherwise, FALSE. GetLastError returns ERROR_NOT_ENOUGH_QUOTA when the limit is hit.</returns>
        [DllImport(NativeMethods.User32, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage(IntPtr hWnd, [MarshalAs(UnmanagedType.U4)] int Msg, [MarshalAs(UnmanagedType.SysUInt)] IntPtr wParam, [MarshalAs(UnmanagedType.SysInt)] IntPtr lParam);

        /// <summary>
        /// Retrieves information about the specified window. The function also retrieves the 32-bit (DWORD) value at the specified offset into the extra window memory.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">
        /// The zero-based offset to the value to be retrieved. Valid values are in the range zero through the number of bytes of extra window memory, minus four.
        /// To retrieve any other value, specify one of the <see cref="T:Elysium.Native.NativeMethods.WindowInfo"/> values.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the requested value.
        /// If the function fails, the return value is zero.
        /// If <see cref="M:Elysium.Native.UnsafeNativeMethods.SetWindowLong(System.IntPtr,System.Int32,System.Int32)"/> has not been called previously,
        /// GetWindowLong returns zero for values in the extra window or class memory.
        /// </returns>
        [DllImport(NativeMethods.User32, EntryPoint = "GetWindowLong", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Retrieves information about the specified window. The function also retrieves the value at a specified offset into the extra window memory.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">
        /// The zero-based offset to the value to be retrieved. Valid values are in the range zero through the number of bytes of extra window memory, minus eight.
        /// To retrieve any other value, specify one of the <see cref="T:Elysium.Native.NativeMethods.WindowInfo"/> values.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the requested value.
        /// If the function fails, the return value is zero.
        /// If <see cref="M:Elysium.Native.UnsafeNativeMethods.SetWindowLong(System.IntPtr,System.Int32,System.Int32)"/> or
        /// <see cref="M:Elysium.Native.UnsafeNativeMethods.SetWindowLongPtr(System.IntPtr,System.Int32,System.Int64)"/> has not been called previously,
        /// GetWindowLongPtr returns zero for values in the extra window or class memory.
        /// </returns>
        [DllImport(NativeMethods.User32, EntryPoint = "GetWindowLongPtr", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern long GetWindowLongPtr(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Changes an attribute of the specified window. The function also sets the 32-bit (long) value at the specified offset into the extra window memory.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">The zero-based offset to the value to be set. Valid values are in the range zero through the number of bytes of extra window memory, minus four.
        /// To set any other value, specify one of the <see cref="T:Elysium.Native.NativeMethods.WindowInfo"/> values.</param>
        /// <param name="dwNewLong">The replacement value.</param>
        /// <returns>
        /// If the function succeeds, the return value is the previous value of the specified 32-bit integer.
        /// If the function fails, the return value is zero.
        /// If the previous value of the specified 32-bit integer is zero, and the function succeeds, the return value is zero, but the function does not clear the last error information.
        /// This makes it difficult to determine success or failure. To deal with this, you should clear the last error information by calling SetLastError with 0 before calling SetWindowLong.
        /// Then, function failure will be indicated by a return value of zero and a GetLastError result that is nonzero.
        /// </returns>
        [DllImport(NativeMethods.User32, EntryPoint = "SetWindowLong", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        /// <summary>
        /// Changes an attribute of the specified window. The function also sets a value at the specified offset in the extra window memory.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window and, indirectly, the class to which the window belongs.
        /// The SetWindowLongPtr function fails if the process that owns the window specified by the hWnd parameter is at a higher process privilege in the UIPI hierarchy than the process the calling thread resides in.
        /// Windows XP/2000:   The SetWindowLongPtr function fails if the window specified by the hWnd parameter does not belong to the same process as the calling thread.
        /// </param>
        /// <param name="nIndex">The zero-based offset to the value to be set. Valid values are in the range zero through the number of bytes of extra window memory, minus eight.
        /// To set any other value, specify one of the <see cref="T:Elysium.Native.NativeMethods.WindowInfo"/> values.</param>
        /// <param name="dwNewLong">The replacement value.</param>
        /// <returns>
        /// If the function succeeds, the return value is the previous value of the specified offset.
        /// If the function fails, the return value is zero.
        /// If the previous value is zero and the function succeeds, the return value is zero, but the function does not clear the last error information.
        /// To determine success or failure, clear the last error information by calling SetLastError with 0, then call SetWindowLongPtr.
        /// Function failure will be indicated by a return value of zero and a GetLastError result that is nonzero.
        /// </returns>
        [DllImport(NativeMethods.User32, EntryPoint = "SetWindowLongPtr", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern long SetWindowLongPtr(IntPtr hWnd, int nIndex, long dwNewLong);

        /// <summary>
        /// Changes the size, position, and Z order of a child, pop-up, or top-level window. 
        /// These windows are ordered according to their appearance on the screen. 
        /// The topmost window receives the highest rank and is the first window in the Z order.
        /// </summary>
        /// <param name="hWnd">A handle to the window</param>
        /// <param name="hWndInsertAfter">A handle to the window to precede the positioned window in the Z order</param>
        /// <param name="X">The new position of the left side of the window, in client coordinates</param>
        /// <param name="Y">The new position of the top of the window, in client coordinates</param>
        /// <param name="cx">The new width of the window, in pixels</param>
        /// <param name="cy">The new height of the window, in pixels</param>
        /// <param name="uFlags">The window sizing and positioning flags.</param>
        /// <returns>TRUE if success; otherwise, FALSE.</returns>
        [DllImport(NativeMethods.User32, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, [MarshalAs(UnmanagedType.U4)] NativeMethods.WindowPosition uFlags);

        #endregion

        #region Menu

        /// <summary>
        /// Enables the application to access the window menu (also known as the system menu or the control menu) for copying and modifying.
        /// </summary>
        /// <param name="hWnd">A handle to the window that will own a copy of the window menu.</param>
        /// <param name="bRevert">
        /// The action to be taken.
        /// If this parameter is FALSE, GetSystemMenu returns a handle to the copy of the window menu currently in use. The copy is initially identical to the window menu, but it can be modified.
        /// If this parameter is TRUE, GetSystemMenu resets the window menu back to the default state. The previous window menu, if any, is destroyed.
        /// </param>
        /// <returns>If the bRevert parameter is FALSE, the return value is a handle to a copy of the window menu. If the bRevert parameter is TRUE, the return value is IntPtr.Zero.</returns>
        [DllImport(NativeMethods.User32, ExactSpelling = true, SetLastError = false)]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)] bool bRevert);

        /// <summary>
        /// Enables, disables, or grays the specified menu item.
        /// </summary>
        /// <param name="hMenu">A handle to the menu.</param>
        /// <param name="uIDEnableItem">The menu item to be enabled, disabled, or grayed, as determined by the uEnable parameter. This parameter specifies an item in a menu bar, menu, or submenu.</param>
        /// <param name="uEnable">Controls the interpretation of the uIDEnableItem parameter and indicate whether the menu item is enabled, disabled, or grayed.</param>
        /// <returns> The return value specifies the previous state of the menu item (it is either MF_DISABLED, MF_ENABLED, or MF_GRAYED). If the menu item does not exist, the return value is -1. </returns>
        [DllImport(NativeMethods.User32, ExactSpelling = true, SetLastError = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern int EnableMenuItem(IntPtr hMenu, [MarshalAs(UnmanagedType.U4)] int uIDEnableItem, [MarshalAs(UnmanagedType.U4)] NativeMethods.MenuItemState uEnable);

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
        /// If the function succeeds, the return value is the handle to the hook procedure.
        /// If the function fails, the return value is IntPtr.Zero. 
        /// </returns>
        [DllImport(NativeMethods.User32, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx([MarshalAs(UnmanagedType.I4)] NativeMethods.HookType hookType, [MarshalAs(UnmanagedType.FunctionPtr)] NativeMethods.HookProc lpfn, IntPtr hMod, int dwThreadId);

        /// <summary>
        /// Removes a hook procedure installed in a hook chain
        /// by the <see cref="M:Elysium.Native.UnsafeNativeMethods.SetWindowsHookEx(Elysium.Native.NativeMethods.HookType,Elysium.Native.NativeMethods.HookProc,System.IntPtr,System.Int32)"/> function.
        /// </summary>
        /// <param name="hhk">
        /// A handle to the hook to be removed. This parameter is a hook handle obtained
        /// by a previous call to <see cref="M:Elysium.Native.UnsafeNativeMethods.SetWindowsHookEx(Elysium.Native.NativeMethods.HookType,Elysium.Native.NativeMethods.HookProc,System.IntPtr,System.Int32)"/>.
        /// </param>
        /// <returns>TRUE if success; otherwise, FALSE.</returns>
        [DllImport(NativeMethods.User32, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        #endregion
    }
}