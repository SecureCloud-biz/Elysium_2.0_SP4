using System;
using System.Diagnostics.Contracts;
using System.Security;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

using ProgressBarAutomationPeer = Elysium.Theme.Controls.Automation.ProgressBarAutomationPeer;

namespace Elysium.Theme.Controls.Primitives
{
    [TemplatePart(Name = TrackName, Type = typeof(FrameworkElement))]
    [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Indeterminate", GroupName = "CommonStates")]
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
            get
            {
                var value = GetValue(PercentProperty);
                Contract.Assume(value != null);
                return (double)value;
            }
            private set { SetValue(PercentKey, value); }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();
            var progressBar = (ProgressBarBase)d;
            progressBar.Percent = progressBar.State != ProgressBarState.Normal || progressBar.Maximum <= progressBar.Minimum
                                      ? double.NaN
                                      : (progressBar.Value - progressBar.Minimum) / (progressBar.Maximum - progressBar.Minimum);
        }

        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(ProgressBarState), typeof(ProgressBarBase),
                                        new FrameworkPropertyMetadata(ProgressBarState.Normal, OnStateChanged));

        public ProgressBarState State
        {
            get
            {
                var value = GetValue(StateProperty);
                Contract.Assume(value != null);
                return (ProgressBarState)value;
            }
            set { SetValue(StateProperty, value); }
        }

        private static void OnStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();

            var progressBar = (ProgressBarBase)d;

            var peer = UIElementAutomationPeer.FromElement(progressBar) as ProgressBarAutomationPeer;
            if (peer != null)
            {
                peer.InvalidatePeer();
            }

            Contract.Assume(progressBar != null);
            if (progressBar.IsEnabled)
            {
                Contract.Assume(e.NewValue != null);
                switch ((ProgressBarState)e.NewValue)
                {
                    case ProgressBarState.Loading:
                        VisualStateManager.GoToState(progressBar, "Loading", true);
                        if (progressBar.IndeterminateAnimation != null)
                        {
                            progressBar.IndeterminateAnimation.Stop(progressBar);
                        }
                        if (progressBar.LoadingAnimation != null)
                        {
                            progressBar.LoadingAnimation.Begin(progressBar, progressBar.Template, true);
                        }
                        break;
                    case ProgressBarState.Indeterminate:
                        VisualStateManager.GoToState(progressBar, "Indeterminate", true);
                        if (progressBar.LoadingAnimation != null)
                        {
                            progressBar.LoadingAnimation.Stop(progressBar);
                        }
                        if (progressBar.IndeterminateAnimation != null)
                        {
                            progressBar.IndeterminateAnimation.Begin(progressBar, progressBar.Template, true);
                        }
                        break;
                    case ProgressBarState.Normal:
                        VisualStateManager.GoToState(progressBar, "Normal", true);
                        if (progressBar.IndeterminateAnimation != null)
                        {
                            progressBar.IndeterminateAnimation.Stop(progressBar);
                        }
                        if (progressBar.LoadingAnimation != null)
                        {
                            progressBar.LoadingAnimation.Stop(progressBar);
                        }
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
            if (Template != null)
            {
                Track = Template.FindName(TrackName, this) as FrameworkElement;

                if (Track != null)
                {
                    Track.SizeChanged += (sender, e) =>
                                             {
                                                 OnAnimationsUpdating(new RoutedEventArgs(AnimationsUpdatingEvent));
                                                 OnAnimationsUpdated(new RoutedEventArgs(AnimationsUpdatedEvent));
                                             };
                }
            }
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ProgressBarAutomationPeer(this);
        }
    }
} ;