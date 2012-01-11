using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

using Microsoft.Expression.Shapes;

using Elysium.Theme.Controls.Primitives;

namespace Elysium.Theme.Controls
{
    [TemplatePart(Name = ArcName, Type = typeof(Arc))]
    [TemplatePart(Name = LoadingBarName, Type = typeof(Canvas))]
    public class CircularProgressBar : ProgressBarBase
    {
        private const string ArcName = "PART_Arc";
        private const string LoadingBarName = "PART_LoadingBar";

        private Arc _arc;
        private Canvas _loadingBar;

        static CircularProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircularProgressBar), new FrameworkPropertyMetadata(typeof(CircularProgressBar)));
        }

        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.RegisterAttached("Angle", typeof(double), typeof(CircularProgressBar),
                                                new FrameworkPropertyMetadata(-1.0, FrameworkPropertyMetadataOptions.AffectsArrange));

        public static double GetAngle(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            var value = obj.GetValue(AngleProperty);
            Contract.Assume(value != null);
            return (double)value;
        }

        public static void SetAngle(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(AngleProperty, value);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            var desiredSize = base.MeasureOverride(constraint);
            var sizeValue = Math.Min(desiredSize.Width, desiredSize.Height);
            Contract.Assume(sizeValue >= 0.0);
            desiredSize.Width = sizeValue;
            desiredSize.Height = sizeValue;
            return desiredSize;
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            var sizeValue = Math.Min(arrangeBounds.Width, arrangeBounds.Height);
            Contract.Assume(sizeValue >= 0.0);
            return base.ArrangeOverride(new Size(sizeValue, sizeValue));
        }

        [SecuritySafeCritical]
        internal override void OnApplyTemplateInternal()
        {
            if (Template != null)
            {
                _arc = Template.FindName(ArcName, this) as Arc;
                Contract.Assume(Template != null);
                _loadingBar = Template.FindName(LoadingBarName, this) as Canvas;
            }

            base.OnApplyTemplateInternal();
        }

        protected override void OnAnimationsUpdating(RoutedEventArgs e)
        {
            OnAnimationsUpdatingInternal(e);

            base.OnAnimationsUpdating(e);
        }

        [SecuritySafeCritical]
        internal virtual void OnAnimationsUpdatingInternal(RoutedEventArgs e)
        {
            if (IndeterminateAnimation != null && IndeterminateAnimation.Name == DefaultIndeterminateAnimationName && Track != null && _arc != null)
            {
                var isStarted = State == ProgressBarState.Indeterminate && IsEnabled;
                if (isStarted)
                {
                    IndeterminateAnimation.Stop(this);
                    IndeterminateAnimation.Remove(this);
                }

                IndeterminateAnimation = new Storyboard { Name = DefaultIndeterminateAnimationName, RepeatBehavior = RepeatBehavior.Forever };

                var trackSize = Math.Min(Track.ActualWidth, Track.ActualHeight);

                var time = (trackSize * Math.PI) / 100;

                var startAngleSetValueAnimation = new DoubleAnimation(0, new Duration(TimeSpan.Parse("00:00:00.0")));

                Storyboard.SetTarget(startAngleSetValueAnimation, _arc);
                Storyboard.SetTargetProperty(startAngleSetValueAnimation, new PropertyPath(Arc.StartAngleProperty));

                var endAngleSetValueAnimation = new DoubleAnimation(-270, new Duration(TimeSpan.Parse("00:00:00.0")));

                Storyboard.SetTarget(endAngleSetValueAnimation, _arc);
                Storyboard.SetTargetProperty(endAngleSetValueAnimation, new PropertyPath(Arc.EndAngleProperty));

                var startAngleAnimation = new DoubleAnimationUsingKeyFrames();
                Contract.Assume(startAngleAnimation.KeyFrames != null);
                startAngleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(360, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(time))));

                Storyboard.SetTarget(startAngleAnimation, _arc);
                Storyboard.SetTargetProperty(startAngleAnimation, new PropertyPath(Arc.StartAngleProperty));

                var endAngleAnimation = new DoubleAnimationUsingKeyFrames();
                Contract.Assume(endAngleAnimation.KeyFrames != null);
                endAngleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(90, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(time))));

                Storyboard.SetTarget(endAngleAnimation, _arc);
                Storyboard.SetTargetProperty(endAngleAnimation, new PropertyPath(Arc.EndAngleProperty));

                // Bug in Code Contracts
                Contract.Assume(IndeterminateAnimation != null);
                Contract.Assume(IndeterminateAnimation.Children != null);

                IndeterminateAnimation.Children.Add(startAngleSetValueAnimation);
                IndeterminateAnimation.Children.Add(endAngleSetValueAnimation);
                IndeterminateAnimation.Children.Add(startAngleAnimation);
                IndeterminateAnimation.Children.Add(endAngleAnimation);

                if (isStarted)
                {
                    IndeterminateAnimation.Begin(this, Template, true);
                }
            }

            if (LoadingAnimation != null && LoadingAnimation.Name == DefaultLoadingAnimationName && Track != null && _loadingBar != null)
            {
                var isStarted = State == ProgressBarState.Loading && IsEnabled;
                if (isStarted)
                {
                    LoadingAnimation.Stop(this);
                    LoadingAnimation.Remove(this);
                }

                Contract.Assume(_loadingBar.Children != null);

                LoadingAnimation = new Storyboard { Name = DefaultLoadingAnimationName, RepeatBehavior = RepeatBehavior.Forever };

                var firstCycleAnimations = new Collection<DoubleAnimation>();
                var secondCycleAnimations = new Collection<DoubleAnimation>();

                const double time = 0.25;
                const double durationTime = time * 2;
                const double beginTimeIncrement = time / 2;
                const double shortPauseTime = time;
                const double longPauseTime = time * 1.5;
                var partMotionTime = (_loadingBar.Children.Count - 1) * beginTimeIncrement + durationTime + shortPauseTime;

                var length = Math.Min(Track.ActualWidth, Track.ActualHeight) * Math.PI;

                for (var i = 0; i < _loadingBar.Children.Count; i++)
                {
                    var element = (FrameworkElement)_loadingBar.Children[_loadingBar.Children.Count - i - 1];
                    if (element != null)
                    {
                        var elementLength = Math.Max(element.Width, element.Height);

                        var index = (_loadingBar.Children.Count - 1) / 2 - i;

                        var endPosition = length / 2 + index * (elementLength * 2);
                        var endAngle = endPosition / length * 360.0;

                        var duration = new Duration(TimeSpan.FromSeconds(durationTime));

                        var firstCycleAnimation =
                            new DoubleAnimation(0.0, endAngle, duration) { BeginTime = TimeSpan.FromSeconds(i * beginTimeIncrement) };
                        Storyboard.SetTarget(firstCycleAnimation, element);
                        Storyboard.SetTargetProperty(firstCycleAnimation, new PropertyPath(AngleProperty));

                        var secondCycleAnimation =
                            new DoubleAnimation(0.0, endAngle, duration)
                                { BeginTime = TimeSpan.FromSeconds(partMotionTime + durationTime + i * beginTimeIncrement) };
                        Storyboard.SetTarget(secondCycleAnimation, element);
                        Storyboard.SetTargetProperty(secondCycleAnimation, new PropertyPath(AngleProperty));

                        firstCycleAnimations.Add(firstCycleAnimation);
                        secondCycleAnimations.Add(secondCycleAnimation);
                    }
                }

                for (var i = 0; i < _loadingBar.Children.Count; i++)
                {
                    var element = (FrameworkElement)_loadingBar.Children[_loadingBar.Children.Count - i - 1];
                    if (element != null)
                    {
                        var duration = new Duration(TimeSpan.FromSeconds(durationTime));

                        var firstCycleAnimation =
                            new DoubleAnimation(360.0, duration) { BeginTime = TimeSpan.FromSeconds(partMotionTime + i * beginTimeIncrement) };
                        Storyboard.SetTarget(firstCycleAnimation, element);
                        Storyboard.SetTargetProperty(firstCycleAnimation, new PropertyPath(AngleProperty));

                        var secondCycleAnimation =
                            new DoubleAnimation(360.0, duration)
                                { BeginTime = TimeSpan.FromSeconds(partMotionTime * 2 + durationTime + i * beginTimeIncrement) };
                        Storyboard.SetTarget(secondCycleAnimation, element);
                        Storyboard.SetTargetProperty(secondCycleAnimation, new PropertyPath(AngleProperty));

                        var moveAnimation =
                            new DoubleAnimation(-1.0, new Duration(TimeSpan.FromSeconds(0)))
                                { BeginTime = TimeSpan.FromSeconds(partMotionTime * 2 + durationTime * 2 + i * beginTimeIncrement) };
                        Storyboard.SetTarget(moveAnimation, element);
                        Storyboard.SetTargetProperty(moveAnimation, new PropertyPath(AngleProperty));

                        firstCycleAnimations.Add(firstCycleAnimation);
                        secondCycleAnimations.Add(secondCycleAnimation);
                        secondCycleAnimations.Add(moveAnimation);
                    }
                }

                LoadingAnimation.Duration = new Duration(TimeSpan.FromSeconds(longPauseTime + partMotionTime * 3 + shortPauseTime * 2 + durationTime));
                
                Contract.Assume(LoadingAnimation.Children != null);
                foreach (var animation in firstCycleAnimations)
                {
                    LoadingAnimation.Children.Add(animation);
                }

                foreach (var animation in secondCycleAnimations)
                {
                    LoadingAnimation.Children.Add(animation);
                }

                if (isStarted)
                {
                    LoadingAnimation.Begin(this, Template, true);
                }
            }
        }
    }
} ;