using System.Security;

using Elysium.NativeExtensions;

using JetBrains.Annotations;

namespace Elysium.Platform.Views
{
    public sealed partial class AccessRequestView
    {
        public AccessRequestView()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        [SecurityCritical]
        private new void Show()
        {
            base.Show();
        }

        [UsedImplicitly]
        [SecurityCritical]
        private new bool? ShowDialog()
        {
            return base.ShowDialog();
        }

        [SecurityCritical]
        public void Demand()
        {
            SecureDesktop.Show(this);
        }
    }
} ;