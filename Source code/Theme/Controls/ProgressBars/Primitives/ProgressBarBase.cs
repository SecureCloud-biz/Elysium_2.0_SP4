using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Security;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

using Elysium.Extensions;

using JetBrains.Annotations;

using ProgressBarAutomationPeer = Elysium.Controls.Automation.ProgressBarAutomationPeer;

namespace Elysium.Controls.Primitives
{
    [PublicAPI]
    [TemplatePart(Name = TrackName, Type = typeof(FrameworkElement))]
    public abstract class ProgressBarBase : RangeBase
    {
        private const string TrackName = "PART_Track";

        internal FrameworkElement Track;

        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static ProgressBarBase()
        {
            MaximumProperty.OverrideMetadata(typeof(ProgressBarBase), new FrameworkPropertyMetadata(100d));
        }

        private static readonly DependencyPropertyKey PercentPropertyKey =
            DependencyProperty.RegisterReadOnly("Percent", typeof(double), typeof(ProgressBarBase),
                                                new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.None, OnPercentChanged));

        [PublicAPI]
        public static readonly DependencyProperty PercentProperty = PercentPropertyKey.DependencyProperty;

        [PublicAPI]
        [Browsable(false)]
        public double Percent
        {
            get { return BoxingHelper<double>.Unbox(GetValue(PercentProperty)); }
            private set { SetValue(PercentPropertyKey, value); }
        }

        private static void OnPercentChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ValidationHelper.NotNull(obj, () => obj);
            var instance = (ProgressBarBase)obj;
            instance.OnPercentChanged(BoxingHelper<double>.Unbox(e.OldValue), BoxingHelper<double>.Unbox(e.NewValue));
        }

        [PublicAPI]
// ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnPercentChanged(double oldPercent, double newPercent)
// ReSharper restore VirtualMemberNeverOverriden.Global
        {
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            Percent = State != ProgressBarState.Normal || Maximum <= Minimum ? double.NaN : (Value - Minimum) / (Maximum - Minimum);
        }

        [PublicAPI]
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(ProgressBarState), typeof(ProgressBarBase),
                                        new FrameworkPropertyMetadata(ProgressBarState.Normal, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnStateChanged));

        [PublicAPI]
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines the state of control.")]
        public ProgressBarState State
        {
            get { return BoxingHelper<ProgressBarState>.Unbox(GetValue(StateProperty)); }
            set { SetValue(StateProperty, value); }
        }

        private static void OnStateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ValidationHelper.NotNull(obj, () => obj);
            var progressBar = (ProgressBarBase)obj;
            progressBar.OnStateChanged(BoxingHelper<ProgressBarState>.Unbox(e.OldValue), BoxingHelper<ProgressBarState>.Unbox(e.NewValue));
        }

        [PublicAPI]
// ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnStateChanged(ProgressBarState oldState, ProgressBarState newState)
// ReSharper restore VirtualMemberNeverOverriden.Global
        {
            var peer = UIElementAutomationPeer.FromElement(this) as ProgressBarAutomationPeer;
            if (peer != null)
            {
                peer.InvalidatePeer();
            }

            if (IsEnabled)
            {
                switch (newState)
                {
                    case ProgressBarState.Busy:
                        VisualStateManager.GoToState(this, "Busy", true);
                        if (IndeterminateAnimation != null)
                        {
                            IndeterminateAnimation.Stop(this);
                        }
                        if (BusyAnimation != null)
                        {
                            BusyAnimation.Begin(this, Template, true);
                        }
                        break;
                    case ProgressBarState.Indeterminate:
                        VisualStateManager.GoToState(this, "Indeterminate", true);
                        if (BusyAnimation != null)
                        {
                            BusyAnimation.Stop(this);
                        }
                        if (IndeterminateAnimation != null)
                        {
                            IndeterminateAnimation.Begin(this, Template, true);
                        }
                        break;
                    case ProgressBarState.Normal:
                        VisualStateManager.GoToState(this, "Normal", true);
                        if (IndeterminateAnimation != null)
                        {
                            IndeterminateAnimation.Stop(this);
                        }
                        if (BusyAnimation != null)
                        {
                            BusyAnimation.Stop(this);
                        }
                        break;
                }
            }
        }

        internal const string DefaultIndeterminateAnimationName = "CF98B9E7AB2F4CBD9EA654552441CD6A";

        [PublicAPI]
        public static readonly DependencyProperty IndeterminateAnimationProperty =
            DependencyProperty.Register("IndeterminateAnimation", typeof(Storyboard), typeof(ProgressBarBase),
                                        new FrameworkPropertyMetadata(
                                            new Storyboard { Name = DefaultIndeterminateAnimationName, RepeatBehavior = RepeatBehavior.Forever },
                                            FrameworkPropertyMetadataOptions.AffectsRender));

        [PublicAPI]
        [Category("Appearance")]
        [Description("Determines the animation that playing in Indeterminate state.")]
        public Storyboard IndeterminateAnimation
        {
            get { return (Storyboard)GetValue(IndeterminateAnimationProperty); }
            set { SetValue(IndeterminateAnimationProperty, value); }
        }

        internal const string DefaultBusyAnimationName = "B45C62BF28AC49FDB8F172249BF56E5B";

        [PublicAPI]
        public static readonly DependencyProperty BusyAnimationProperty =
            DependencyProperty.Register("BusyAnimation", typeof(Storyboard), typeof(ProgressBarBase),
                                        new FrameworkPropertyMetadata(
                                            new Storyboard { Name = DefaultBusyAnimationName, RepeatBehavior = RepeatBehavior.Forever },
                                            FrameworkPropertyMetadataOptions.AffectsRender));

        [PublicAPI]
        [Category("Appearance")]
        [Description("Determines the animation that playing in Busy state.")]
        public Storyboard BusyAnimation
        {
            get { return (Storyboard)GetValue(BusyAnimationProperty); }
            set { SetValue(BusyAnimationProperty, value); }
        }

        [PublicAPI]
        public static readonly RoutedEvent AnimationsUpdatingEvent = EventManager.RegisterRoutedEvent("AnimationsUpdating", RoutingStrategy.Tunnel,
                                                                                                      typeof(RoutedEventHandler), typeof(ProgressBarBase));

        [PublicAPI]
        [Category("Appearance")]
        [Description("Occurs when a state's animations updating.")]
        public event RoutedEventHandler AnimationsUpdating
        {
            add { AddHandler(AnimationsUpdatingEvent, value); }
            remove { RemoveHandler(AnimationsUpdatingEvent, value); }
        }

        [PublicAPI]
// ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnAnimationsUpdating(RoutedEventArgs e)
// ReSharper restore VirtualMemberNeverOverriden.Global
        {
            RaiseEvent(e);
        }

        [PublicAPI]
        public static readonly RoutedEvent AnimationsUpdatedEvent = EventManager.RegisterRoutedEvent("AnimationsUpdated", RoutingStrategy.Bubble,
                                                                                                     typeof(RoutedEventHandler), typeof(ProgressBarBase));

        [PublicAPI]
        [Category("Appearance")]
        [Description("Occurs when a state's animations updated.")]
        public event RoutedEventHandler AnimationsUpdated
        {
            add { AddHandler(AnimationsUpdatedEvent, value); }
            remove { RemoveHandler(AnimationsUpdatedEvent, value); }
        }

        [PublicAPI]
// ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnAnimationsUpdated(RoutedEventArgs e)
// ReSharper restore VirtualMemberNeverOverriden.Global
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

                if (Track == null)
                {
                    Trace.TraceWarning(TrackName + " not found.");
                }
                else
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