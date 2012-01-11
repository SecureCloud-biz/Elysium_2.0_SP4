using System;
using System.Collections.ObjectModel;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Elysium.Theme.Controls.Primitives;

namespace Elysium.Theme.Controls
{
    [TemplatePart(Name = IndicatorName, Type = typeof(Rectangle))]
    [TemplatePart(Name = LoadingBarName, Type = typeof(Canvas))]
    public class LinearProgressBar : ProgressBarBase
    {
        private const string IndicatorName = "PART_Indicator";
        private const string LoadingBarName = "PART_LoadingBar";

        private Rectangle _indicator;
        private Canvas _loadingBar;

        static LinearProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearProgressBar), new FrameworkPropertyMetadata(typeof(LinearProgressBar)));
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ProgressBarBase),
                                        new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsMeasure),
                                        IsValidOrientation);

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        private static bool IsValidOrientation(object value)
        {
            var orientation = (Orientation)value;
            return orientation == Orientation.Horizontal || orientation == Orientation.Vertical;
        }

        [SecuritySafeCritical]
        internal override void OnApplyTemplateInternal()
        {
            _indicator = GetTemplateChild(IndicatorName) as Rectangle;
            _loadingBar = GetTemplateChild(LoadingBarName) as Canvas;

            base.OnApplyTemplateInternal();
        }

        protected override void OnAnimationsUpdating(RoutedEventArgs e)
        {
            base.OnAnimationsUpdating(e);

            if (IndeterminateAnimation != null && IndeterminateAnimation.Name == DefaultIndeterminateAnimationName && Track != null && _indicator != null)
            {
                var isStarted = IsIndeterminate == true && IsEnabled;
                if (isStarted)
                {
                    IndeterminateAnimation.Stop(this);
                    IndeterminateAnimation.Remove(this);
                }

                IndeterminateAnimation = new Storyboard { Name = DefaultIndeterminateAnimationName, RepeatBehavior = RepeatBehavior.Forever };

                var indicatorSize = Orientation == Orientation.Horizontal ? _indicator.Width : _indicator.Height;

                var trackSize = Orientation == Orientation.Horizontal ? Track.ActualWidth : Track.ActualHeight;

                var time = trackSize / 100;

                var animation = new DoubleAnimationUsingKeyFrames { Duration = new Duration(TimeSpan.FromSeconds(time + 0.5)) };
                animation.KeyFrames.Add(new DiscreteDoubleKeyFrame(-indicatorSize - 1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
                animation.KeyFrames.Add(new LinearDoubleKeyFrame(trackSize + 1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(time))));

                Storyboard.SetTarget(animation, _indicator);
                Storyboard.SetTargetProperty(animation,
                                             new PropertyPath(Orientation == Orientation.Horizontal ? Canvas.LeftProperty : Canvas.TopProperty));

                IndeterminateAnimation.Children.Add(animation);

                if (isStarted)
                    IndeterminateAnimation.Begin(this, Template, true);
            }

            if (LoadingAnimation != null && LoadingAnimation.Name == DefaultLoadingAnimationName && Track != null && _loadingBar != null)
            {
                var isStarted = IsIndeterminate == null && IsEnabled;
                if (isStarted)
                {
                    LoadingAnimation.Stop(this);
                    LoadingAnimation.Remove(this);
                }

                LoadingAnimation = new Storyboard { Name = DefaultLoadingAnimationName, RepeatBehavior = RepeatBehavior.Forever };

                const double time = 0.25;
                const double durationTime = time * 2;
                const double beginTimeIncrement = time / 2;
                const double shortPauseTime = time;
                const double longPauseTime = time * 1.5;
                var partMotionTime = (_loadingBar.Children.Count - 1) * beginTimeIncrement + durationTime;

                var loadingAnimations = new Collection<DoubleAnimation>();

                var width = Track.ActualWidth;
                var height = Track.ActualHeight;

                for (var i = 0; i < _loadingBar.Children.Count; i++)
                {
                    var element = (FrameworkElement)_loadingBar.Children[_loadingBar.Children.Count - i - 1];

                    var elementWidth = element.Width;
                    var elementHeight = element.Height;

                    var index = (_loadingBar.Children.Count - 1) / 2 - i;

                    var center = (Orientation == Orientation.Horizontal ? width : height) / 2;
                    var margin = Orientation == Orientation.Horizontal ? elementWidth : elementHeight;

                    var startPosition = -(Orientation == Orientation.Horizontal ? elementWidth : elementHeight) - 1;
                    var endPosition = center + index * ((Orientation == Orientation.Horizontal ? elementWidth : elementHeight) + margin);

                    var duration = new Duration(TimeSpan.FromSeconds(durationTime));
                    var animation = new DoubleAnimation(startPosition, endPosition, duration) { BeginTime = TimeSpan.FromSeconds(i * beginTimeIncrement) };
                    Storyboard.SetTarget(animation, element);
                    Storyboard.SetTargetProperty(animation, new PropertyPath(Orientation == Orientation.Horizontal ? Canvas.LeftProperty : Canvas.TopProperty));

                    loadingAnimations.Add(animation);
                }

                for (var i = 0; i < _loadingBar.Children.Count; i++)
                {
                    var element = (FrameworkElement)_loadingBar.Children[_loadingBar.Children.Count - i - 1];

                    var elementWidth = element.Width;
                    var elementHeight = element.Height;

                    var endPosition = (Orientation == Orientation.Horizontal ? width : height) +
                                      (Orientation == Orientation.Horizontal ? elementWidth : elementHeight) + 1;

                    var duration = new Duration(TimeSpan.FromSeconds(durationTime));
                    var animation = new DoubleAnimation(endPosition, duration)
                                        { BeginTime = TimeSpan.FromSeconds(partMotionTime + shortPauseTime + i * beginTimeIncrement) };
                    Storyboard.SetTarget(animation, element);
                    Storyboard.SetTargetProperty(animation, new PropertyPath(Orientation == Orientation.Horizontal ? Canvas.LeftProperty : Canvas.TopProperty));

                    loadingAnimations.Add(animation);
                }

                LoadingAnimation.Duration = new Duration(TimeSpan.FromSeconds(partMotionTime * 2 + shortPauseTime + longPauseTime));

                foreach (var animation in loadingAnimations)
                {
                    LoadingAnimation.Children.Add(animation);
                }

                if (isStarted)
                    LoadingAnimation.Begin(this, Template, true);
            }
        }
    }
} ;