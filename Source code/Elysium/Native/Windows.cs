using System;

using JetBrains.Annotations;

namespace Elysium.Native
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    internal static class Windows
    {
        internal static bool IsWindows2000
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform == PlatformID.Win32NT && os.Version.Major == 5 && os.Version.Minor == 0;
            }
        }

        internal static bool IsWindow2000OrHigher
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform == PlatformID.Win32NT && os.Version.Major >= 5;
            }
        }

        internal static bool IsWindowsXP
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform == PlatformID.Win32NT && os.Version.Major == 5 && (os.Version.Minor == 1 || os.Version.Minor == 2);
            }
        }

        internal static bool IsWindowXPOrHigher
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform == PlatformID.Win32NT && (IsWindowsXP || IsWindowVistaOrHigher);
            }
        }

        internal static bool IsWindowsVista
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform == PlatformID.Win32NT && os.Version.Major == 6 && os.Version.Minor == 0;
            }
        }

        internal static bool IsWindowVistaOrHigher
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform == PlatformID.Win32NT && os.Version.Major >= 6;
            }
        }

        internal static bool IsWindows7
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform == PlatformID.Win32NT && os.Version.Major == 6 && os.Version.Minor == 1;
            }
        }

        internal static bool IsWindow7OrHigher
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform == PlatformID.Win32NT && (IsWindows7 || IsWindow8OrHigher);
            }
        }

        internal static bool IsWindows8
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform == PlatformID.Win32NT && os.Version.Major == 6 && os.Version.Minor == 2;
            }
        }

        internal static bool IsWindow8OrHigher
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform == PlatformID.Win32NT && ((os.Version.Major >= 6 && os.Version.Minor >= 2) || os.Version.Major > 6);
            }
        }
    }
}