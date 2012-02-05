using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;

using Elysium.Theme.Extensions;

using JetBrains.Annotations;

namespace Elysium.Theme.Controls.Primitives
{
    [PublicAPI]
    [TemplatePart(Name = DecorName, Type = typeof(Ellipse))]
    [TemplatePart(Name = MaskName, Type = typeof(Ellipse))]
    [TemplatePart(Name = HeaderHostName, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = ContentHostName, Type = typeof(ContentPresenter))]
    public class CommandButtonBase : ButtonBase
    {
        private const string DecorName = "PART_Decor";
        private const string MaskName = "PART_Mask";
        private const string HeaderHostName = "PART_HeaderHost";
        private const string ContentHostName = "PART_ContentHost";

        private Ellipse _decor;
        private Ellipse _mask;
        private ContentPresenter _headerHost;
        private ContentPresenter _contentHost;

        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static CommandButtonBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CommandButtonBase), new FrameworkPropertyMetadata(typeof(CommandButtonBase)));
        }

        [PublicAPI]
        public static readonly DependencyProperty HeaderProperty =
            HeaderedContentControl.HeaderProperty.AddOwner(typeof(CommandButtonBase), new FrameworkPropertyMetadata((object)null, OnHeaderChanged));

        [PublicAPI]
        [Bindable(true)]
        [Category("Content")]
        [Description("The data used for the header of each control.")]
        [Localizability(LocalizationCategory.Label)]
        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();
            var instance = (CommandButtonBase)d;
            instance.OnHeaderChanged(e.OldValue, e.NewValue);
            instance.HasHeader = e.NewValue != null;
        }

        [PublicAPI]
// ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnHeaderChanged(object oldHeader, object newHeader)
// ReSharper restore VirtualMemberNeverOverriden.Global
        {
            RemoveLogicalChild(oldHeader);
            AddLogicalChild(newHeader);
        }

        private static readonly DependencyPropertyKey HasHeaderPropertyKey =
            DependencyProperty.RegisterReadOnly("HasHeader", typeof(bool), typeof(CommandButtonBase),
                                                new FrameworkPropertyMetadata(BooleanBoxingHelper.FalseBox, OnHasHeaderChanged));

        [PublicAPI]
        public static readonly DependencyProperty HasHeaderProperty = HasHeaderPropertyKey.DependencyProperty;

        [PublicAPI]
        [Bindable(false)]
        [Browsable(false)]
        public bool HasHeader
        {
            get { return BooleanBoxingHelper.Unbox(GetValue(HasHeaderProperty)); }
            private set { SetValue(HasHeaderPropertyKey, BooleanBoxingHelper.Box(value)); }
        }

        private static void OnHasHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();
            var instance = (CommandButtonBase)d;
            instance.OnHasHeaderChanged(BooleanBoxingHelper.Unbox(e.OldValue), BooleanBoxingHelper.Unbox(e.NewValue));
        }

        [PublicAPI]
        // ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnHasHeaderChanged(bool oldHeader, bool newHeader)
            // ReSharper restore VirtualMemberNeverOverriden.Global
        {
        }

        [PublicAPI]
        public static readonly DependencyProperty HeaderStringFormatProperty =
            HeaderedContentControl.HeaderStringFormatProperty.AddOwner(typeof(CommandButtonBase),
                                                                       new FrameworkPropertyMetadata(null, OnHeaderStringFormatChanged));

        [PublicAPI]
        [Bindable(true)]
        [Category("Content")]
        [Description("A composite string that specifies how to format the Header property if it is displayed as a string.")]
        public string HeaderStringFormat
        {
            get { return (string)GetValue(HeaderStringFormatProperty); }
            set { SetValue(HeaderStringFormatProperty, value); }
        }

        private static void OnHeaderStringFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();
            var instance = (CommandButtonBase)d;
            instance.OnHeaderStringFormatChanged((string)e.OldValue, (string)e.NewValue);
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
// ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnHeaderStringFormatChanged(string oldHeaderStringFormat, string newHeaderStringFormat)
// ReSharper restore VirtualMemberNeverOverriden.Global
        {
        }

        [PublicAPI]
        public static readonly DependencyProperty HeaderTemplateProperty =
            HeaderedContentControl.HeaderTemplateProperty.AddOwner(typeof(CommandButtonBase),
                                                                   new FrameworkPropertyMetadata(null, OnHeaderTemplateChanged));

        [PublicAPI]
        [Bindable(true)]
        [Category("Content")]
        [Description("The template used to display the content of the control's header.")]
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        private static void OnHeaderTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();
            var instance = (CommandButtonBase)d;
            instance.OnHeaderTemplateChanged((DataTemplate)e.OldValue, (DataTemplate)e.NewValue);
        }

        [PublicAPI]
// ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnHeaderTemplateChanged(DataTemplate oldHeaderTemplate, DataTemplate newHeaderTemplate)
// ReSharper restore VirtualMemberNeverOverriden.Global
        {
            if (newHeaderTemplate != null && HeaderTemplateSelector != null)
                Trace.TraceError("Template and TemplateSelector defined");
        }

        [PublicAPI]
        public static readonly DependencyProperty HeaderTemplateSelectorProperty =
            HeaderedContentControl.HeaderTemplateSelectorProperty.AddOwner(typeof(CommandButtonBase),
                                                                           new FrameworkPropertyMetadata(null, OnHeaderTemplateSelectorChanged));

        [PublicAPI]
        [Bindable(true)]
        [Category("Content")]
        [Description("A data template selector that provides custom logic for choosing the template used to display the header.")]
        public DataTemplateSelector HeaderTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(HeaderTemplateSelectorProperty); }
            set { SetValue(HeaderTemplateSelectorProperty, value); }
        }

        private static void OnHeaderTemplateSelectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            Contract.EndContractBlock();
            var instance = (CommandButtonBase)d;
            instance.OnHeaderTemplateSelectorChanged((DataTemplateSelector)e.OldValue, (DataTemplateSelector)e.NewValue);
        }

        [PublicAPI]
// ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnHeaderTemplateSelectorChanged(DataTemplateSelector oldHeaderTemplateSelector, DataTemplateSelector newHeaderTemplateSelector)
// ReSharper restore VirtualMemberNeverOverriden.Global
        {
            if (HeaderTemplate != null && newHeaderTemplateSelector != null)
                Trace.TraceError("Template and TemplateSelector defined");
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (Template != null)
            {
                _decor = Template.FindName(DecorName, this) as Ellipse;
                // NOTE: WPF doesn't declare contracts
                Contract.Assume(Template != null);
                _mask = Template.FindName(MaskName, this) as Ellipse;
                // NOTE: WPF doesn't declare contracts
                Contract.Assume(Template != null);
                _headerHost = Template.FindName(HeaderHostName, this) as ContentPresenter;
                // NOTE: WPF doesn't declare contracts
                Contract.Assume(Template != null);
                _contentHost = Template.FindName(ContentHostName, this) as ContentPresenter;
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (_decor != null && _mask != null && _headerHost != null && _contentHost != null)
            {
                var infinitySize = new Size(double.PositiveInfinity, double.PositiveInfinity);
                _contentHost.Measure(infinitySize);
                _headerHost.Measure(infinitySize);

                var contentSize = Math.Max(_contentHost.DesiredSize.Width, _contentHost.DesiredSize.Height) * Math.Sqrt(2.0);

                var constraintWidth = double.IsNaN(constraint.Width) || double.IsInfinity(constraint.Width) ? double.MaxValue : constraint.Width;
                var constraintHeight = double.IsNaN(constraint.Height) || double.IsInfinity(constraint.Height) ? 0.0 : constraint.Height;

                var width = Math.Min(Math.Max(contentSize, _headerHost.DesiredSize.Width), constraintWidth);
                var height = Math.Max(contentSize + _headerHost.DesiredSize.Height, constraintHeight);

                Contract.Assume(width >= 0.0);
                Contract.Assume(height >= 0.0);

                var boxSize = Math.Min(width, height - _headerHost.DesiredSize.Height);

                Contract.Assume(boxSize >= 0.0);

                _decor.Width = boxSize;
                _decor.Height = boxSize;
                _mask.Width = boxSize;
                _mask.Height = boxSize;

                _contentHost.InvalidateMeasure();
                _headerHost.InvalidateMeasure();

                return new Size(width, height);
            }
            return base.MeasureOverride(constraint);
        }
    }
} ;