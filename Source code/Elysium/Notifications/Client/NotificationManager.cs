using System;
using System.ServiceModel;
using System.Windows;

using JetBrains.Annotations;

namespace Elysium.Notifications.Client
{
    [UsedImplicitly]
    internal class NotificationManager : ClientBase<INotificationManager>, INotificationManager
    {
        [UsedImplicitly]
        public NotificationManager() : base(new NetNamedPipeBinding(NetNamedPipeSecurityMode.None)
                                                {
                                                    OpenTimeout = TimeSpan.FromSeconds(2d),
                                                    SendTimeout = TimeSpan.FromSeconds(2d),
                                                    ReceiveTimeout = TimeSpan.FromSeconds(2d),
                                                    CloseTimeout = TimeSpan.FromSeconds(2d),
                                                    TransactionFlow = false,
                                                    TransferMode = TransferMode.Buffered,
                                                    TransactionProtocol = TransactionProtocol.OleTransactions,
                                                    HostNameComparisonMode = HostNameComparisonMode.Exact,
                                                    MaxBufferPoolSize = 2147483520,
                                                    MaxBufferSize = 16777215,
                                                    MaxConnections = 128,
                                                    MaxReceivedMessageSize = 16777215
                                                },
                                            new EndpointAddress("net.pipe://localhost/elysium/notifications"))
        {

        }

        [UsedImplicitly]
        public Notification Reserve(Rect workArea)
        {
            return Channel.Reserve(workArea);
        }

        [UsedImplicitly]
        public void Update(TimeSpan lifetime, Animation animation, Thickness margin, Size size,
                           HorizontalPlacement horizontalPlacement, VerticalPlacement verticalPlacement)
        {
            Channel.Update(lifetime, animation, margin, size, horizontalPlacement, verticalPlacement);
        }

        [UsedImplicitly]
        public void Free(byte id)
        {
            Channel.Free(id);
        }
    }
} ;