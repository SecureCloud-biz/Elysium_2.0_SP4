using System;
using System.Security;

namespace Elysium.Platform.Interop
{
    [SecuritySafeCritical]
    public static class Windows
    {
        public static bool IsWindowsXP
        {
            get { return Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor == 1; }
        }

        public static bool IsWindowsVista
        {
            get { return Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 0; }
        }

        public static bool IsWindows7
        {
            get { return Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 1; }
        }
    }
} ;