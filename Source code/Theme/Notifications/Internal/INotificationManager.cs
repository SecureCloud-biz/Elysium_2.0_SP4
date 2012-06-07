using System;
using System.ServiceModel;
using System.Windows;

using JetBrains.Annotations;

namespace Elysium.Notifications
{
    [UsedImplicitly]
    [ServiceContract(Namespace = "http://namespaces.codeplex.com/elysium/notifications", Name = "NotificationManager")]
    internal interface INotificationManager
    {
        [UsedImplicitly]
        [OperationContract]
        Notification Reserve(Rect workArea);

        [UsedImplicitly]
        [OperationContract]
        void Free(byte id);

        [UsedImplicitly]
        [OperationContract]
        void Update(TimeSpan lifetime, Animation animation, Thickness margin, Size size, HorizontalPlacement horizontalPlacement, VerticalPlacement verticalPlacement);
    }
} ;