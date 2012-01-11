using System.Security;
using System.Windows.Interop;
using Elysium.Platform.Interop;

namespace Elysium.Platform.Views
{
    public sealed partial class Main
    {
        private readonly WindowInteropHelper _helper;

        [SecurityCritical]
        public Main()
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
        }
    }
} ;