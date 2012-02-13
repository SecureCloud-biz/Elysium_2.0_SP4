using System;
using System.AddIn.Contract;
using System.AddIn.Pipeline;
using System.Windows.Media;

using Elysium.Platform.Contracts.Helpers;
using Elysium.Theme;

namespace Elysium.Platform.AddInSideAdapters
{
    [AddInAdapter]
    public class IGadgetViewToContractAddInAdapter : ContractBase, Contracts.IGadget
    {
        private readonly IGadget _view;

        public IGadgetViewToContractAddInAdapter(IGadget view)
        {
            _view = view;
        }

        public Info Info
        {
            get { return _view.Info; }
        }

        public Color AccentColor
        {
            get { return _view.AccentColor; }
            set { _view.AccentColor = value; }
        }

        public ThemeType Theme
        {
            get { return _view.Theme; }
            set { _view.Theme = value; }
        }

        public bool IsExpandable
        {
            get { return _view.IsExpandable; }
        }

        public bool IsExpanded
        {
            get { return _view.IsExpanded; }
            set { _view.IsExpanded = value; }
        }

        public bool IsVisible
        {
            get { return _view.IsVisible; }
            set { _view.IsVisible = value; }
        }

        public Action<Contracts.IApplication> Execute
        {
            get { return _execute ?? (_execute = application => _view.Execute(IApplicationAddInAdapter.ContractToViewAdapter(application))); }
            set
            {
                if (_execute != value)
                {
                    _execute = value;
                    _view.Execute = application => value(IApplicationAddInAdapter.ViewToContractAdapter(application));
                }
            }
        }

        private Action<Contracts.IApplication> _execute;

        public INativeHandleContract Visual
        {
            get { return FrameworkElementAdapters.ViewToContractAdapter(_view.Visual); }
        }

        internal IGadget GetSource()
        {
            return _view;
        }
    }
} ;