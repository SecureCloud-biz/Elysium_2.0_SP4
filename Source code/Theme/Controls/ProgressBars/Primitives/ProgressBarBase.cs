using System.Security;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

using ProgressBarAutomationPeer = Elysium.Theme.Controls.Automation.ProgressBarAutomationPeer;

namespace Elysium.Theme.Controls.Primitives
{
    [TemplatePart(Name = TrackName, Type = typeof(FrameworkElement))]
    [TemplateVisualState(Name = "Indeterminate", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Determinate", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Loading", GroupName = "CommonStates")]
    public abstract class ProgressBarBase : RangeBase
    {
        private const string TrackName = "PART_Track";

        protected FrameworkElement Track;

        static ProgressBarBase()
        {
            ValueProperty.OverrideMetadata(typeof(ProgressBarBase), new FrameworkPropertyMetadata(OnValueChanged));
            MaximumProperty.OverrideMetadata(typeof(ProgressBarBase), new FrameworkPropertyMetadata(100.0));
        }

        private static readonly DependencyPropertyKey PercentKey =
            DependencyProperty.RegisterReadOnly("Percent", typeof(double), typeof(ProgressBarBase), new FrameworkPropertyMetadata(0.0));

        public static readonly DependencyProperty PercentProperty = PercentKey.DependencyProperty;

        public double Percent
        {
            get { return (double)GetValue(PercentProperty); }
            private set { SetValue(PercentKey, value); }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var progressBar = (ProgressBarBase)d;

            progressBar.Percent = progressBar.IsIndeterminate != false || progressBar.Maximum <= progressBar.Minimum
                                      ? double.NaN
                                      : (progressBar.Value - progressBar.Minimum) / (progressBar.Maximum - progressBar.Minimum);
        }

        public static readonly DependencyProperty IsIndeterminateProperty =
            DependencyProperty.Register("IsIndeterminate", typeof(bool?), typeof(ProgressBarBase),
                                        new FrameworkPropertyMetadata(false, OnIsIndeterminateChanged));

        public bool? IsIndeterminate
        {
            get { return (bool?)GetValue(IsIndeterminateProperty); }
            set { SetValue(IsIndeterminateProperty, value); }
        }

        private static void OnIsIndeterminateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var progressBar = (ProgressBarBase)d;

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

        protected const string DefaultIndeterminateAnimationName = "DefaultIndeterminateAnimation";

        public static readonly DependencyProperty IndeterminateAnimationProperty =
            DependencyProperty.Register("IndeterminateAnimation", typeof(Storyboard), typeof(ProgressBarBase),
                                        new FrameworkPropertyMetadata(
                                            new Storyboard { Name = DefaultIndeterminateAnimationName, RepeatBehavior = RepeatBehavior.Forever },
                                            FrameworkPropertyMetadataOptions.AffectsRender));

        public Storyboard IndeterminateAnimation
        {
            get { return (Storyboard)GetValue(IndeterminateAnimationProperty); }
            set { SetValue(IndeterminateAnimationProperty, value); }
        }

        protected const string DefaultLoadingAnimationName = "DefaultLoadingAnimation";

        public static readonly DependencyProperty LoadingAnimationProperty =
            DependencyProperty.Register("LoadingAnimation", typeof(Storyboard), typeof(ProgressBarBase),
                                        new FrameworkPropertyMetadata(
                                            new Storyboard { Name = DefaultLoadingAnimationName, RepeatBehavior = RepeatBehavior.Forever },
                                            FrameworkPropertyMetadataOptions.AffectsRender));

        public Storyboard LoadingAnimation
        {
            get { return (Storyboard)GetValue(LoadingAnimationProperty); }
            set { SetValue(LoadingAnimationProperty, value); }
        }

        public static readonly RoutedEvent AnimationsUpdatingEvent = EventManager.RegisterRoutedEvent("AnimationsUpdating", RoutingStrategy.Tunnel,
                                                                                                      typeof(RoutedEventHandler), typeof(ProgressBarBase));

        public event RoutedEventHandler AnimationsUpdating
        {
            add { AddHandler(AnimationsUpdatingEvent, value); }
            remove { RemoveHandler(AnimationsUpdatingEvent, value); }
        }

        protected virtual void OnAnimationsUpdating(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        public static readonly RoutedEvent AnimationsUpdatedEvent = EventManager.RegisterRoutedEvent("AnimationsUpdated", RoutingStrategy.Bubble,
                                                                                                     typeof(RoutedEventHandler), typeof(ProgressBarBase));

        public event RoutedEventHandler AnimationsUpdated
        {
            add { AddHandler(AnimationsUpdatedEvent, value); }
            remove { RemoveHandler(AnimationsUpdatedEvent, value); }
        }

        protected virtual void OnAnimationsUpdated(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            OnApplyTemplateInternal();
        }

        [SecuritySafeCritical]
        internal virtual void OnApplyTemplateInternal()
        {
            Track = GetTemplateChild(TrackName) as FrameworkElement;

            if (Track != null)
            {
                Track.SizeChanged += (sender, e) =>
                                         {
                                             OnAnimationsUpdating(new RoutedEventArgs(AnimationsUpdatingEvent));
                                             OnAnimationsUpdated(new RoutedEventArgs(AnimationsUpdatedEvent));
                                         };
            }
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ProgressBarAutomationPeer(this);
        }
    }
} ;