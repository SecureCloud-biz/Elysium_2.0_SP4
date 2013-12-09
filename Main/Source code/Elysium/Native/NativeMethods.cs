using System;
using System.Runtime.InteropServices;
using System.Security;

using JetBrains.Annotations;

namespace Elysium.Native
{
    [SecurityCritical]
    internal static class NativeMethods
    {
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global

        #region DLL names

        public const string Kernel = "kernel32.dll";
        public const string User32 = "user32.dll";
        public const string GDI32 = "user32.dll";
        public const string DWMAPI = "dwmapi.dll";
        public const string Shell32 = "shell32.dll";

        #endregion

        #region Messages

        /// <summary>
        /// Informs all top-level windows that Desktop Window Manager (DWM) composition has been enabled or disabled.
        /// wParam: Not used.
        /// lParam: Not used.
        /// Return value: If an application processes this message, it should return zero.
        /// </summary>
        public const int WM_DWMCOMPOSITIONCHANGED = 0x031E;

        /// <summary>
        /// Sets the text of a window.
        /// wParam: This parameter is not used.
        /// lParam: A pointer to a null-terminated string that is the window text.
        /// Return value: The return value is TRUE if the text is set. It is FALSE (for an edit control), LB_ERRSPACE (for a list box), or CB_ERRSPACE (for a combo box)
        /// if insufficient space is available to set the text in the edit control. It is CB_ERR if this message is sent to a combo box without an edit control.
        /// </summary>
        public const int WM_SETTEXT = 0x000C;

        /// <summary>
        /// Associates a new large or small icon with a window. The system displays the large icon in the ALT+TAB dialog box, and the small icon in the window caption.
        /// wParam: The type of icon to be set. This parameter can be one of the values of <see cref="T:Elysium.Native.NativeMethods.IconTypes"/>.
        /// lParam: A handle to the new large or small icon. If this parameter is NULL, the icon indicated by wParamis removed.
        /// Return value: The return value is a handle to the previous large or small icon, depending on the value of wParam. It is NULL if the window previously had no icon of the type indicated by wParam.
        /// </summary>
        public const int WM_SETICON = 0x0080;

        /// <summary>
        /// A window receives this message when the user chooses a command from the Window menu (formerly known as the system or control menu) or
        /// when the user chooses the maximize button, minimize button, restore button, or close button.
        /// Parameters
        /// wParam: The type of system command requested. <see cref="T:Elysium.Native.NativeMethods.SystemCommands"/>
        /// lParam:
        /// The low-order word specifies the horizontal position of the cursor, in screen coordinates, if a window menu command is chosen with the mouse. Otherwise, this parameter is not used.
        /// The high-order word specifies the vertical position of the cursor, in screen coordinates, if a window menu command is chosen with the mouse.
        /// This parameter is –1 if the command is chosen using a system accelerator, or zero if using a mnemonic.
        /// Return value: An application should return zero if it processes this message.
        /// </summary>
        public const int WM_SYSCOMMAND = 0x0112;

        /// <summary>
        /// Sent to a window when its nonclient area needs to be changed to indicate an active or inactive state.
        /// wParam:
        /// Indicates when a title bar or icon needs to be changed to indicate an active or inactive state. If an active title bar or icon is to be drawn, the wParam parameter is TRUE.
        /// If an inactive title bar or icon is to be drawn, wParam is FALSE.
        /// lParam:
        /// When a visual style is active for this window, this parameter is not used.
        /// When a visual style is not active for this window, this parameter is a handle to an optional update region for the nonclient area of the window.
        /// If this parameter is set to -1, DefWindowProc does not repaint the nonclient area to reflect the state change.
        /// Return value:
        /// When the wParam parameter is FALSE, an application should return TRUE to indicate that the system should proceed with the default processing,
        /// or it should return FALSE to prevent the change. When wParam is TRUE, the return value is ignored.
        /// </summary>
        public const int WM_NCACTIVATE = 0x0086;

        /// <summary>
        /// Sent when the size and position of a window's client area must be calculated.
        /// By processing this message, an application can control the content of the window's client area when the size or position of the window changes.
        /// wParam:
        /// If wParam is TRUE, it specifies that the application should indicate which part of the client area contains valid information.
        /// The system copies the valid information to the specified area within the new client area.
        /// If wParam is FALSE, the application does not need to indicate the valid part of the client area.
        /// lParam:
        /// If wParam is TRUE, lParam points to an NCCALCSIZE_PARAMS structure that contains information an application can use to calculate the new size and position of the client rectangle.
        /// If wParam is FALSE, lParam points to a RECT structure.
        /// On entry, the structure contains the proposed window rectangle for the window.
        /// On exit, the structure should contain the screen coordinates of the corresponding window client area.
        /// </summary>
        public const int WM_NCCALCSIZE = 0x0083;

        public const int WM_NCHITTEST = 0x0084;

        public const int WM_NCRBUTTONUP = 0x00A5;

        public const int WM_SIZE = 0x0005;

        public const int WM_WINDOWPOSCHANGED = 0x0047;

        #endregion

        #region Desktop and Monitors

        public enum MonitorDefaults
        {
            /// <summary>
            /// Returns a handle to the display monitor that is nearest to the window.
            /// </summary>
            MONITOR_DEFAULTTONULL = 0x00000000,

            /// <summary>
            /// Returns NULL.
            /// </summary>
            MONITOR_DEFAULTTOPRIMARY = 0x00000001,

            /// <summary>
            /// Returns a handle to the primary display monitor.
            /// </summary>
            MONITOR_DEFAULTTONEAREST = 0x00000002
        }

        /// <summary>
        /// The MONITORINFO structure contains information about a display monitor.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class MONITORINFO
        {
            /// <summary>
            /// The size of the structure, in bytes. Initialized by default.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));

            /// <summary>
            /// A <see cref="T:Elysium.Native.NativeMethods.RECT"/> structure that specifies the display monitor rectangle, expressed in virtual-screen coordinates.
            /// Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.
            /// </summary>
            public RECT rcMonitor;

            /// <summary>
            /// A <see cref="T:Elysium.Native.NativeMethods.RECT"/> structure that specifies the work area rectangle of the display monitor, expressed in virtual-screen coordinates.
            /// Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.
            /// </summary>
            public RECT rcWork;

            /// <summary>
            /// A set of flags that represent attributes of the display monitor.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public int dwFlags;
        }

        /// <summary>
        /// Number of pixels per logical inch along the screen width. In a system with multiple display monitors, this value is the same for all monitors.
        /// </summary>
        public const int LOGPIXELSX = 88;

        /// <summary>
        /// Number of pixels per logical inch along the screen height. In a system with multiple display monitors, this value is the same for all monitors.
        /// </summary>
        public const int LOGPIXELSY = 90;

        public enum ShellMessages
        {
            /// <summary>
            /// <para>Retrieves the autohide and always-on-top states of the Windows taskbar.</para>
            /// <code>uState = (UINT) SHAppBarMessage(ABM_GETSTATE, pabd);</code>
            /// <para>Parameters:</para>
            /// <para><c>pabd</c>: Pointer to an APPBARDATA structure. You must specify the cbSize member when sending this message; all other members are ignored.</para>
            /// <para>Return value:</para>
            /// <para>Returns zero if the taskbar is neither in the autohide nor always-on-top state. Otherwise, the return value is one or both of the <see cref="T:Elysium.Native.NativeMethods.TaskbarState"/> enumeration values.</para>
            /// </summary>
            ABM_GETSTATE = 0x00000004,

            /// <summary>
            /// <para>Retrieves the bounding rectangle of the Windows taskbar.</para>
            /// </summary>
            ABM_GETTASKBARPOS = 0x00000005
        }

        public enum TaskbarState
        {
            /// <summary>
            /// The taskbar is in the autohide state.
            /// </summary>
            ABS_AUTOHIDE = 0x0000001,

            /// <summary>
            /// <para>The taskbar is in the always-on-top state.</para>
            /// <para>Note: As of Windows 7, ABS_ALWAYSONTOP is no longer returned because the taskbar is always in that state.
            /// Older code should be updated to ignore the absence of this value in not assume that return value to mean that the taskbar is not in the always-on-top state.</para>
            /// </summary>
            ABS_ALWAYSONTOP = 0x0000002
        }

        public enum AppBarEdge
        {
            /// <summary>
            /// Left edge.
            /// </summary>
            ABE_LEFT = 0,

            /// <summary>
            /// Top edge.
            /// </summary>
            ABE_TOP,

            /// <summary>
            /// Right edge.
            /// </summary>
            ABE_RIGHT,

            /// <summary>
            /// Bottom edge.
            /// </summary>
            ABE_BOTTOM
        }

        [StructLayout(LayoutKind.Sequential)]
        public class APPBARDATA
        {
            public APPBARDATA(IntPtr hWnd)
            {
                this.hWnd = hWnd;
            }

            /// <summary>
            /// Contains information about a system appbar message.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize = Marshal.SizeOf(typeof(APPBARDATA));

            /// <summary>
            /// The handle to the appbar window.
            /// </summary>
            public IntPtr hWnd;

            /// <summary>
            /// An application-defined message identifier. The application uses the specified identifier for notification messages that it sends to the appbar identified by the hWnd member.
            /// This member is used when sending the ABM_NEW message.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public int uCallbackMessage;

            /// <summary>
            /// A value that specifies an edge of the screen.This member is used when sending one of these messages:
            /// <list type="bullet">
            /// <item>
            /// <description>ABM_GETAUTOHIDEBAR</description>
            /// </item>
            /// <item>
            /// <description>ABM_SETAUTOHIDEBAR</description>
            /// </item>
            /// <item>
            /// <description>ABM_GETAUTOHIDEBAREX</description>
            /// </item>
            /// <item>
            /// <description>ABM_SETAUTOHIDEBAREX</description>
            /// </item>
            /// <item>
            /// <description>ABM_QUERYPOS</description>
            /// </item>
            /// <item>
            /// <description>ABM_SETPOS</description>
            /// </item>
            /// </list>
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public AppBarEdge uEdge;

            /// <summary>
            /// A RECT structure whose use varies depending on the message:
            /// <list type="bullet">
            /// <item>
            /// <description>ABM_GETTASKBARPOS, ABM_QUERYPOS, ABM_SETPOS: The bounding rectangle, in screen coordinates, of an appbar or the Windows taskbar.</description>
            /// </item>
            /// <item>
            /// <description>ABM_GETAUTOHIDEBAREX, ABM_SETAUTOHIDEBAREX: The monitor on which the operation is being performed. This information can be retrieved through the GetMonitorInfo function.</description>
            /// </item>
            /// </list>
            /// </summary>
            public RECT rc;

            /// <summary>
            /// A message-dependent value. This member is used with these messages:
            /// <list type="bullet">
            /// <item>
            /// <descriptions>ABM_SETAUTOHIDEBAR</descriptions>
            /// </item>
            /// <item>
            /// <descriptions>ABM_SETAUTOHIDEBAREX</descriptions>
            /// </item>
            /// <item>
            /// <descriptions>ABM_SETSTATE</descriptions>
            /// </item>
            /// </list>
            /// See the individual message pages for details.
            /// </summary>
            [MarshalAs(UnmanagedType.SysInt)]
            public IntPtr lParam;
        }

        #endregion

        #region Desktop Window Manager

        /// <summary>
        /// A ratio used with the Desktop Window Manager (DWM) timing API.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct UNSIGNED_RATIO
        {
            /// <summary>
            /// The ratio numerator.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public int uiNumerator;

            /// <summary>
            /// The ratio denominator
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public int uiDenominator;
        }

        /// <summary>
        /// Contains Desktop Window Manager (DWM) composition timing information.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class DWM_TIMING_INFO
        {
            /// <summary>
            /// The size of this DWM_TIMING_INFO structure. 
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize = Marshal.SizeOf(typeof(DWM_TIMING_INFO));

            /// <summary>
            /// The monitor refresh rate
            /// </summary>
            public UNSIGNED_RATIO rateRefresh;

            /// <summary>
            /// The monitor refresh rate.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long qpcRefreshPeriod;

            /// <summary>
            /// The composition rate.
            /// </summary>
            public UNSIGNED_RATIO rateCompose;

            /// <summary>
            /// The query performance counter value before the vertical blank.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long qpcVBlank;

            /// <summary>
            /// The DWM refresh counter.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cRefresh;

            /// <summary>
            /// The Microsoft DirectX refresh counter.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public int cDXRefresh;

            /// <summary>
            /// The query performance counter value for a frame composition.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long qpcCompose;

            /// <summary>
            /// The frame number that was composed at qpcCompose. 
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cFrame;

            /// <summary>
            /// The DirectX present number used to identify rendering frames.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public int cDXPresent;

            /// <summary>
            /// The refresh count of the frame that was composed at qpcCompose. 
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cRefreshFrame;

            /// <summary>
            /// The DWM frame number that was last submitted.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cFrameSubmitted;

            /// <summary>
            /// The DirectX present number that was last submitted.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public int cDXPresentSubmitted;

            /// <summary>
            /// The DWM frame number that was last confirmed as presented.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cFrameConfirmed;

            /// <summary>
            /// The DirectX present number that was last confirmed as presented.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public int cDXPresentConfirmed;

            /// <summary>
            /// The target refresh count of the last frame confirmed completed by the graphics processing unit (GPU).
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cRefreshConfirmed;

            /// <summary>
            /// The DirectX refresh count when the frame was confirmed as presented.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public int cDXRefreshConfirmed;

            /// <summary>
            /// The number of frames the DWM presented late.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cFramesLate;

            /// <summary>
            /// The number of composition frames that have been issued but have not been confirmed as completed.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public int cFramesOutstanding;

            /// <summary>
            /// The last frame displayed.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cFrameDisplayed;

            /// <summary>
            /// The query performance counter (QPC) time of the composition pass when the frame was displayed.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long qpcFrameDisplayed;

            /// <summary>
            /// The vertical refresh count when the frame should have become visible.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cRefreshFrameDisplayed;

            /// <summary>
            /// The ID of the last frame marked complete.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cFrameComplete;

            /// <summary>
            /// /The QPC time when the last frame was marked as completed.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long qpcFrameComplete;

            /// <summary>
            /// The ID of the last frame marked as pending.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cFramePending;

            /// <summary>
            /// The QPC time when the last frame was marked pending.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long qpcFramePending;

            /// <summary>
            /// The number of unique frames displayed. This value is valid only after a second call to DwmGetCompositionTimingInfo. 
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cFramesDisplayed;

            /// <summary>
            /// The number of new completed frames that have been received.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cFramesComplete;

            /// <summary>
            /// The number of new frames submitted to DirectX but not yet completed.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cFramesPending;

            /// <summary>
            /// The number of frames available but not displayed, used, or dropped. This value is valid only after a second call to DwmGetCompositionTimingInfo. 
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cFramesAvailable;

            /// <summary>
            /// The number of rendered frames that were never displayed because composition occurred too late. This value is valid only after a second call to DwmGetCompositionTimingInfo. 
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cFramesDropped;

            /// <summary>
            /// The number of times an old frame was composed when a new frame should have been used but was not available.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cFramesMissed;

            /// <summary>
            /// The frame count at which the next frame is scheduled to be displayed.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cRefreshNextDisplayed;

            /// <summary>
            /// The frame count at which the next DirectX present is scheduled to be displayed.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cRefreshNextPresented;

            /// <summary>
            /// The total number of refreshes that have been displayed for the application since DwmSetPresentParameters was last called. 
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cRefreshesDisplayed;

            /// <summary>
            /// The total number of refreshes that have been presented by the application since DwmSetPresentParameters was last called. 
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cRefreshesPresented;

            /// <summary>
            /// The refresh number when content for this window started to be displayed.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cRefreshStarted;

            /// <summary>
            /// The total number of pixels DirectX redirected to the DWM.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cPixelsReceived;

            /// <summary>
            /// The number of pixels drawn.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cPixelsDrawn;

            /// <summary>
            /// The number of empty buffers in the flip chain.
            /// </summary>
            [MarshalAs(UnmanagedType.U8)]
            public long cBuffersEmpty;
        }

        #endregion

        #region Graphics

        /// <summary>
        /// Defines the x- and y- coordinates of a point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            /// <summary>
            /// The x-coordinate of the point.
            /// </summary>
            public int x;

            /// <summary>
            /// The y-coordinate of the point.
            /// </summary>
            public int y;
        }

        /// <summary>
        /// Defines the coordinates of the upper-left and lower-right corners of a rectangle.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            /// <summary>
            /// The x-coordinate of the upper-left corner of the rectangle.
            /// </summary>
            public int left;

            /// <summary>
            /// The y-coordinate of the upper-left corner of the rectangle.
            /// </summary>
            public int top;

            /// <summary>
            /// The x-coordinate of the lower-right corner of the rectangle.
            /// </summary>
            public int right;

            /// <summary>
            /// The y-coordinate of the lower-right corner of the rectangle.
            /// </summary>
            public int bottom;
        }

        /// <summary>
        /// Defines the margins of windows that have visual styles applied.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class MARGINS
        {
            /// <summary>
            /// Width of the left border that retains its size.
            /// </summary>
            public int cxLeftWidth;

            /// <summary>
            /// Width of the right border that retains its size.
            /// </summary>
            public int cxRightWidth;

            /// <summary>
            /// Height of the top border that retains its size.
            /// </summary>
            public int cyTopHeight;

            /// <summary>
            /// Height of the bottom border that retains its size.
            /// </summary>
            public int cyBottomHeight;

            /// <summary>
            /// Initializes a new instance of the <see cref="T:Elysium.Native.NativeMethods.MARGINS"/> class.
            /// </summary>
            /// <param name="left">Width of the left border that retains its size.</param>
            /// <param name="top">Width of the right border that retains its size.</param>
            /// <param name="right">Height of the top border that retains its size.</param>
            /// <param name="bottom">Height of the bottom border that retains its size.</param>
            public MARGINS(int left, int top, int right, int bottom)
            {
                cxLeftWidth = left;
                cyTopHeight = top;
                cxRightWidth = right;
                cyBottomHeight = bottom;
            }
        }

        public enum RegionCombinations
        {
            /// <summary>
            /// Creates the intersection of the two combined regions.
            /// </summary>
            RGN_AND = 1,

            /// <summary>
            /// Creates the union of two combined regions.
            /// </summary>
            RGN_OR = 2,

            /// <summary>
            /// Creates the union of two combined regions except for any overlapping areas.
            /// </summary>
            RGN_XOR = 3,

            /// <summary>
            /// Combines the parts of first region that are not part of second region.
            /// </summary>
            RGN_DIFF = 4,

            /// <summary>
            /// Creates a copy of the region.
            /// </summary>
            RGN_COPY = 5
        }

        public enum RegionCombinationResult
        {
            /// <summary>
            /// No region is created.
            /// </summary>
            ERROR = 0,

            /// <summary>
            /// The region is empty.
            /// </summary>
            NULLREGION = 1,

            /// <summary>
            /// The region is a single rectangle.
            /// </summary>
            SIMPLEREGION = 2,

            /// <summary>
            /// The region is more than a single rectangle.
            /// </summary>
            COMPLEXREGION = 3,
        }

        public enum IconTypes
        {
            /// <summary>
            /// Set the small icon for the window.
            /// </summary>
            ICON_SMALL = 0,

            /// <summary>
            /// Set the large icon for the window.
            /// </summary>
            ICON_BIG = 1
        }

        #endregion

        #region Window

        #region Communication

        /// <summary>
        /// The message is sent to all top-level windows in the system
        /// </summary>
        public static readonly IntPtr HWND_BROADCAST = new IntPtr(0xFFFF);

        #endregion

        #region Information

        public enum WindowInfo
        {
            /// <summary>
            /// Retrieves extra information private to the application, such as handles or pointers.
            /// </summary>
            DWLP_USER = 16,

            /// <summary>
            /// Retrieves extra information private to the application, such as handles or pointers.
            /// </summary>
            DWL_USER = 8,

            /// <summary>
            /// Retrieves the address of the dialog box procedure, or a handle representing the address of the dialog box procedure. You must use the CallWindowProc function to call the dialog box procedure.
            /// </summary>
            DWLP_DLGPROC = 8,

            /// <summary>
            /// Retrieves the address of the dialog box procedure, or a handle representing the address of the dialog box procedure. You must use the CallWindowProc function to call the dialog box procedure.
            /// </summary>
            DWL_DLGPROC = 4,

            /// <summary>
            /// Retrieves the return value of a message processed in the dialog box procedure.
            /// </summary>
            DWLP_MSGRESULT = 0,

            /// <summary>
            /// Retrieves the address of the window procedure, or a handle representing the address of the window procedure. You must use the CallWindowProc function to call the window procedure.
            /// </summary>
            GWLP_WNDPROC = -4,

            /// <summary>
            /// Retrieves a handle to the application instance.
            /// </summary>
            GWLP_HINSTANCE = -6,

            /// <summary>
            /// Retrieves a handle to the parent window, if any.
            /// </summary>
            GWLP_HWNDPARENT = -8,

            /// <summary>
            /// Retrieves the identifier of the window.
            /// </summary>
            GWLP_ID = -12,

            /// <summary>
            /// Retrieves the window styles.
            /// </summary>
            GWL_STYLE = -16,

            /// <summary>
            /// Retrieves the extended window styles.
            /// </summary>
            GWL_EXSTYLE = -20,


            /// <summary>
            /// Retrieves the user data associated with the window. This data is intended for use by the application that created the window. Its value is initially zero.
            /// </summary>
            GWLP_USERDATA = -21
        }

        #endregion

        #region Styles

        [Flags]
        public enum WindowStyles
        {
            /// <summary>
            /// The window is an overlapped window. An overlapped window has a title bar and a border. Same as the WS_TILED style.
            /// </summary>
            WS_OVERLAPPED = 0x00000000,

            /// <summary>
            /// The windows is a pop-up window. This style cannot be used with the WS_CHILD style.
            /// </summary>
            WS_POPUP = unchecked((int)0x80000000),

            /// <summary>
            /// The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the WS_POPUP style.
            /// </summary>
            WS_CHILD = 0x40000000,

            /// <summary>
            /// The window is initially minimized. Same as the WS_ICONIC style.
            /// </summary>
            WS_MINIMIZE = 0x20000000,

            /// <summary>
            /// The window is initially visible. This style can be turned on and off by using the ShowWindow or SetWindowPos function.
            /// </summary>
            WS_VISIBLE = 0x10000000,

            /// <summary>
            /// The window is initially disabled. A disabled window cannot receive input from the user. To change this after a window has been created, use the EnableWindow function.
            /// </summary>
            WS_DISABLED = 0x08000000,

            /// <summary>
            /// Clips child windows relative to each other; that is, when a particular child window receives a WM_PAINT message,
            /// the WS_CLIPSIBLINGS style clips all other overlapping child windows out of the region of the child window to be updated.
            /// If WS_CLIPSIBLINGS is not specified and child windows overlap, it is possible, when drawing within the client area of a child window, to draw within the client area of a neighboring child window.
            /// </summary>
            WS_CLIPSIBLINGS = 0x04000000,

            /// <summary>
            /// Excludes the area occupied by child windows when drawing occurs within the parent window. This style is used when creating the parent window.
            /// </summary>
            WS_CLIPCHILDREN = 0x02000000,

            /// <summary>
            /// The window is initially maximized.
            /// </summary>
            WS_MAXIMIZE = 0x01000000,

            /// <summary>
            /// The window has a title bar (includes the the WS_BORDER and WS_DLGFRAME styles).
            /// </summary>
            WS_CAPTION = WS_BORDER | WS_DLGFRAME,

            /// <summary>
            /// The window has a thin-line border.
            /// </summary>
            WS_BORDER = 0x00800000,

            /// <summary>
            /// The window has a border of a style typically used with dialog boxes. A window with this style cannot have a title bar.
            /// </summary>
            WS_DLGFRAME = 0x00400000,

            /// <summary>
            /// The window has a vertical scroll bar.
            /// </summary>
            WS_VSCROLL = 0x00200000,

            /// <summary>
            /// The window has a horizontal scroll bar.
            /// </summary>
            WS_HSCROLL = 0x00100000,

            /// <summary>
            /// The window has a window menu on its title bar. The WS_CAPTION style must also be specified.
            /// </summary>
            WS_SYSMENU = 0x00080000,

            /// <summary>
            /// The window has a sizing border. Same as the WS_SIZEBOX style.
            /// </summary>
            WS_THICKFRAME = 0x00040000,

            /// <summary>
            /// The window is the first control of a group of controls. The group consists of this first control and all controls defined after it, up to the next control with the WS_GROUP style.
            /// The first control in each group usually has the <see cref="F:Elysium.Native.NativeMethods.WindowStyles.WS_TABSTOP"/> style so that the user can move from group to group.
            /// The user can subsequently change the keyboard focus from one control in the group to the next control in the group by using the direction keys.
            /// You can turn this style on and off to change dialog box navigation.
            /// To change this style after a window has been created, use the <see cref="M:Elysium.Native.Interop.SetWindowLongPtr(System.IntPtr,Elysium.Native.NativeMethods.WindowInfo,System.IntPtr)"/> function.
            /// </summary>
            WS_GROUP = 0x00020000,

            /// <summary>
            /// The window is a control that can receive the keyboard focus when the user presses the TAB key. Pressing the TAB key changes the keyboard focus to the next control with the WS_TABSTOP style.
            /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
            /// For user-created windows and modeless dialogs to work with tab stops, alter the message loop to call the IsDialogMessage function.
            /// </summary>
            WS_TABSTOP = 0x00010000,


            /// <summary>
            /// The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.
            /// </summary>
            WS_MINIMIZEBOX = 0x00020000,

            /// <summary>
            /// The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.
            /// </summary>
            WS_MAXIMIZEBOX = 0x00010000,


            /// <summary>
            /// The window is an overlapped window. An overlapped window has a title bar and a border. Same as the WS_OVERLAPPED style.
            /// </summary>
            WS_TILED = WS_OVERLAPPED,

            /// <summary>
            /// The window is initially minimized. Same as the WS_MINIMIZE style.
            /// </summary>
            WS_ICONIC = WS_MINIMIZE,

            /// <summary>
            /// The window has a sizing border. Same as the WS_THICKFRAME style.
            /// </summary>
            WS_SIZEBOX = WS_THICKFRAME,

            /// <summary>
            /// The window is an overlapped window. Same as the WS_OVERLAPPEDWINDOW style.
            /// </summary>
            WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,


            /// <summary>
            /// The window is an overlapped window. Same as the WS_TILEDWINDOW style.
            /// </summary>
            WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,

            /// <summary>
            /// The window is a pop-up window. The WS_CAPTION and WS_POPUPWINDOW styles must be combined to make the window menu visible.
            /// </summary>
            WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,

            /// <summary>
            /// Same as the WS_CHILD style.
            /// </summary>
            WS_CHILDWINDOW = WS_CHILD,


            /// <summary>
            /// The window has a double border; the window can, optionally, be created with a title bar by specifying the WS_CAPTION style in the dwStyle parameter.
            /// </summary>
            WS_EX_DLGMODALFRAME = 0x00000001,

            /// <summary>
            /// The child window created with this style does not send the WM_PARENTNOTIFY message to its parent window when it is created or destroyed.
            /// </summary>
            WS_EX_NOPARENTNOTIFY = 0x00000004,

            /// <summary>
            /// The window should be placed above all non-topmost windows and should stay above them, even when the window is deactivated. To add or remove this style, use the SetWindowPos function.
            /// </summary>
            WS_EX_TOPMOST = 0x00000008,

            /// <summary>
            /// The window accepts drag-drop files.
            /// </summary>
            WS_EX_ACCEPTFILES = 0x00000010,

            /// <summary>
            /// The window should not be painted until siblings beneath the window (that were created by the same thread) have been painted.
            /// The window appears transparent because the bits of underlying sibling windows have already been painted.
            /// To achieve transparency without these restrictions, use the SetWindowRgn function.
            /// </summary>
            WS_EX_TRANSPARENT = 0x00000020,


            /// <summary>
            /// The window is a MDI child window.
            /// </summary>
            WS_EX_MDICHILD = 0x00000040,

            /// <summary>
            /// The window is intended to be used as a floating toolbar. A tool window has a title bar that is shorter than a normal title bar, and the window title is drawn using a smaller font.
            /// A tool window does not appear in the taskbar or in the dialog that appears when the user presses ALT+TAB. If a tool window has a system menu, its icon is not displayed on the title bar.
            /// However, you can display the system menu by right-clicking or by typing ALT+SPACE.
            /// </summary>
            WS_EX_TOOLWINDOW = 0x00000080,

            /// <summary>
            /// The window has a border with a raised edge.
            /// </summary>
            WS_EX_WINDOWEDGE = 0x00000100,

            /// <summary>
            /// The window has a border with a sunken edge.
            /// </summary>
            WS_EX_CLIENTEDGE = 0x00000200,

            /// <summary>
            /// The title bar of the window includes a question mark. When the user clicks the question mark, the cursor changes to a question mark with a pointer.
            /// If the user then clicks a child window, the child receives a WM_HELP message.
            /// The child window should pass the message to the parent window procedure, which should call the WinHelp function using the HELP_WM_HELP command.
            /// The Help application displays a pop-up window that typically contains help for the child window.
            /// WS_EX_CONTEXTHELP cannot be used with the WS_MAXIMIZEBOX or WS_MINIMIZEBOX styles.
            /// </summary>
            WS_EX_CONTEXTHELP = 0x00000400,


            /// <summary>
            /// The window has generic "right-aligned" properties. This depends on the window class.
            /// This style has an effect only if the shell language is Hebrew, Arabic, or another language that supports reading-order alignment; otherwise, the style is ignored.
            /// Using the WS_EX_RIGHT style for static or edit controls has the same effect as using the SS_RIGHT or ES_RIGHT style, respectively.
            /// Using this style with button controls has the same effect as using BS_RIGHT and BS_RIGHTBUTTON styles.
            /// </summary>
            WS_EX_RIGHT = 0x00001000,

            /// <summary>
            /// The window has generic left-aligned properties. This is the default.
            /// </summary>
            WS_EX_LEFT = 0x00000000,

            /// <summary>
            /// If the shell language is Hebrew, Arabic, or another language that supports reading-order alignment, the window text is displayed using right-to-left reading-order properties. For other languages, the style is ignored.
            /// </summary>
            WS_EX_RTLREADING = 0x00002000,

            /// <summary>
            /// The window text is displayed using left-to-right reading-order properties. This is the default.
            /// </summary>
            WS_EX_LTRREADING = 0x00000000,

            /// <summary>
            /// If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the vertical scroll bar (if present) is to the left of the client area. For other languages, the style is ignored.
            /// </summary>
            WS_EX_LEFTSCROLLBAR = 0x00004000,

            /// <summary>
            /// The vertical scroll bar (if present) is to the right of the client area. This is the default.
            /// </summary>
            WS_EX_RIGHTSCROLLBAR = 0x00000000,


            /// <summary>
            /// The window itself contains child windows that should take part in dialog box navigation.
            /// If this style is specified, the dialog manager recurses into children of this window when performing navigation operations such as handling the TAB key, an arrow key, or a keyboard mnemonic.
            /// </summary>
            WS_EX_CONTROLPARENT = 0x00010000,

            /// <summary>
            /// The window has a three-dimensional border style intended to be used for items that do not accept user input.
            /// </summary>
            WS_EX_STATICEDGE = 0x00020000,

            /// <summary>
            /// Forces a top-level window onto the taskbar when the window is visible.
            /// </summary>
            WS_EX_APPWINDOW = 0x00040000,


            /// <summary>
            /// The window is an overlapped window.
            /// </summary>
            WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,

            /// <summary>
            /// The window is palette window, which is a modeless dialog box that presents an array of commands.
            /// </summary>
            WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,


            /// <summary>
            /// The window is a layered window. This style cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC.
            /// Windows 8:  The WS_EX_LAYERED style is supported for top-level windows and child windows. Previous Windows versions support WS_EX_LAYERED only for top-level windows.
            /// </summary>
            WS_EX_LAYERED = 0x00080000,


            /// <summary>
            /// The window does not pass its window layout to its child windows.
            /// </summary>
            WS_EX_NOINHERITLAYOUT = 0x00100000,

            /// <summary>
            /// The window does not render to a redirection surface. This is for windows that do not have visible content or that use mechanisms other than surfaces to provide their visual.
            /// </summary>
            WS_EX_NOREDIRECTIONBITMAP = 0x00200000, // Window 8

            /// <summary>
            /// If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the horizontal origin of the window is on the right edge. Increasing horizontal values advance to the left.
            /// </summary>
            WS_EX_LAYOUTRTL = 0x00400000,


            /// <summary>
            /// Paints all descendants of a window in bottom-to-top painting order using double-buffering. For more information, see Remarks. This cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC.
            /// </summary>
            WS_EX_COMPOSITED = 0x02000000,

            /// <summary>
            /// A top-level window created with this style does not become the foreground window when the user clicks it. The system does not bring this window to the foreground when the user minimizes or closes the foreground window.
            /// To activate the window, use the SetActiveWindow or SetForegroundWindow function.
            /// The window does not appear on the taskbar by default. To force the window to appear on the taskbar, use the WS_EX_APPWINDOW style.
            /// </summary>
            WS_EX_NOACTIVATE = 0x08000000
        }

        /// <summary>
        /// Indicates the position of the cursor hot spot.
        /// </summary>
        public enum HitTest
        {
            /// <summary>
            /// On the screen background or on a dividing line between windows (same as HTNOWHERE, except that the DefWindowProc function produces a system beep to indicate an error).
            /// </summary>
            HTERROR = -2,

            /// <summary>
            /// In a window currently covered by another window in the same thread (the message will be sent to underlying windows in the same thread until one of them returns a code that is not HTTRANSPARENT).
            /// </summary>
            HTTRANSPARENT = -1,

            /// <summary>
            /// On the screen background or on a dividing line between windows.
            /// </summary>
            HTNOWHERE = 0,

            /// <summary>
            /// In a client area.
            /// </summary>
            HTCLIENT = 1,

            /// <summary>
            /// In a title bar.
            /// </summary>
            HTCAPTION = 2,

            /// <summary>
            /// In a window menu or in a Close button in a child window.
            /// </summary>
            HTSYSMENU = 3,

            /// <summary>
            /// In a size box (same as HTSIZE).
            /// </summary>
            HTGROWBOX = 4,

            /// <summary>
            /// In a size box (same as HTGROWBOX).
            /// </summary>
            HTSIZE = 4,

            /// <summary>
            /// In a menu.
            /// </summary>
            HTMENU = 5,

            /// <summary>
            /// In a horizontal scroll bar.
            /// </summary>
            HTHSCROLL = 6,

            /// <summary>
            /// In the vertical scroll bar.
            /// </summary>
            HTVSCROLL = 7,

            /// <summary>
            /// In a Minimize button.
            /// </summary>
            HTMINBUTTON = 8,

            /// <summary>
            /// In a Minimize button.
            /// </summary>
            HTREDUCE = 8,

            /// <summary>
            /// In a Maximize button.
            /// </summary>
            HTMAXBUTTON = 9,

            /// <summary>
            /// In a Maximize button.
            /// </summary>
            HTZOOM = 9,

            /// <summary>
            /// In the left border of a resizable window (the user can click the mouse to resize the window horizontally).
            /// </summary>
            HTLEFT = 10,

            /// <summary>
            /// In the right border of a resizable window (the user can click the mouse to resize the window horizontally).
            /// </summary>
            HTRIGHT = 11,

            /// <summary>
            /// In the upper-horizontal border of a window.
            /// </summary>
            HTTOP = 12,

            /// <summary>
            /// In the upper-left corner of a window border.
            /// </summary>
            HTTOPLEFT = 13,

            /// <summary>
            /// In the upper-right corner of a window border.
            /// </summary>
            HTTOPRIGHT = 14,

            /// <summary>
            /// In the lower-horizontal border of a resizable window (the user can click the mouse to resize the window vertically).
            /// </summary>
            HTBOTTOM = 15,

            /// <summary>
            /// In the lower-left corner of a border of a resizable window (the user can click the mouse to resize the window diagonally).
            /// </summary>
            HTBOTTOMLEFT = 16,

            /// <summary>
            /// In the lower-right corner of a border of a resizable window (the user can click the mouse to resize the window diagonally).
            /// </summary>
            HTBOTTOMRIGHT = 17,

            /// <summary>
            /// In the border of a window that does not have a sizing border.
            /// </summary>
            HTBORDER = 18,

            /// <summary>
            /// In a Close button.
            /// </summary>
            HTCLOSE = 20,

            /// <summary>
            /// In a Help button.
            /// </summary>
            HTHELP = 21,
        }

        #endregion

        #region Position

        /// <summary>
        /// Contains information about the size and position of a window.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            /// <summary>
            /// A handle to the window.
            /// </summary>
            public IntPtr hwnd;

            /// <summary>
            /// The position of the window in Z order (front-to-back position). This member can be a handle to the window behind which this window is placed, or can be one of the special values listed with the SetWindowPos function.
            /// </summary>
            public IntPtr hwndInsertAfter;

            /// <summary>
            /// The position of the left edge of the window.
            /// </summary>
            public int x;

            /// <summary>
            /// The position of the top edge of the window.
            /// </summary>
            public int y;

            /// <summary>
            /// The window width, in pixels.
            /// </summary>
            public int cx;

            /// <summary>
            /// The window height, in pixels.
            /// </summary>
            public int cy;

            /// <summary>
            /// The window position. This member can be one or more of the following values.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public WindowPosition flags;
        }

        [Flags]
        public enum WindowPosition
        {
            /// <summary>
            /// Retains the current size.
            /// </summary>
            SWP_NOSIZE = 0x0001,

            /// <summary>
            /// Retains the current position.
            /// </summary>
            SWP_NOMOVE = 0x0002,

            /// <summary>
            /// Retains the current Z order.
            /// </summary>
            SWP_NOZORDER = 0x0004,

            /// <summary>
            /// Does not redraw changes. If this flag is set, no repainting of any kind occurs.
            /// This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved.
            /// When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
            /// </summary>
            SWP_NOREDRAW = 0x0008,

            /// <summary>
            /// Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOACTIVATE = 0x0010,

            /// <summary>
            /// Draws a frame (defined in the window's class description) around the window.
            /// </summary>
            SWP_DRAWFRAME = 0x0020,

            /// <summary>
            /// Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed.
            /// If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
            /// </summary>
            SWP_FRAMECHANGED = 0x0020,

            /// <summary>
            /// Displays the window.
            /// </summary>
            SWP_SHOWWINDOW = 0x0040,

            /// <summary>
            /// Hides the window.
            /// </summary>
            SWP_HIDEWINDOW = 0x0080,

            /// <summary>
            /// Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
            /// </summary>
            SWP_NOCOPYBITS = 0x0100,

            /// <summary>
            /// Does not change the owner window's position in the Z order.
            /// </summary>
            SWP_NOOWNERZORDER = 0x0200,

            /// <summary>
            /// Same as the SWP_NOOWNERZORDER flag.
            /// </summary>
            SWP_NOREPOSITION = SWP_NOOWNERZORDER,

            /// <summary>
            /// Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
            /// </summary>
            SWP_NOSENDCHANGING = 0x0400,

            /// <summary>
            /// Prevents generation of the WM_SYNCPAINT message.
            /// </summary>
            SWP_DEFERERASE = 0x2000,

            /// <summary>
            /// If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window.
            /// This prevents the calling thread from blocking its execution while other threads process the request.
            /// </summary>
            SWP_ASYNCWINDOWPOS = 0x4000
        }

        public static class WindowZOrder
        {
            /// <summary>
            /// Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window.
            /// </summary>
            public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

            /// <summary>
            /// Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.
            /// </summary>
            public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

            /// <summary>
            /// Places the window at the top of the Z order.
            /// </summary>
            public static readonly IntPtr HWND_TOP = new IntPtr(0);

            /// <summary>
            /// Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
            /// </summary>
            public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        }

        /// <summary>
        /// Contains information about the placement of a window on the screen.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class WINDOWPLACEMENT
        {
            /// <summary>
            /// The length of the structure, in bytes. Before calling the GetWindowPlacement or SetWindowPlacement functions, set this member to sizeof(WINDOWPLACEMENT).
            /// GetWindowPlacement and SetWindowPlacement fail if this member is not set correctly.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public int length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));
            
            /// <summary>
            /// The flags that control the position of the minimized window and the method by which the window is restored.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public WindowPlacementOptions flags;

            /// <summary>
            /// The current show state of the window. T
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public WindowState showCmd;

            /// <summary>
            /// The coordinates of the window's upper-left corner when the window is minimized.
            /// </summary>
            public POINT ptMinPosition;

            /// <summary>
            /// The coordinates of the window's upper-left corner when the window is maximized.
            /// </summary>
            public POINT ptMaxPosition;

            /// <summary>
            /// The window's coordinates when the window is in the restored position.
            /// </summary>
            public RECT rcNormalPosition;
        }

        [Flags]
        public enum WindowPlacementOptions
        {
            /// <summary>
            /// If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window.
            /// This prevents the calling thread from blocking its execution while other threads process the request.
            /// </summary>
            WPF_ASYNCWINDOWPLACEMENT = 0x0004,

            /// <summary>
            /// The restored window will be maximized, regardless of whether it was maximized before it was minimized. This setting is only valid the next time the window is restored. It does not change the default restoration behavior.
            /// This flag is only valid when the SW_SHOWMINIMIZED value is specified for the showCmd member.
            /// </summary>
            WPF_RESTORETOMAXIMIZED = 0x0002,

            /// <summary>
            /// The coordinates of the minimized window may be specified.
            /// This flag must be specified if the coordinates are set in the ptMinPosition member.
            /// </summary>
            WPF_SETMINPOSITION = 0x0001
        }

        public enum WindowStateChanges
        {
            SIZE_RESTORED = 0,
            SIZE_MINIMIZED = 1,
            SIZE_MAXIMIZED = 2,
            SIZE_MAXSHOW = 3,
            SIZE_MAXHIDE = 4
        }

        [Flags]
        public enum NonClientAreaSizeCalculationOptions
        {
            /// <summary>
            /// Specifies that the client area of the window is to be preserved and aligned with the top of the new position of the window.
            /// For example, to align the client area to the upper-left corner, return the WVR_ALIGNTOP and WVR_ALIGNLEFT values.
            /// </summary>
            WVR_ALIGNTOP = 0x0010,

            /// <summary>
            /// Specifies that the client area of the window is to be preserved and aligned with the left side of the new position of the window.
            /// For example, to align the client area to the lower-left corner, return the WVR_ALIGNLEFT and WVR_ALIGNBOTTOM values.
            /// </summary>
            WVR_ALIGNLEFT = 0x0020,

            /// <summary>
            /// Specifies that the client area of the window is to be preserved and aligned with the bottom of the new position of the window.
            /// For example, to align the client area to the top-left corner, return the WVR_ALIGNTOP and WVR_ALIGNLEFT values.
            /// </summary>
            WVR_ALIGNBOTTOM = 0x0040,

            /// <summary>
            /// Specifies that the client area of the window is to be preserved and aligned with the right side of the new position of the window.
            /// For example, to align the client area to the lower-right corner, return the WVR_ALIGNRIGHT and WVR_ALIGNBOTTOM values.
            /// </summary>
            WVR_ALIGNRIGHT = 0x0080,

            /// <summary>
            /// Used in combination with any other values, except WVR_VALIDRECTS, causes the window to be completely redrawn if the client rectangle changes size horizontally.
            /// This value is similar to CS_HREDRAW class style
            /// </summary>
            WVR_HREDRAW = 0x0100,

            /// <summary>
            /// Used in combination with any other values, except WVR_VALIDRECTS, causes the window to be completely redrawn if the client rectangle changes size vertically.
            /// This value is similar to CS_VREDRAW class style
            /// </summary>
            WVR_VREDRAW = 0x0200,

            /// <summary>
            /// This value causes the entire window to be redrawn. It is a combination of WVR_HREDRAW and WVR_VREDRAW values.
            /// </summary>
            WVR_REDRAW = WVR_HREDRAW | WVR_VREDRAW,

            /// <summary>
            /// This value indicates that, upon return from WM_NCCALCSIZE, the rectangles specified by the rgrc[1] and rgrc[2] members of the NCCALCSIZE_PARAMS structure contain
            /// valid destination and source area rectangles, respectively. The system combines these rectangles to calculate the area of the window to be preserved.
            /// The system copies any part of the window image that is within the source rectangle and clips the image to the destination rectangle. Both rectangles are in parent-relative or screen-relative coordinates.
            /// This flag cannot be combined with any other flags.
            /// This return value allows an application to implement more elaborate client-area preservation strategies, such as centering or preserving a subset of the client area.
            /// </summary>
            WVR_VALIDRECTS = 0x0400
        }
        
        /// <summary>
        /// The NCCALCSIZE_PARAMS structure contains information that an application can use while processing the WM_NCCALCSIZE message to calculate the size, position, and valid contents of the client area of a window. 
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NCCALCSIZE_PARAMS
        {
            /// <summary>
            /// Specifies an array of rectangles. The meaning of the array of rectangles changes during the processing of the WM_NCALCSIZE message
            /// </summary>
            public RECT rect0;

            /// <summary>
            /// Specifies an array of rectangles. The meaning of the array of rectangles changes during the processing of the WM_NCALCSIZE message
            /// </summary>
            public RECT rect1;

            /// <summary>
            /// Specifies an array of rectangles. The meaning of the array of rectangles changes during the processing of the WM_NCALCSIZE message
            /// </summary>
            public RECT rect2;

            /// <summary>
            /// Pointer to a WINDOWPOS structure that contains the size and position values specified in the operation that moved or resized the window. 
            /// </summary>
            [MarshalAs(UnmanagedType.LPStruct)]
            public WINDOWPOS lppos;
        }

        #endregion

        #endregion

        #region Menu

        #region System menu

        public enum SystemCommands
        {
            /// <summary>
            /// Indicates whether the screen saver is secure.
            /// </summary>
            SCF_ISSECURE = 0x00000001,

            /// <summary>
            /// Sizes the window.
            /// </summary>
            SC_SIZE = 0xF000,

            /// <summary>
            /// Moves the window.
            /// </summary>
            SC_MOVE = 0xF010,

            /// <summary>
            /// Minimizes the window.
            /// </summary>
            SC_MINIMIZE = 0xF020,

            /// <summary>
            /// Maximizes the window.
            /// </summary>
            SC_MAXIMIZE = 0xF030,

            /// <summary>
            /// Moves to the next window.
            /// </summary>
            SC_NEXTWINDOW = 0xF040,

            /// <summary>
            /// Moves to the previous window.
            /// </summary>
            SC_PREVWINDOW = 0xF050,

            /// <summary>
            /// Closes the window.
            /// </summary>
            SC_CLOSE = 0xF060,

            /// <summary>
            /// Scrolls vertically.
            /// </summary>
            SC_VSCROLL = 0xF070,

            /// <summary>
            /// Scrolls horizontally.
            /// </summary>
            SC_HSCROLL = 0xF080,

            /// <summary>
            /// Retrieves the window menu as a result of a mouse click.
            /// </summary>
            SC_MOUSEMENU = 0xF090,

            /// <summary>
            /// Retrieves the window menu as a result of a keystroke.
            /// </summary>
            SC_KEYMENU = 0xF100,

            /// <summary>
            /// Restores the window to its normal position and size.
            /// </summary>
            SC_RESTORE = 0xF120,

            /// <summary>
            /// Activates the Start menu.
            /// </summary>
            SC_TASKLIST = 0xF130,

            /// <summary>
            /// Executes the screen saver application specified in the [boot] section of the System.ini file.
            /// </summary>
            SC_SCREENSAVE = 0xF140,

            /// <summary>
            /// Activates the window associated with the application-specified hot key. The lParam parameter identifies the window to activate.
            /// </summary>
            SC_HOTKEY = 0xF150,

            /// <summary>
            /// Selects the default item; the user double-clicked the window menu.
            /// </summary>
            SC_DEFAULT = 0xF160,

            /// <summary>
            /// Sets the state of the display. This command supports devices that have power-saving features, such as a battery-powered personal computer.
            /// </summary>
            SC_MONITORPOWER = 0xF170,

            /// <summary>
            /// Changes the cursor to a question mark with a pointer. If the user then clicks a control in the dialog box, the control receives a WM_HELP message.
            /// </summary>
            SC_CONTEXTHELP = 0xF180
        }

        #endregion

        #region Popup menu

        public enum PopupMenuTracks
        {
            /// <summary>
            /// If the menu cannot be shown at the specified location without overlapping the excluded rectangle, the system tries to accommodate the requested horizontal alignment before the requested vertical alignment.
            /// </summary>
            TPM_HORIZONTAL = 0x0000,

            /// <summary>
            /// If the menu cannot be shown at the specified location without overlapping the excluded rectangle, the system tries to accommodate the requested vertical alignment before the requested horizontal alignment.
            /// </summary>
            TPM_VERTICAL = 0x0040,


            /// <summary>
            /// Positions the shortcut menu so that its left side is aligned with the coordinate specified by the x parameter.
            /// </summary>
            TPM_LEFTALIGN = 0x0000,

            /// <summary>
            /// Centers the shortcut menu horizontally relative to the coordinate specified by the x parameter.
            /// </summary>
            TPM_CENTERALIGN = 0x0004,

            /// <summary>
            /// Positions the shortcut menu so that its right side is aligned with the coordinate specified by the x parameter.
            /// </summary>
            TPM_RIGHTALIGN = 0x0008,


            /// <summary>
            /// Positions the shortcut menu so that its top side is aligned with the coordinate specified by the y parameter.
            /// </summary>
            TPM_TOPALIGN = 0x0000,

            /// <summary>
            /// Centers the shortcut menu vertically relative to the coordinate specified by the y parameter.
            /// </summary>
            TPM_VCENTERALIGN = 0x0010,

            /// <summary>
            /// Positions the shortcut menu so that its bottom side is aligned with the coordinate specified by the y parameter.
            /// </summary>
            TPM_BOTTOMALIGN = 0x0020,


            /// <summary>
            /// Animates the menu from right to left.
            /// </summary>
            TPM_HORNEGANIMATION = 0x0800,

            /// <summary>
            /// Animates the menu from left to right.
            /// </summary>
            TPM_HORPOSANIMATION = 0x0400,

            /// <summary>
            /// Displays menu without animation.
            /// </summary>
            TPM_NOANIMATION = 0x4000,

            /// <summary>
            /// Animates the menu from bottom to top.
            /// </summary>
            TPM_VERNEGANIMATION = 0x2000,

            /// <summary>
            /// Animates the menu from top to bottom.
            /// </summary>
            TPM_VERPOSANIMATION = 0x1000,


            /// <summary>
            /// The user can select menu items with only the left mouse button.
            /// </summary>
            TPM_LEFTBUTTON = 0x0000,

            /// <summary>
            /// The user can select menu items with both the left and right mouse buttons.
            /// </summary>
            TPM_RIGHTBUTTON = 0x0002,


            /// <summary>
            /// The function does not send notification messages when the user clicks a menu item.
            /// </summary>
            TPM_NONOTIFY = 0x0080,

            /// <summary>
            /// The function returns the menu item identifier of the user's selection in the return value.
            /// </summary>
            TPM_RETURNCMD = 0x0100,


            /// <summary>
            /// Use the TPM_RECURSE flag to display a menu when another menu is already displayed. This is intended to support context menus within a menu.
            /// </summary>
            TPM_RECURSE = 0x0001,

            /// <summary>
            /// For right-to-left text layout, use TPM_LAYOUTRTL. By default, the text layout is left-to-right.
            /// </summary>
            TPM_LAYOUTRTL = 0x8000
        }

        #endregion

        #region Menu item

        public enum MenuItemState
        {
            /// <summary>
            /// Indicates that uIDEnableItem gives the identifier of the menu item. If neither the MF_BYCOMMAND nor MF_BYPOSITION flag is specified, the MF_BYCOMMAND flag is the default flag.
            /// </summary>
            MF_BYCOMMAND = 0x00000000,

            /// <summary>
            /// Indicates that uIDEnableItem gives the zero-based relative position of the menu item.
            /// </summary>
            MF_BYPOSITION = 0x00000400,


            /// <summary>
            /// Indicates that the menu item is enabled and restored from a grayed state so that it can be selected.
            /// </summary>
            MF_ENABLED = 0x00000000,

            /// <summary>
            /// Indicates that the menu item is disabled and grayed so that it cannot be selected.
            /// </summary>
            MF_GRAYED = 0x00000001,

            /// <summary>
            /// Indicates that the menu item is disabled, but not grayed, so it cannot be selected.
            /// </summary>
            MF_DISABLED = 0x00000002
        }

        #endregion

        #endregion

        #region Hooks

        public enum HookType
        {
            /// <summary>
            /// Installs a hook procedure that monitors messages generated as a result of an input event in a dialog box, message box, menu, or scroll bar.
            /// For more information, see the MessageProc hook procedure.
            /// </summary>
            WH_MSGFILTER = -1,

            /// <summary>
            /// Installs a hook procedure that records input messages posted to the system message queue. This hook is useful for recording macros.
            /// For more information, see the JournalRecordProc hook procedure.
            /// </summary>
            WH_JOURNALRECORD = 0,

            /// <summary>
            /// Installs a hook procedure that posts messages previously recorded by a WH_JOURNALRECORD hook procedure.
            /// For more information, see the JournalPlaybackProc hook procedure.
            /// </summary>
            WH_JOURNALPLAYBACK = 1,

            /// <summary>
            /// Installs a hook procedure that monitors keystroke messages.
            /// For more information, see the KeyboardProc hook procedure.
            /// </summary>
            WH_KEYBOARD = 2,

            /// <summary>
            /// Installs a hook procedure that monitors messages posted to a message queue.
            /// For more information, see the GetMsgProc hook procedure.
            /// </summary>
            WH_GETMESSAGE = 3,

            /// <summary>
            /// Installs a hook procedure that monitors messages before the system sends them to the destination window procedure.
            /// For more information, see the CallWndProc hook procedure.
            /// </summary>
            WH_CALLWNDPROC = 4,

            /// <summary>
            /// Installs a hook procedure that receives notifications useful to a CBT application.
            /// For more information, see the CBTProc hook procedure.
            /// </summary>
            WH_CBT = 5,

            /// <summary>
            /// Installs a hook procedure that monitors messages generated as a result of an input event in a dialog box, message box, menu, or scroll bar.
            /// The hook procedure monitors these messages for all applications in the same desktop as the calling thread.
            /// For more information, see the SysMsgProc hook procedure.
            /// </summary>
            WH_SYSMSGFILTER = 6,

            /// <summary>
            /// Installs a hook procedure that monitors mouse messages.
            /// For more information, see the MouseProc hook procedure.
            /// </summary>
            WH_MOUSE = 7,

            /// <summary>
            /// Installs a hook procedure useful for debugging other hook procedures.
            /// For more information, see the DebugProc hook procedure.
            /// </summary>
            WH_DEBUG = 9,

            /// <summary>
            /// Installs a hook procedure that receives notifications useful to shell applications.
            /// For more information, see the ShellProc hook procedure.
            /// </summary>
            WH_SHELL = 10,

            /// <summary>
            /// Installs a hook procedure that will be called when the application's foreground thread is about to become idle. This hook is useful for performing low priority tasks during idle time.
            /// For more information, see the ForegroundIdleProc hook procedure.
            /// </summary>
            WH_FOREGROUNDIDLE = 11,

            /// <summary>
            /// Installs a hook procedure that monitors messages after they have been processed by the destination window procedure.
            /// For more information, see the CallWndRetProc hook procedure.
            /// </summary>
            WH_CALLWNDPROCRET = 12,

            /// <summary>
            /// Installs a hook procedure that monitors low-level keyboard input events.
            /// For more information, see the LowLevelKeyboardProc hook procedure.
            /// </summary>
            WH_KEYBOARD_LL = 13,

            /// <summary>
            /// Installs a hook procedure that monitors low-level mouse input events.
            /// For more information, see the LowLevelMouseProc hook procedure.
            /// </summary>
            WH_MOUSE_LL = 14
        }

        public delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEHOOKSTRUCT
        {
            public POINT pt;

            public IntPtr hwnd;

            [MarshalAs(UnmanagedType.U4)]
            public int wHitTestCode;

            [MarshalAs(UnmanagedType.SysUInt)]
            public IntPtr dwExtraInfo;
        }

        #endregion

// ReSharper restore FieldCanBeMadeReadOnly.Global
// ReSharper restore MemberCanBePrivate.Global
// ReSharper restore InconsistentNaming
    }
}