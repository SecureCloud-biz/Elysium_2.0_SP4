using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace Elysium.Theme.WPF.Controls
{
    public class ProgressBarAutomationPeer : RangeBaseAutomationPeer, IRangeValueProvider
    {
        public ProgressBarAutomationPeer(ProgressBar owner) : base(owner)
        {
        }

        protected override string GetClassNameCore()
        {
            return "ProgressBar";
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.ProgressBar;
        }

        public override object GetPattern(PatternInterface patternInterface)
        {
            var isIndeterminate = ((ProgressBar)Owner).IsIndeterminate;
            if (isIndeterminate != null && (patternInterface == PatternInterface.RangeValue && isIndeterminate.Value)) return null;

            return base.GetPattern(patternInterface);
        }

        void IRangeValueProvider.SetValue(double val)
        {
            throw new InvalidOperationException("ProgressBar is read-only");
        }

        bool IRangeValueProvider.IsReadOnly
        {
            get { return true; }
        }

        double IRangeValueProvider.LargeChange
        {
            get { return double.NaN; }
        }

        double IRangeValueProvider.SmallChange
        {
            get { return double.NaN; }
        }
    }

    [TemplatePart(Name = TrackName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = IndicatorName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = LoadingBarName, Type = typeof(Canvas))]
    [TemplateVisualState(Name = "Indeterminate", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Determinate", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Loading", GroupName = "CommonStates")]
    public class ProgressBar : RangeBase
    {
        private const string TrackName = "PART_Track";
        private const string IndicatorName = "PART_Indicator";
        private const string LoadingBarName = "PART_LoadingBar";

        private FrameworkElement _track;
        private FrameworkElement _indicator;
        private Canvas _loadingBar;

        static ProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressBar), new FrameworkPropertyMetadata(typeof(ProgressBar)));
            ValueProperty.OverrideMetadata(typeof(ProgressBar), new FrameworkPropertyMetadata(OnValueChanged));
            MaximumProperty.OverrideMetadata(typeof(ProgressBar), new FrameworkPropertyMetadata(100.0));
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ProgressBarAutomationPeer(this);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _track = GetTemplateChild(TrackName) as FrameworkElement;
            _indicator = GetTemplateChild(IndicatorName) as FrameworkElement;
            _loadingBar = GetTemplateChild(LoadingBarName) as Canvas;

            if (_track != null)
            {
                _track.SizeChanged += (sender, e) => UpdateAnimations();
            }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var progressBar = (ProgressBar)d;

            progressBar.Percent = progressBar.IsIndeterminate != false || progressBar.Maximum <= progressBar.Minimum
                                      ? double.NaN
                                      : (progressBar.Value - progressBar.Minimum) / (progressBar.Maximum - progressBar.Minimum);
        }

        private static readonly DependencyPropertyKey PercentKey =
            DependencyProperty.RegisterReadOnly("Percent", typeof(double), typeof(ProgressBar), new FrameworkPropertyMetadata(0.0));

        public static readonly DependencyProperty PercentProperty = PercentKey.DependencyProperty;

        public double Percent
        {
            get { return (double)GetValue(PercentProperty); }
            private set { SetValue(PercentKey, value); }
        }

        public static readonly DependencyProperty IsIndeterminateProperty =
            DependencyProperty.Register("IsIndeterminate", typeof(bool?), typeof(ProgressBar), new FrameworkPropertyMetadata(false, OnIsIndeterminateChanged));

        public bool? IsIndeterminate
        {
            get { return (bool?)GetValue(IsIndeterminateProperty); }
            set { SetValue(IsIndeterminateProperty, value); }
        }

        private static void OnIsIndeterminateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var progressBar = (ProgressBar)d;

            var peer = UIElementAutomationPeer.FromElement(progressBar) as ProgressBarAutomationPeer;
            if (peer != null)
            {
                peer.InvalidatePeer();
            }

            if (progressBar.IsEnabled)
            {
                switch ((bool?)e.NewValue)
                {
                    case null:
                        VisualStateManager.GoToState(progressBar, "Loading", true);
                        progressBar.IndeterminateAnimation.Stop(progressBar);
                        progressBar.LoadingAnimation.Begin(progressBar, progressBar.Template, true);
                        break;
                    case true:
                        VisualStateManager.GoToState(progressBar, "Indeterminate", true);
                        progressBar.LoadingAnimation.Stop(progressBar);
                        progressBar.IndeterminateAnimation.Begin(progressBar, progressBar.Template, true);
                        break;
                    case false:
                        VisualStateManager.GoToState(progressBar, "Determinate", true);
                        progressBar.IndeterminateAnimation.Stop(progressBar);
                        progressBar.LoadingAnimation.Stop(progressBar);
                        break;
                }
            }
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ProgressBar),
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

        private const string DefaultIndeterminateAnimationName = "DefaultIndeterminateAnimation";

        public static readonly DependencyProperty IndeterminateAnimationProperty =
            DependencyProperty.Register("IndeterminateAnimation", typeof(Storyboard), typeof(ProgressBar),
                                        new UIPropertyMetadata(new Storyboard
                                                                   { Name = DefaultIndeterminateAnimationName, RepeatBehavior = RepeatBehavior.Forever }));

        public Storyboard IndeterminateAnimation
        {
            get { return (Storyboard)GetValue(IndeterminateAnimationProperty); }
            set { SetValue(IndeterminateAnimationProperty, value); }
        }

        private const string DefaultLoadingAnimationName = "DefaultLoadingAnimation";

        public static readonly DependencyProperty LoadingAnimationProperty =
            DependencyProperty.Register("LoadingAnimation", typeof(Storyboard), typeof(ProgressBar),
                                        new UIPropertyMetadata(new Storyboard
                                                                   { Name = DefaultLoadingAnimationName, RepeatBehavior = RepeatBehavior.Forever }));

        public Storyboard LoadingAnimation
        {
            get { return (Storyboard)GetValue(LoadingAnimationProperty); }
            set { SetValue(LoadingAnimationProperty, value); }
        }

        protected virtual void UpdateAnimations()
        {
            if (IndeterminateAnimation != null && IndeterminateAnimation.Name == DefaultIndeterminateAnimationName && _track != null && _indicator != null)
            {
                var isStarted = IsIndeterminate == true;
                if (isStarted)
                {
                    IndeterminateAnimation.Stop(this);
                    IndeterminateAnimation.Remove(this);
                }

                IndeterminateAnimation = new Storyboard { Name = DefaultIndeterminateAnimationName, RepeatBehavior = RepeatBehavior.Forever };

                var indicatorSize = Orientation == Orientation.Horizontal ? _indicator.ActualWidth : _indicator.ActualHeight;

                var toStartAnimation = new DoubleAnimation(-(indicatorSize + 1), new Duration(TimeSpan.Parse("00:00:00.0")));

                Storyboard.SetTarget(toStartAnimation, _indicator);
                Storyboard.SetTargetProperty(toStartAnimation,
                                             new PropertyPath(Orientation == Orientation.Horizontal ? Canvas.LeftProperty : Canvas.TopProperty));

                var trackSize = Orientation == Orientation.Horizontal ? _track.ActualWidth : _track.ActualHeight;

                var time = trackSize / 100;

                var motionAnimation = new DoubleAnimationUsingKeyFrames { Duration = new Duration(TimeSpan.FromSeconds(time + 0.5)) };
                motionAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(trackSize + 1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(time))));

                Storyboard.SetTarget(motionAnimation, _indicator);
                Storyboard.SetTargetProperty(motionAnimation, new PropertyPath(Orientation == Orientation.Horizontal ? Canvas.LeftProperty : Canvas.TopProperty));

                IndeterminateAnimation.Children.Add(toStartAnimation);
                IndeterminateAnimation.Children.Add(motionAnimation);

                if (isStarted) IndeterminateAnimation.Begin(this, Template, true);
            }

            if (LoadingAnimation != null && LoadingAnimation.Name == DefaultLoadingAnimationName && _track != null && _loadingBar != null)
            {
                var isStarted = IsIndeterminate == null;
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

                for (var i = 0; i < _loadingBar.Children.Count; i++)
                {
                    var element = _loadingBar.Children[_loadingBar.Children.Count - i - 1] as FrameworkElement;

                    var index = (_loadingBar.Children.Count - 1) / 2 - i;

                    var center = (Orientation == Orientation.Horizontal ? _track.ActualWidth : _track.ActualHeight) / 2;
                    var margin = Orientation == Orientation.Horizontal ? element.Width : element.Height;
                    var startPosition = (Orientation == Orientation.Horizontal ? element.Width : element.Height) - 1;
                    var endPosition = center + index * ((Orientation == Orientation.Horizontal ? element.Width : element.Height) + margin);

                    var duration = new Duration(TimeSpan.FromSeconds(durationTime));
                    var animation = new DoubleAnimation(startPosition, endPosition, duration)
                                        { BeginTime = TimeSpan.FromSeconds(i * beginTimeIncrement) };
                    Storyboard.SetTarget(animation, element);
                    Storyboard.SetTargetProperty(animation, new PropertyPath(Orientation == Orientation.Horizontal ? Canvas.LeftProperty : Canvas.TopProperty));
                    loadingAnimations.Add(animation);
                }

                for (int i = 0; i < _loadingBar.Children.Count; i++)
                {
                    var element = _loadingBar.Children[_loadingBar.Children.Count - i - 1] as FrameworkElement;

                    var endPosition = (Orientation == Orientation.Horizontal ? _track.ActualWidth : _track.ActualHeight) +
                                      (Orientation == Orientation.Horizontal ? element.Width : element.Height) + 1;

                    var duration = new Duration(TimeSpan.FromSeconds(durationTime));
                    var animation = new DoubleAnimation(endPosition, duration)
                                        { BeginTime = TimeSpan.FromSeconds(partMotionTime + shortPauseTime + i * beginTimeIncrement) };
                    Storyboard.SetTarget(animation, element);
                    Storyboard.SetTargetProperty(animation, new PropertyPath(Orientation == Orientation.Horizontal ? Canvas.LeftProperty : Canvas.TopProperty));
                    loadingAnimations.Add(animation);
                }

                LoadingAnimation.Duration =
                    new Duration(TimeSpan.FromSeconds(partMotionTime * 2 + shortPauseTime + longPauseTime));

                foreach (var animation in loadingAnimations)
                {
                    LoadingAnimation.Children.Add(animation);
                }

                if (isStarted) LoadingAnimation.Begin(this, Template, true);
            }
        }
    }
} ;