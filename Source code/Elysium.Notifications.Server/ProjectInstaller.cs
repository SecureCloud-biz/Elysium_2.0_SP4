using System.ComponentModel;
using System.Configuration.Install;

using JetBrains.Annotations;

namespace Elysium.Notifications.Server
{
    [UsedImplicitly]
    [RunInstaller(true)]
    public sealed partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
} ;