using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;

using Elysium.Notifications.Server.Properties;

using JetBrains.Annotations;

namespace Elysium.Notifications.Server
{
    [UsedImplicitly]
    [ServiceBehavior(Namespace = "http://namespaces.codeplex.com/elysium/notifications", Name = "NotificationManager",
        InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true)]
    internal sealed class NotificationManager : INotificationManager
    {
        private static readonly Dictionary<byte, bool> Slots = new Dictionary<byte, bool>(5);

        private static TimeSpan _lifetimeCache = Settings.Default.Lifetime;
        private static Animation _animationCache = Settings.Default.Animation;
        private static Thickness _marginCache = Settings.Default.Margin;
        private static Size _sizeCache = Settings.Default.Size;
        private static HorizontalPlacement _horizontalPlacementCache = Settings.Default.HorizontalPlacement;
        private static VerticalPlacement _verticalPlacementCache = Settings.Default.VerticalPlacement;

// ReSharper disable InconsistentNaming
        private static readonly object _lock = new object();
// ReSharper restore InconsistentNaming

        [UsedImplicitly]
        public Notification Reserve(Rect workArea)
        {
            byte slotIndex;
            lock (_lock)
            {
                var freeSlots = Slots.AsParallel().Where(slot => !slot.Value).OrderBy(slot => slot.Key).ToList();
                slotIndex = freeSlots.Any() ? freeSlots.First().Key : (byte)Slots.Count;
                if (freeSlots.Any())
                {
                    Slots[slotIndex] = true;
                }
                else
                {
                    Slots.Add(slotIndex, true);
                }
            }

            var offset = slotIndex * (_marginCache.Top + _sizeCache.Height + _marginCache.Bottom);
            var left = _horizontalPlacementCache == HorizontalPlacement.Left
                           ? _marginCache.Left
                           : workArea.Right - _sizeCache.Width - _marginCache.Right;
            var top = _verticalPlacementCache == VerticalPlacement.Top
                          ? _marginCache.Top + offset
                          : workArea.Bottom - _sizeCache.Height - _marginCache.Bottom - offset;
            var hiddenPosition = _horizontalPlacementCache == HorizontalPlacement.Left
                                     ? -_sizeCache.Width
                                     : workArea.Right;

            return new Notification(slotIndex, _lifetimeCache, _animationCache, new Point(hiddenPosition, top), new Point(left, top),
                                    new Size(_sizeCache.Width, _sizeCache.Height));
        }

        [UsedImplicitly]
        public void Free(byte id)
        {
            lock (_lock)
            {
                if (Slots.ContainsKey(id))
                {
                    Slots[id] = false;
                }
            }
        }

        [UsedImplicitly]
        public void Update(TimeSpan lifetime, Animation animation, Thickness margin, Size size,
                           HorizontalPlacement horizontalPlacement, VerticalPlacement verticalPlacement)
        {
            lock (_lock)
            {
                Parallel.Invoke(
                    () =>
                    {
                        Settings.Default.Lifetime = lifetime;
                        Settings.Default.Animation = animation;
                        Settings.Default.Margin = margin;
                        Settings.Default.Size = size;
                        Settings.Default.HorizontalPlacement = horizontalPlacement;
                        Settings.Default.VerticalPlacement = verticalPlacement;
                        Settings.Default.Save();
                    },
                    () =>
                    {
                        _lifetimeCache = lifetime;
                        _animationCache = animation;
                        _marginCache = margin;
                        _sizeCache = size;
                        _horizontalPlacementCache = horizontalPlacement;
                        _verticalPlacementCache = verticalPlacement;
                    });
            }
        }
    }
} ;