using System;
using System.Runtime.Serialization;
using System.Windows;

using JetBrains.Annotations;

namespace Elysium.Notifications
{
    [DataContract(Namespace = "http://namespaces.codeplex.com/elysium/notifications", Name = "Notification")]
    internal sealed class Notification
    {
        [UsedImplicitly]
        private Notification()
        {
        }

        internal Notification(byte id, TimeSpan lifetime, Animation animation, Point hiddenPosition, Point position, Size size)
        {
            _id = id;
            _lifetime = lifetime;
            _animation = animation;
            _hiddenPosition = hiddenPosition;
            _position = position;
            _size = size;
        }

        [UsedImplicitly]
        public byte ID
        {
            get { return _id; }
        }

        [DataMember(Name = "ID")]
        private readonly byte _id;

        [UsedImplicitly]
        public TimeSpan Lifetime
        {
            get { return _lifetime; }
        }

        [DataMember(Name = "Lifetime")]
        private readonly TimeSpan _lifetime;

        [UsedImplicitly]
        public Animation Animation
        {
            get { return _animation; }
        }

        [DataMember(Name = "Animation")]
        private readonly Animation _animation;

        [UsedImplicitly]
        public Point HiddenPosition
        {
            get { return _hiddenPosition; }
        }

        [DataMember(Name = "HiddenPosition")]
        private readonly Point _hiddenPosition;

        [UsedImplicitly]
        public Point Position
        {
            get { return _position; }
        }

        [DataMember(Name = "Position")]
        private readonly Point _position;

        [UsedImplicitly]
        public Size Size
        {
            get { return _size; }
        }

        [DataMember(Name = "Size")]
        private readonly Size _size;
    }
}
