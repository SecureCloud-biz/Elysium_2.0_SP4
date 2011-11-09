using System;
using System.Security;

namespace Elysium.Platform.Interop
{
    [SecurityCritical]
    internal static class Windows
    {
        internal static bool IsWindowsXP
        {
            get { return Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor == 1; }
        }

        internal static bool IsWindowsVista
        {
            get { return Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 0; }
        }

        internal static bool IsWindows7
        {
            get { return Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 1; }
        }
    }
} ;