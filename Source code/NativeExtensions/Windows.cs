using System;

using JetBrains.Annotations;

namespace Elysium.NativeExtensions
{
    [PublicAPI]
    public static class Windows
    {
        [PublicAPI]
        public static bool IsWindows2000
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform == PlatformID.Win32NT && os.Version.Major == 5 && os.Version.Minor == 0;
            }
        }

        [PublicAPI]
        public static bool IsWindowsXP
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform == PlatformID.Win32NT && os.Version.Major == 5 && (os.Version.Minor == 1 || os.Version.Minor == 2);
            }
        }

        [PublicAPI]
        public static bool IsWindowsVista
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform == PlatformID.Win32NT && os.Version.Major == 6 && os.Version.Minor == 0;
            }
        }

        [PublicAPI]
        public static bool IsWindows7
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform == PlatformID.Win32NT && os.Version.Major == 6 && os.Version.Minor == 1;
            }
        }
    }
} ;