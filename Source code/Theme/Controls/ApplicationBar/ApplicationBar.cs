using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

using Elysium.Theme.Controls.Automation;
using Elysium.Theme.Extensions;

using JetBrains.Annotations;

namespace Elysium.Theme.Controls
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
        public static ApplicationBarDock GetDock(UIElement obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<ApplicationBarDock>.Unbox(obj.GetValue(DockProperty));
        }

        [PublicAPI]
        public static void SetDock(UIElement obj, ApplicationBarDock value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
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

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();
            var instance = (ApplicationBar)d;
            instance.OnIsOpenChanged(BooleanBoxingHelper.Unbox(e.OldValue), BooleanBoxingHelper.Unbox(e.NewValue));
        }

        private void OnIsOpenChanged(bool oldIsOpen, bool newIsOpen)
        {
            if (newIsOpen)
            {
                if (!oldIsOpen)
                {
                    OnOpening(EventArgs.Empty);

                    _isOpen = true;
                    InvalidateArrange();

                    var storyboard = new Storyboard { FillBehavior = FillBehavior.Stop };
                    Timeline animation;
                    switch (TransitionMode)
                    {
                        case ApplicationBarTransitionMode.Fade:
                            animation = new DoubleAnimation(0.0, 1.0, Parameters.GetMinimumDuration(this));
                            Storyboard.SetTarget(animation, this);
                            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
                            break;
                        default:
                            animation = new DoubleAnimation(0.0, DesiredSize.Height, Parameters.GetMinimumDuration(this));
                            Storyboard.SetTarget(animation, this);
                            Storyboard.SetTargetProperty(animation, new PropertyPath("Height"));
                            break;
                    }
                    Contract.Assume(storyboard.Children != null);
                    storyboard.Children.Add(animation);
                    storyboard.Completed += (sender, e) =>
                    {
                        OnOpened(EventArgs.Empty);

                        storyboard.Remove();
                    };
                    BeginStoryboard(storyboard);

                    Mouse.Capture(this, CaptureMode.SubTree);
                }
            }
            else if (oldIsOpen)
            {
                OnClosing(EventArgs.Empty);

                if (Mouse.Captured == this)
                {
                    Mouse.Capture(null);
                }

                var storyboard = new Storyboard { FillBehavior = FillBehavior.Stop };
                Timeline animation;
                switch (TransitionMode)
                {
                    case ApplicationBarTransitionMode.Fade:
                        animation = new DoubleAnimation(1.0, 0.0, Parameters.GetMinimumDuration(this));
                        Storyboard.SetTarget(animation, this);
                        Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
                        break;
                    default:
                        animation = new DoubleAnimation(DesiredSize.Height, 0.0, Parameters.GetMinimumDuration(this));
                        Storyboard.SetTarget(animation, this);
                        Storyboard.SetTargetProperty(animation, new PropertyPath("Height"));
                        break;
                }
                Contract.Assume(storyboard.Children != null);
                storyboard.Children.Add(animation);
                storyboard.Completed += (sender, e) =>
                {
                    _isOpen = false;
                    InvalidateArrange();

                    OnClosed(EventArgs.Empty);

                    storyboard.Remove();
                };
                BeginStoryboard(storyboard);
            }
        }

        [PublicAPI]
        public event EventHandler Opening;

        [PublicAPI]
        protected virtual void OnOpening(EventArgs e)
        {
            if (Opening != null)
            {
                Opening(this, e);
            }
        }

        [PublicAPI]
        public event EventHandler Opened;

        [PublicAPI]
        protected virtual void OnOpened(EventArgs e)
        {
            if (Opened != null)
            {
                Opened(this, e);
            }
        }

        [PublicAPI]
        public event EventHandler Closing;

        [PublicAPI]
        protected virtual void OnClosing(EventArgs e)
        {
            if (Closing != null)
            {
                Closing(this, e);
            }
        }

        [PublicAPI]
        public event EventHandler Closed;

        [PublicAPI]
        protected virtual void OnClosed(EventArgs e)
        {
            if (Closed != null)
            {
                Closed(this, e);
            }
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
        public static bool GetPreventsOpen(UIElement obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BooleanBoxingHelper.Unbox(obj.GetValue(PreventsOpenProperty));
        }

        [PublicAPI]
        public static void SetPreventsOpen(UIElement obj, bool value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
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
            return (item is CommandButton || item is ToggleCommandButton || item is DropDownCommandButton);
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