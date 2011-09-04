using System.ComponentModel;
using System.Windows.Media;

namespace Elysium.Theme
{
    public enum Theme
    {
        Dark,
        Light
    }

    public sealed class ThemeManager : INotifyPropertyChanged
    {
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
                ForegroundContrast = DarkColors.ForegroundContrast;
                ForegroundHighlight = DarkColors.ForegroundHighlight;
                ForegroundMiddlelight = DarkColors.ForegroundMiddlelight;
                ForegroundLowlight = DarkColors.ForegroundLowlight;
                Disabled = DarkColors.Disabled;
            }
            finally
            {
                Theme = Theme.Dark;
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
                ForegroundContrast = LightColors.ForegroundContrast;
                ForegroundHighlight = LightColors.ForegroundHighlight;
                ForegroundMiddlelight = LightColors.ForegroundMiddlelight;
                ForegroundLowlight = LightColors.ForegroundLowlight;
                Disabled = LightColors.Disabled;
            }
            finally
            {
                Theme = Theme.Light;
            }
        }

        public Theme Theme
        {
            get { return _theme; }
            private set
            {
                _theme = value;
                OnPropertyChanged("Theme");
            }
        }

        private Theme _theme;

        public Color Accent
        {
            get { return _accent; }
            private set
            {
                _accent = value;
                OnPropertyChanged("Accent");
            }
        }

        private Color _accent;

        public Color Transparent
        {
            get { return _transparent; }
            private set
            {
                _transparent = value;
                OnPropertyChanged("Transparent");
            }
        }

        private Color _transparent;

        public Color Semitransparent
        {
            get { return _semitransparent; }

            private set
            {
                _semitransparent = value;
                OnPropertyChanged("Semitransparent");
            }
        }

        private Color _semitransparent;

        public Color Background
        {
            get { return _background; }
            private set
            {
                _background = value;
                OnPropertyChanged("Background");
            }
        }

        private Color _background;

        public Color Foreground
        {
            get { return _foreground; }
            private set
            {
                _foreground = value;
                OnPropertyChanged("Foreground");
            }
        }

        private Color _foreground;

        public Color ForegroundContrast
        {
            get { return _foregroundContrast; }
            private set
            {
                _foregroundContrast = value;
                OnPropertyChanged("ForegroundContrast");
            }
        }

        private Color _foregroundContrast;

        public Color ForegroundHighlight
        {
            get { return _foregroundHighlight; }
            private set
            {
                _foregroundHighlight = value;
                OnPropertyChanged("ForegroundHighlight");
            }
        }

        private Color _foregroundHighlight;


        public Color ForegroundMiddlelight
        {
            get { return _foregroundMiddlelight; }
            private set
            {
                _foregroundMiddlelight = value;
                OnPropertyChanged("ForegroundMiddlelight");
            }
        }

        private Color _foregroundMiddlelight;


        public Color ForegroundLowlight
        {
            get { return _foregroundLowlight; }
            private set
            {
                _foregroundLowlight = value;
                OnPropertyChanged("ForegroundLowlight");
            }
        }

        private Color _foregroundLowlight;

        public Color Disabled
        {
            get { return _disabled; }
            private set
            {
                _disabled = value;
                OnPropertyChanged("Disabled");
            }
        }

        private Color _disabled;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} ;