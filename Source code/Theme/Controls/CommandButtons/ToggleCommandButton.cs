using System.ComponentModel;
using System.Windows;
using System.Windows.Automation.Peers;

using Elysium.Theme.Controls.Automation;
using Elysium.Theme.Controls.Primitives;

namespace Elysium.Theme.Controls
{
    [DefaultEvent("Checked")]
    public class ToggleCommandButton : CommandButtonBase
    {
        static ToggleCommandButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleCommandButton), new FrameworkPropertyMetadata(typeof(ToggleCommandButton)));
        }

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool?), typeof(ToggleCommandButton),
                                        new FrameworkPropertyMetadata(false,
                                                                      FrameworkPropertyMetadataOptions.BindsTwoWayByDefault |
                                                                      FrameworkPropertyMetadataOptions.Journal, OnIsCheckedChanged));

        [Category("Appearance")]
        [TypeConverter(typeof(NullableBoolConverter))]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public bool? IsChecked
        {
            get { return (bool?)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (ToggleCommandButton)d;
            instance.OnIsCheckedChanged((bool?)e.OldValue, (bool?)e.NewValue);
        }

        protected virtual void OnIsCheckedChanged(bool? oldIsChecked, bool? newIsChecked)
        {
            var peer = UIElementAutomationPeer.FromElement(this) as ToggleCommandButtonAutomationPeer;
            if (peer != null)
            {
                peer.RaiseToggleStatePropertyChangedEvent(oldIsChecked, newIsChecked);
            }

            switch (newIsChecked)
            {
                case true:
                    OnChecked(new RoutedEventArgs(CheckedEvent));
                    VisualStateManager.GoToState(this, "Checked", true);
                    break;
                case false:
                    OnUnchecked(new RoutedEventArgs(UncheckedEvent));
                    VisualStateManager.GoToState(this, "Unchecked", true);
                    break;
                case null:
                    OnIndeterminate(new RoutedEventArgs(IndeterminateEvent));
                    VisualStateManager.GoToState(this, "Indeterminate", true);
                    break;
            }
        }

        public static readonly RoutedEvent CheckedEvent = EventManager.RegisterRoutedEvent("Checked", RoutingStrategy.Bubble,
                                                                                           typeof(RoutedEventHandler), typeof(ToggleCommandButton));

        [Category("Behavior")]
        public event RoutedEventHandler Checked
        {
            add { AddHandler(CheckedEvent, value); }

            remove { RemoveHandler(CheckedEvent, value); }
        }

        protected virtual void OnChecked(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        public static readonly RoutedEvent UncheckedEvent = EventManager.RegisterRoutedEvent("Unchecked", RoutingStrategy.Bubble,
                                                                                             typeof(RoutedEventHandler), typeof(ToggleCommandButton));

        [Category("Behavior")]
        public event RoutedEventHandler Unchecked
        {
            add { AddHandler(UncheckedEvent, value); }

            remove { RemoveHandler(UncheckedEvent, value); }
        }

        protected virtual void OnUnchecked(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        public static readonly RoutedEvent IndeterminateEvent = EventManager.RegisterRoutedEvent("Indeterminate", RoutingStrategy.Bubble,
                                                                                                 typeof(RoutedEventHandler), typeof(ToggleCommandButton));

        [Category("Behavior")]
        public event RoutedEventHandler Indeterminate
        {
            add { AddHandler(IndeterminateEvent, value); }

            remove { RemoveHandler(IndeterminateEvent, value); }
        }

        protected virtual void OnIndeterminate(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        public static readonly DependencyProperty IsThreeStateProperty =
            DependencyProperty.Register("IsThreeState", typeof(bool), typeof(ToggleCommandButton), new FrameworkPropertyMetadata(false));

        [Bindable(true)]
        [Category("Behavior")]
        public bool IsThreeState
        {
            get { return (bool)GetValue(IsThreeStateProperty); }
            set { SetValue(IsThreeStateProperty, value); }
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ToggleCommandButtonAutomationPeer(this);
        }

        protected override void OnClick()
        {
            OnToggle();
            base.OnClick();
        }

        protected internal virtual void OnToggle()
        {
            bool? isChecked;
            if (IsChecked == true)
            {
                isChecked = IsThreeState ? (bool?)null : false;
            }
            else
            {
                isChecked = IsChecked.HasValue;
            }
            IsChecked = isChecked;
        }
    }
} ;