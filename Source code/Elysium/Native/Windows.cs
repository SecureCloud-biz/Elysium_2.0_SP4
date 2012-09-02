using System;

using JetBrains.Annotations;

namespace Elysium.Native
{
    [PublicAPI]
    internal static class Windows
    {
        [PublicAPI]
        internal static bool IsWindows2000
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform == PlatformID.Win32NT && os.Version.Major == 5 && os.Version.Minor == 0;
            }
        }

        [PublicAPI]
        internal static bool IsWindowsXP
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform == PlatformID.Win32NT && os.Version.Major == 5 && (os.Version.Minor == 1 || os.Version.Minor == 2);
            }
        }

        [PublicAPI]
        internal static bool IsWindowsVista
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform == PlatformID.Win32NT && os.Version.Major == 6 && os.Version.Minor == 0;
            }
        }

        [PublicAPI]
        internal static bool IsWindows7
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform == PlatformID.Win32NT && os.Version.Major == 6 && os.Version.Minor == 1;
            }
        }
    }
}