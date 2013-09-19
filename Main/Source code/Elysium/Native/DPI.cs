using System;
using System.Security;
using System.Windows;
using System.Windows.Media;

using JetBrains.Annotations;

namespace Elysium.Native
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public static class DPI
    {
        private static Matrix _transformToDevice;
        private static Matrix _transformToDip;

        [SecuritySafeCritical]
        public static void Invalidate()
        {
            InvalidateInternal();
        }

        [SecurityCritical]
        private static void InvalidateInternal()
        {
            var desktopHandle = Interop.GetDC(IntPtr.Zero);

            var pixelsPerInchX = Interop.GetDeviceCaps(desktopHandle, NativeMethods.LOGPIXELSX);
            var pixelsPerInchY = Interop.GetDeviceCaps(desktopHandle, NativeMethods.LOGPIXELSY);

            _transformToDip = Matrix.Identity;
            _transformToDip.Scale(96d / pixelsPerInchX, 96d / pixelsPerInchY);
            _transformToDevice = Matrix.Identity;
            _transformToDevice.Scale(pixelsPerInchX / 96d, pixelsPerInchY / 96d);
            Interop.ReleaseDC(IntPtr.Zero, desktopHandle);
        }

        public static Point LogicalPixelsToDevice(Point logicalPoint)
        {
            return _transformToDevice.Transform(logicalPoint);
        }

        public static Point DevicePixelsToLogical(Point devicePoint)
        {
            return _transformToDip.Transform(devicePoint);
        }

        public static Rect LogicalRectToDevice(Rect logicalRectangle)
        {
            var topLeft = LogicalPixelsToDevice(new Point(logicalRectangle.Left, logicalRectangle.Top));
            var bottomRight = LogicalPixelsToDevice(new Point(logicalRectangle.Right, logicalRectangle.Bottom));
            return new Rect(topLeft, bottomRight);
        }

        public static Rect DeviceRectToLogical(Rect deviceRectangle)
        {
            var topLeft = DevicePixelsToLogical(new Point(deviceRectangle.Left, deviceRectangle.Top));
            var bottomRight = DevicePixelsToLogical(new Point(deviceRectangle.Right, deviceRectangle.Bottom));
            return new Rect(topLeft, bottomRight);
        }

        public static Size LogicalSizeToDevice(Size logicalSize)
        {
            var point = LogicalPixelsToDevice(new Point(logicalSize.Width, logicalSize.Height));
            return new Size(point.X, point.Y);
        }

        public static Size DeviceSizeToLogical(Size deviceSize)
        {
            var point = DevicePixelsToLogical(new Point(deviceSize.Width, deviceSize.Height));
            return new Size(point.X, point.Y);
        }
    }
}