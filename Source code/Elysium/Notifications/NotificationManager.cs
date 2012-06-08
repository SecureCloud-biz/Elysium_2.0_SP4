using System;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

using Elysium.Extensions;

using JetBrains.Annotations;

using Window = Elysium.Controls.Window;

namespace Elysium.Notifications
{
    [PublicAPI]
    public static class NotificationManager
    {
        private static Notification Reserve()
        {
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
                return null;
            }
#pragma warning disable 168
            catch (CommunicationException communicationException)
#pragma warning restore 168
            {
                notificationManager.Abort();
                return null;
            }
#pragma warning disable 168
            catch (TimeoutException timeoutException)
#pragma warning restore 168
            {
                notificationManager.Abort();
                return null;
            }
        }

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
            }
#pragma warning disable 168
            catch (CommunicationException communicationException)
#pragma warning restore 168
            {
                notificationManager.Abort();
            }
#pragma warning disable 168
            catch (TimeoutException timeoutException)
#pragma warning restore 168
            {
                notificationManager.Abort();
            }
        }

        [PublicAPI]
        public static Task<bool> PushAsync([NotNull] string message, [CanBeNull] string remark)
        {
            ValidationHelper.NotNullOrWhitespace(message, () => message);

            var task = new Task<bool>(() => Push(message, remark));
            task.Start();
            return task;
        }

        [PublicAPI]
        public static bool Push([NotNull] string message, [CanBeNull] string remark)
        {
            ValidationHelper.NotNullOrWhitespace(message, () => message);

            var window = new Window { Title = message, Focusable = false, ShowActivated = false, ShowInTaskbar = false, Topmost = true, WindowStyle = WindowStyle.ToolWindow };
            if (!string.IsNullOrWhiteSpace(remark))
            {
                window.Content = new TextBlock { FontStyle = FontStyles.Italic, Margin = new Thickness(10d, 0d, 10d, 5d), Text = remark };
            }

            var slot = Reserve();

            if (slot == null)
            {
                return false;
            }

            window.Left = slot.Position.X;
            window.Top = slot.Position.Y;
            window.Width = slot.Size.Width;
            window.Height = slot.Size.Height;

            var timer = new Timer(delegate { window.Dispatcher.Invoke(new Action(window.Close)); });

            window.Closing += (s, e) =>
            {
                timer.Dispose();
                Free(slot.ID);
                CloseAnimation(window, slot);
            };

            BeginOpenAnimation(window, slot);
            window.Show();
            EndOpenAnimation(window, slot);

            window.BeginAnimation(Window.ProgressPercentProperty, new DoubleAnimation(100d, 0d, slot.Lifetime));

            timer.Change(slot.Lifetime, TimeSpan.FromSeconds(-1d));

            return true;
        }

        private static void BeginOpenAnimation(Window window, Notification slot)
        {
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
} ;