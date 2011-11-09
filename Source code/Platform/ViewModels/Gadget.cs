using System;
using System.AddIn.Pipeline;
using System.Diagnostics.Contracts;
using System.Windows;
using Elysium.Theme.ViewModels;

namespace Elysium.Platform.ViewModels
{
    internal sealed class Gadget : ViewModelBase
    {
        private readonly Models.Gadget _model;

        private readonly Communication.Gadget _proxy;

        internal Gadget(Models.Gadget model)
        {
            _model = model;
            if (!Communication.GadgetHelper.Load(_model.ID, out _proxy))
                Dispose();
        }

        internal string Assembly
        {
            get { return _model.Assembly; }
        }

        internal string Type
        {
            get { return _model.Type; }
        }

        internal AppDomain Domain
        {
            get
            {
                Contract.Ensures(Contract.Result<AppDomain>() != null);

                if (_domain == null)
                    throw new InvalidOperationException(Resources.Gadget.IsNotInitialized);

                return _domain;
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null, "value");

                _domain = value;
                OnPropertyChanged("Domain");
            }
        }

        private AppDomain _domain;

        internal Communication.Info Info
        {
            get
            {
                Contract.Ensures(Contract.Result<Communication.Info>() != null);

                if (_proxy == null)
                    throw new InvalidOperationException(Resources.Gadget.IsNotInitialized);

                return _proxy.Info;
            }
        }

        internal Communication.GadgetCallback Callback
        {
            get
            {
                Contract.Ensures(Contract.Result<Communication.GadgetCallback>() != null);

                if (_proxy == null)
                    throw new InvalidOperationException(Resources.Gadget.IsNotInitialized);

                return _proxy.Callback;
            }
        }

        internal FrameworkElement Visual
        {
            get
            {
                Contract.Ensures(Contract.Result<FrameworkElement>() != null);

                if (_visual == null)
                {
                    if (_proxy == null)
                        throw new InvalidOperationException(Resources.Gadget.IsNotInitialized);
                    _visual = FrameworkElementAdapters.ContractToViewAdapter(_proxy.Visual);
                }

                return _visual;
            }
        }

        private FrameworkElement _visual;

        internal string Page
        {
            get { return _model.Page; }
            set
            {
                _model.Page = value;
                OnPropertyChanged("Page");
            }
        }

        internal string Group
        {
            get { return _model.Group; }
            set
            {
                _model.Group = value;
                OnPropertyChanged("Group");
            }
        }

        internal int Column
        {
            get { return _model.Column; }
            set
            {
                Contract.Requires<ArgumentException>(value >= 0, Resources.Gadget.ColumnValueMustBeGreaterThanOrEqualToZero);

                _model.Column = value;
                OnPropertyChanged("Column");
            }
        }

        internal int ColumnSpan
        {
            get { return _model.ColumnSpan; }
            set
            {
                Contract.Requires<ArgumentException>(value > 0, Resources.Gadget.ColumnSpanValueMustBeGreaterThanZero);
                Contract.Requires<ArgumentException>(value <= 2, Resources.Gadget.ColumnSpanValueMustBeLessThanOrEqualToTwo);

                _model.ColumnSpan = value;
                OnPropertyChanged("ColumnSpan");
            }
        }

        internal int Row
        {
            get { return _model.Row; }
            set
            {
                Contract.Requires<ArgumentException>(value >= 0, Resources.Gadget.RowValueMustBeGreaterThanOrEqualToZero);

                _model.Row = value;
                OnPropertyChanged("Row");
            }
        }

        internal bool IsExpandable
        {
            get { return _proxy.IsExpandable; }
        }

        internal bool IsExpanded
        {
            get { return _model.IsExpanded; }
            set
            {
                _model.IsExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }

        internal bool IsVisible
        {
            get { return _model.IsVisible; }
            set
            {
                _model.IsVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (!Disposed)
            {
                if (disposing)
                {
                    Communication.GadgetHelper.Unload(_model.ID);
                }

                Disposed = true;
            }
        }
    }
} ;