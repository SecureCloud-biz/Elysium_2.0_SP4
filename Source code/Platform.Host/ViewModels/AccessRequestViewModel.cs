using System.Windows.Input;
using System.Windows.Media;

using Elysium.NativeExtensions;
using Elysium.Platform.Permissions;
using Elysium.Theme.Commands;
using Elysium.Theme.ViewModels;

namespace Elysium.Platform.ViewModels
{
    internal class AccessRequestViewModel : ViewModelBase
    {
        public ImageBrush Background
        {
            get
            {
                Desktop.Invalidate();
                var image = new ImageBrush { ImageSource = Desktop.Wallpaper };
                switch (Desktop.WallpaperPosition)
                {
                    case WallpaperPosition.Fill:
                        image.Stretch = Stretch.UniformToFill;
                        break;
                    case WallpaperPosition.Fit:
                        image.Stretch = Stretch.Uniform;
                        break;
                    case WallpaperPosition.Center:
                        image.Stretch = Stretch.None;
                        image.AlignmentX = AlignmentX.Center;
                        image.AlignmentY = AlignmentY.Center;
                        break;
                    case WallpaperPosition.Stretch:
                        image.Stretch = Stretch.Fill;
                        break;
                    case WallpaperPosition.Tile:
                        image.Stretch = Stretch.None;
                        image.TileMode = TileMode.Tile;
                        break;
                }
                return image;
            }
        }

        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                OnPropertyChanged(() => Header);
            }
        }

        private string _header;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(() => Message);
            }
        }

        private string _message;

        public ICommand AllowCommand
        {
            get { return _allowCommand ?? (_allowCommand = new DelegateCommand(Allow)); }
        }

        private ICommand _allowCommand;

        private void Allow()
        {
            Result = PermissionState.Allow;
        }

        public ICommand DenyCommand
        {
            get { return _denyCommand ?? (_denyCommand = new DelegateCommand(Deny)); }
        }

        private ICommand _denyCommand;

        private void Deny()
        {
            Result = PermissionState.Deny;
        }

        public PermissionState Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged(() => Result);
            }
        }

        private PermissionState _result;
    }
} ;