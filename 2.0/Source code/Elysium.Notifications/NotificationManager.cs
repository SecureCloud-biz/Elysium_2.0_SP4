using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.ServiceModel;
using System.Threading;
#if NETFX45
using System.Threading.Tasks;
#endif
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

using Elysium.Extensions;

using JetBrains.Annotations;

using Window = Elysium.Controls.Window;

namespace Elysium.Notifications
{
    [PublicAPI]
    public static class NotificationManager
    {
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "NotificationManager disposed by Close method")]
        private static Notification Reserve()
        {
            Contract.Ensures(Contract.Result<Notification>() != null);

            var notificationManager = new Client.NotificationManager();
            try
            {
                var slot = notificationManager.Reserve(SystemParameters.WorkArea);
                notificationManager.Close();
                return slot;
            }
#pragma warning disable 168
            catch (FaultException faultException)
#pragma warning restore 168
            {
                notificationManager.Abort();
                throw new ServerUnavailableException("Notification Server is unavailable.", faultException);
            }
#pragma warning disable 168
            catch (CommunicationException communicationException)
#pragma warning restore 168
            {
                notificationManager.Abort();
                throw new ServerUnavailableException("Notification Server is unavailable.", communicationException);
            }
#pragma warning disable 168
            catch (TimeoutException timeoutException)
#pragma warning restore 168
            {
                notificationManager.Abort();
                throw new ServerUnavailableException("Notification Server is unavailable.", timeoutException);
            }
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "NotificationManager disposed by Close method")]
        private static void Free(byte id)
        {
            var notificationManager = new Client.NotificationManager();
            try
            {
                notificationManager.Free(id);
                notificationManager.Close();
            }
#pragma warning disable 168
            catch (FaultException faultException)
#pragma warning restore 168
            {
                notificationManager.Abort();
                throw new ServerUnavailableException("Notification Server is unavailable.", faultException);
            }
#pragma warning disable 168
            catch (CommunicationException communicationException)
#pragma warning restore 168
            {
                notificationManager.Abort();
                throw new ServerUnavailableException("Notification Server is unavailable.", communicationException);
            }
#pragma warning disable 168
            catch (TimeoutException timeoutException)
#pragma warning restore 168
            {
                notificationManager.Abort();
                throw new ServerUnavailableException("Notification Server is unavailable.", timeoutException);
            }
        }

#if NETFX4
        [PublicAPI]
        public static DispatcherOperation BeginTryPush([NotNull] string message, [CanBeNull] string remark)
        {
            ValidationHelper.NotNullOrWhitespace(message, "message");

            return Dispatcher.CurrentDispatcher.BeginInvoke(() => TryPush(message, remark), DispatcherPriority.Render);
        }
#elif NETFX45
        [PublicAPI]
        public static async Task<bool> TryPushAsync([NotNull] string message, [CanBeNull] string remark)
        {
            ValidationHelper.NotNullOrWhitespace(message, "message");

            return await Dispatcher.CurrentDispatcher.InvokeAsync(() => TryPush(message, remark), DispatcherPriority.Render);
        }
#endif

        [PublicAPI]
        public static bool TryPush([NotNull] string message, [CanBeNull] string remark)
        {
            ValidationHelper.NotNullOrWhitespace(message, "message");

            try
            {
                Push(message, remark);
            }
            catch (ServerUnavailableException)
            {
                return false;
            }
            catch (PushNotificationFailedException)
            {
                return false;
            }
            return true;
        }

#if NETFX4
        [PublicAPI]
        public static DispatcherOperation BeginPush([NotNull] string message, [CanBeNull] string remark)
        {
            ValidationHelper.NotNullOrWhitespace(message, "message");

            return Dispatcher.CurrentDispatcher.BeginInvoke(() => Push(message, remark), DispatcherPriority.Render);
        }
#elif NETFX45
        [PublicAPI]
        public static async Task PushAsync([NotNull] string message, [CanBeNull] string remark)
        {
            ValidationHelper.NotNullOrWhitespace(message, "message");

            await Dispatcher.CurrentDispatcher.InvokeAsync(() => Push(message, remark), DispatcherPriority.Render);
        }
#endif

        [PublicAPI]
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "This version contains a minimal code in method.")]
        public static void Push([NotNull] string message, [CanBeNull] string remark)
        {
            ValidationHelper.NotNullOrWhitespace(message, "message");

            Window window = null;
            try
            {
                window = new Window
                                 {
                                     Title = message,
                                     Focusable = false,
                                     ShowActivated = false,
                                     ShowInTaskbar = false,
                                     Topmost = true,
                                     WindowStyle = WindowStyle.ToolWindow
                                 };
                if (!string.IsNullOrWhiteSpace(remark))
                {
                    window.Content = new TextBlock { Margin = new Thickness(10d, 5d, 10d, 5d), Text = remark };
                }

                var slot = Reserve();

                window.Left = slot.Position.X;
                window.Top = slot.Position.Y;
                window.Width = slot.Size.Width;
                window.Height = slot.Size.Height;

                // Capture closure
                var closingSlot = slot;
                var timer = new Timer(delegate
                {
                    closingSlot = slot;
                    window.Dispatcher.Invoke(window.Close, DispatcherPriority.Normal);
                });

                window.Closing += (s, e) =>
                {
                    closingSlot = slot;
                    timer.Dispose();
                    Free(closingSlot.ID);
                    CloseAnimation(window, closingSlot);
                };

                BeginOpenAnimation(window, slot);
                window.Show();
                EndOpenAnimation(window, slot);

                window.BeginAnimation(Window.ProgressProperty, new DoubleAnimation(100d, 0d, slot.Lifetime));

                timer.Change((int)Math.Ceiling(slot.Lifetime.TotalMilliseconds), -1);
            }
            catch (ServerUnavailableException)
            {
                if (window != null)
                {
                    window.Close();
                }
                throw;
            }
            catch (Exception exception)
            {
                if (window != null)
                {
                    window.Close();
                }
                throw new PushNotificationFailedException("Push notification failed.", exception);
            }
        }

        private static void BeginOpenAnimation(Window window, Notification slot)
        {
            // Can't be proven
            Contract.Assume(window != null);

            switch (slot.Animation)
            {
                case Animation.None:
                    break;
                case Animation.Fade:
                    window.Opacity = 0;
                    break;
                case Animation.Slide:
                    window.Left = slot.HiddenPosition.X;
                    window.Top = slot.HiddenPosition.Y;
                    break;
            }
        }

        private static void EndOpenAnimation(Window window, Notification slot)
        {
            // Can't be proven
            Contract.Assume(window != null);

            switch (slot.Animation)
            {
                case Animation.None:
                    break;
                case Animation.Fade:
                    window.BeginAnimation(UIElement.OpacityProperty,
                                          new DoubleAnimation(0d, 1d, new Duration(TimeSpan.FromSeconds(0.2))));
                    break;
                case Animation.Slide:
                    window.BeginAnimation(System.Windows.Window.LeftProperty,
                                          new DoubleAnimation(slot.Position.X, new Duration(TimeSpan.FromSeconds(0.2))));
                    window.BeginAnimation(System.Windows.Window.TopProperty,
                                          new DoubleAnimation(slot.Position.Y, new Duration(TimeSpan.FromSeconds(0.2))));
                    break;
            }
        }

        private static void CloseAnimation(Window window, Notification slot)
        {
            // Can't be proven
            Contract.Assume(window != null);

            switch (slot.Animation)
            {
                case Animation.None:
                    break;
                case Animation.Fade:
                    window.BeginAnimation(UIElement.OpacityProperty,
                                          new DoubleAnimation(1d, 0d, new Duration(TimeSpan.FromSeconds(0.2))));
                    break;
                case Animation.Slide:
                    window.BeginAnimation(System.Windows.Window.LeftProperty,
                                          new DoubleAnimation(slot.HiddenPosition.X, new Duration(TimeSpan.FromSeconds(0.2))));
                    window.BeginAnimation(System.Windows.Window.TopProperty,
                                          new DoubleAnimation(slot.HiddenPosition.Y, new Duration(TimeSpan.FromSeconds(0.2))));
                    break;
            }
        }
    }
}
