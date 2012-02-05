using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Windows.Media;

using Elysium.Theme.ViewModels;

using JetBrains.Annotations;

namespace Elysium.Theme
{
    [PublicAPI]
    public sealed class ThemeManager : ViewModelBase
    {
        [PublicAPI]
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "ThemeManager is singleton")]
        public static readonly ThemeManager Instance = new ThemeManager();

        private ThemeManager()
        {
            Light(AccentColors.Blue);
        }

        [PublicAPI]
        public void Dark(Color accent)
        {
            Contract.Ensures(ThemeType == ThemeType.Dark);
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

        [PublicAPI]
        public void Light(Color accent)
        {
            Contract.Ensures(ThemeType == ThemeType.Light);
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

        [PublicAPI]
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

        [PublicAPI]
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

        [PublicAPI]
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

        [PublicAPI]
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

        [PublicAPI]
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

        [PublicAPI]
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

        [PublicAPI]
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

        [PublicAPI]
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

        [PublicAPI]
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

        [PublicAPI]
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

        [PublicAPI]
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