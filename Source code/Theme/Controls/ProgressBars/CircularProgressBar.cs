using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

using Elysium.Theme.Extensions;

using JetBrains.Annotations;

using Microsoft.Expression.Shapes;

using Elysium.Theme.Controls.Primitives;

namespace Elysium.Theme.Controls
{
    [PublicAPI]
    [TemplatePart(Name = ArcName, Type = typeof(Arc))]
    [TemplatePart(Name = BusyBarName, Type = typeof(Canvas))]
    public class CircularProgressBar : ProgressBarBase
    {
        private const string ArcName = "PART_Arc";
        private const string BusyBarName = "PART_BusyBar";

        private Arc _arc;
        private Canvas _busyBar;

        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static CircularProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircularProgressBar), new FrameworkPropertyMetadata(typeof(CircularProgressBar)));
        }

        [PublicAPI]
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.RegisterAttached("Angle", typeof(double), typeof(CircularProgressBar),
                                                new FrameworkPropertyMetadata(-1.0, FrameworkPropertyMetadataOptions.AffectsArrange));

        [PublicAPI]
        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        public static double GetAngle(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(AngleProperty));
        }

        [PublicAPI]
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
            Contract.Assume(!desiredSize.IsEmpty);
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
                // NOTE: WPF doesn't declare contracts
                Contract.Assume(Template != null);
                _busyBar = Template.FindName(BusyBarName, this) as Canvas;
            }

            base.OnApplyTemplateInternal();
        }

        protected override void OnAnimationsUpdating(RoutedEventArgs e)
        {
            base.OnAnimationsUpdating(e);

            OnAnimationsUpdatingInternal();
        }

        [SecuritySafeCritical]
        private void OnAnimationsUpdatingInternal()
        {
            UpdateIndeterminateAnimation();

            UpdateBusyAnimation();
        }

        [SecurityCritical]
        private void UpdateIndeterminateAnimation()
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

                var startAngleSetValueAnimation = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.0)));

                Storyboard.SetTarget(startAngleSetValueAnimation, _arc);
                Storyboard.SetTargetProperty(startAngleSetValueAnimation, new PropertyPath(Arc.StartAngleProperty));

                var endAngleSetValueAnimation = new DoubleAnimation(-270, new Duration(TimeSpan.FromSeconds(0.0)));

                Storyboard.SetTarget(endAngleSetValueAnimation, _arc);
                Storyboard.SetTargetProperty(endAngleSetValueAnimation, new PropertyPath(Arc.EndAngleProperty));

                var startAngleAnimation = new DoubleAnimationUsingKeyFrames();
                // NOTE: WPF doesn't declare contracts
                Contract.Assume(startAngleAnimation.KeyFrames != null);
                startAngleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(360, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(time))));

                Storyboard.SetTarget(startAngleAnimation, _arc);
                Storyboard.SetTargetProperty(startAngleAnimation, new PropertyPath(Arc.StartAngleProperty));

                var endAngleAnimation = new DoubleAnimationUsingKeyFrames();
                // NOTE: WPF doesn't declare contracts
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
        }

        [SecurityCritical]
        private void UpdateBusyAnimation()
        {
            if (BusyAnimation != null && BusyAnimation.Name == DefaultBusyAnimationName && Track != null && _busyBar != null)
            {
                var isStarted = State == ProgressBarState.Busy && IsEnabled;
                if (isStarted)
                {
                    BusyAnimation.Stop(this);
                    BusyAnimation.Remove(this);
                }

                // NOTE: WPF doesn't declare contracts
                Contract.Assume(_busyBar.Children != null);

                BusyAnimation = new Storyboard { Name = DefaultBusyAnimationName, RepeatBehavior = RepeatBehavior.Forever };

                var firstCycleAnimations = new Collection<DoubleAnimation>();
                var secondCycleAnimations = new Collection<DoubleAnimation>();

                const double time = 0.25;
                const double durationTime = time * 2;
                const double beginTimeIncrement = time / 2;
                const double shortPauseTime = time;
                const double longPauseTime = time * 1.5;
                var partMotionTime = (_busyBar.Children.Count - 1) * beginTimeIncrement + durationTime + shortPauseTime;

                var length = Math.Min(Track.ActualWidth, Track.ActualHeight) * Math.PI;

                for (var i = 0; i < _busyBar.Children.Count; i++)
                {
                    var element = (FrameworkElement)_busyBar.Children[_busyBar.Children.Count - i - 1];
                    if (element != null)
                    {
                        var elementLength = Math.Max(element.Width, element.Height);

                        var index = (_busyBar.Children.Count - 1) / 2 - i;

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

                for (var i = 0; i < _busyBar.Children.Count; i++)
                {
                    var element = (FrameworkElement)_busyBar.Children[_busyBar.Children.Count - i - 1];
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

                BusyAnimation.Duration = new Duration(TimeSpan.FromSeconds(longPauseTime + partMotionTime * 3 + shortPauseTime * 2 + durationTime));

                // NOTE: WPF doesn't declare contracts
                Contract.Assume(BusyAnimation.Children != null);
                foreach (var animation in firstCycleAnimations)
                {
                    BusyAnimation.Children.Add(animation);
                }

                foreach (var animation in secondCycleAnimations)
                {
                    BusyAnimation.Children.Add(animation);
                }

                if (isStarted)
                {
                    BusyAnimation.Begin(this, Template, true);
                }
            }
        }
    }
} ;