using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Elysium.Theme.Controls
{
    public enum HorizontalPlacement
    {
        Left,
        Right
    }

    public enum VerticalPlacement
    {
        Top,
        Bottom
    }

    public enum ToastNotificationAnimation
    {
        None,
        Fade,
        Slide
    }

    public sealed class ToastNotification
    {
        public static readonly DependencyObject Desktop = new DependencyObject();

        private static readonly DependencyProperty FreeIndexesProperty =
            DependencyProperty.RegisterAttached("FreeIndexes", typeof(IDictionary<int, bool>), typeof(ToastNotification),
                                                new UIPropertyMetadata(new Dictionary<int, bool>()));

        private static IDictionary<int, bool> GetFreeIndexes(DependencyObject obj)
        {
            return (IDictionary<int, bool>)obj.GetValue(FreeIndexesProperty);
        }

        private static void SetFreeIndexes(DependencyObject obj, IDictionary<int, bool> value)
        {
            obj.SetValue(FreeIndexesProperty, value);
        }

        public static readonly DependencyProperty AnimationProperty =
            DependencyProperty.RegisterAttached("Animation", typeof(ToastNotificationAnimation), typeof(ToastNotification),
                                                new UIPropertyMetadata(ToastNotificationAnimation.Slide));

        public static ToastNotificationAnimation GetAnimation(DependencyObject obj)
        {
            return (ToastNotificationAnimation)obj.GetValue(AnimationProperty);
        }

        public static void SetAnimation(DependencyObject obj, ToastNotificationAnimation value)
        {
            obj.SetValue(AnimationProperty, value);
        }

        public static readonly DependencyProperty LifetimeProperty =
            DependencyProperty.RegisterAttached("Lifetime", typeof(TimeSpan), typeof(ToastNotification), new UIPropertyMetadata(TimeSpan.FromSeconds(10.0)));

        public static TimeSpan GetLifetime(DependencyObject obj)
        {
            return (TimeSpan)obj.GetValue(LifetimeProperty);
        }

        public static void SetLifetime(DependencyObject obj, TimeSpan value)
        {
            obj.SetValue(LifetimeProperty, value);
        }

        public static readonly DependencyProperty MarginProperty =
            DependencyProperty.RegisterAttached("Margin", typeof(Thickness), typeof(ToastNotification), new UIPropertyMetadata(new Thickness(10.0)));

        public static Thickness GetMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(MarginProperty);
        }

        public static void SetMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(MarginProperty, value);
        }

        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.RegisterAttached("Width", typeof(double), typeof(ToastNotification), new UIPropertyMetadata(480.0));

        public static double GetWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(WidthProperty);
        }

        public static void SetWidth(DependencyObject obj, double value)
        {
            obj.SetValue(WidthProperty, value);
        }

        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.RegisterAttached("Height", typeof(double), typeof(ToastNotification), new UIPropertyMetadata(64.0));

        public static double GetHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(HeightProperty);
        }

        public static void SetHeight(DependencyObject obj, double value)
        {
            obj.SetValue(HeightProperty, value);
        }

        public static readonly DependencyProperty HorizontalPlacementProperty =
            DependencyProperty.RegisterAttached("HorizontalPlacement", typeof(HorizontalPlacement), typeof(ToastNotification),
                                                new UIPropertyMetadata(HorizontalPlacement.Right));

        public static HorizontalPlacement GetHorizontalPlacement(DependencyObject obj)
        {
            return (HorizontalPlacement)obj.GetValue(HorizontalPlacementProperty);
        }

        public static void SetHorizontalPlacement(DependencyObject obj, HorizontalPlacement value)
        {
            obj.SetValue(HorizontalPlacementProperty, value);
        }

        public static readonly DependencyProperty VerticalPlacementProperty =
            DependencyProperty.RegisterAttached("VerticalPlacement", typeof(VerticalPlacement), typeof(ToastNotification),
                                                new UIPropertyMetadata(VerticalPlacement.Bottom));

        public static VerticalPlacement GetVerticalPlacement(DependencyObject obj)
        {
            return (VerticalPlacement)obj.GetValue(VerticalPlacementProperty);
        }

        public static void SetVerticalPlacement(DependencyObject obj, VerticalPlacement value)
        {
            obj.SetValue(VerticalPlacementProperty, value);
        }

        public static void Show(string message, string remark = null, FrameworkElement target = null)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(message), "message");

            var window = new Window { Focusable = false, ShowActivated = false, ShowInTaskbar = false, WindowStyle = WindowStyle.ToolWindow };
            var content = new Grid();
            var messagePresenter = new TextBlock { FontWeight = FontWeights.SemiBold, Text = message };
            Grid.SetRow(messagePresenter, 0);
            content.Children.Add(messagePresenter);
            if (!string.IsNullOrWhiteSpace(remark))
            {
                content.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                content.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                var remarkPresenter = new TextBlock { FontWeight = FontWeights.SemiBold, Text = remark };
                Grid.SetRow(remarkPresenter, 1);
                content.Children.Add(remarkPresenter);
            }
            window.Content = content;

            var host = target ?? Desktop;

            var freeIndexes = GetFreeIndexes(host);
            var indexes = freeIndexes.Where(x => x.Value != true).ToList();
            var index = indexes.Count > 0 ? indexes.OrderBy(x => x.Key).First().Key : freeIndexes.Count();
            if (indexes.Count > 0)
                freeIndexes[index] = true;
            else
                freeIndexes.Add(index, true);

            var startPoint = target != null ? target.PointToScreen(new Point(0, 0)) : new Point(0, 0);
            var width = GetWidth(host);
            var height = GetHeight(host);
            var margin = GetMargin(host);
            var offset = index * (margin.Top + height + margin.Bottom);

            var left = startPoint.X;
            if (GetHorizontalPlacement(host) == HorizontalPlacement.Left)
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
            if (GetVerticalPlacement(host) == VerticalPlacement.Top)
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

            var timer = new Timer(state => window.Close(), null, TimeSpan.FromSeconds(-1), TimeSpan.FromSeconds(-1));

            window.Closed += (s, e) =>
                                 {
                                     timer.Dispose();
                                     switch (GetAnimation(host))
                                     {
                                         case ToastNotificationAnimation.None:
                                             break;
                                         case ToastNotificationAnimation.Fade:
                                             window.BeginAnimation(UIElement.OpacityProperty,
                                                                   new DoubleAnimation(1.0, 0.0, Parameters.GetMinimumDurationProperty(host)));
                                             break;
                                         case ToastNotificationAnimation.Slide:
                                             window.BeginAnimation(System.Windows.Window.LeftProperty,
                                                                   new DoubleAnimation(left, -width, Parameters.GetOptimumDurationProperty(host)));
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
                    window.Left = -width;
                    break;
            }

            window.Show();

            switch (GetAnimation(host))
            {
                case ToastNotificationAnimation.None:
                    break;
                case ToastNotificationAnimation.Fade:
                    window.BeginAnimation(UIElement.OpacityProperty, new DoubleAnimation(0.0, 1.0, Parameters.GetMinimumDurationProperty(host)));
                    break;
                case ToastNotificationAnimation.Slide:
                    window.BeginAnimation(System.Windows.Window.LeftProperty, new DoubleAnimation(-width, left, Parameters.GetOptimumDurationProperty(host)));
                    break;
            }

            timer.Change(TimeSpan.FromSeconds(0), GetLifetime(host));
        }
    }
} ;