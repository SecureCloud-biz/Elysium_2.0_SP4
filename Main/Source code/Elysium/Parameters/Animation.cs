using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Animation;

using Elysium.Extensions;
using JetBrains.Annotations;

namespace Elysium.Parameters
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public static class Animation
    {
        #region Base

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(Animation),
                                                new FrameworkPropertyMetadata(BooleanBoxingHelper.FalseBox,
                                                                              FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                              FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits,
                                                                              null, CoerceIsEnabled));

        public static bool GetIsEnabled(UIElement obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            return BooleanBoxingHelper.Unbox(obj.GetValue(IsEnabledProperty));
        }

        public static void SetIsEnabled(UIElement obj, bool value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(IsEnabledProperty, BooleanBoxingHelper.Box(value));
        }

        private static object CoerceIsEnabled(DependencyObject obj, object baseValue)
        {
            ValidationHelper.NotNull(obj, "obj");
            ValidationHelper.OfType(obj, "obj", typeof(UIElement));
            var instance = (UIElement)obj;
            return BooleanBoxingHelper.Unbox(baseValue) && GetAnimation(instance) != null;
        }

        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.RegisterAttached("Type", typeof(Elysium.Animation), typeof(Animation),
                                                new FrameworkPropertyMetadata(Elysium.Animation.Custom,
                                                                              FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                                              null, AnimationUtil.CoerceValid));

        public static Elysium.Animation GetType(UIElement obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            AnimationUtil.EnsureValid(obj);
            return BoxingHelper<Elysium.Animation>.Unbox(obj.GetValue(TypeProperty));
        }

        public static void SetType(UIElement obj, Elysium.Animation value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(TypeProperty, value);
        }

        public static readonly DependencyProperty SupportedProperty =
            DependencyProperty.RegisterAttached("Supported", typeof(Elysium.Animation), typeof(Animation),
                                                new FrameworkPropertyMetadata(Elysium.Animation.None, FrameworkPropertyMetadataOptions.None, OnSupportedChanged));

        public static Elysium.Animation GetSupported(UIElement obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            return BoxingHelper<Elysium.Animation>.Unbox(obj.GetValue(SupportedProperty));
        }

        public static void SetSupported(UIElement obj, Elysium.Animation value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(SupportedProperty, value);
        }

        private static void OnSupportedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ValidationHelper.NotNull(obj, "obj");
            ValidationHelper.OfType(obj, "obj", typeof(UIElement));
            var instance = (UIElement)obj;
            instance.CoerceValue(TypeProperty);
        }

        public static readonly DependencyProperty AnimationProperty =
            DependencyProperty.RegisterAttached("Animation", typeof(Storyboard), typeof(Animation),
                                                new FrameworkPropertyMetadata(null,
                                                                              FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                                              OnAnimationChanged));

        public static Storyboard GetAnimation(UIElement obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            return (Storyboard)obj.GetValue(AnimationProperty);
        }

        public static void SetAnimation(UIElement obj, Storyboard value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(AnimationProperty, value);
        }

        private static void OnAnimationChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ValidationHelper.NotNull(obj, "obj");
            ValidationHelper.OfType(obj, "obj", typeof(UIElement));
            var instance = (UIElement)obj;
            instance.CoerceValue(IsEnabledProperty);
        }

        #endregion

        #region Duration

        [PublicAPI]
        public static readonly DependencyProperty DefaultDurationProperty =
            DependencyProperty.RegisterAttached("DefaultDuration", typeof(Duration), typeof(Animation), new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(0d)), FrameworkPropertyMetadataOptions.Inherits));

        [PublicAPI]
        public static Duration GetDefaultDuration([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            return BoxingHelper<Duration>.Unbox(obj.GetValue(DefaultDurationProperty));
        }

        [PublicAPI]
        public static void SetDefaultDuration([NotNull] DependencyObject obj, Duration value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(DefaultDurationProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty MinimumDurationProperty =
            DependencyProperty.RegisterAttached("MinimumDuration", typeof(Duration), typeof(Animation), new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(0.2d)), FrameworkPropertyMetadataOptions.Inherits));

        [PublicAPI]
        public static Duration GetMinimumDuration([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            return BoxingHelper<Duration>.Unbox(obj.GetValue(MinimumDurationProperty));
        }

        [PublicAPI]
        public static void SetMinimumDuration([NotNull] DependencyObject obj, Duration value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(MinimumDurationProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty OptimumDurationProperty =
            DependencyProperty.RegisterAttached("OptimumDuration", typeof(Duration), typeof(Animation), new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(0.5d)), FrameworkPropertyMetadataOptions.Inherits));

        [PublicAPI]
        public static Duration GetOptimumDuration([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            return BoxingHelper<Duration>.Unbox(obj.GetValue(OptimumDurationProperty));
        }

        [PublicAPI]
        public static void SetOptimumDuration([NotNull] DependencyObject obj, Duration value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(OptimumDurationProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty MaximumDurationProperty =
            DependencyProperty.RegisterAttached("MaximumDuration", typeof(Duration), typeof(Animation), new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(1.0d)), FrameworkPropertyMetadataOptions.Inherits));

        [PublicAPI]
        public static Duration GetMaximumDuration([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, "obj");
            return BoxingHelper<Duration>.Unbox(obj.GetValue(MaximumDurationProperty));
        }

        [PublicAPI]
        public static void SetMaximumDuration([NotNull] DependencyObject obj, Duration value)
        {
            ValidationHelper.NotNull(obj, "obj");
            obj.SetValue(MaximumDurationProperty, value);
        }

        #endregion

        #region Events

        public static readonly RoutedEvent AnimationsUpdatingEvent = EventManager.RegisterRoutedEvent("AnimationsUpdating", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(Animation));

        [Category("Appearance")]
        [Description("Occurs when a control's animations updating.")]
        public static void AddAnimationsUpdatingHandler(UIElement obj, RoutedEventHandler handler)
        {
            ValidationHelper.NotNull(obj, "obj");
            ValidationHelper.NotNull(handler, "handler");
            obj.AddHandler(AnimationsUpdatingEvent, handler);
        }

        public static void RemoveAnimationsUpdatingHandler(UIElement obj, RoutedEventHandler handler)
        {
            ValidationHelper.NotNull(obj, "obj");
            ValidationHelper.NotNull(handler, "handler");
            obj.RemoveHandler(AnimationsUpdatingEvent, handler);
        }

        public static readonly RoutedEvent AnimationsUpdatedEvent = EventManager.RegisterRoutedEvent("AnimationsUpdated", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Animation));

        [Category("Appearance")]
        [Description("Occurs when a control's animations updated.")]
        public static void AddAnimationsUpdatedHandler(UIElement obj, RoutedEventHandler handler)
        {
            ValidationHelper.NotNull(obj, "obj");
            ValidationHelper.NotNull(handler, "handler");
            obj.AddHandler(AnimationsUpdatedEvent, handler);
        }

        public static void RemoveAnimationsUpdatedHandler(UIElement obj, RoutedEventHandler handler)
        {
            ValidationHelper.NotNull(obj, "obj");
            ValidationHelper.NotNull(handler, "handler");
            obj.RemoveHandler(AnimationsUpdatedEvent, handler);
        }

        #endregion
    }
}