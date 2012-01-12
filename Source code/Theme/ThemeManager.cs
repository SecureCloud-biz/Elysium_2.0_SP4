using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;

using Elysium.Theme.ViewModels;

namespace Elysium.Theme
{
    public enum ThemeType
    {
        Dark,
        Light
    }

    public sealed class ThemeManager : ViewModelBase
    {
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "ThemeManager is singleton")]
        public static readonly ThemeManager Instance = new ThemeManager();

        private ThemeManager()
        {
            Light(AccentColors.Blue);
        }

        public void Dark(Color accent)
        {
            try
            {

                Accent = accent;
                Transparent = DarkColors.Transparent;
                Semitransparent = DarkColors.Semitransparent;
                Background = DarkColors.Background;
                Foreground = DarkColors.Foreground;
                Contrast = DarkColors.Contrast;
                Highlight = DarkColors.Highlight;
                MiddleLight = DarkColors.MiddleLight;
                Lowlight = DarkColors.Lowlight;
                Disabled = DarkColors.Disabled;
            }
            finally
            {
                ThemeType = ThemeType.Dark;
            }
        }

        public void Light(Color accent)
        {
            try
            {
                Accent = accent;
                Transparent = LightColors.Transparent;
                Semitransparent = LightColors.Semitransparent;
                Background = LightColors.Background;
                Foreground = LightColors.Foreground;
                Contrast = LightColors.Contrast;
                Highlight = LightColors.Highlight;
                MiddleLight = LightColors.MiddleLight;
                Lowlight = LightColors.Lowlight;
                Disabled = LightColors.Disabled;
            }
            finally
            {
                ThemeType = ThemeType.Light;
            }
        }

        public ThemeType ThemeType
        {
            get { return _themeType; }
            private set
            {
                _themeType = value;
                OnPropertyChanged(() => ThemeType);
            }
        }

        private ThemeType _themeType;

        public Color Accent
        {
            get { return _accent; }
            private set
            {
                _accent = value;
                OnPropertyChanged(() => Accent);
            }
        }

        private Color _accent;

        public Color Transparent
        {
            get { return _transparent; }
            private set
            {
                _transparent = value;
                OnPropertyChanged(() => Transparent);
            }
        }

        private Color _transparent;

        public Color Semitransparent
        {
            get { return _semitransparent; }

            private set
            {
                _semitransparent = value;
                OnPropertyChanged(() => Semitransparent);
            }
        }

        private Color _semitransparent;

        public Color Background
        {
            get { return _background; }
            private set
            {
                _background = value;
                OnPropertyChanged(() => Background);
            }
        }

        private Color _background;

        public Color Foreground
        {
            get { return _foreground; }
            private set
            {
                _foreground = value;
                OnPropertyChanged(() => Foreground);
            }
        }

        private Color _foreground;

        public Color Contrast
        {
            get { return _contrast; }
            private set
            {
                _contrast = value;
                OnPropertyChanged(() => Contrast);
            }
        }

        private Color _contrast;

        public Color Highlight
        {
            get { return _highlight; }
            private set
            {
                _highlight = value;
                OnPropertyChanged(() => Highlight);
            }
        }

        private Color _highlight;


        public Color MiddleLight
        {
            get { return _middleLight; }
            private set
            {
                _middleLight = value;
                OnPropertyChanged(() => MiddleLight);
            }
        }

        private Color _middleLight;


        public Color Lowlight
        {
            get { return _lowlight; }
            private set
            {
                _lowlight = value;
                OnPropertyChanged(() => Lowlight);
            }
        }

        private Color _lowlight;

        public Color Disabled
        {
            get { return _disabled; }
            private set
            {
                _disabled = value;
                OnPropertyChanged(() => Disabled);
            }
        }

        private Color _disabled;
    }
} ;