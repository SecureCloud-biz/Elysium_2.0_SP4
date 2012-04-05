using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium.Controls
{
    [PublicAPI]
    public static class ToastNotification
    {
        [PublicAPI]
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Desktop is constant")]
        public static readonly FrameworkElement Desktop = new FrameworkElement();

        private static readonly DependencyPropertyKey FreeIndexesPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly("FreeIndexes", typeof(IDictionary<int, bool>), typeof(ToastNotification),
                                                        new PropertyMetadata(new Dictionary<int, bool>()));

        private static readonly DependencyProperty FreeIndexesProperty = FreeIndexesPropertyKey.DependencyProperty;

        private static IDictionary<int, bool> GetFreeIndexes(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.Ensures(Contract.Result<IDictionary<int, bool>>() != null);
            Contract.EndContractBlock();
            var value = (IDictionary<int, bool>)obj.GetValue(FreeIndexesProperty);
            Contract.Assume(value != null);
            return value;
        }

        [PublicAPI]
        public static readonly DependencyProperty AnimationProperty =
            DependencyProperty.RegisterAttached("Animation", typeof(ToastNotificationAnimation), typeof(ToastNotification),
                                                new FrameworkPropertyMetadata(ToastNotificationAnimation.Slide,
                                                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.AffectsRender));

        [PublicAPI]
        public static ToastNotificationAnimation GetAnimation(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<ToastNotificationAnimation>.Unbox(obj.GetValue(AnimationProperty));
        }

        [PublicAPI]
        public static void SetAnimation(DependencyObject obj, ToastNotificationAnimation value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(AnimationProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty LifetimeProperty =
            DependencyProperty.RegisterAttached("Lifetime", typeof(TimeSpan), typeof(ToastNotification),
                                                new FrameworkPropertyMetadata(TimeSpan.FromSeconds(10.0), FrameworkPropertyMetadataOptions.AffectsRender));

        [PublicAPI]
        public static TimeSpan GetLifetime(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<TimeSpan>.Unbox(obj.GetValue(LifetimeProperty));
        }

        [PublicAPI]
        public static void SetLifetime(DependencyObject obj, TimeSpan value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(LifetimeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty MarginProperty =
            DependencyProperty.RegisterAttached("Margin", typeof(Thickness), typeof(ToastNotification),
                                                new FrameworkPropertyMetadata(new Thickness(10.0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        public static Thickness GetMargin(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(MarginProperty));
        }

        [PublicAPI]
        public static void SetMargin(DependencyObject obj, Thickness value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(MarginProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.RegisterAttached("Width", typeof(double), typeof(ToastNotification),
                                                new FrameworkPropertyMetadata(480.0, FrameworkPropertyMetadataOptions.AffectsMeasure), IsWidthHeightValid);

        [PublicAPI]
        public static double GetWidth(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.Ensures(Contract.Result<double>() >= 0 || double.IsNaN(Contract.Result<double>()));
            Contract.EndContractBlock();
            var unboxedValue = BoxingHelper<double>.Unbox(obj.GetValue(WidthProperty));
            Contract.Assume(unboxedValue >= 0 || double.IsNaN(unboxedValue));
            return unboxedValue;
        }

        [PublicAPI]
        public static void SetWidth(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (!(value >= 0 || double.IsNaN(value)))
            {
                throw new ArgumentException("Width must be greater than zero, equal to zero or NaN", "value");
            }
            Contract.EndContractBlock();
            obj.SetValue(WidthProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.RegisterAttached("Height", typeof(double), typeof(ToastNotification),
                                                new FrameworkPropertyMetadata(64.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        public static double GetHeight(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.Ensures(Contract.Result<double>() >= 0 || double.IsNaN(Contract.Result<double>()));
            Contract.EndContractBlock();
            var unboxedValue = BoxingHelper<double>.Unbox(obj.GetValue(HeightProperty));
            Contract.Assume(unboxedValue >= 0 || double.IsNaN(unboxedValue));
            return unboxedValue;
        }

        [PublicAPI]
        public static void SetHeight(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (!(value >= 0 || double.IsNaN(value)))
            {
                throw new ArgumentException("Height must be greater than zero, equal to zero or NaN", "value");
            }
            Contract.EndContractBlock();
            obj.SetValue(HeightProperty, value);
        }

        private static bool IsWidthHeightValid(object value)
        {
            var unboxedValue = BoxingHelper<double>.Unbox(value);
            return !double.IsInfinity(unboxedValue) && (unboxedValue >= 0.0 || double.IsNaN(unboxedValue));
        }

        [PublicAPI]
        public static readonly DependencyProperty HorizontalPlacementProperty =
            DependencyProperty.RegisterAttached("HorizontalPlacement", typeof(HorizontalPlacement), typeof(ToastNotification),
                                                new FrameworkPropertyMetadata(HorizontalPlacement.Right, FrameworkPropertyMetadataOptions.AffectsArrange));

        [PublicAPI]
        public static HorizontalPlacement GetHorizontalPlacement(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<HorizontalPlacement>.Unbox(obj.GetValue(HorizontalPlacementProperty));
        }

        [PublicAPI]
        public static void SetHorizontalPlacement(DependencyObject obj, HorizontalPlacement value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(HorizontalPlacementProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty VerticalPlacementProperty =
            DependencyProperty.RegisterAttached("VerticalPlacement", typeof(VerticalPlacement), typeof(ToastNotification),
                                                new FrameworkPropertyMetadata(VerticalPlacement.Bottom, FrameworkPropertyMetadataOptions.AffectsArrange));

        [PublicAPI]
        public static VerticalPlacement GetVerticalPlacement(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<VerticalPlacement>.Unbox(obj.GetValue(VerticalPlacementProperty));
        }

        [PublicAPI]
        public static void SetVerticalPlacement(DependencyObject obj, VerticalPlacement value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(VerticalPlacementProperty, value);
        }

        [PublicAPI]
        public static void Show(string message, string remark = null, FrameworkElement target = null)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Message can't be null", "message");
            }
            Contract.EndContractBlock();

            // Create window
            var window = new Window { Title = message, Focusable = false, ShowActivated = false, ShowInTaskbar = false, WindowStyle = WindowStyle.ToolWindow };
            if (!string.IsNullOrWhiteSpace(remark))
            {
                window.Content = new TextBlock { FontStyle = FontStyles.Italic, Margin = new Thickness(10, 0, 10, 5), Text = remark };
            }

            // Get host
            var host = target ?? Desktop;

            // Calculate index
            var freeIndexes = GetFreeIndexes(host);
            var indexes = freeIndexes.Where(x => x.Value != true).ToList();
            var index = indexes.Count > 0 ? indexes.OrderBy(x => x.Key).First().Key : freeIndexes.Count();
            if (indexes.Count > 0)
                freeIndexes[index] = true;
            else
                freeIndexes.Add(index, true);

            // Calculate coordinates
            var startPoint = target != null ? target.PointToScreen(new Point(0, 0)) : new Point(0, 0);
            var width = GetWidth(host);
            var height = GetHeight(host);
            var margin = GetMargin(host);
            var offset = index * (margin.Top + height + margin.Bottom);

            var left = startPoint.X;
            var horizontalPlacement = GetHorizontalPlacement(host);
            if (horizontalPlacement == HorizontalPlacement.Left)
            {
                left += margin.Left;
            }
            else
            {
                if (target != null)
                    left += target.Width;
                else
                    left += SystemParameters.WorkArea.Right;
                left -= width + margin.Right;
            }

            var top = startPoint.Y;
            var verticalPlacement = GetVerticalPlacement(host);
            if (verticalPlacement == VerticalPlacement.Top)
            {
                top += margin.Top;
                top += offset;
            }
            else
            {
                if (target != null)
                    top += target.Height;
                else
                    top += SystemParameters.WorkArea.Bottom;
                top -= height + margin.Bottom;
                top -= offset;
            }

            window.Left = left;
            window.Top = top;
            window.Width = width;
            window.Height = height;

            // Create timer
            var timer = new Timer(state => window.Dispatcher.Invoke(new Action(window.Close)), null, -1, -1);

            // Closing animation
            window.Closing += (s, e) =>
                                  {
                                      timer.Dispose();
                                      switch (GetAnimation(host))
                                      {
                                          case ToastNotificationAnimation.None:
                                              break;
                                          case ToastNotificationAnimation.Fade:
                                              window.BeginAnimation(UIElement.OpacityProperty,
                                                                    new DoubleAnimation(1.0, 0.0, Parameters.GetMinimumDuration(host)));
                                              break;
                                          case ToastNotificationAnimation.Slide:
                                              window.BeginAnimation(System.Windows.Window.LeftProperty,
                                                                    new DoubleAnimation(left, -width, Parameters.GetOptimumDuration(host)));
                                              break;
                                      }
                                      freeIndexes[index] = false;
                                  };

            switch (GetAnimation(host))
            {
                case ToastNotificationAnimation.None:
                    break;
                case ToastNotificationAnimation.Fade:
                    window.Opacity = 0;
                    break;
                case ToastNotificationAnimation.Slide:
                    window.Left += horizontalPlacement == HorizontalPlacement.Left ? -margin.Left : width + margin.Right;
                    break;
            }

            window.Show();

            // Opening animation
            switch (GetAnimation(host))
            {
                case ToastNotificationAnimation.None:
                    break;
                case ToastNotificationAnimation.Fade:
                    window.BeginAnimation(UIElement.OpacityProperty,
                                          new DoubleAnimation(0.0, 1.0, Parameters.GetMinimumDuration(host)));
                    break;
                case ToastNotificationAnimation.Slide:
                    window.BeginAnimation(System.Windows.Window.LeftProperty,
                                          new DoubleAnimation(window.Left, left, Parameters.GetOptimumDuration(host)));
                    break;
            }

            var lifetime = GetLifetime(host);
            window.BeginAnimation(Window.ProgressPercentProperty, new DoubleAnimation(100.0, 0.0, lifetime));

            // Start timer
            timer.Change(lifetime, TimeSpan.FromSeconds(0));
        }
    }
} ;