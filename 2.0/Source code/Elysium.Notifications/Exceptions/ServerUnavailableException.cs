using System;
using System.Runtime.Serialization;
using System.ServiceModel;

using JetBrains.Annotations;

namespace Elysium.Notifications
{
    [PublicAPI]
    [Serializable]
    public class ServerUnavailableException : CommunicationException
    {
        [PublicAPI]
        public ServerUnavailableException()
        {
        }

        [PublicAPI]
        public ServerUnavailableException(string message)
            : base(message)
        {
        }

        [PublicAPI]
        public ServerUnavailableException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        [PublicAPI]
        protected ServerUnavailableException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
