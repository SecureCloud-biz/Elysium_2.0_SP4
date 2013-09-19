using System;
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

using ProgressAutomationPeer = Elysium.Controls.Automation.ProgressAutomationPeer;

namespace Elysium.Controls.Primitives
{
    [PublicAPI]
    [TemplatePart(Name = TrackName, Type = typeof(FrameworkElement))]
    public abstract class ProgressBase : RangeBase
    {
        private const string TrackName = "PART_Track";

        internal FrameworkElement Track { get; private set; }

        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "We need to use static constructor for custom actions during dependency properties initialization")]
        static ProgressBase()
        {
            MaximumProperty.OverrideMetadata(typeof(ProgressBase), new FrameworkPropertyMetadata(100d));
            Parameters.Animation.IsEnabledProperty.OverrideMetadata(typeof(ProgressBase), new FrameworkPropertyMetadata(OnAnimationIsEnabledChanged));
            Parameters.Animation.TypeProperty.OverrideMetadata(typeof(ProgressBase), new FrameworkPropertyMetadata(OnAnimationTypeChanged));
        }

        private static readonly DependencyPropertyKey PercentPropertyKey =
            DependencyProperty.RegisterReadOnly("Percent", typeof(double), typeof(ProgressBase), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.None, OnPercentChanged));

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
            ValidationHelper.NotNull(obj, "obj");
            var instance = (ProgressBase)obj;
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
            Percent = State != ProgressState.Normal || Maximum <= Minimum ? double.NaN : (Value - Minimum) / (Maximum - Minimum);
        }

        [PublicAPI]
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(ProgressState), typeof(ProgressBase), new FrameworkPropertyMetadata(ProgressState.Normal, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnStateChanged));

        [PublicAPI]
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines the state of control.")]
        public ProgressState State
        {
            get { return BoxingHelper<ProgressState>.Unbox(GetValue(StateProperty)); }
            set { SetValue(StateProperty, value); }
        }

        private static void OnStateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ValidationHelper.NotNull(obj, "obj");
            var progressBar = (ProgressBase)obj;
            progressBar.OnStateChanged(BoxingHelper<ProgressState>.Unbox(e.OldValue), BoxingHelper<ProgressState>.Unbox(e.NewValue));
        }

        [PublicAPI]
// ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnStateChanged(ProgressState oldState, ProgressState newState)
// ReSharper restore VirtualMemberNeverOverriden.Global
        {
            var peer = UIElementAutomationPeer.FromElement(this) as ProgressAutomationPeer;
            if (peer != null)
            {
                peer.InvalidatePeer();
            }

            OnAnimatedStateChanged(newState);
        }

        private static void OnAnimationTypeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ValidationHelper.NotNull(obj, "obj");
            var instance = obj as ProgressBase;
            if (instance != null)
            {
                instance.OnAnimatedStateChanged(instance.State);
            }
        }

        private static void OnAnimationIsEnabledChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ValidationHelper.NotNull(obj, "obj");
            var instance = obj as ProgressBase;
            if (instance != null)
            {
                instance.OnAnimatedStateChanged(instance.State);
            }
        }

        private void OnAnimatedStateChanged(ProgressState state)
        {
            switch (state)
            {
                case ProgressState.Busy:
                    VisualStateManager.GoToState(this, "Busy", true);
                    TryStopIndeterminateAnimation();
                    TryStartAnimation(BusyAnimation, value => IsBusyAnimationRunning = value);
                    break;
                case ProgressState.Indeterminate:
                    VisualStateManager.GoToState(this, "Indeterminate", true);
                    TryStopBusyAnimation();
                    TryStartAnimation(IndeterminateAnimation, value => IsIndeterminateAnimationRunning = value);
                    break;
                case ProgressState.Normal:
                    VisualStateManager.GoToState(this, "Normal", true);
                    TryStopIndeterminateAnimation();
                    TryStopBusyAnimation();
                    break;
            }
        }

        private void TryStartAnimation(Storyboard animation, Action<bool> setIsRunning)
        {
            if (animation != null && Parameters.Animation.GetIsEnabled(this) && Parameters.Animation.GetType(this) == Animation.Slide)
            {
                animation.Begin(this, Template, true);
                setIsRunning(true);
            }
        }

        private void TryStopIndeterminateAnimation()
        {
            TryStopAnimation(IndeterminateAnimation, () => IsIndeterminateAnimationRunning, value => IsIndeterminateAnimationRunning = value);
        }

        private void TryStopBusyAnimation()
        {
            TryStopAnimation(BusyAnimation, () => IsBusyAnimationRunning, value => IsBusyAnimationRunning = value);
        }

        private void TryStopAnimation(Storyboard animation, Func<bool> getIsRunning, Action<bool> setIsRunning)
        {
            if (animation != null && getIsRunning())
            {
                setIsRunning(false);
                animation.Stop(this);
            }
        }

        internal const string DefaultIndeterminateAnimationName = "CF98B9E7AB2F4CBD9EA654552441CD6A";

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "DependencyPropertyKey is immutable type")]
        [PublicAPI]
        protected static readonly DependencyPropertyKey IndeterminateAnimationPropertyKey =
            DependencyProperty.RegisterReadOnly("IndeterminateAnimation", typeof(Storyboard), typeof(ProgressBase), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        [PublicAPI]
        public static readonly DependencyProperty IndeterminateAnimationProperty = IndeterminateAnimationPropertyKey.DependencyProperty;

        [PublicAPI]
        public Storyboard IndeterminateAnimation
        {
            get { return (Storyboard)GetValue(IndeterminateAnimationProperty); }
            protected set { SetValue(IndeterminateAnimationPropertyKey, value); }
        }

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "DependencyPropertyKey is immutable type")]
        [PublicAPI]
        protected static readonly DependencyPropertyKey IsIndeterminateAnimationRunningPropertyKey =
            DependencyProperty.RegisterReadOnly("IsIndeterminateAnimationRunning", typeof(bool), typeof(ProgressBase), new FrameworkPropertyMetadata(BooleanBoxingHelper.FalseBox, FrameworkPropertyMetadataOptions.None));

        [PublicAPI]
        public static readonly DependencyProperty IsIndeterminateAnimationRunningProperty = IsIndeterminateAnimationRunningPropertyKey.DependencyProperty;

        [PublicAPI]
        [Browsable(false)]
        public bool IsIndeterminateAnimationRunning
        {
            get { return BooleanBoxingHelper.Unbox(GetValue(IsIndeterminateAnimationRunningProperty)); }
            protected set { SetValue(IsIndeterminateAnimationRunningPropertyKey, BooleanBoxingHelper.Box(value)); }
        }

        internal const string DefaultBusyAnimationName = "B45C62BF28AC49FDB8F172249BF56E5B";

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "DependencyPropertyKey is immutable type")]
        [PublicAPI]
        protected static readonly DependencyPropertyKey BusyAnimationPropertyKey =
            DependencyProperty.RegisterReadOnly("BusyAnimation", typeof(Storyboard), typeof(ProgressBase), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        [PublicAPI]
        public static readonly DependencyProperty BusyAnimationProperty = BusyAnimationPropertyKey.DependencyProperty;

        [PublicAPI]
        public Storyboard BusyAnimation
        {
            get { return (Storyboard)GetValue(BusyAnimationProperty); }
            protected set { SetValue(BusyAnimationPropertyKey, value); }
        }

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "DependencyPropertyKey is immutable type")]
        [PublicAPI]
        protected static readonly DependencyPropertyKey IsBusyAnimationRunningPropertyKey =
            DependencyProperty.RegisterReadOnly("IsBusyAnimationRunning", typeof(bool), typeof(ProgressBase), new FrameworkPropertyMetadata(BooleanBoxingHelper.FalseBox, FrameworkPropertyMetadataOptions.None));

        [PublicAPI]
        public static readonly DependencyProperty IsBusyAnimationRunningProperty = IsBusyAnimationRunningPropertyKey.DependencyProperty;

        [PublicAPI]
        [Browsable(false)]
        public bool IsBusyAnimationRunning
        {
            get { return BooleanBoxingHelper.Unbox(GetValue(IsBusyAnimationRunningProperty)); }
            protected set { SetValue(IsBusyAnimationRunningPropertyKey, BooleanBoxingHelper.Box(value)); }
        }

        internal const double Magic = 11d;
        private const double Time = 0.25d;
        internal const double DurationTime = Time * 2;
        internal const double BeginTimeIncrement = Time / 2;
        internal const double ShortPauseTime = Time;
        internal const double LongPauseTime = Time * 1.5;
        internal const double Turn = 360d;

        [PublicAPI]
        // ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnAnimationsUpdating(RoutedEventArgs e)
        // ReSharper restore VirtualMemberNeverOverriden.Global
        {
            RaiseEvent(e);
        }

        [PublicAPI]
        // ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnAnimationsUpdated(RoutedEventArgs e)
        // ReSharper restore VirtualMemberNeverOverriden.Global
        {
            RaiseEvent(e);
        }

        [SecuritySafeCritical]
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            OnApplyTemplateInternal();
        }

        [SecurityCritical]
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
                        OnAnimationsUpdating(new RoutedEventArgs(Parameters.Animation.AnimationsUpdatingEvent));
                        OnAnimationsUpdated(new RoutedEventArgs(Parameters.Animation.AnimationsUpdatedEvent));
                    };
                }
            }
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ProgressAutomationPeer(this);
        }
    }
}