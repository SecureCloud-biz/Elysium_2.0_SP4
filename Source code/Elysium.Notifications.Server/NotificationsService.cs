using System.Diagnostics;
using System.ServiceModel;
using System.ServiceProcess;

namespace Elysium.Notifications.Server
{
    internal sealed partial class NotificationsService : ServiceBase
    {
        private ServiceHost _serviceHost;

        [Conditional("DEBUG")]
        private static void Debug()
        {
            Debugger.Launch();
        }

        internal NotificationsService()
        {
            //Debug();
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
            }
            _serviceHost = new ServiceHost(typeof(NotificationManager));
            _serviceHost.Open();
        }

        protected override void OnStop()
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
                _serviceHost = null;
            }
        }
    }
} ;