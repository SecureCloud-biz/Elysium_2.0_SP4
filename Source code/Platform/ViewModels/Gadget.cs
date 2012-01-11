using System;
using System.AddIn.Pipeline;
using System.Diagnostics.Contracts;
using System.Windows;
using Elysium.Theme.ViewModels;

namespace Elysium.Platform.ViewModels
{
    public sealed class Gadget : ViewModelBase
    {
        private readonly Models.Gadget _model;

        private readonly Communication.Gadget _proxy;

        public Gadget(Models.Gadget model)
        {
            _model = model;
            if (!Communication.GadgetHelper.Load(_model.ID, out _domain, out _proxy))
                Dispose();
        }

        public string Assembly
        {
            get { return _model.Assembly; }
        }

        public string Type
        {
            get { return _model.Type; }
        }

        public AppDomain Domain
        {
            get
            {
                Contract.Ensures(Contract.Result<AppDomain>() != null);

                if (_domain == null)
                    throw new InvalidOperationException(Resources.Gadget.IsNotInitialized);

                return _domain;
            }
        }

        private AppDomain _domain;

        public Communication.Info Info
        {
            get
            {
                Contract.Ensures(Contract.Result<Communication.Info>() != null);

                if (_proxy == null)
                    throw new InvalidOperationException(Resources.Gadget.IsNotInitialized);

                return _proxy.Info;
            }
        }

        public Communication.GadgetCallback Callback
        {
            get
            {
                Contract.Ensures(Contract.Result<Communication.GadgetCallback>() != null);

                if (_proxy == null)
                    throw new InvalidOperationException(Resources.Gadget.IsNotInitialized);

                return _proxy.Callback;
            }
        }

        public FrameworkElement Visual
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

        public string Page
        {
            get { return _model.Page; }
            set
            {
                _model.Page = value;
                OnPropertyChanged("Page");
            }
        }

        public string Group
        {
            get { return _model.Group; }
            set
            {
                _model.Group = value;
                OnPropertyChanged("Group");
            }
        }

        public int Column
        {
            get { return _model.Column; }
            set
            {
                if (value < 0)
                    throw new ArgumentException(Resources.Gadget.ColumnValueMustBeGreaterThanOrEqualToZero, "value");
                Contract.EndContractBlock();

                _model.Column = value;
                OnPropertyChanged("Column");
            }
        }

        public int ColumnSpan
        {
            get { return _model.ColumnSpan; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException(Resources.Gadget.ColumnSpanValueMustBeGreaterThanZero, "value");
                if (value > 2)
                    throw new ArgumentException(Resources.Gadget.ColumnSpanValueMustBeLessThanOrEqualToTwo, "value");
                Contract.EndContractBlock();

                _model.ColumnSpan = value;
                OnPropertyChanged("ColumnSpan");
            }
        }

        public int Row
        {
            get { return _model.Row; }
            set
            {
                if (value < 0)
                    throw new ArgumentException(Resources.Gadget.RowValueMustBeGreaterThanOrEqualToZero, "value");
                Contract.EndContractBlock();

                _model.Row = value;
                OnPropertyChanged("Row");
            }
        }

        public bool IsExpandable
        {
            get { return _proxy.IsExpandable; }
        }

        public bool IsExpanded
        {
            get { return _model.IsExpanded; }
            set
            {
                _model.IsExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }

        public bool IsVisible
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

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(_domain != null);
            Contract.Invariant(_model != null);
            Contract.Invariant(_proxy != null);
        }
    }
} ;