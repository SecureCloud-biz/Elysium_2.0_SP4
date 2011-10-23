using System.Windows.Interop;
using Elysium.Platform.Interop;
using Elysium.Theme;

namespace Elysium.Platform
{
    public sealed partial class MainWindow
    {
        private readonly WindowInteropHelper _helper;

        public MainWindow()
        {
            InitializeComponent();

            _helper = new WindowInteropHelper(this);
            _helper.EnsureHandle();

            Loaded += (sender, e) =>
                          {
                              Window.RemoveFromAltTab(_helper.Handle);
                              Window.RemoveFromAeroPeek(_helper.Handle);
                              Window.RemoveFromFlip3D(_helper.Handle);
                          };

            Activated += (sender, e) => Window.SetFullScreenAndBottomMost(_helper.Handle);

            ThemeManager.Instance.Dark(AccentColors.Blue);
        }
    }
} ;