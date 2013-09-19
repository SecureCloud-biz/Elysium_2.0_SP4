using System;

using JetBrains.Annotations;

namespace Elysium.Native
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public static class Windows
    {
        public static bool IsWindows2000
        {
            get
            {
                if (!_isWindows2000.HasValue)
                {
                    var os = Environment.OSVersion;
                    _isWindows2000 = os.Platform == PlatformID.Win32NT && os.Version.Major == 5 && os.Version.Minor == 0;
                }
                return _isWindows2000.Value;
            }
        }

        private static bool? _isWindows2000;

        public static bool IsWindows2000OrHigher
        {
            get
            {
                if (!_isWindows2000OrHigher.HasValue)
                {
                    var os = Environment.OSVersion;
                    _isWindows2000OrHigher = os.Platform == PlatformID.Win32NT && os.Version.Major >= 5;
                }
                return _isWindows2000OrHigher.Value;
            }
        }

        private static bool? _isWindows2000OrHigher;

        public static bool IsWindowsXP
        {
            get
            {
                if (!_isWindowsXP.HasValue)
                {
                    var os = Environment.OSVersion;
                    _isWindowsXP = os.Platform == PlatformID.Win32NT && os.Version.Major == 5 && (os.Version.Minor == 1 || os.Version.Minor == 2);
                }
                return _isWindowsXP.Value;
            }
        }

        private static bool? _isWindowsXP;

        public static bool IsWindowsXPOrHigher
        {
            get
            {
                if (!_isWindowsXPOrHigher.HasValue)
                {
                    var os = Environment.OSVersion;
                    _isWindowsXPOrHigher = os.Platform == PlatformID.Win32NT && (IsWindowsXP || IsWindowsVistaOrHigher);
                }
                return _isWindowsXPOrHigher.Value;
            }
        }

        private static bool? _isWindowsXPOrHigher;

        public static bool IsWindowsVista
        {
            get
            {
                if (!_isWindowsVista.HasValue)
                {
                    var os = Environment.OSVersion;
                    _isWindowsVista = os.Platform == PlatformID.Win32NT && os.Version.Major == 6 && os.Version.Minor == 0;
                }
                return _isWindowsVista.Value;
            }
        }

        private static bool? _isWindowsVista;

        public static bool IsWindowsVistaOrHigher
        {
            get
            {
                if (!_isWindowsVistaOrHigher.HasValue)
                {
                    var os = Environment.OSVersion;
                    _isWindowsVistaOrHigher = os.Platform == PlatformID.Win32NT && os.Version.Major >= 6;
                }
                return _isWindowsVistaOrHigher.Value;
            }
        }

        private static bool? _isWindowsVistaOrHigher;

        public static bool IsWindows7
        {
            get
            {
                if (!_isWindows7.HasValue)
                {
                    var os = Environment.OSVersion;
                    _isWindows7 = os.Platform == PlatformID.Win32NT && os.Version.Major == 6 && os.Version.Minor == 1;
                }
                return _isWindows7.Value;
            }
        }

        private static bool? _isWindows7;

        public static bool IsWindows7OrHigher
        {
            get
            {
                if (!_isWindows7OrHigher.HasValue)
                {
                    var os = Environment.OSVersion;
                    _isWindows7OrHigher = os.Platform == PlatformID.Win32NT && (IsWindows7 || IsWindows8OrHigher);
                }
                return _isWindows7OrHigher.Value;
            }
        }

        private static bool? _isWindows7OrHigher;

        public static bool IsWindows8
        {
            get
            {
                if (!_isWindows8.HasValue)
                {
                    var os = Environment.OSVersion;
                    _isWindows8 = os.Platform == PlatformID.Win32NT && os.Version.Major == 6 && os.Version.Minor == 2;
                }
                return _isWindows8.Value;
            }
        }

        private static bool? _isWindows8;

        public static bool IsWindows8OrHigher
        {
            get
            {
                if (!_isWindows8.HasValue)
                {
                    var os = Environment.OSVersion;
                    _isWindows8 = os.Platform == PlatformID.Win32NT && ((os.Version.Major >= 6 && os.Version.Minor >= 2) || os.Version.Major > 6);
                }
                return _isWindows8.Value;
            }
        }

        private static bool? _isWindows8OrHigher;

        public static bool IsWindows81
        {
            get
            {
                if (!_isWindows81.HasValue)
                {
                    var os = Environment.OSVersion;
                    _isWindows81 = os.Platform == PlatformID.Win32NT && os.Version.Major == 6 && os.Version.Minor == 3;
                }
                return _isWindows81.Value;
            }
        }

        private static bool? _isWindows81;

        public static bool IsWindows81OrHigher
        {
            get
            {
                if (!_isWindows81.HasValue)
                {
                    var os = Environment.OSVersion;
                    _isWindows81 = os.Platform == PlatformID.Win32NT && ((os.Version.Major >= 6 && os.Version.Minor >= 3) || os.Version.Major > 6);
                }
                return _isWindows81.Value;
            }
        }

        private static bool? _isWindows81OrHigher;
    }
}