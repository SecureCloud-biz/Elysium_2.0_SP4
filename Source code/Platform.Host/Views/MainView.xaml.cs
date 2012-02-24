using System;
using System.Windows.Interop;

using Elysium.NativeExtensions;

namespace Elysium.Platform.Views
{
    public sealed partial class MainView
    {
        private readonly WindowInteropHelper _helper;

        public MainView()
        {
            InitializeComponent();

            _helper = new WindowInteropHelper(this);
            _helper.EnsureHandle();

            Loaded += (sender, e) =>
            {
                Window.RemoveFromAltTab(_helper);
                Window.RemoveFromAeroPeek(_helper);
                Window.RemoveFromFlip3D(_helper);

                Views.Locator.AccessRequest.Demand();
                //System.Windows.MessageBox.Show("a");
            };

            Activated += (sender, e) => Window.SetFullScreenAndBottomMost(_helper);
        }
    }
} ;