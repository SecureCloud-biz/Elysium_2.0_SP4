using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

using Elysium.Theme.Controls.Automation;
using Elysium.Theme.Extensions;

using JetBrains.Annotations;

namespace Elysium.Theme.Controls
{
    [PublicAPI]
    [DefaultEvent("Checked")]
    [TemplatePart(Name = TrackName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = SwitchName, Type = typeof(Button))]
    [TemplatePart(Name = ThumbName, Type = typeof(Thumb))]
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
    public class ToggleSwitch : Control
// ReSharper restore ClassWithVirtualMembersNeverInherited.Global
    {
        private const string TrackName = "PART_Track";
        private const string SwitchName = "PART_Switch";
        private const string ThumbName = "PART_Thumb";

        private FrameworkElement _track;
        private Button _switch;
        private Thumb _thumb;

        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static ToggleSwitch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleSwitch), new FrameworkPropertyMetadata(typeof(ToggleSwitch)));
        }

        [PublicAPI]
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(ToggleSwitch),
                                        new FrameworkPropertyMetadata(BooleanBoxingHelper.FalseBox,
                                                                      FrameworkPropertyMetadataOptions.BindsTwoWayByDefault |
                                                                      FrameworkPropertyMetadataOptions.Journal, OnIsCheckedChanged));

        [PublicAPI]
        [Category("Appearance")]
        [Description("Indicates whether the button is checked.")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public bool IsChecked
        {
            get { return BooleanBoxingHelper.Unbox(GetValue(IsCheckedProperty)); }
            set { SetValue(IsCheckedProperty, BooleanBoxingHelper.Box(value)); }
        }

        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();
            var instance = (ToggleSwitch)d;
            instance.OnIsCheckedChanged(BooleanBoxingHelper.Unbox(e.OldValue), BooleanBoxingHelper.Unbox(e.NewValue));
        }

        [PublicAPI]
        protected void OnIsCheckedChanged(bool oldIsChecked, bool newIsChecked)
        {
            var peer = UIElementAutomationPeer.FromElement(this) as ToggleSwitchAutomationPeer;
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
                    VisualStateManager.GoToState(this, "Normal", true);
                    break;
            }
        }

        [PublicAPI]
        public static readonly RoutedEvent CheckedEvent = EventManager.RegisterRoutedEvent("Checked", RoutingStrategy.Bubble,
                                                                                           typeof(RoutedEventHandler), typeof(ToggleSwitch));

        [PublicAPI]
        [Category("Behavior")]
        [Description("Occurs when a button is checked.")]
        public event RoutedEventHandler Checked
        {
            add { AddHandler(CheckedEvent, value); }

            remove { RemoveHandler(CheckedEvent, value); }
        }

        [PublicAPI]
        protected virtual void OnChecked(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        [PublicAPI]
        [Category("Behavior")]
        [Description("Occurs when a button is unchecked.")]
        public event RoutedEventHandler Unchecked
        {
            add { AddHandler(UncheckedEvent, value); }

            remove { RemoveHandler(UncheckedEvent, value); }
        }

        [PublicAPI]
        protected virtual void OnUnchecked(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        [PublicAPI]
        public static readonly RoutedEvent UncheckedEvent = EventManager.RegisterRoutedEvent("Unchecked", RoutingStrategy.Bubble,
                                                                                             typeof(RoutedEventHandler), typeof(ToggleSwitch));

        private static readonly DependencyPropertyKey IsSwitchingPropertyKey =
            DependencyProperty.RegisterReadOnly("IsSwitching", typeof(bool), typeof(ToggleSwitch),
                                                new FrameworkPropertyMetadata(BooleanBoxingHelper.FalseBox, OnIsSwitchingChanged));

        [PublicAPI]
        public static readonly DependencyProperty IsSwitchingProperty = IsSwitchingPropertyKey.DependencyProperty;

        [PublicAPI]
        public bool IsSwitching
        {
            get { return BooleanBoxingHelper.Unbox(GetValue(IsSwitchingProperty)); }
            private set { SetValue(IsSwitchingPropertyKey, BooleanBoxingHelper.Box(value)); }
        }

        private static void OnIsSwitchingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();
            var instance = (ToggleSwitch)d;
            instance.OnIsSwitchingChanged(BooleanBoxingHelper.Unbox(e.OldValue), BooleanBoxingHelper.Unbox(e.NewValue));
        }

// ReSharper disable UnusedParameter.Local
        private void OnIsSwitchingChanged(bool oldIsSwitching, bool newIsSwitchging)
// ReSharper restore UnusedParameter.Local
        {
            if (newIsSwitchging)
            {
                OnSwitchStarted(new RoutedEventArgs(SwitchStartedEvent));
                VisualStateManager.GoToState(this, "Switch", true);
            }
            else
            {
                OnSwitchCompleted(new RoutedEventArgs(SwitchCompletedEvent));
            }
        }

        [PublicAPI]
        public static readonly RoutedEvent SwitchStartedEvent = EventManager.RegisterRoutedEvent("SwitchStarted", RoutingStrategy.Bubble,
                                                                                                 typeof(RoutedEventHandler), typeof(ToggleSwitch));

        [PublicAPI]
        [Category("Behavior")]
        [Description("Occurs when a button is SwitchStarted.")]
        public event RoutedEventHandler SwitchStarted
        {
            add { AddHandler(SwitchStartedEvent, value); }

            remove { RemoveHandler(SwitchStartedEvent, value); }
        }

        [PublicAPI]
        protected virtual void OnSwitchStarted(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        [PublicAPI]
        public static readonly RoutedEvent SwitchCompletedEvent = EventManager.RegisterRoutedEvent("SwitchCompleted", RoutingStrategy.Bubble,
                                                                                                   typeof(RoutedEventHandler), typeof(ToggleSwitch));

        [PublicAPI]
        [Category("Behavior")]
        [Description("Occurs when a button is SwitchCompleted.")]
        public event RoutedEventHandler SwitchCompleted
        {
            add { AddHandler(SwitchCompletedEvent, value); }

            remove { RemoveHandler(SwitchCompletedEvent, value); }
        }

        [PublicAPI]
        protected virtual void OnSwitchCompleted(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        [PublicAPI]
        public static readonly DependencyProperty OnHeaderProperty =
            DependencyProperty.Register("OnHeader", typeof(object), typeof(ToggleSwitch), new FrameworkPropertyMetadata("On", OnOnHeaderChanged));

        [PublicAPI]
        [Bindable(true)]
        [Category("Content")]
        [Description("The data used for the on header of each control.")]
        [Localizability(LocalizationCategory.Label)]
        public object OnHeader
        {
            get { return GetValue(OnHeaderProperty); }
            set { SetValue(OnHeaderProperty, value); }
        }

        private static void OnOnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();
            var instance = (ToggleSwitch)d;
            instance.OnOnHeaderChanged(e.OldValue, e.NewValue);
            instance.HasOnHeader = e.NewValue != null;
        }

        [PublicAPI]
        // ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnOnHeaderChanged(object oldOnHeader, object newOnHeader)
            // ReSharper restore VirtualMemberNeverOverriden.Global
        {
            RemoveLogicalChild(oldOnHeader);
            AddLogicalChild(newOnHeader);
        }

        private static readonly DependencyPropertyKey HasOnHeaderPropertyKey =
            DependencyProperty.RegisterReadOnly("HasOnHeader", typeof(bool), typeof(ToggleSwitch),
                                                new FrameworkPropertyMetadata(BooleanBoxingHelper.FalseBox, OnHasOnHeaderChanged));

        [PublicAPI]
        public static readonly DependencyProperty HasOnHeaderProperty = HasOnHeaderPropertyKey.DependencyProperty;

        [PublicAPI]
        [Bindable(false)]
        [Browsable(false)]
        public bool HasOnHeader
        {
            get { return BooleanBoxingHelper.Unbox(GetValue(HasOnHeaderProperty)); }
            private set { SetValue(HasOnHeaderPropertyKey, BooleanBoxingHelper.Box(value)); }
        }

        private static void OnHasOnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();
            var instance = (ToggleSwitch)d;
            instance.OnHasOnHeaderChanged(BooleanBoxingHelper.Unbox(e.OldValue), BooleanBoxingHelper.Unbox(e.NewValue));
        }

        [PublicAPI]
        // ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnHasOnHeaderChanged(bool oldOnHeader, bool newOnHeader)
            // ReSharper restore VirtualMemberNeverOverriden.Global
        {
        }

        [PublicAPI]
        public static readonly DependencyProperty OnHeaderStringFormatProperty =
            DependencyProperty.Register("OnHeaderStringFormat", typeof(string), typeof(ToggleSwitch),
                                        new FrameworkPropertyMetadata(null, OnOnHeaderStringFormatChanged));

        [PublicAPI]
        [Bindable(true)]
        [Category("Content")]
        [Description("A composite string that specifies how to format the OnHeader property if it is displayed as a string.")]
        public string OnHeaderStringFormat
        {
            get { return (string)GetValue(OnHeaderStringFormatProperty); }
            set { SetValue(OnHeaderStringFormatProperty, value); }
        }

        private static void OnOnHeaderStringFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();
            var instance = (ToggleSwitch)d;
            instance.OnOnHeaderStringFormatChanged((string)e.OldValue, (string)e.NewValue);
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
        // ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnOnHeaderStringFormatChanged(string oldOnHeaderStringFormat, string newOnHeaderStringFormat)
            // ReSharper restore VirtualMemberNeverOverriden.Global
        {
        }

        [PublicAPI]
        public static readonly DependencyProperty OnHeaderTemplateProperty =
            DependencyProperty.Register("OnHeaderTemplate", typeof(DataTemplate), typeof(ToggleSwitch),
                                        new FrameworkPropertyMetadata(null, OnOnHeaderTemplateChanged));

        [PublicAPI]
        [Bindable(true)]
        [Category("Content")]
        [Description("The template used to display the content of the control's on header.")]
        public DataTemplate OnHeaderTemplate
        {
            get { return (DataTemplate)GetValue(OnHeaderTemplateProperty); }
            set { SetValue(OnHeaderTemplateProperty, value); }
        }

        private static void OnOnHeaderTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();
            var instance = (ToggleSwitch)d;
            instance.OnOnHeaderTemplateChanged((DataTemplate)e.OldValue, (DataTemplate)e.NewValue);
        }

        [PublicAPI]
        // ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnOnHeaderTemplateChanged(DataTemplate oldOnHeaderTemplate, DataTemplate newOnHeaderTemplate)
            // ReSharper restore VirtualMemberNeverOverriden.Global
        {
            if (newOnHeaderTemplate != null && OnHeaderTemplateSelector != null)
                Trace.TraceError("OnHeaderTemplate and OnHeaderTemplateSelector defined");
        }

        [PublicAPI]
        public static readonly DependencyProperty OnHeaderTemplateSelectorProperty =
            DependencyProperty.Register("OnHeaderTemplateSelector", typeof(DataTemplateSelector), typeof(ToggleSwitch),
                                        new FrameworkPropertyMetadata(null, OnOnHeaderTemplateSelectorChanged));

        [PublicAPI]
        [Bindable(true)]
        [Category("Content")]
        [Description("A data template selector that provides custom logic for choosing the template used to display the on header.")]
        public DataTemplateSelector OnHeaderTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(OnHeaderTemplateSelectorProperty); }
            set { SetValue(OnHeaderTemplateSelectorProperty, value); }
        }

        private static void OnOnHeaderTemplateSelectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();
            var instance = (ToggleSwitch)d;
            instance.OnOnHeaderTemplateSelectorChanged((DataTemplateSelector)e.OldValue, (DataTemplateSelector)e.NewValue);
        }

        [PublicAPI]
        // ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnOnHeaderTemplateSelectorChanged(DataTemplateSelector oldOnHeaderTemplateSelector,
                                                                 DataTemplateSelector newOnHeaderTemplateSelector)
            // ReSharper restore VirtualMemberNeverOverriden.Global
        {
            if (OnHeaderTemplate != null && newOnHeaderTemplateSelector != null)
                Trace.TraceError("OnHeaderTemplate and OnHeaderTemplateSelector defined");
        }

        [PublicAPI]
        public static readonly DependencyProperty OffHeaderProperty =
            DependencyProperty.Register("OffHeader", typeof(object), typeof(ToggleSwitch), new FrameworkPropertyMetadata("Off", OnOffHeaderChanged));

        [PublicAPI]
        [Bindable(true)]
        [Category("Content")]
        [Description("The data used for the off header of each control.")]
        [Localizability(LocalizationCategory.Label)]
        public object OffHeader
        {
            get { return GetValue(OffHeaderProperty); }
            set { SetValue(OffHeaderProperty, value); }
        }

        private static void OnOffHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();
            var instance = (ToggleSwitch)d;
            instance.OnOffHeaderChanged(e.OldValue, e.NewValue);
            instance.HasOffHeader = e.NewValue != null;
        }

        [PublicAPI]
        // ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnOffHeaderChanged(object oldOffHeader, object newOffHeader)
            // ReSharper restore VirtualMemberNeverOverriden.Global
        {
            RemoveLogicalChild(oldOffHeader);
            AddLogicalChild(newOffHeader);
        }

        private static readonly DependencyPropertyKey HasOffHeaderPropertyKey =
            DependencyProperty.RegisterReadOnly("HasOffHeader", typeof(bool), typeof(ToggleSwitch),
                                                new FrameworkPropertyMetadata(BooleanBoxingHelper.FalseBox, OnHasOffHeaderChanged));

        [PublicAPI]
        public static readonly DependencyProperty HasOffHeaderProperty = HasOffHeaderPropertyKey.DependencyProperty;

        [PublicAPI]
        [Bindable(false)]
        [Browsable(false)]
        public bool HasOffHeader
        {
            get { return BooleanBoxingHelper.Unbox(GetValue(HasOffHeaderProperty)); }
            private set { SetValue(HasOffHeaderPropertyKey, BooleanBoxingHelper.Box(value)); }
        }

        private static void OnHasOffHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();
            var instance = (ToggleSwitch)d;
            instance.OnHasOffHeaderChanged(BooleanBoxingHelper.Unbox(e.OldValue), BooleanBoxingHelper.Unbox(e.NewValue));
        }

        [PublicAPI]
        // ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnHasOffHeaderChanged(bool oldOffHeader, bool newOffHeader)
            // ReSharper restore VirtualMemberNeverOverriden.Global
        {
        }

        [PublicAPI]
        public static readonly DependencyProperty OffHeaderStringFormatProperty =
            DependencyProperty.Register("OffHeaderStringFormat", typeof(string), typeof(ToggleSwitch),
                                        new FrameworkPropertyMetadata(null, OnOffHeaderStringFormatChanged));

        [PublicAPI]
        [Bindable(true)]
        [Category("Content")]
        [Description("A composite string that specifies how to format the OffHeader property if it is displayed as a string.")]
        public string OffHeaderStringFormat
        {
            get { return (string)GetValue(OffHeaderStringFormatProperty); }
            set { SetValue(OffHeaderStringFormatProperty, value); }
        }

        private static void OnOffHeaderStringFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();
            var instance = (ToggleSwitch)d;
            instance.OnOffHeaderStringFormatChanged((string)e.OldValue, (string)e.NewValue);
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
        // ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnOffHeaderStringFormatChanged(string oldOffHeaderStringFormat, string newOffHeaderStringFormat)
            // ReSharper restore VirtualMemberNeverOverriden.Global
        {
        }

        [PublicAPI]
        public static readonly DependencyProperty OffHeaderTemplateProperty =
            DependencyProperty.Register("OffHeaderTemplate", typeof(DataTemplate), typeof(ToggleSwitch),
                                        new FrameworkPropertyMetadata(null, OnOffHeaderTemplateChanged));

        [PublicAPI]
        [Bindable(true)]
        [Category("Content")]
        [Description("The template used to display the content of the control's off header.")]
        public DataTemplate OffHeaderTemplate
        {
            get { return (DataTemplate)GetValue(OffHeaderTemplateProperty); }
            set { SetValue(OffHeaderTemplateProperty, value); }
        }

        private static void OnOffHeaderTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();
            var instance = (ToggleSwitch)d;
            instance.OnOffHeaderTemplateChanged((DataTemplate)e.OldValue, (DataTemplate)e.NewValue);
        }

        [PublicAPI]
        // ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnOffHeaderTemplateChanged(DataTemplate oldOffHeaderTemplate, DataTemplate newOffHeaderTemplate)
            // ReSharper restore VirtualMemberNeverOverriden.Global
        {
            if (newOffHeaderTemplate != null && OffHeaderTemplateSelector != null)
                Trace.TraceError("OffHeaderTemplate and OffHeaderTemplateSelector defined");
        }

        [PublicAPI]
        public static readonly DependencyProperty OffHeaderTemplateSelectorProperty =
            DependencyProperty.Register("OffHeaderTemplateSelector", typeof(DataTemplateSelector), typeof(ToggleSwitch),
                                        new FrameworkPropertyMetadata(null, OnOffHeaderTemplateSelectorChanged));

        [PublicAPI]
        [Bindable(true)]
        [Category("Content")]
        [Description("A data template selector that provides custom logic for choosing the template used to display the off header.")]
        public DataTemplateSelector OffHeaderTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(OffHeaderTemplateSelectorProperty); }
            set { SetValue(OffHeaderTemplateSelectorProperty, value); }
        }

        private static void OnOffHeaderTemplateSelectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();
            var instance = (ToggleSwitch)d;
            instance.OnOffHeaderTemplateSelectorChanged((DataTemplateSelector)e.OldValue, (DataTemplateSelector)e.NewValue);
        }

        [PublicAPI]
        // ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnOffHeaderTemplateSelectorChanged(DataTemplateSelector oldOffHeaderTemplateSelector,
                                                                  DataTemplateSelector newOffHeaderTemplateSelector)
            // ReSharper restore VirtualMemberNeverOverriden.Global
        {
            if (OffHeaderTemplate != null && newOffHeaderTemplateSelector != null)
                Trace.TraceError("OffHeaderTemplate and OffHeaderTemplateSelector defined");
        }

        internal virtual void OnToggle()
        {
            IsChecked = !IsChecked;
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ToggleSwitchAutomationPeer(this);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (Template != null)
            {
                _track = Template.FindName(TrackName, this) as FrameworkElement;
                if (_track == null)
                {
                    Trace.TraceWarning(TrackName + " not found.");
                }
                if (_switch != null)
                {
                    _switch.Click -= OnToggle;
                }
                _switch = Template.FindName(SwitchName, this) as Button;
                if (_switch == null)
                {
                    Trace.TraceWarning(SwitchName + " not found.");
                }
                else
                {
                    _switch.Click += OnToggle;
                }
                if (_thumb != null)
                {
                    _thumb.DragStarted -= OnSwitchStarted;
                    _thumb.DragDelta -= OnSwitchChanging;
                    _thumb.DragCompleted -= OnSwitchCompleted;
                }
                _thumb = Template.FindName(ThumbName, this) as Thumb;
                if (_thumb == null)
                {
                    Trace.TraceWarning(ThumbName + " not found.");
                }
                else
                {
                    _thumb.DragStarted += OnSwitchStarted;
                    _thumb.DragDelta += OnSwitchChanging;
                    _thumb.DragCompleted += OnSwitchCompleted;
                }
            }
        }

        private Thickness _originalThumbMargin;

        private void OnSwitchStarted(object sender, DragStartedEventArgs e)
        {
            _originalThumbMargin = _thumb.Margin;
            IsSwitching = true;
        }

        private void OnSwitchChanging(object sender, DragDeltaEventArgs e)
        {
            var margin = !IsChecked ? _thumb.Margin.Left + e.HorizontalChange : _thumb.Margin.Right - e.HorizontalChange;

            if (margin < _originalThumbMargin.Left)
            {
                margin = _originalThumbMargin.Left;
            }
            else if (margin > _track.ActualWidth - _thumb.ActualWidth - _originalThumbMargin.Right)
            {
                margin = _track.ActualWidth - _thumb.ActualWidth - _originalThumbMargin.Right;
            }

            _thumb.Margin = new Thickness(!IsChecked ? margin : _thumb.Margin.Left, _thumb.Margin.Top,
                                          !IsChecked ? _thumb.Margin.Right : margin, _thumb.Margin.Bottom);
        }

        private void OnSwitchCompleted(object sender, DragCompletedEventArgs e)
        {
            _thumb.Margin = _originalThumbMargin;
            IsSwitching = false;
            IsChecked = _thumb.TranslatePoint(new Point(_thumb.ActualWidth / 2.0, 0), _track).X > _track.ActualWidth / 2;
        }

        private void OnToggle(object sender, RoutedEventArgs e)
        {
            OnToggle();
        }
    }
} ;