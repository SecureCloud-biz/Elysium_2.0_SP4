using System;
using System.Collections.Generic;
using System.Security;
using System.Security.AccessControl;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using JetBrains.Annotations;

using Microsoft.Win32;

namespace Elysium.NativeExtensions
{
    [PublicAPI]
    public static class Desktop
    {
        [SecurityCritical]
        private static bool _isCacheValid;

        [PublicAPI]
        [SecuritySafeCritical]
        public static void Invalidate()
        {
            _isCacheValid = false;
        }

        [SecurityCritical]
        private static void InvalidateInternal()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ReadKey))
            {
                if (key == null)
                {
                    throw new KeyNotFoundException();
                }

                var path = key.GetValue("Wallpaper").ToString();
                _wallpaper = new BitmapImage(new Uri(path, UriKind.Absolute));

                var wallpaperStyle = key.GetValue("WallpaperStyle").ToString();
                var tileWallpaper = key.GetValue("TileWallpaper").ToString();
                switch (wallpaperStyle)
                {
                    case "0":
                        switch (tileWallpaper)
                        {
                            case "0":
                                _wallpaperPosition = WallpaperPosition.Center;
                                break;
                            case "1":
                                _wallpaperPosition = WallpaperPosition.Tile;
                                break;
                        }
                        break;
                    case "2":
                        _wallpaperPosition = WallpaperPosition.Stretch;
                        break;
                    case "6":
                        _wallpaperPosition = WallpaperPosition.Fit;
                        break;
                    case "10":
                        _wallpaperPosition = WallpaperPosition.Fill;
                        break;
                }
            }

            _isCacheValid = true;
        }

        [PublicAPI]
        public static ImageSource Wallpaper
        {
            [SecuritySafeCritical]
            get
            {
                if (!_isCacheValid)
                {
                    InvalidateInternal();
                }
                return _wallpaper;
            }
        }

        private static ImageSource _wallpaper;

        [PublicAPI]
        public static WallpaperPosition WallpaperPosition
        {
            [SecuritySafeCritical]
            get
            {
                if (!_isCacheValid)
                {
                    InvalidateInternal();
                }
                return _wallpaperPosition;
            }
        }

        private static WallpaperPosition _wallpaperPosition;
    }
} ;