using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

using Elysium.Controls.Automation;
using Elysium.Controls.Primitives;
using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium.Controls
{
    [PublicAPI]
    [DefaultEvent("Opened")]
    [Localizability(LocalizationCategory.Menu)]
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(CommandButton))]
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
    public class ApplicationBar : ItemsControl
// ReSharper restore ClassWithVirtualMembersNeverInherited.Global
    {
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static ApplicationBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ApplicationBar), new FrameworkPropertyMetadata(typeof(ApplicationBar)));

            var itemsPanelTemplate = new ItemsPanelTemplate(new FrameworkElementFactory(typeof(ApplicationBarPanel)));
            itemsPanelTemplate.Seal();
            ItemsPanelProperty.OverrideMetadata(typeof(ApplicationBar), new FrameworkPropertyMetadata(itemsPanelTemplate));

            IsTabStopProperty.OverrideMetadata(typeof(ApplicationBar), new FrameworkPropertyMetadata(false));
            FocusableProperty.OverrideMetadata(typeof(ApplicationBar), new FrameworkPropertyMetadata(false));
            FocusManager.IsFocusScopeProperty.OverrideMetadata(typeof(ApplicationBar), new FrameworkPropertyMetadata(true));
            KeyboardNavigation.DirectionalNavigationProperty.OverrideMetadata(
                typeof(ApplicationBar), new FrameworkPropertyMetadata(KeyboardNavigationMode.Cycle));
            KeyboardNavigation.TabNavigationProperty.OverrideMetadata(
                typeof(ApplicationBar), new FrameworkPropertyMetadata(KeyboardNavigationMode.Cycle));
            KeyboardNavigation.ControlTabNavigationProperty.OverrideMetadata(
                typeof(ApplicationBar), new FrameworkPropertyMetadata(KeyboardNavigationMode.Once));

            HorizontalAlignmentProperty.OverrideMetadata(typeof(ApplicationBar), new FrameworkPropertyMetadata(HorizontalAlignment.Stretch));
            VerticalAlignmentProperty.OverrideMetadata(typeof(ApplicationBar), new FrameworkPropertyMetadata(VerticalAlignment.Bottom));

            EventManager.RegisterClassHandler(typeof(ApplicationBar), Mouse.LostMouseCaptureEvent,
                                              new MouseEventHandler(OnLostMouseCapture));
            EventManager.RegisterClassHandler(typeof(ApplicationBar), Mouse.PreviewMouseDownOutsideCapturedElementEvent,
                                              new MouseButtonEventHandler(OnPreviewMouseButtonOutsideCapturedElement));
            EventManager.RegisterClassHandler(typeof(ApplicationBar), Mouse.PreviewMouseUpOutsideCapturedElementEvent,
                                              new MouseButtonEventHandler(OnPreviewMouseButtonOutsideCapturedElement));
        }

        [PublicAPI]
        public static readonly DependencyProperty DockProperty =
            DependencyProperty.RegisterAttached("Dock", typeof(ApplicationBarDock), typeof(ApplicationBar),
                                                new FrameworkPropertyMetadata(ApplicationBarDock.Left, FrameworkPropertyMetadataOptions.AffectsArrange));

        [PublicAPI]
        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        [AttachedPropertyBrowsableForChildren]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static ApplicationBarDock GetDock([NotNull] UIElement obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return BoxingHelper<ApplicationBarDock>.Unbox(obj.GetValue(DockProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetDock([NotNull] UIElement obj, ApplicationBarDock value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(DockProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(ApplicationBar),
                                        new FrameworkPropertyMetadata(BooleanBoxingHelper.FalseBox, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                                                      OnIsOpenChanged));

        [PublicAPI]
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Indicates whether the ApplicationBar is visible.")]
        public bool IsOpen
        {
            get { return BooleanBoxingHelper.Unbox(GetValue(IsOpenProperty)); }
            set { SetValue(IsOpenProperty, BooleanBoxingHelper.Box(value)); }
        }

        private bool _isOpen;

        private static void OnIsOpenChanged([NotNull] DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ValidationHelper.NotNull(obj, () => obj);
            var instance = (ApplicationBar)obj;
            instance.OnIsOpenChanged(/*BooleanBoxingHelper.Unbox(e.OldValue),*/ BooleanBoxingHelper.Unbox(e.NewValue));
        }

        private void OnIsOpenChanged(/*bool oldIsOpen,*/ bool newIsOpen)
        {
            if (newIsOpen)
            {
                OnOpening(new RoutedEventArgs(OpeningEvent, this));

                _isOpen = true;
                InvalidateArrange();

                var storyboard = new Storyboard { FillBehavior = FillBehavior.Stop };
                Timeline animation;
                switch (TransitionMode)
                {
                    case ApplicationBarTransitionMode.Fade:
                        animation = new DoubleAnimation(0d, 1.0, Parameters.GetMinimumDuration(this));
                        Storyboard.SetTarget(animation, this);
                        Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
                        break;
                    default:
                        animation = new DoubleAnimation(0d, DesiredSize.Height, Parameters.GetMinimumDuration(this));
                        Storyboard.SetTarget(animation, this);
                        Storyboard.SetTargetProperty(animation, new PropertyPath("Height"));
                        break;
                }
                // NOTE: Lack of contracts
                Contract.Assume(storyboard.Children != null);
                storyboard.Children.Add(animation);
                storyboard.Completed += (sender, e) =>
                {
                    OnOpened(new RoutedEventArgs(OpenedEvent, this));

                    storyboard.Remove();
                };
                BeginStoryboard(storyboard);

                Mouse.Capture(this, CaptureMode.SubTree);
            }
            else
            {
                OnClosing(new RoutedEventArgs(ClosingEvent, this));

                if (Mouse.Captured == this)
                {
                    Mouse.Capture(null);
                }

                var storyboard = new Storyboard { FillBehavior = FillBehavior.Stop };
                Timeline animation;
                switch (TransitionMode)
                {
                    case ApplicationBarTransitionMode.Fade:
                        animation = new DoubleAnimation(1.0, 0d, Parameters.GetMinimumDuration(this));
                        Storyboard.SetTarget(animation, this);
                        Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
                        break;
                    default:
                        animation = new DoubleAnimation(DesiredSize.Height, 0d, Parameters.GetMinimumDuration(this));
                        Storyboard.SetTarget(animation, this);
                        Storyboard.SetTargetProperty(animation, new PropertyPath("Height"));
                        break;
                }
                // NOTE: Lack of contracts
                Contract.Assume(storyboard.Children != null);
                storyboard.Children.Add(animation);
                storyboard.Completed += (sender, e) =>
                {
                    _isOpen = false;
                    InvalidateArrange();

                    OnClosed(new RoutedEventArgs(ClosedEvent, this));

                    storyboard.Remove();
                };
                BeginStoryboard(storyboard);
            }
        }

        [PublicAPI]
        public static readonly RoutedEvent OpeningEvent =
            EventManager.RegisterRoutedEvent("Opening", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(ApplicationBar));

        [PublicAPI]
        public event RoutedEventHandler Opening
        {
            add { AddHandler(OpeningEvent, value); }
            remove { RemoveHandler(OpeningEvent, value); }
        }

        [PublicAPI]
        protected virtual void OnOpening(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        [PublicAPI]
        public static readonly RoutedEvent OpenedEvent =
            EventManager.RegisterRoutedEvent("Opened", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ApplicationBar));

        [PublicAPI]
        public event RoutedEventHandler Opened
        {
            add { AddHandler(OpenedEvent, value); }
            remove { RemoveHandler(OpenedEvent, value); }
        }

        [PublicAPI]
        protected virtual void OnOpened(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        [PublicAPI]
        public static readonly RoutedEvent ClosingEvent =
            EventManager.RegisterRoutedEvent("Closing", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(ApplicationBar));

        [PublicAPI]
        public event RoutedEventHandler Closing
        {
            add { AddHandler(ClosingEvent, value); }
            remove { RemoveHandler(ClosingEvent, value); }
        }

        [PublicAPI]
        protected virtual void OnClosing(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        [PublicAPI]
        public static readonly RoutedEvent ClosedEvent =
            EventManager.RegisterRoutedEvent("Closed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ApplicationBar));

        [PublicAPI]
        public event RoutedEventHandler Closed
        {
            add { AddHandler(ClosedEvent, value); }
            remove { RemoveHandler(ClosedEvent, value); }
        }

        [PublicAPI]
        protected virtual void OnClosed(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        [PublicAPI]
        public static readonly DependencyProperty StaysOpenProperty =
            DependencyProperty.Register("StaysOpen", typeof(bool), typeof(ApplicationBar),
                                        new FrameworkPropertyMetadata(BooleanBoxingHelper.FalseBox, FrameworkPropertyMetadataOptions.None));

        [PublicAPI]
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Indicates whether the ApplicationBar control closes when the control is no longer in focus.")]
        public bool StaysOpen
        {
            get { return BooleanBoxingHelper.Unbox(GetValue(StaysOpenProperty)); }
            set { SetValue(StaysOpenProperty, BooleanBoxingHelper.Box(value)); }
        }

        [PublicAPI]
        public static readonly DependencyProperty PreventsOpenProperty =
            DependencyProperty.RegisterAttached("PreventsOpen", typeof(bool), typeof(ApplicationBar),
                                                new FrameworkPropertyMetadata(BooleanBoxingHelper.FalseBox, FrameworkPropertyMetadataOptions.Inherits));

        [PublicAPI]
        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static bool GetPreventsOpen([NotNull] UIElement obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return BooleanBoxingHelper.Unbox(obj.GetValue(PreventsOpenProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetPreventsOpen([NotNull] UIElement obj, bool value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(PreventsOpenProperty, BooleanBoxingHelper.Box(value));
        }

        [PublicAPI]
        public static readonly DependencyProperty TransitionModeProperty =
            DependencyProperty.Register("TransitionMode", typeof(ApplicationBarTransitionMode), typeof(ApplicationBar),
                                        new FrameworkPropertyMetadata(ApplicationBarTransitionMode.Slide, FrameworkPropertyMetadataOptions.None));

        [PublicAPI]
        [Bindable(true)]
        [Category("Appearance")]
        [Description("Animation for the opening and closing of a ApplicationBar.")]
        public ApplicationBarTransitionMode TransitionMode
        {
            get { return BoxingHelper<ApplicationBarTransitionMode>.Unbox(GetValue(TransitionModeProperty)); }
            set { SetValue(TransitionModeProperty, value); }
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is CommandButtonBase;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new CommandButton();
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ApplicationBarAutomationPeer(this);
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            return _isOpen ? base.ArrangeOverride(arrangeBounds) : new Size(0, 0);
        }

        private static void OnLostMouseCapture(object sender, MouseEventArgs e)
        {
            var instance = sender as ApplicationBar;
            var source = e.Source as DependencyObject;
            if (instance != null && source != null)
            {
                var parent = VisualTreeHelperExtensions.FindParent<ApplicationBar>(source);
                if (instance == parent)
                {
                    Mouse.Capture(instance, CaptureMode.SubTree);
                    e.Handled = true;
                }
            }
        }

        private static void OnPreviewMouseButtonOutsideCapturedElement(object sender, MouseButtonEventArgs e)
        {
            var instance = sender as ApplicationBar;
            var source = e.Source as DependencyObject;
            if (instance != null && source != null)
            {
                var parent = VisualTreeHelperExtensions.FindParent<ApplicationBar>(source);
                if (instance != parent && instance.IsOpen && !instance.StaysOpen)
                {
                    instance.IsOpen = false;
                }
            }
        }
    }
} ;