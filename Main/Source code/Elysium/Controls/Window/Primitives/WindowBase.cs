// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowBase.cs" company="Aleksandr Vishnyakov & Codeplex community">
//   Copyright (c) 2013 Aleksandr Vishnyakov
//
//   Microsoft Public License (Ms-PL)
//   
//   This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.
//   
//   1. Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//   
//   2. Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license
//   to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its
//   licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
//   
//   3. Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of
//   the software in compiled or object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which
//   this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
//   
//   Based on RibbonWindow class from Fluent Ribbon Control Suite (http://fluent.codeplex.com/), commit dccdd67e2c5b, Jul 31, 2013.
//   Copyright © Degtyarev Daniel, Rikker Serg. 2009-2010. All rights reserved.
//   Distributed under the terms of the Microsoft Public License (Ms-PL).
//   The license is available online (http://fluent.codeplex.com/license)
//   
//   Based on WindowChrome class from WPF Shell Integration Library (http://archive.msdn.microsoft.com/WPFShell/)
//   Copyright © Microsoft Corporation. All Rights Reserved.
//   Distributed under the terms of the Microsoft Public License (Ms-PL).
//   The license is available online (http://archive.msdn.microsoft.com/WPFShell/Project/License.aspx)
// </copyright>
// <summary>
//   Defines the ObservableDictionary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;

using Elysium.Extensions;
using Elysium.Native;

namespace Elysium.Controls.Primitives
{
    [TemplatePart(Name = LayoutRootName, Type = typeof(Panel))]
    [TemplatePart(Name = CaptionName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = IconName, Type = typeof(Image))]
    [TemplatePart(Name = TitleName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = IconBarHostName, Type = typeof(Decorator))]
    [TemplatePart(Name = TitleBarHostName, Type = typeof(Decorator))]
    [TemplatePart(Name = MinimizeName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = MaximizeName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = RestoreName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = CloseName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ResizeGripName, Type = typeof(ResizeGrip))]
    public abstract class WindowBase : System.Windows.Window
    {
        #region Private consts

        private const string LayoutRootName = "PART_LayoutRoot";
        private const string CaptionName = "PART_Caption";
        private const string IconName = "PART_Icon";
        private const string TitleName = "PART_Title";
        private const string IconBarHostName = "PART_IconBarHost";
        private const string TitleBarHostName = "PART_TitleBarHost";
        private const string MinimizeName = "PART_Minimize";
        private const string MaximizeName = "PART_Maximize";
        private const string RestoreName = "PART_Restore";
        private const string CloseName = "PART_Close";
        private const string ResizeGripName = "PART_ResizeGrip";

        #endregion

        #region Parts

        private Panel _layoutRoot;
        private Image _icon;
        private ResizeGrip _resizeGrip;

        #endregion

        #region Handles

        private Native.Monitor _monitor;

        [SecurityCritical]
        private IntPtr _handle;

        [SecurityCritical]
        private HwndSource _hwndSource;

        private const NativeMethods.WindowPosition WindowPosition = NativeMethods.WindowPosition.SWP_FRAMECHANGED | NativeMethods.WindowPosition.SWP_NOMOVE | NativeMethods.WindowPosition.SWP_NOSIZE |
                                                                    NativeMethods.WindowPosition.SWP_NOACTIVATE | NativeMethods.WindowPosition.SWP_NOOWNERZORDER | NativeMethods.WindowPosition.SWP_NOZORDER;

        private static readonly NativeMethods.HitTest[,] HitTestBorders =
        {
            { NativeMethods.HitTest.HTTOPLEFT, NativeMethods.HitTest.HTTOP, NativeMethods.HitTest.HTTOPRIGHT },
            { NativeMethods.HitTest.HTLEFT, NativeMethods.HitTest.HTCLIENT, NativeMethods.HitTest.HTRIGHT },
            { NativeMethods.HitTest.HTBOTTOMLEFT, NativeMethods.HitTest.HTBOTTOM, NativeMethods.HitTest.HTBOTTOMRIGHT }
        };

        private bool _isHooked;

        [SecurityCritical]
        private IntPtr _mouseHook;

        private NativeMethods.HookProc _mouseProc;

        #endregion

        #region Fixes

        // Field to track attempts to force off Device Bitmaps on Windows 7.
        private int _blackGlassFixupAttemptCount;

        #endregion

        #region States

        // Keep track of this so we can detect when we need to apply changes.  Tracking these separately
        // as I've seen using just one cause things to get enough out of sync that occasionally the caption will redraw.
        private WindowState _lastWindowStateAtSystemMenu;
        private WindowState _lastWindowStateAtRegion;

        #endregion

        #region Constructors

        static WindowBase()
        {
            CommandManager.RegisterClassCommandBinding(typeof(WindowBase), new CommandBinding(WindowCommands.MinimizeCommand, OnMinimizeCommandExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(WindowBase), new CommandBinding(WindowCommands.MaximizeCommand, OnMaximizeCommandExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(WindowBase), new CommandBinding(WindowCommands.RestoreCommand, OnRestoreCommandExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(WindowBase), new CommandBinding(WindowCommands.CloseCommand, OnCloseCommandExecuted));
        }

        #endregion

        #region Initialization

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            Initialize();
        }

        [SecurityCritical]
        private void Initialize()
        {
            _handle = new WindowInteropHelper(this).EnsureHandle();
            _hwndSource = HwndSource.FromHwnd(_handle);
            _monitor = new Native.Monitor(_handle);

            SecurityHelper.DemandUnmanagedCode();
            IsDesktopCompositionEnabled = Interop.IsDesktopCompositionEnabled();

            if (DesignerProperties.GetIsInDesignMode(this) == false)
            {
                ApplyCustomChrome();
            }
        }

        #endregion

        #region Overrides of Window

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (Template != null)
            {
                _layoutRoot = Template.FindName(LayoutRootName, this) as Panel;
                if (_layoutRoot == null)
                {
                    Trace.TraceError(LayoutRootName + " not found.");
                }

                if (_icon != null)
                {
                    _icon.MouseDown -= IconMouseDown;
                    _icon.MouseUp -= IconMouseUp;
                }

                // TODO: Are you need to assume contract analyzer warning?
                _icon = Template.FindName(IconName, this) as Image;
                if (_icon != null)
                {
                    _icon.MouseUp += IconMouseUp;
                    _icon.MouseDown += IconMouseDown;
                }
                else
                {
                    Trace.TraceError(IconName + " not found.");
                }

                _resizeGrip = Template.FindName(ResizeGripName, this) as ResizeGrip;
                if (_resizeGrip == null)
                {
                    Trace.TraceError(ResizeGripName + " not found.");
                }
            }
        }

        private void IconMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                Close();
            }
        }

        private void IconMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (e.ClickCount == 1)
                {
                    var systemMenuPosition = _icon.PointToScreen(new Point(0, 0));
                    var iconSize = new Size(_icon.ActualWidth, _icon.ActualHeight);
                    iconSize = DPI.LogicalSizeToDevice(iconSize);

                    if (FlowDirection == FlowDirection.LeftToRight)
                    {
                        ShowSystemMenu(new Point(systemMenuPosition.X, systemMenuPosition.Y + iconSize.Height));
                    }
                    else
                    {
                        // BUG: with incorrect coordinate detection when system menu is opened on RightToLeft FlowDirection so for correct double click we need to pause system menu opening
                        var timer = new DispatcherTimer(DispatcherPriority.SystemIdle) { Interval = TimeSpan.FromSeconds(0.1) };
                        timer.Tick += (s, ee) =>
                        {
                            ShowSystemMenu(new Point(systemMenuPosition.X, systemMenuPosition.Y + iconSize.Height));
                            ((DispatcherTimer)s).Stop();
                        };
                        timer.Start();
                    }
                }
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                ShowSystemMenu(_icon.PointToScreen(Mouse.GetPosition(_icon)));
            }
        }

        #endregion

        #region Finalization

        [SecuritySafeCritical]
        protected override void OnClosed(EventArgs e)
        {
            Release();
            base.OnClosed(e);
        }

        [SecurityCritical]
        [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
        private void Release()
        {
            Interop.UnhookWindowsHookEx(_mouseHook);
        }

        #endregion

        #region Non-Client Area

        private static readonly DependencyPropertyKey IsDesktopCompositionEnabledPropertyKey =
            DependencyProperty.RegisterReadOnly("IsDesktopCompositionEnabled", typeof(bool), typeof(WindowBase),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty IsDesktopCompositionEnabledProperty = IsDesktopCompositionEnabledPropertyKey.DependencyProperty;

        public bool IsDesktopCompositionEnabled
        {
            get { return BooleanBoxingHelper.Unbox(GetValue(IsDesktopCompositionEnabledProperty)); }
            private set { SetValue(IsDesktopCompositionEnabledPropertyKey, BooleanBoxingHelper.Box(value)); }
        }

        private static void OnIsDesktopCompositionEnabledChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var instance = (WindowBase)obj;
            instance.OnIsDesktopCompositionEnabledChanged(BooleanBoxingHelper.Unbox(e.OldValue), BooleanBoxingHelper.Unbox(e.NewValue));
        }

        protected virtual void OnIsDesktopCompositionEnabledChanged(bool oldIsDesktopCompositionEnabled, bool newIsDesktopCompositionEnabled)
        {
            UpdateFrameState(false);
        }

        private static readonly DependencyPropertyKey IsNonClientAreaActivePropertyKey =
            DependencyProperty.RegisterReadOnly("IsNonClientAreaActive", typeof(bool), typeof(WindowBase), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty IsNonClientAreaActiveProperty = IsNonClientAreaActivePropertyKey.DependencyProperty;

        public bool IsNonClientAreaActive
        {
            get { return BooleanBoxingHelper.Unbox(GetValue(IsNonClientAreaActiveProperty)); }
            private set { SetValue(IsNonClientAreaActivePropertyKey, BooleanBoxingHelper.Box(value)); }
        }

        public static readonly DependencyProperty GlassFrameThicknessProperty =
            DependencyProperty.Register("GlassFrameThickness", typeof(Thickness), typeof(WindowBase),
                new FrameworkPropertyMetadata(new Thickness(9, 29, 9, 9),
                    FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnGlassFrameThicknessChanged));

        public Thickness GlassFrameThickness
        {
            get { return BoxingHelper<Thickness>.Unbox(GetValue(GlassFrameThicknessProperty)); }
            set { SetValue(GlassFrameThicknessProperty, value); }
        }

        private static void OnGlassFrameThicknessChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var instance = (WindowBase)obj;
            instance.OnGlassFrameThicknessChanged(BoxingHelper<Thickness>.Unbox(e.OldValue), BoxingHelper<Thickness>.Unbox(e.NewValue));
        }

        protected virtual void OnGlassFrameThicknessChanged(Thickness oldGlassFrameThickness, Thickness newGlassFrameThickness)
        {
            UpdateFrameState(false);
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(WindowBase), new FrameworkPropertyMetadata(new CornerRadius(9, 9, 9, 9),
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public CornerRadius CornerRadius
        {
            get { return BoxingHelper<CornerRadius>.Unbox(GetValue(CornerRadiusProperty)); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CaptionHeightProperty =
            DependencyProperty.Register("CaptionHeight", typeof(double), typeof(WindowBase), new FrameworkPropertyMetadata(20.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnCaptionHeightChanged));

        public double CaptionHeight
        {
            get { return (double)GetValue(CaptionHeightProperty); }
            set { SetValue(CaptionHeightProperty, value); }
        }

        private static void OnCaptionHeightChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var instance = (WindowBase)obj;
            instance.OnCaptionHeightChanged(BoxingHelper<double>.Unbox(e.OldValue), BoxingHelper<double>.Unbox(e.NewValue));
        }

        protected virtual void OnCaptionHeightChanged(double oldCaptionHeight, double newCaptionHeight)
        {
            UpdateFrameState(false);
        }

        public static readonly DependencyProperty ResizeBorderThicknessProperty =
            DependencyProperty.Register("ResizeBorderThickness", typeof(Thickness), typeof(WindowBase), new FrameworkPropertyMetadata(new Thickness(9), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Thickness ResizeBorderThickness
        {
            get { return BoxingHelper<Thickness>.Unbox(GetValue(ResizeBorderThicknessProperty)); }
            set { SetValue(ResizeBorderThicknessProperty, value); }
        }

        #region Chrome

        [SecurityCritical]
        [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
        private void ApplyCustomChrome()
        {
            if (!_isHooked)
            {
                _hwndSource.AddHook(WndProc);
                _mouseProc = MouseWndProc;
                _mouseHook = Interop.SetWindowsHookEx(NativeMethods.HookType.WH_MOUSE, _mouseProc, IntPtr.Zero, Thread.CurrentThread.ManagedThreadId);
                _isHooked = true;
            }

            // Force this the first time.
            UpdateSystemMenu(WindowState);
            UpdateFrameState(true);

            Interop.SetWindowPos(_handle, NativeMethods.WindowZOrder.HWND_TOP, 0, 0, 0, 0, WindowPosition);
        }

        [SecurityCritical]
        [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
        private void UpdateFrameState(bool force)
        {
            if (_handle == IntPtr.Zero)
            {
                return;
            }

            if (!IsDesktopCompositionEnabled)
            {
                SetRoundingRegion(null);
            }
            else
            {
                ClearRoundingRegion();
                ExtendGlassFrame();

                FixWindows7Issues();
            }

            Interop.SetWindowPos(_handle, NativeMethods.WindowZOrder.HWND_TOP, 0, 0, 0, 0, WindowPosition);
            UpdateLayout();
        }

        [SecurityCritical]
        [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
        private void ClearRoundingRegion()
        {
            var hrgn = IntPtr.Zero;
            Interop.SetWindowRgn(_handle, ref hrgn, Interop.IsWindowVisible(_handle));
        }

        [SecurityCritical]
        [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
        private void SetRoundingRegion(NativeMethods.WINDOWPOS? wp)
        {
            var wpl = new NativeMethods.WINDOWPLACEMENT();
            SafeNativeMethods.GetWindowPlacement(_handle, ref wpl);

            if (wpl.showCmd == NativeMethods.WindowState.SW_SHOWMAXIMIZED && IsDesktopCompositionEnabled)
            {
                int left;
                int top;

                if (wp.HasValue)
                {
                    left = wp.Value.x;
                    top = wp.Value.y;
                }
                else
                {
                    NativeMethods.RECT rect;
                    Interop.GetWindowRect(_handle, out rect);
                    left = rect.left;
                    top = rect.top;
                }

                _monitor.Invalidate();
                var rcMax = _monitor.WorkArea;

                // The location of maximized window takes into account the border that Windows was
                // going to remove, so we also need to consider it.
                rcMax.left -= left;
                rcMax.right -= left;
                rcMax.top -= top;
                rcMax.bottom -= top;

                var hrgn = IntPtr.Zero;
                try
                {
                    hrgn = Interop.CreateRectRgnIndirect(ref rcMax);
                    Interop.SetWindowRgn(_handle, ref hrgn, Interop.IsWindowVisible(_handle));
                }
                finally
                {
                    Interop.DeleteObject(hrgn);
                }
            }
            else
            {
                Size windowSize;

                // Use the size if it's specified.
                if (wp != null && !wp.Value.flags.HasFlag(NativeMethods.WindowPosition.SWP_NOSIZE))
                {
                    windowSize = new Size(wp.Value.cx, wp.Value.cy);
                }
                else if (wp != null && (_lastWindowStateAtRegion == WindowState))
                {
                    return;
                }
                else
                {
                    NativeMethods.RECT nativeRect;
                    Interop.GetWindowRect(_handle, out nativeRect);
                    var rect = new Rect(nativeRect.left, nativeRect.top, nativeRect.right - nativeRect.left, nativeRect.bottom - nativeRect.top);
                    windowSize = rect.Size;
                }

                _lastWindowStateAtRegion = WindowState;

                var hrgn = IntPtr.Zero;
                try
                {
                    var shortestDimension = Math.Min(windowSize.Width, windowSize.Height);

                    double topLeftRadius = DPI.LogicalPixelsToDevice(new Point(CornerRadius.TopLeft, 0)).X;
                    topLeftRadius = Math.Min(topLeftRadius, shortestDimension / 2);

                    if (CornerRadiusUtil.IsUniform(CornerRadius))
                    {
                        // RoundedRect HRGNs require an additional pixel of padding.
                        hrgn = Interop.CreateRoundRectRgn(new Rect(windowSize), topLeftRadius);
                    }
                    else
                    {
                        // We need to combine HRGNs for each of the corners.
                        // Create one for each quadrant, but let it overlap into the two adjacent ones
                        // by the radius amount to ensure that there aren't corners etched into the middle
                        // of the window.
                        hrgn = Interop.CreateRoundRectRgn(new Rect(0, 0, windowSize.Width / 2 + topLeftRadius, windowSize.Height / 2 + topLeftRadius), topLeftRadius);

                        var topRightRadius = DPI.LogicalPixelsToDevice(new Point(CornerRadius.TopRight, 0)).X;
                        topRightRadius = Math.Min(topRightRadius, shortestDimension / 2);
                        var topRightRegionRect = new Rect(0, 0, windowSize.Width / 2 + topRightRadius, windowSize.Height / 2 + topRightRadius);
                        topRightRegionRect.Offset(windowSize.Width / 2 - topRightRadius, 0);

                        Interop.CreateAndCombineRoundRectRgn(hrgn, topRightRegionRect, topRightRadius);

                        // TODO: Why DPI was commented?
                        var bottomLeftRadius = DPI.LogicalPixelsToDevice(new Point(CornerRadius.BottomLeft, 0)).X;
                        bottomLeftRadius = Math.Min(bottomLeftRadius, shortestDimension / 2);
                        var bottomLeftRegionRect = new Rect(0, 0, windowSize.Width / 2 + bottomLeftRadius, windowSize.Height / 2 + bottomLeftRadius);
                        bottomLeftRegionRect.Offset(0, windowSize.Height / 2 - bottomLeftRadius);

                        Interop.CreateAndCombineRoundRectRgn(hrgn, bottomLeftRegionRect, bottomLeftRadius);

                        var bottomRightRadius = DPI.LogicalPixelsToDevice(new Point(CornerRadius.BottomRight, 0)).X;
                        bottomRightRadius = Math.Min(bottomRightRadius, shortestDimension / 2);
                        var bottomRightRegionRect = new Rect(0, 0, windowSize.Width / 2 + bottomRightRadius, windowSize.Height / 2 + bottomRightRadius);
                        bottomRightRegionRect.Offset(windowSize.Width / 2 - bottomRightRadius, windowSize.Height / 2 - bottomRightRadius);

                        Interop.CreateAndCombineRoundRectRgn(hrgn, bottomRightRegionRect, bottomRightRadius);
                    }

                    Interop.SetWindowRgn(_handle, ref hrgn, Interop.IsWindowVisible(_handle));
                }
                finally
                {
                    // Free the memory associated with the HRGN if it wasn't assigned to the HWND.
                    Interop.DeleteObject(hrgn);
                }
            }
        }

        [SecurityCritical]
        [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
        private void ExtendGlassFrame()
        {
            // Ensure standard HWND background painting when DWM isn't enabled.
            if (!IsDesktopCompositionEnabled)
            {
// ReSharper disable PossibleNullReferenceException
                _hwndSource.CompositionTarget.BackgroundColor = SystemColors.WindowColor;
            }
            else
            {
                // This makes the glass visible at a Win32 level so long as nothing else is covering it.
                // The Window's Background needs to be changed independent of this.

                // Apply the transparent background to the HWND
                _hwndSource.CompositionTarget.BackgroundColor = Colors.Transparent;
// ReSharper restore PossibleNullReferenceException

                // Thickness is going to be DIPs, need to convert to system coordinates.
                var deviceTopLeft = DPI.LogicalPixelsToDevice(new Point(GlassFrameThickness.Left, GlassFrameThickness.Top));
                var deviceBottomRight = DPI.LogicalPixelsToDevice(new Point(GlassFrameThickness.Right, GlassFrameThickness.Bottom));

                var dwmMargin = new NativeMethods.MARGINS((int)Math.Ceiling(deviceTopLeft.X), (int)Math.Ceiling(deviceTopLeft.Y), (int)Math.Ceiling(deviceBottomRight.X), (int)Math.Ceiling(deviceBottomRight.Y));
                UnsafeNativeMethods.DwmExtendFrameIntoClientArea(_handle, ref dwmMargin);
            }
        }

        // There was a regression in DWM in Windows 7 with regard to handling WM_NCCALCSIZE to effect custom chrome.
        // When windows with glass are maximized on a multimonitor setup the glass frame tends to turn black.
        // Also when windows are resized they tend to flicker black, sometimes staying that way until resized again.
        //
        // This appears to be a bug in DWM related to device bitmap optimizations.  At least on RTM Win7 we can
        // evoke a legacy code path that bypasses the bug by calling an esoteric DWM function.  This doesn't affect
        // the system, just the application.
        // WPF also tends to call this function anyways during animations, so we're just forcing the issue
        // consistently and a bit earlier.
        private void FixWindows7Issues()
        {
            if (!Windows.IsWindows7OrHigher || _blackGlassFixupAttemptCount > 5)
            {
                // Don't keep trying if there's an endemic problem with this.
                return;
            }

            if (IsDesktopCompositionEnabled)
            {
                ++_blackGlassFixupAttemptCount;

                bool success = false;
                try
                {
                    var dti = Interop.DwmGetCompositionTimingInfo(_handle);
                    success = dti != null;
                }
                catch (Exception)
                {
                    // We aren't sure of all the reasons this could fail.
                    // If we find new ones we should consider making the NativeMethod swallow them as well.
                    // Since we have a limited number of retries and this method isn't actually critical, just repost.

                    // Disabling this for the published code to reduce debug noise.  This will get compiled away for retail binaries anyways.
                    //Assert.Fail(e.Message);
                }

                // NativeMethods.DwmGetCompositionTimingInfo swallows E_PENDING.
                // If the call wasn't successful, try again later.
                if (!success)
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Loaded, (ThreadStart)FixWindows7Issues);
                }
                else
                {
                    // Reset this.  We will want to force this again if DWM composition changes.
                    _blackGlassFixupAttemptCount = 0;
                }
            }
        }

        private void FixClientRect(IntPtr lParam)
        {
            if (WindowState == WindowState.Maximized)
            {
                Taskbar.Invalidate();
                if (Taskbar.AutoHide)
                {
                    var ncParams = (NativeMethods.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(lParam, typeof(NativeMethods.NCCALCSIZE_PARAMS));
                    ncParams.rect0.bottom -= 9;
                    Marshal.StructureToPtr(ncParams, lParam, false);
                }
            }

            // below code should fix issue 18229, but causes the minimize, restore and close button to be unreachable when window is maximized
            ////// Fixes the client rect to render edge to edge on one display if maximized
            ////// Issue fixed with this method: 
            ////// http://fluent.codeplex.com/workitem/18229
            ////// "When maximized, client area goes 8px offscreen, killing perf on multimonitor"
            ////if (this.WindowState == WindowState.Maximized)
            ////{
            ////    var ncParams = (NativeMethods.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(lParam, typeof(NativeMethods.NCCALCSIZE_PARAMS));

            ////    ncParams.rect0.Left += 8;
            ////    ncParams.rect0.Top += 8;
            ////    ncParams.rect0.Right -= 8;
            ////    ncParams.rect0.Bottom -= 8;

            ////    Marshal.StructureToPtr(ncParams, lParam, false);

            ////    if (VisualTreeHelper.GetChildrenCount(this) != 0)
            ////    {
            ////        var rootElement = (FrameworkElement)VisualTreeHelper.GetChild(this, 0);
            ////        rootElement.Margin = new Thickness(-8);
            ////    }
            ////}
            ////else
            ////{
            ////    if (VisualTreeHelper.GetChildrenCount(this) != 0)
            ////    {
            ////        var rootElement = (FrameworkElement)VisualTreeHelper.GetChild(this, 0);
            ////        rootElement.Margin = new Thickness(0);
            ////    }
            ////}
        }

        private NativeMethods.HitTest HitTestNonClientArea(Rect windowPosition, Point mousePosition)
        {
            // Determine if hit test is for resizing, default middle (1,1).
            var uRow = 1;
            var uCol = 1;
            var onResizeBorder = false;

            // Determine if the point is at the top or bottom of the window.
            if (mousePosition.Y >= windowPosition.Top && mousePosition.Y < windowPosition.Top + ResizeBorderThickness.Top + CaptionHeight)
            {
                onResizeBorder = mousePosition.Y < (windowPosition.Top + ResizeBorderThickness.Top);
                uRow = 0; // top (caption or resize border)
            }
            else if (mousePosition.Y < windowPosition.Bottom && mousePosition.Y >= windowPosition.Bottom - (int)ResizeBorderThickness.Bottom)
            {
                uRow = 2; // bottom
            }

            // Determine if the point is at the left or right of the window.
            if (mousePosition.X >= windowPosition.Left && mousePosition.X < windowPosition.Left + (int)ResizeBorderThickness.Left)
            {
                uCol = 0; // left side
            }
            else if (mousePosition.X < windowPosition.Right && mousePosition.X >= windowPosition.Right - ResizeBorderThickness.Right)
            {
                uCol = 2; // right side
            }

            // If the cursor is in one of the top edges by the caption bar, but below the top resize border,
            // then resize left-right rather than diagonally.
            if (uRow == 0 && uCol != 1 && !onResizeBorder)
            {
                uRow = 1;
            }

            var ht = HitTestBorders[uRow, uCol];

            if (ht == NativeMethods.HitTest.HTTOP && !onResizeBorder)
            {
                ht = NativeMethods.HitTest.HTCAPTION;
            }

            return ht;
        }

        private IntPtr DoHitTest(int msg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            IntPtr lRet = IntPtr.Zero;
            handled = false;

            // Give DWM a chance at this first.
            if (IsDesktopCompositionEnabled && (Mouse.Captured == null))
            {
                // If we're on Vista, give the DWM a chance to handle the message first.
                handled = UnsafeNativeMethods.DwmDefWindowProc(_handle, msg, wParam, lParam, ref lRet);
            }

            // Handle letting the system know if we consider the mouse to be in our effective non-client area.
            // If DWM already handled this by way of DwmDefWindowProc, then respect their call.
            if (IntPtr.Zero == lRet)
            {
                var mousePosScreen = new Point(lParam.LowWord(), lParam.HiWord());
                NativeMethods.RECT wndPosition;
                Interop.GetWindowRect(_handle, out wndPosition);
                var windowPosition = new Rect(wndPosition.left, wndPosition.top, wndPosition.right - wndPosition.left, wndPosition.bottom - wndPosition.top);

                var ht = HitTestNonClientArea(DPI.DeviceRectToLogical(windowPosition), DPI.DevicePixelsToLogical(mousePosScreen));

                // Don't blindly respect HTCAPTION.
                // We want UIElements in the caption area to be actionable so run through a hittest first.
                if ((ht != NativeMethods.HitTest.HTCLIENT) && (_layoutRoot != null) && _layoutRoot.IsLoaded)
                {
                    /*int mp = lParam.ToInt32();
                    if (!mainGrid.IsVisible) return IntPtr.Zero;
                    Point ptMouse = new Point((short)(mp & 0x0000FFFF), (short)((mp >> 16) & 0x0000FFFF));
                    //ptMouse = DpiHelper.DevicePixelsToLogical(ptMouse);
                    ptMouse = mainGrid.PointFromScreen(ptMouse);*/
                    var ptMouse = _layoutRoot.PointFromScreen(mousePosScreen);

                    /*
                    Point mousePosWindow = mousePosScreen;
                    mousePosWindow.Offset(-windowPosition.X, -windowPosition.Y);*/

                    var inputElement = _layoutRoot.InputHitTest(ptMouse);
                    if (inputElement != null)
                    {
                        var frameworkElement = inputElement as FrameworkElement;
                        if ((frameworkElement != null) && (frameworkElement.Name == "PART_TitleBar"))
                        {
                            ht = NativeMethods.HitTest.HTCAPTION;
                        }
                        else if (inputElement != _layoutRoot)
                        {
                            ht = NativeMethods.HitTest.HTCLIENT;
                        }
                    }
                }

                // Check resize grip
                if (_resizeGrip != null && _resizeGrip.IsLoaded && _resizeGrip.InputHitTest(_resizeGrip.PointFromScreen(mousePosScreen)) != null)
                {
                    ht = FlowDirection == FlowDirection.LeftToRight ? NativeMethods.HitTest.HTBOTTOMRIGHT : NativeMethods.HitTest.HTBOTTOMLEFT;
                }

                handled = true;
                lRet = new IntPtr((int)ht);
            }
            return lRet;
        }

        #region Window Function

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case NativeMethods.WM_SETTEXT:
                case NativeMethods.WM_SETICON:
                    {
                        var modified = Interop.ModifyStyle(_handle, NativeMethods.WindowStyles.WS_VISIBLE, null);

                        // Setting the caption text and icon cause Windows to redraw the caption.
                        // Letting the default WndProc handle the message without the WS_VISIBLE
                        // style applied bypasses the redraw.
                        var lRet = UnsafeNativeMethods.DefWindowProc(_handle, msg, wParam, lParam);

                        // Put back the style we removed.
                        if (modified)
                        {
                            Interop.ModifyStyle(_handle, null, NativeMethods.WindowStyles.WS_VISIBLE);
                        }
                        handled = true;
                        return lRet;
                    }
                case NativeMethods.WM_NCACTIVATE:
                    {
                        // Despite MSDN's documentation of lParam not being used,
                        // calling DefWindowProc with lParam set to -1 causes Windows not to draw over the caption.

                        // Directly call DefWindowProc with a custom parameter
                        // which bypasses any other handling of the message.
                        var lRet = UnsafeNativeMethods.DefWindowProc(_handle, NativeMethods.WM_NCACTIVATE, wParam, new IntPtr(-1));
                        IsNonClientAreaActive = wParam != IntPtr.Zero;
                        handled = true;
                        return lRet;
                    }
                case NativeMethods.WM_NCCALCSIZE:
                    {
                        FixClientRect(lParam);

                        handled = true;
                        return new IntPtr((int)NativeMethods.NonClientAreaSizeCalculationOptions.WVR_REDRAW);
                    }
                case NativeMethods.WM_NCHITTEST:
                    {
                        return DoHitTest(msg, wParam, lParam, out handled);
                    }
                case NativeMethods.WM_NCRBUTTONUP:
                    {
                        // Emulate the system behavior of clicking the right mouse button over the caption area
                        // to bring up the system menu.
                        if (NativeMethods.HitTest.HTCAPTION == (NativeMethods.HitTest)wParam.ToInt32())
                        {
                            ShowSystemMenu(new Point(lParam.LowWord(), lParam.HiWord()));
                        }
                        handled = false;
                        return IntPtr.Zero;
                    }
                case NativeMethods.WM_SIZE:
                    {
                        // Force when maximized.
                        // We can tell what's happening right now, but the Window doesn't yet know it's
                        // maximized.  Not forcing this update will eventually cause the
                        // default caption to be drawn.
                        WindowState? state = null;
                        if (wParam.ToInt32() == (int)NativeMethods.WindowStateChanges.SIZE_MAXIMIZED)
                        {
                            state = WindowState.Maximized;
                        }
                        UpdateSystemMenu(state);

                        // Still let the default WndProc handle this.
                        handled = false;
                        return IntPtr.Zero;
                    }
                case NativeMethods.WM_WINDOWPOSCHANGED:
                    {
                        if (!IsDesktopCompositionEnabled)
                        {
                            var wp = (NativeMethods.WINDOWPOS)Marshal.PtrToStructure(lParam, typeof(NativeMethods.WINDOWPOS));
                            //ModifyStyle(NativeMethods.WS_VISIBLE, 0);
                            SetRoundingRegion(wp);
                            //ModifyStyle(0, NativeMethods.WS_VISIBLE);
                        }

                        // Still want to pass this to DefWndProc
                        handled = false;
                        return IntPtr.Zero;
                    }
                case NativeMethods.WM_DWMCOMPOSITIONCHANGED:
                    {
                        IsDesktopCompositionEnabled = Interop.IsDesktopCompositionEnabled();

                        UpdateFrameState(false);

                        handled = false;
                        return IntPtr.Zero;
                    }
            }

            return IntPtr.Zero;
        }

        private IntPtr MouseWndProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0 && Mouse.Captured != null && IsActive)
            {
                int msg = wParam.ToInt32();
                NativeMethods.MOUSEHOOKSTRUCT cc = (NativeMethods.MOUSEHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(NativeMethods.MOUSEHOOKSTRUCT));
                switch (msg)
                {
                    case 0x0203:
                    case 0x0201:
                    case 0x0204:
                    case 0x0206:
                        // Check popups
                        if (PopupHelper.IsInRootPopup(Mouse.Captured as DependencyObject))
                            return IntPtr.Zero;

                        //
                        IntPtr pp = Marshal.AllocHGlobal(Marshal.SizeOf(cc.pt));
                        Marshal.StructureToPtr(cc.pt, pp, false);
                        bool handled;
                        IntPtr htResult = DoHitTest(NativeMethods.WM_NCHITTEST, IntPtr.Zero, LParamUtil.ConvertFrom(cc.pt.x, cc.pt.y), out handled);
                        if (!handled)
                        {
                            htResult = UnsafeNativeMethods.DefWindowProc(_handle, NativeMethods.WM_NCHITTEST, IntPtr.Zero, LParamUtil.ConvertFrom(cc.pt.x, cc.pt.y));
                        }
                        int htR = htResult.ToInt32();
                        int ncMessage = 0x00A1;
                        if (msg == 0x0203)
                        {
                            ncMessage = 0x00A3;
                        }
                        else if ((msg == 0x0204) && ((htR == NativeMethods.HitTest.HTCAPTION) || (htR == NativeMethods.HitTest.HTTOP)))
                        {
                            NativeMethods.ReleaseCapture();
                            if (htR == NativeMethods.HitTest.HTCAPTION) ShowSystemMenu(new Point(cc.pt.x, cc.pt.y));
                            ncMessage = 0x00A4;
                        }

                        if ((htR == NativeMethods.HitTest.HTCAPTION) || (htR == NativeMethods.HitTest.HTTOP))
                        {
                            NativeMethods.ReleaseCapture();
                            htResult = DoHitTest(NativeMethods.WM_NCHITTEST, IntPtr.Zero, LParamUtil.ConvertFrom(cc.pt.x, cc.pt.y), out handled);
                            if (!handled) htResult = UnsafeNativeMethods.DefWindowProc(_handle, NativeMethods.WM_NCHITTEST, IntPtr.Zero, LParamUtil.ConvertFrom(cc.pt.x, cc.pt.y));
                            htR = htResult.ToInt32();
                        }

                        if ((htR == 3) || (htR == 8) || (htR == 9) || (htR == 20) || (htR == 21))
                        {
                            NativeMethods.ReleaseCapture();
                            UnsafeNativeMethods.SendMessage(_handle, ncMessage, htResult, pp);
                        }
                        else if (htR != NativeMethods.HTCLIENT)
                        {
                            NativeMethods.ReleaseCapture();
                            Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                                                   (ThreadStart)
                                                   (() => UnsafeNativeMethods.SendMessage(_handle, ncMessage, htResult, pp)));
                        }
                        return IntPtr.Zero;
                }
            }
            return Interop.CallNextHookEx(_mouseHook, code, wParam, lParam);
        }

        #endregion

        #endregion

        #region System menu

        [SecurityCritical]
        [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
        private void ShowSystemMenu(Point screenLocation)
        {
            if (_handle == IntPtr.Zero)
            {
                return;
            }

            var hmenu = UnsafeNativeMethods.GetSystemMenu(_handle, false);
            var cmd = Interop.TrackPopupMenuEx(hmenu, NativeMethods.PopupMenuTracks.TPM_LEFTBUTTON | NativeMethods.PopupMenuTracks.TPM_RETURNCMD, (int)screenLocation.X, (int)screenLocation.Y, _handle, IntPtr.Zero);
            Interop.PostMessage(_handle, NativeMethods.WM_SYSCOMMAND, new IntPtr(cmd), IntPtr.Zero);
        }

        private const NativeMethods.MenuItemState MenuItemEnabled = NativeMethods.MenuItemState.MF_ENABLED | NativeMethods.MenuItemState.MF_BYCOMMAND;
        private const NativeMethods.MenuItemState MenuItemDisabled = NativeMethods.MenuItemState.MF_GRAYED | NativeMethods.MenuItemState.MF_DISABLED | NativeMethods.MenuItemState.MF_BYCOMMAND;

        [SecurityCritical]
        [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
        private void UpdateSystemMenu(WindowState? assumeState)
        {
            var state = assumeState ?? Interop.GetHwndState(_handle);

            if (_lastWindowStateAtSystemMenu != state || assumeState != null)
            {
                _lastWindowStateAtSystemMenu = state;

                var isVisible = !Interop.ModifyStyle(_handle, NativeMethods.WindowStyles.WS_VISIBLE, null);

                var hmenu = UnsafeNativeMethods.GetSystemMenu(_handle, false);
                if (IntPtr.Zero != hmenu)
                {
                    var dwStyle = (NativeMethods.WindowStyles)Interop.GetWindowLongPtr(_handle, (int)NativeMethods.WindowInfo.GWL_STYLE).ToInt32();

                    var canMinimize = dwStyle.HasFlag(NativeMethods.WindowStyles.WS_MINIMIZEBOX);
                    var canMaximize = dwStyle.HasFlag(NativeMethods.WindowStyles.WS_MAXIMIZEBOX);
                    var canSize = dwStyle.HasFlag(NativeMethods.WindowStyles.WS_THICKFRAME);

                    switch (state)
                    {
                        case WindowState.Maximized:
                            Interop.EnableMenuItem(hmenu, (int)NativeMethods.SystemCommands.SC_RESTORE, MenuItemEnabled);
                            Interop.EnableMenuItem(hmenu, (int)NativeMethods.SystemCommands.SC_MOVE, MenuItemDisabled);
                            Interop.EnableMenuItem(hmenu, (int)NativeMethods.SystemCommands.SC_SIZE, MenuItemDisabled);
                            Interop.EnableMenuItem(hmenu, (int)NativeMethods.SystemCommands.SC_MINIMIZE, canMinimize ? MenuItemEnabled : MenuItemDisabled);
                            Interop.EnableMenuItem(hmenu, (int)NativeMethods.SystemCommands.SC_MAXIMIZE, MenuItemDisabled);
                            break;
                        case WindowState.Minimized:
                            Interop.EnableMenuItem(hmenu, (int)NativeMethods.SystemCommands.SC_RESTORE, MenuItemEnabled);
                            Interop.EnableMenuItem(hmenu, (int)NativeMethods.SystemCommands.SC_MOVE, MenuItemDisabled);
                            Interop.EnableMenuItem(hmenu, (int)NativeMethods.SystemCommands.SC_SIZE, MenuItemDisabled);
                            Interop.EnableMenuItem(hmenu, (int)NativeMethods.SystemCommands.SC_MINIMIZE, MenuItemDisabled);
                            Interop.EnableMenuItem(hmenu, (int)NativeMethods.SystemCommands.SC_MAXIMIZE, canMaximize ? MenuItemEnabled : MenuItemDisabled);
                            break;
                        default:
                            Interop.EnableMenuItem(hmenu, (int)NativeMethods.SystemCommands.SC_RESTORE, MenuItemDisabled);
                            Interop.EnableMenuItem(hmenu, (int)NativeMethods.SystemCommands.SC_MOVE, MenuItemEnabled);
                            Interop.EnableMenuItem(hmenu, (int)NativeMethods.SystemCommands.SC_SIZE, canSize ? MenuItemEnabled : MenuItemDisabled);
                            Interop.EnableMenuItem(hmenu, (int)NativeMethods.SystemCommands.SC_MINIMIZE, canMinimize ? MenuItemEnabled : MenuItemDisabled);
                            Interop.EnableMenuItem(hmenu, (int)NativeMethods.SystemCommands.SC_MAXIMIZE, canMaximize ? MenuItemEnabled : MenuItemDisabled);
                            break;
                    }
                }

                if (!isVisible)
                {
                    Interop.ModifyStyle(_handle, null, NativeMethods.WindowStyles.WS_VISIBLE);
                }
            }
        }

        #endregion

        #endregion

        #region Commands handles

        private static void OnCloseCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var instance = sender as WindowBase;
            if (instance != null)
            {
                instance.Close();
            }
        }

        private static void OnMaximizeCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var instance = sender as WindowBase;
            if (instance != null)
            {
                instance.WindowState = WindowState.Maximized;
            }
        }

        private static void OnRestoreCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var instance = sender as WindowBase;
            if (instance != null)
            {
                instance.WindowState = WindowState.Normal;
            }
        }

        private static void OnMinimizeCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var instance = sender as WindowBase;
            if (instance != null)
            {
                instance.WindowState = WindowState.Minimized;
            }
        }

        #endregion
    }
}