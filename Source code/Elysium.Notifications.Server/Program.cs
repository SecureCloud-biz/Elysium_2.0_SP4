using System.ServiceProcess;

namespace Elysium.Notifications.Server
{
    internal static class Program
    {
        private static void Main()
        {
            ServiceBase.Run(new ServiceBase[]
                                {
                                    new NotificationsService()
                                });
        }
    }
} ;