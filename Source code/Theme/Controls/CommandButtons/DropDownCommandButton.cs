using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

using Elysium.Theme.Controls.Automation;
using Elysium.Theme.Controls.Primitives;

namespace Elysium.Theme.Controls
{
    [TemplatePart(Name = PopupName, Type = typeof(Popup))]
    public class DropDownCommandButton : CommandButtonBase
    {
        private const string PopupName = "PART_Popup";

        private Popup _popup;

        static DropDownCommandButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownCommandButton), new FrameworkPropertyMetadata(typeof(DropDownCommandButton)));
            EventManager.RegisterClassHandler(typeof(DropDownCommandButton), MenuItem.ClickEvent, new RoutedEventHandler(OnMenuItemClick), true);
        }

        public static readonly DependencyProperty SubmenuProperty =
            DependencyProperty.Register("Submenu", typeof(Submenu), typeof(DropDownCommandButton),
                                        new FrameworkPropertyMetadata((Submenu)null, OnSubmenuChanged));

        [Bindable(true)]
        [Category("Content")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Submenu Submenu
        {
            get { return (Submenu)GetValue(SubmenuProperty); }
            set { SetValue(SubmenuProperty, value); }
        }

        private static void OnSubmenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (DropDownCommandButton)d;
            instance.OnSubmenuChanged((Submenu)e.OldValue, (Submenu)e.NewValue);
        }

        protected virtual void OnSubmenuChanged(Submenu oldSubmenu, Submenu newSubmenu)
        {
            if (_popup != null)
            {
                _popup.Child = newSubmenu;
            }
            HasSubmenu = newSubmenu != null;
        }

        private static readonly DependencyPropertyKey HasSubmenuPropertyKey =
            DependencyProperty.RegisterReadOnly("HasSubmenu", typeof(bool), typeof(DropDownCommandButton),
                                                new FrameworkPropertyMetadata(false, OnHasSubmenuChanged));

        public static readonly DependencyProperty HasSubmenuProperty = HasSubmenuPropertyKey.DependencyProperty;

        [Bindable(false)]
        [Browsable(false)]
        public bool HasSubmenu
        {
            get { return (bool)GetValue(HasSubmenuProperty); }
            private set { SetValue(HasSubmenuPropertyKey, value); }
        }

        private static void OnHasSubmenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (DropDownCommandButton)d;
            instance.OnHasSubmenuChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnHasSubmenuChanged(bool oldHasSubmenu, bool newHasSubmenu)
        {
            var peer = UIElementAutomationPeer.FromElement(this) as DropDownCommandButtonAutomationPeer;
            if (peer != null)
            {
                peer.RaiseExpandCollapseStatePropertyChangedEvent(
                    oldHasSubmenu ? IsDropDownOpen ? ExpandCollapseState.Expanded : ExpandCollapseState.Collapsed : ExpandCollapseState.LeafNode,
                    newHasSubmenu ? IsDropDownOpen ? ExpandCollapseState.Expanded : ExpandCollapseState.Collapsed : ExpandCollapseState.LeafNode);
            }
        }

        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(DropDownCommandButton),
                                        new FrameworkPropertyMetadata(false, OnIsDropDownOpenChanged, CoerceIsDropDownOpen));

        [Bindable(true)]
        [Browsable(false)]
        [Category("Appearance")]
        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        private static void OnIsDropDownOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (DropDownCommandButton)d;
            instance.OnIsDropDownOpenChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnIsDropDownOpenChanged(bool oldIsDropDownOpen, bool newIsDropDownOpen)
        {
            var peer = UIElementAutomationPeer.FromElement(this) as DropDownCommandButtonAutomationPeer;
            if (peer != null)
            {
                if (!HasSubmenu)
                {
                    peer.RaiseExpandCollapseStatePropertyChangedEvent(oldIsDropDownOpen ? ExpandCollapseState.Expanded : ExpandCollapseState.Collapsed,
                                                                      newIsDropDownOpen ? ExpandCollapseState.Expanded : ExpandCollapseState.Collapsed);
                }
            }

            switch (newIsDropDownOpen)
            {
                case true:
                    VisualStateManager.GoToState(this, "DropDown", true);
                    break;
                case false:
                    VisualStateManager.GoToState(this, "Normal", true);
                    break;
            }
        }

        private static object CoerceIsDropDownOpen(DependencyObject d, object baseValue)
        {
            var instance = (DropDownCommandButton)d;
            return instance.CoerceIsDropDownOpen((bool)baseValue);
        }

        protected virtual object CoerceIsDropDownOpen(bool baseValue)
        {
            return HasSubmenu && baseValue;
        }

        public event EventHandler DropDownOpened;

        protected virtual void OnDropDownOpened(EventArgs e)
        {
            if (DropDownOpened != null)
            {
                DropDownOpened(this, e);
            }
        }

        private void OnDropDownOpened(object sender, EventArgs e)
        {
            OnDropDownOpened(e);
        }

        public event EventHandler DropDownClosed;

        protected virtual void OnDropDownClosed(EventArgs e)
        {
            if (DropDownClosed != null)
            {
                DropDownClosed(this, e);
            }
        }

        private void OnDropDownClosed(object sender, EventArgs e)
        {
            OnDropDownClosed(e);
        }

        public static readonly DependencyProperty MaxDropDownHeightProperty =
            DependencyProperty.Register("MaxDropDownHeight", typeof(double), typeof(DropDownCommandButton),
                                        new FrameworkPropertyMetadata(SystemParameters.PrimaryScreenHeight / 3));

        [Bindable(true)]
        [Category("Layout")]
        [TypeConverter(typeof(LengthConverter))]
        public double MaxDropDownHeight
        {
            get { return (double)GetValue(MaxDropDownHeightProperty); }
            set { SetValue(MaxDropDownHeightProperty, value); }
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new DropDownCommandButtonAutomationPeer(this);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_popup != null)
            {
                _popup.CustomPopupPlacementCallback = null;
                _popup.Opened -= OnDropDownOpened;
                _popup.Closed -= OnDropDownClosed;
            }
            _popup = (Popup)Template.FindName(PopupName, this);
            _popup.CustomPopupPlacementCallback = PlacePopup;
            _popup.Opened += OnDropDownOpened;
            _popup.Closed += OnDropDownClosed;
            if (Submenu != null)
            {
                _popup.Child = Submenu;
            }
        }

        protected override void OnClick()
        {
            IsDropDownOpen = true;
            base.OnClick();
        }

        private static void OnMenuItemClick(object sender, RoutedEventArgs e)
        {
            var instance = sender as DropDownCommandButton;
            if (instance != null)
            {
                instance.IsDropDownOpen = false;
            }
        }

        private CustomPopupPlacement[] PlacePopup(Size popupsize, Size targetsize, Point offset)
        {
            var window = System.Windows.Window.GetWindow(this);
            if (window != null)
            {
                var transformToAncestor = TransformToAncestor(window);
                var position = new Point(targetsize.Width / 2 - popupsize.Width / 2 + offset.X,
                                         -popupsize.Height + offset.Y - (Margin.Left + Margin.Top + Margin.Right + Margin.Bottom) / 2);
                var relativePosition = transformToAncestor.Transform(position);
                if (relativePosition.X < 0)
                {
                    relativePosition.X = 0;
                }
                if (relativePosition.X + popupsize.Width > window.Width)
                {
                    relativePosition.X = window.Width - popupsize.Width;
                }
                var transformToDescendant = window.TransformToDescendant(this);
                position = transformToDescendant.Transform(relativePosition);
                return new[]
                           {
                               new CustomPopupPlacement(position, PopupPrimaryAxis.None)
                           };
            }
            return null;
        }
    }
} ;