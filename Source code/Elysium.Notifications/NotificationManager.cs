using System;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
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

        [PublicAPI]
        public static Task<bool> TryPushAsync([NotNull] string message, [CanBeNull] string remark)
        {
            var task = new Task<bool>(() =>
            {
                var result = true;
                var thread = new Thread(() =>
                {
                    Application.Current.DispatcherUnhandledException += (sender, e) =>
                    {
                        if (e.Exception is ServerUnavailableException || e.Exception is PushNotificationFailedException)
                        {
                            result = false;
                        }
                    };
                    PushInternal(message, remark, () => Dispatcher.CurrentDispatcher.InvokeShutdown());
                    Dispatcher.Run();
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.IsBackground = true;
                thread.Start();
                thread.Join();
                return result;
            });
            task.Start();
            return task;
        }

        [PublicAPI]
        public static bool TryPush([NotNull] string message, [CanBeNull] string remark)
        {
            try
            {
                Push(message, remark);
            }
            catch
            {
                return false;
            }
            return true;
        }

        [PublicAPI]
        public static void PushAsync([NotNull] string message, [CanBeNull] string remark)
        {
            var thread = new Thread(() =>
            {
                PushInternal(message, remark, () => Dispatcher.CurrentDispatcher.InvokeShutdown());
                Dispatcher.Run();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }
        
        [PublicAPI]
        public static void Push([NotNull] string message, [CanBeNull] string remark)
        {
            PushInternal(message, remark, null);
        }

        [PublicAPI]
        private static void PushInternal([NotNull] string message, [CanBeNull] string remark, Action closingAction)
        {
            ValidationHelper.NotNullOrWhitespace(message, () => message);

            try
            {
                var window = new Window
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
                    window.Content = new TextBlock { FontStyle = FontStyles.Italic, Margin = new Thickness(10d, 0d, 10d, 5d), Text = remark };
                }

                var slot = Reserve();

                window.Left = slot.Position.X;
                window.Top = slot.Position.Y;
                window.Width = slot.Size.Width;
                window.Height = slot.Size.Height;

                var timer = new Timer(delegate { window.Dispatcher.Invoke(new Action(window.Close), DispatcherPriority.Normal); });

                window.Closing += (s, e) =>
                {
                    timer.Dispose();
                    Free(slot.ID);
                    CloseAnimation(window, slot);
                    if (closingAction != null)
                    {
                        closingAction();
                    }
                };

                BeginOpenAnimation(window, slot);
                window.Show();
                EndOpenAnimation(window, slot);

                window.BeginAnimation(Window.ProgressProperty, new DoubleAnimation(100d, 0d, slot.Lifetime));

                timer.Change((int)Math.Ceiling(slot.Lifetime.TotalMilliseconds), -1);
            }
            catch (ServerUnavailableException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new PushNotificationFailedException("Push notification failed.", exception);
            }
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