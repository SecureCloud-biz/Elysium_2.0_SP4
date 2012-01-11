using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;

namespace Elysium.Theme.Controls.Primitives
{
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

        static CommandButtonBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CommandButtonBase), new FrameworkPropertyMetadata(typeof(CommandButtonBase)));
        }

        public static readonly DependencyProperty HeaderProperty =
            HeaderedContentControl.HeaderProperty.AddOwner(typeof(CommandButtonBase), new FrameworkPropertyMetadata((object)null, OnHeaderChanged));

        [Bindable(true)]
        [Category("Content")]
        [Localizability(LocalizationCategory.Label)]
        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (CommandButtonBase)d;
            control.HasHeader = e.NewValue != null;
        }

        protected virtual void OnHeaderChanged(object oldHeader, object newHeader)
        {
            RemoveLogicalChild(oldHeader);
            AddLogicalChild(newHeader);
        }

        private static readonly DependencyPropertyKey HasHeaderPropertyKey =
            DependencyProperty.RegisterReadOnly("HasHeader", typeof(bool), typeof(CommandButtonBase), new FrameworkPropertyMetadata(false));

        public static readonly DependencyProperty HasHeaderProperty = HasHeaderPropertyKey.DependencyProperty;

        [Bindable(false)]
        [Browsable(false)]
        public bool HasHeader
        {
            get { return (bool)GetValue(HasHeaderProperty); }
            private set { SetValue(HasHeaderPropertyKey, value); }
        }

        public static readonly DependencyProperty HeaderStringFormatProperty =
            HeaderedContentControl.HeaderStringFormatProperty.AddOwner(typeof(CommandButtonBase),
                                                                       new FrameworkPropertyMetadata((string)null, OnHeaderStringFormatChanged));

        [Bindable(true)]
        [Category("Content")]
        public string HeaderStringFormat
        {
            get { return (string)GetValue(HeaderStringFormatProperty); }
            set { SetValue(HeaderStringFormatProperty, value); }
        }

        private static void OnHeaderStringFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (CommandButtonBase)d;
            instance.OnHeaderStringFormatChanged((string)e.OldValue, (string)e.NewValue);
        }

        protected virtual void OnHeaderStringFormatChanged(string oldHeaderStringFormat, string newHeaderStringFormat)
        {
        }

        public static readonly DependencyProperty HeaderTemplateProperty =
            HeaderedContentControl.HeaderTemplateProperty.AddOwner(typeof(CommandButtonBase),
                                                                   new FrameworkPropertyMetadata((DataTemplate)null, OnHeaderTemplateChanged));

        [Bindable(true)]
        [Category("Content")]
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        private static void OnHeaderTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (CommandButtonBase)d;
            instance.OnHeaderTemplateChanged((DataTemplate)e.OldValue, (DataTemplate)e.NewValue);
        }

        protected virtual void OnHeaderTemplateChanged(DataTemplate oldHeaderTemplate, DataTemplate newHeaderTemplate)
        {
            if (newHeaderTemplate != null && HeaderTemplateSelector != null)
                Trace.TraceError("Template and TemplateSelector defined");
        }

        public static readonly DependencyProperty HeaderTemplateSelectorProperty =
            HeaderedContentControl.HeaderTemplateSelectorProperty.AddOwner(typeof(CommandButtonBase),
                                                                           new FrameworkPropertyMetadata((DataTemplateSelector)null,
                                                                                                         OnHeaderTemplateSelectorChanged));

        [Bindable(true)]
        [Category("Content")]
        public DataTemplateSelector HeaderTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(HeaderTemplateSelectorProperty); }
            set { SetValue(HeaderTemplateSelectorProperty, value); }
        }

        private static void OnHeaderTemplateSelectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (CommandButtonBase)d;
            instance.OnHeaderTemplateSelectorChanged((DataTemplateSelector)e.OldValue, (DataTemplateSelector)e.NewValue);
        }

        protected virtual void OnHeaderTemplateSelectorChanged(DataTemplateSelector oldHeaderTemplateSelector, DataTemplateSelector newHeaderTemplateSelector)
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
                _mask = Template.FindName(MaskName, this) as Ellipse;
                _headerHost = Template.FindName(HeaderHostName, this) as ContentPresenter;
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

                var boxSize = Math.Min(width, height - _headerHost.DesiredSize.Height);
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