using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

using Elysium.Theme.Controls.Automation;
using Elysium.Theme.Extensions;

namespace Elysium.Theme.Controls
{
    [DefaultEvent("Opened")]
    [Localizability(LocalizationCategory.Menu)]
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(CommandButton))]
    public class ApplicationBar : ItemsControl
    {
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

        public static readonly DependencyProperty DockProperty =
            DependencyProperty.RegisterAttached("Dock", typeof(ApplicationBarDock), typeof(ApplicationBar),
                                                new FrameworkPropertyMetadata(ApplicationBarDock.Left, FrameworkPropertyMetadataOptions.AffectsArrange));

        public static ApplicationBarDock GetDock(DependencyObject obj)
        {
            return (ApplicationBarDock)obj.GetValue(DockProperty);
        }

        public static void SetDock(DependencyObject obj, ApplicationBarDock value)
        {
            obj.SetValue(DockProperty, value);
        }

        [Bindable(true)]
        [Category("Appearance")]
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(ApplicationBar),
                                        new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsOpenChanged));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        private bool _isOpen;

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (ApplicationBar)d;
            instance.OnIsOpenChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnIsOpenChanged(bool oldIsOpen, bool newIsOpen)
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
            else
            {
                if (oldIsOpen)
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
        }

        public event EventHandler Opening;

        protected virtual void OnOpening(EventArgs e)
        {
            if (Opening != null)
            {
                Opening(this, e);
            }
        }

        public event EventHandler Opened;

        protected virtual void OnOpened(EventArgs e)
        {
            if (Opened != null)
            {
                Opened(this, e);
            }
        }

        public event EventHandler Closing;

        protected virtual void OnClosing(EventArgs e)
        {
            if (Closing != null)
            {
                Closing(this, e);
            }
        }

        public event EventHandler Closed;

        protected virtual void OnClosed(EventArgs e)
        {
            if (Closed != null)
            {
                Closed(this, e);
            }
        }

        public static readonly DependencyProperty StaysOpenProperty =
            DependencyProperty.Register("StaysOpen", typeof(bool), typeof(ApplicationBar), new FrameworkPropertyMetadata(false));

        public bool StaysOpen
        {
            get { return (bool)GetValue(StaysOpenProperty); }
            set { SetValue(StaysOpenProperty, value); }
        }

        public static readonly DependencyProperty TransitionModeProperty =
            DependencyProperty.Register("TransitionMode", typeof(ApplicationBarTransitionMode), typeof(ApplicationBar),
                                        new FrameworkPropertyMetadata(ApplicationBarTransitionMode.Slide));

        public ApplicationBarTransitionMode TransitionMode
        {
            get { return (ApplicationBarTransitionMode)GetValue(TransitionModeProperty); }
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
                else
                {
                    if (instance.IsOpen && !instance.StaysOpen)
                    {
                        instance.IsOpen = true;
                    }
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