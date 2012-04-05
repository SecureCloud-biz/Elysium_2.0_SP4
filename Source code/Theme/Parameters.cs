using System;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

using Elysium.Controls;
using Elysium.Controls.Primitives;
using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium
{
    [PublicAPI]
    public sealed class Parameters
    {
        [PublicAPI]
        public static Parameters Instance { get; private set; }

        static Parameters()
        {
            Instance = new Parameters();
        }

        private Parameters()
        {
        }

        private readonly object _lock = new object();

        #region Font

        [PublicAPI]
        public FontFamily FontFamily
        {
            get
            {
                lock (_lock)
                {
                    return _fontFamily;
                }
            }
            set
            {
                lock (_lock)
                {
                    _fontFamily = value;
                }
            }
        }

        private FontFamily _fontFamily = new FontFamily("Segoe UI");

        [PublicAPI]
        public double TitleFontSize
        {
            get
            {
                lock (_lock)
                {
                    return _titleFontSize;
                }
            }
            set
            {
                lock (_lock)
                {
                    _titleFontSize = value;
                }
            }
        }

        private double _titleFontSize = 12.0 * (96.0 / 72.0);

        [PublicAPI]
        public double HeaderFontSize
        {
            get
            {
                lock (_lock)
                {
                    return _headerFontSize;
                }
            }
            set
            {
                lock (_lock)
                {
                    _headerFontSize = value;
                }
            }
        }

        private double _headerFontSize = 16.0 * (96.0 / 72.0);

        [PublicAPI]
        public double ContentFontSize
        {
            get
            {
                lock (_lock)
                {
                    return _contentFontSize;
                }
            }
            set
            {
                lock (_lock)
                {
                    _contentFontSize = value;
                }
            }
        }

        private double _contentFontSize = 10.0 * (96.0 / 72.0);

        [PublicAPI]
        public double TextFontSize
        {
            get
            {
                lock (_lock)
                {
                    return _textFontSize;
                }
            }
            set
            {
                lock (_lock)
                {
                    _textFontSize = value;
                }
            }
        }

        private double _textFontSize = 9.0 * (96.0 / 72.0);

        #endregion

        #region Thickness

        [PublicAPI]
        public Thickness DefaultThickness
        {
            get
            {
                lock (_lock)
                {
                    return _defaultThickness;
                }
            }
            set
            {
                lock (_lock)
                {
                    _defaultThickness = value;
                }
            }
        }

        private Thickness _defaultThickness = new Thickness(1.0);

        [PublicAPI]
        public Thickness SemiBoldThickness
        {
            get
            {
                lock (_lock)
                {
                    return _semiBoldThickness;
                }
            }
            set
            {
                lock (_lock)
                {
                    _semiBoldThickness = value;
                }
            }
        }

        private Thickness _semiBoldThickness = new Thickness(1.5);

        [PublicAPI]
        public Thickness BoldThickness
        {
            get
            {
                lock (_lock)
                {
                    return _boldThickness;
                }
            }
            set
            {
                lock (_lock)
                {
                    _boldThickness = value;
                }
            }
        }

        private Thickness _boldThickness = new Thickness(2.0);

        [PublicAPI]
        public double DefaultThicknessValue
        {
            get
            {
                lock (_lock)
                {
                    return _defaultThicknessValue;
                }
            }
            set
            {
                lock (_lock)
                {
                    _defaultThicknessValue = value;
                }
            }
        }

        private double _defaultThicknessValue = 1.0;

        [PublicAPI]
        public double SemiBoldThicknessValue
        {
            get
            {
                lock (_lock)
                {
                    return _semiBoldThicknessValue;
                }
            }
            set
            {
                lock (_lock)
                {
                    _semiBoldThicknessValue = value;
                }
            }
        }

        private double _semiBoldThicknessValue = 1.5;

        [PublicAPI]
        public double BoldThicknessValue
        {
            get
            {
                lock (_lock)
                {
                    return _boldThicknessValue;
                }
            }
            set
            {
                lock (_lock)
                {
                    _boldThicknessValue = value;
                }
            }
        }

        private double _boldThicknessValue = 2.0;

        #endregion

        #region Padding

        [PublicAPI]
        public Thickness DefaultPadding
        {
            get
            {
                lock (_lock)
                {
                    return _defaultPadding;
                }
            }
            set
            {
                lock (_lock)
                {
                    _defaultPadding = value;
                }
            }
        }

        private Thickness _defaultPadding = new Thickness(1.0);

        [PublicAPI]
        public Thickness SemiBoldPadding
        {
            get
            {
                lock (_lock)
                {
                    return _semiBoldPadding;
                }
            }
            set
            {
                lock (_lock)
                {
                    _semiBoldPadding = value;
                }
            }
        }

        private Thickness _semiBoldPadding = new Thickness(2.0);

        [PublicAPI]
        public Thickness BoldPadding
        {
            get
            {
                lock (_lock)
                {
                    return _boldPadding;
                }
            }
            set
            {
                lock (_lock)
                {
                    _boldPadding = value;
                }
            }
        }

        private Thickness _boldPadding = new Thickness(5.0);

        [PublicAPI]
        public double DefaultPaddingValue
        {
            get
            {
                lock (_lock)
                {
                    return _defaultPaddingValue;
                }
            }
            set
            {
                lock (_lock)
                {
                    _defaultPaddingValue = value;
                }
            }
        }

        private double _defaultPaddingValue = 1.0;

        [PublicAPI]
        public double SemiBoldPaddingValue
        {
            get
            {
                lock (_lock)
                {
                    return _semiBoldPaddingValue;
                }
            }
            set
            {
                lock (_lock)
                {
                    _semiBoldPaddingValue = value;
                }
            }
        }

        private double _semiBoldPaddingValue = 2.0;

        [PublicAPI]
        public double BoldPaddingValue
        {
            get
            {
                lock (_lock)
                {
                    return _boldPaddingValue;
                }
            }
            set
            {
                lock (_lock)
                {
                    _boldPaddingValue = value;
                }
            }
        }

        private double _boldPaddingValue = 5.0;

        #endregion

        #region Animation

        [PublicAPI]
        public static readonly DependencyProperty DefaultDurationProperty =
            DependencyProperty.RegisterAttached("DefaultDuration", typeof(Duration), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(0.0)), FrameworkPropertyMetadataOptions.None));

        [PublicAPI]
        public static Duration GetDefaultDuration(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<Duration>.Unbox(obj.GetValue(DefaultDurationProperty));
        }

        [PublicAPI]
        public static void SetDefaultDuration(DependencyObject obj, Duration value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(DefaultDurationProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty MinimumDurationProperty =
            DependencyProperty.RegisterAttached("MinimumDuration", typeof(Duration), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(0.2)), FrameworkPropertyMetadataOptions.None));

        [PublicAPI]
        public static Duration GetMinimumDuration(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<Duration>.Unbox(obj.GetValue(MinimumDurationProperty));
        }

        [PublicAPI]
        public static void SetMinimumDuration(DependencyObject obj, Duration value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(MinimumDurationProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty OptimumDurationProperty =
            DependencyProperty.RegisterAttached("OptimumDuration", typeof(Duration), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(0.5)), FrameworkPropertyMetadataOptions.None));

        [PublicAPI]
        public static Duration GetOptimumDuration(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<Duration>.Unbox(obj.GetValue(OptimumDurationProperty));
        }

        [PublicAPI]
        public static void SetOptimumDuration(DependencyObject obj, Duration value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(OptimumDurationProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty MaximumDurationProperty =
            DependencyProperty.RegisterAttached("MaximumDuration", typeof(Duration), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(1.0)), FrameworkPropertyMetadataOptions.None));

        [PublicAPI]
        public static Duration GetMaximumDuration(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<Duration>.Unbox(obj.GetValue(MaximumDurationProperty));
        }

        [PublicAPI]
        public static void SetMaximumDuration(DependencyObject obj, Duration value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(MaximumDurationProperty, value);
        }

        #endregion

        #region Buttons

        [PublicAPI]
        public static readonly DependencyProperty CommandButtonMaskProperty =
            DependencyProperty.RegisterAttached("CommandButtonMask", typeof(Brush), typeof(Parameters),
                                                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                    FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(CommandButton))]
        public static Brush GetCommandButtonMask(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return (Brush)obj.GetValue(CommandButtonMaskProperty);
        }

        [PublicAPI]
        public static void SetCommandButtonMask(DependencyObject obj, Brush value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(CommandButtonMaskProperty, value);
        }

        #endregion

        #region CheckBox & RadioButton

        [PublicAPI]
        public static readonly DependencyProperty BulletDecoratorSizeProperty =
            DependencyProperty.RegisterAttached("BulletDecoratorSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(16.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(CheckBox))]
        [AttachedPropertyBrowsableForType(typeof(RadioButton))]
        public static double GetBulletDecoratorSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(BulletDecoratorSizeProperty));
        }

        [PublicAPI]
        public static void SetBulletDecoratorSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(BulletDecoratorSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty BulletSizeProperty =
            DependencyProperty.RegisterAttached("BulletSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(8.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(CheckBox))]
        [AttachedPropertyBrowsableForType(typeof(RadioButton))]
        public static double GetBulletSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(BulletSizeProperty));
        }

        [PublicAPI]
        public static void SetBulletSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(BulletSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty CheckSizeProperty =
            DependencyProperty.RegisterAttached("CheckSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(CheckBox))]
        public static double GetCheckSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(CheckSizeProperty));
        }

        [PublicAPI]
        public static void SetCheckSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(CheckSizeProperty, value);
        }

        #endregion

        #region ToggleSwitch

        [PublicAPI]
        public static readonly DependencyProperty ToggleSwitchTrackSizeProperty =
            DependencyProperty.RegisterAttached("ToggleSwitchTrackSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(48.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ToggleSwitch))]
        public static double GetToggleSwitchTrackSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(ToggleSwitchTrackSizeProperty));
        }

        [PublicAPI]
        public static void SetToggleSwitchTrackSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(ToggleSwitchTrackSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty ToggleSwitchThumbThicknessProperty =
            DependencyProperty.RegisterAttached("ToggleSwitchThumbThickness", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ToggleSwitch))]
        public static double GetToggleSwitchThumbThickness(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(ToggleSwitchThumbThicknessProperty));
        }

        [PublicAPI]
        public static void SetToggleSwitchThumbThickness(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(ToggleSwitchThumbThicknessProperty, value);
        }

        #endregion

        #region ComboBox

        [PublicAPI]
        public static readonly DependencyProperty ComboBoxButtonSizeProperty =
            DependencyProperty.RegisterAttached("ComboBoxButtonSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(18.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static double GetComboBoxButtonSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(ComboBoxButtonSizeProperty));
        }

        [PublicAPI]
        public static void SetComboBoxButtonSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(ComboBoxButtonSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty ComboBoxArrowSizeProperty =
            DependencyProperty.RegisterAttached("ComboBoxArrowSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(8.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static double GetComboBoxArrowSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(ComboBoxArrowSizeProperty));
        }

        [PublicAPI]
        public static void SetComboBoxArrowSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(ComboBoxArrowSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty ComboBoxArrowMarginProperty =
            DependencyProperty.RegisterAttached("ComboBoxArrowMargin", typeof(Thickness), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Thickness(5, 0, 5, 0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static Thickness GetComboBoxArrowMargin(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(ComboBoxArrowMarginProperty));
        }

        [PublicAPI]
        public static void SetComboBoxArrowMargin(DependencyObject obj, Thickness value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(ComboBoxArrowMarginProperty, value);
        }

        #endregion

        #region Slider

        [PublicAPI]
        public static readonly DependencyProperty SliderTrackSizeProperty =
            DependencyProperty.RegisterAttached("SliderTrackSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(4.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(Slider))]
        public static double GetSliderTrackSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(SliderTrackSizeProperty));
        }

        [PublicAPI]
        public static void SetSliderTrackSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(SliderTrackSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty SliderThumbThicknessProperty =
            DependencyProperty.RegisterAttached("SliderThumbThickness", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(6.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(Slider))]
        public static double GetSliderThumbThickness(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(SliderThumbThicknessProperty));
        }

        [PublicAPI]
        public static void SetSliderThumbThickness(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(SliderThumbThicknessProperty, value);
        }

        #endregion

        #region ProgressBar

        [PublicAPI]
        public static readonly DependencyProperty ProgressBarBusyElementSizeProperty =
            DependencyProperty.RegisterAttached("ProgressBarBusyElementSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(4.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ProgressBarBase))]
        public static double GetProgressBarBusyElementSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(ProgressBarBusyElementSizeProperty));
        }

        [PublicAPI]
        public static void SetProgressBarBusyElementSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(ProgressBarBusyElementSizeProperty, value);
        }

        #endregion

        #region ScrollBar

        [PublicAPI]
        public static readonly DependencyProperty ScrollBarArrowSizeProperty =
            DependencyProperty.RegisterAttached("ScrollBarArrowSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(6.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ScrollBar))]
        public static double GetScrollBarArrowSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(ScrollBarArrowSizeProperty));
        }

        [PublicAPI]
        public static void SetScrollBarArrowSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(ScrollBarArrowSizeProperty, value);
        }

        #endregion

        #region TabControl

        [PublicAPI]
        public static readonly DependencyProperty TabControlIndicatorBrushProperty =
            DependencyProperty.RegisterAttached("TabControlIndicatorBrush", typeof(Brush), typeof(Parameters),
                                                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                    FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(TabControl))]
        [AttachedPropertyBrowsableForType(typeof(TabItem))]
        public static Brush GetTabControlIndicatorBrush(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return (Brush)obj.GetValue(TabControlIndicatorBrushProperty);
        }

        [PublicAPI]
        public static void SetTabControlIndicatorBrush(DependencyObject obj, Brush value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(TabControlIndicatorBrushProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty TabControlIndicatorThicknessProperty =
            DependencyProperty.RegisterAttached("TabControlIndicatorThickness", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(2.0, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                   FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(TabControl))]
        public static Thickness GetTabControlIndicatorThickness(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(TabControlIndicatorThicknessProperty));
        }

        [PublicAPI]
        public static void SetTabControlIndicatorThickness(DependencyObject obj, Thickness value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(TabControlIndicatorThicknessProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty TabItemHeaderStyleProperty =
            DependencyProperty.RegisterAttached("TabItemHeaderStyle", typeof(Style), typeof(Parameters),
                                                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(TabItem))]
        public static Style GetTabItemHeaderStyle(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return (Style)obj.GetValue(TabItemHeaderStyleProperty);
        }

        [PublicAPI]
        public static void SetTabItemHeaderStyle(DependencyObject obj, Style value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(TabItemHeaderStyleProperty, value);
        }

        #endregion

        #region Menu

        [PublicAPI]
        public static readonly DependencyProperty SubmenuItemBulletSizeProperty =
            DependencyProperty.RegisterAttached("SubmenuItemBulletSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(12.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(MenuItem))]
        public static double GetSubmenuItemBulletSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(SubmenuItemBulletSizeProperty));
        }

        [PublicAPI]
        public static void SetSubmenuItemBulletSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(SubmenuItemBulletSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty SubmenuHeaderArrowSizeProperty =
            DependencyProperty.RegisterAttached("SubmenuHeaderArrowSize", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(8.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(MenuItem))]
        public static double GetSubmenuHeaderArrowSize(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(SubmenuHeaderArrowSizeProperty));
        }

        [PublicAPI]
        public static void SetSubmenuHeaderArrowSize(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(SubmenuHeaderArrowSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty SubmenuHeaderArrowMarginProperty =
            DependencyProperty.RegisterAttached("SubmenuHeaderArrowMargin", typeof(Thickness), typeof(Parameters),
                                                new FrameworkPropertyMetadata(new Thickness(3, 0, 3, 0), FrameworkPropertyMetadataOptions.AffectsMeasure));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(MenuItem))]
        public static Thickness GetSubmenuHeaderArrowMargin(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(SubmenuHeaderArrowMarginProperty));
        }

        [PublicAPI]
        public static void SetSubmenuHeaderArrowMargin(DependencyObject obj, Thickness value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(SubmenuHeaderArrowMarginProperty, value);
        }

        #endregion

        #region Window

        [PublicAPI]
        public static readonly DependencyProperty WindowResizeBorderThicknessProperty =
            DependencyProperty.RegisterAttached("WindowResizeBorderThickness", typeof(double), typeof(Parameters),
                                                new FrameworkPropertyMetadata(3.0, FrameworkPropertyMetadataOptions.None));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(Controls.Window))]
        public static double GetWindowResizeBorderThickness(Controls.Window obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            return BoxingHelper<double>.Unbox(obj.GetValue(WindowResizeBorderThicknessProperty));
        }

        [PublicAPI]
        public static void SetWindowResizeBorderThickness(Controls.Window obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Contract.EndContractBlock();
            obj.SetValue(WindowResizeBorderThicknessProperty, value);
        }

        #endregion

        #region Invariants

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(!double.IsNaN(TitleFontSize) && !double.IsInfinity(TitleFontSize) && TitleFontSize > 0.0);
            Contract.Invariant(!double.IsNaN(HeaderFontSize) && !double.IsInfinity(HeaderFontSize) && HeaderFontSize > 0.0);
            Contract.Invariant(!double.IsNaN(ContentFontSize) && !double.IsInfinity(ContentFontSize) && ContentFontSize > 0.0);
            Contract.Invariant(!double.IsNaN(TextFontSize) && !double.IsInfinity(TextFontSize) && TextFontSize > 0.0);
        }

        #endregion
    }
} ;