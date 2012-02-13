using System.Windows.Media;

using JetBrains.Annotations;

namespace Elysium.Theme
{
    [PublicAPI]
    public static class AccentColors
    {
        [PublicAPI]
        public static readonly Color Blue = Color.FromArgb(0xFF, 0x1B, 0xA1, 0xE2);

        [PublicAPI]
        public static readonly Color Brown = Color.FromArgb(0xFF, 0xA0, 0x50, 0x00);

        [PublicAPI]
        public static readonly Color Green = Color.FromArgb(0xFF, 0x33, 0x99, 0x33);

        [PublicAPI]
        public static readonly Color Lime = Color.FromArgb(0xFF, 0x8C, 0xBF, 0x26);

        [PublicAPI]
        public static readonly Color Magenta = Color.FromArgb(0xFF, 0xFF, 0x00, 0x97);

        [PublicAPI]
        public static readonly Color Orange = Color.FromArgb(0xFF, 0xF0, 0x96, 0x09);

        [PublicAPI]
        public static readonly Color Pink = Color.FromArgb(0xFF, 0xE6, 0x71, 0xB8);

        [PublicAPI]
        public static readonly Color Purple = Color.FromArgb(0xFF, 0xA2, 0x00, 0xFF);

        [PublicAPI]
        public static readonly Color Red = Color.FromArgb(0xFF, 0xE5, 0x14, 0x00);

        [PublicAPI]
        public static readonly Color Viridian = Color.FromArgb(0xFF, 0x00, 0xAB, 0xA9);
    }

    [PublicAPI]
    public static class DarkColors
    {
        [PublicAPI]
        public static readonly Color Transparent = Color.FromArgb(0x00, 0x11, 0x11, 0x011);

        [PublicAPI]
        public static readonly Color Semitransparent = Color.FromArgb(0xAA, 0x11, 0x11, 0x11);

        [PublicAPI]
        public static readonly Color Background = Color.FromArgb(0xFF, 0x11, 0x11, 0x11);

        [PublicAPI]
        public static readonly Color Foreground = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);

        [PublicAPI]
        public static readonly Color Contrast = Color.FromArgb(0xFF, 0x11, 0x11, 0x11);

        [PublicAPI]
        public static readonly Color Highlight = Color.FromArgb(0xFF, 0x33, 0x33, 0x33);

        [PublicAPI]
        public static readonly Color MiddleLight = Color.FromArgb(0xFF, 0x99, 0x99, 0x99);

        [PublicAPI]
        public static readonly Color Lowlight = Color.FromArgb(0xFF, 0xCC, 0xCC, 0xCC);

        [PublicAPI]
        public static readonly Color Disabled = Color.FromArgb(0xFF, 0x6C, 0x69, 0x66);
    }

    [PublicAPI]
    public static class LightColors
    {
        [PublicAPI]
        public static readonly Color Transparent = Color.FromArgb(0x00, 0xFF, 0xFF, 0xFF);

        [PublicAPI]
        public static readonly Color Semitransparent = Color.FromArgb(0xAA, 0xFF, 0xFF, 0xFF);

        [PublicAPI]
        public static readonly Color Background = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);

        [PublicAPI]
        public static readonly Color Foreground = Color.FromArgb(0xFF, 0x00, 0x00, 0x00);

        [PublicAPI]
        public static readonly Color Contrast = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);

        [PublicAPI]
        public static readonly Color Highlight = Color.FromArgb(0xFF, 0xE2, 0xE2, 0xE2);

        [PublicAPI]
        public static readonly Color MiddleLight = Color.FromArgb(0xFF, 0x77, 0x77, 0x77);

        [PublicAPI]
        public static readonly Color Lowlight = Color.FromArgb(0xFF, 0x4D, 0x4D, 0x4D);

        [PublicAPI]
        public static readonly Color Disabled = Color.FromArgb(0xFF, 0xB8, 0xB5, 0xB2);
    }
} ;