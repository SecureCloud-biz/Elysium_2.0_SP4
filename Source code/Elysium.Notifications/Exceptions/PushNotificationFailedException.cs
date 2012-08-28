using System;
using System.Runtime.Serialization;

using JetBrains.Annotations;

namespace Elysium.Notifications
{
    [PublicAPI]
    [Serializable]
    public class PushNotificationFailedException : Exception
    {
        [PublicAPI]
        public PushNotificationFailedException()
        {
        }

        [PublicAPI]
        public PushNotificationFailedException(string message)
            : base(message)
        {
        }

        [PublicAPI]
        public PushNotificationFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        [PublicAPI]
        protected PushNotificationFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}