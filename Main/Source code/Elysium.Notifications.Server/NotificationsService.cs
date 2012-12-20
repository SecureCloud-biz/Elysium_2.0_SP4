using System;
using System.Diagnostics;
using System.Security;
using System.ServiceModel;
using System.ServiceProcess;
using System.Threading;

namespace Elysium.Notifications.Server
{
    internal sealed partial class NotificationsService : ServiceBase
    {
        [SecurityCritical]
        private ServiceHost _serviceHost;

        [Conditional("DEBUG")]
        private static void Debug()
        {
            Thread.Sleep(TimeSpan.FromSeconds(15));
        }

        internal NotificationsService()
        {
            Debug();
            InitializeComponent();
        }

        [SecurityCritical]
        protected override void OnStart(string[] args)
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
            }
            _serviceHost = new ServiceHost(typeof(NotificationManager));
            _serviceHost.Open();
        }

        [SecurityCritical]
        protected override void OnStop()
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
                _serviceHost = null;
            }
        }
    }
}
