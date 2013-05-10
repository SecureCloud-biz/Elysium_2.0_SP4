using System.Windows;
using Elysium.Controls;

namespace Elysium
{
    public static class Resources
    {
        public static ResourceKey AccentBrushKey
        {
            get { return _accentBrushKey ?? (_accentBrushKey = new ComponentResourceKey(typeof(ToggleSwitch), "AccentBrush")); }
        }

        private static ResourceKey _accentBrushKey;

        public static ResourceKey ContrastBrushKey
        {
            get { return _contrastBrushKey ?? (_contrastBrushKey = new ComponentResourceKey(typeof(ToggleSwitch), "ContrastBrush")); }
        }

        private static ResourceKey _contrastBrushKey;

        public static ResourceKey SemitransparentContrastBrushKey
        {
            get { return _semitransparentContrastBrushKey ?? (_semitransparentContrastBrushKey = new ComponentResourceKey(typeof(ToggleSwitch), "SemitransparentContrastBrush")); }
        }

        private static ResourceKey _semitransparentContrastBrushKey;

        public static ResourceKey TransparentBrushKey
        {
            get { return _transparentBrushKey ?? (_transparentBrushKey = new ComponentResourceKey(typeof(ToggleSwitch), "TransparentBrush")); }
        }

        private static ResourceKey _transparentBrushKey;

        public static ResourceKey SemitransparentBrushKey
        {
            get { return _semitransparentBrushKey ?? (_semitransparentBrushKey = new ComponentResourceKey(typeof(ToggleSwitch), "SemitransparentBrush")); }
        }

        private static ResourceKey _semitransparentBrushKey;

        public static ResourceKey BackgroundBrushKey
        {
            get { return _backgroundBrushKey ?? (_backgroundBrushKey = new ComponentResourceKey(typeof(ToggleSwitch), "BackgroundBrush")); }
        }

        private static ResourceKey _backgroundBrushKey;

        public static ResourceKey ForegroundBrushKey
        {
            get { return _foregroundBrushKey ?? (_foregroundBrushKey = new ComponentResourceKey(typeof(ToggleSwitch), "ForegroundBrush")); }
        }

        private static ResourceKey _foregroundBrushKey;

        public static ResourceKey HighLightBrushKey
        {
            get { return _highLightBrushKey ?? (_highLightBrushKey = new ComponentResourceKey(typeof(ToggleSwitch), "HighLightBrush")); }
        }

        private static ResourceKey _highLightBrushKey;

        public static ResourceKey MiddleLightBrushKey
        {
            get { return _middleLightBrushKey ?? (_middleLightBrushKey = new ComponentResourceKey(typeof(ToggleSwitch), "MiddleLightBrush")); }
        }

        private static ResourceKey _middleLightBrushKey;

        public static ResourceKey LowLightBrushKey
        {
            get { return _lowLightBrushKey ?? (_lowLightBrushKey = new ComponentResourceKey(typeof(ToggleSwitch), "LowLightBrush")); }
        }

        private static ResourceKey _lowLightBrushKey;

        public static ResourceKey DisabledBrushKey
        {
            get { return _disabledBrushKey ?? (_disabledBrushKey = new ComponentResourceKey(typeof(ToggleSwitch), "DisabledBrush")); }
        }

        private static ResourceKey _disabledBrushKey;
    }
}