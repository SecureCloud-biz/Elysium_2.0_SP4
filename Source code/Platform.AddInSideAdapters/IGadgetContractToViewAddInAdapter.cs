using System;
using System.AddIn.Pipeline;
using System.Windows;
using System.Windows.Media;

using Elysium.Platform.Contracts.Helpers;
using Elysium.Theme;

namespace Elysium.Platform.AddInSideAdapters
{
    public class IGadgetContractToViewAddInAdapter : IGadget
    {
        private readonly Contracts.IGadget _contract;
        private ContractHandle _handle;

        public IGadgetContractToViewAddInAdapter(Contracts.IGadget contract)
        {
            _contract = contract;
            _handle = new ContractHandle(contract);
        }

        public Info Info
        {
            get { return _contract.Info; }
        }

        public Color AccentColor
        {
            get { return _contract.AccentColor; }
            set { _contract.AccentColor = value; }
        }

        public ThemeType Theme
        {
            get { return _contract.Theme; }
            set { _contract.Theme = value; }
        }

        public bool IsExpandable
        {
            get { return _contract.IsExpandable; }
        }

        public bool IsExpanded
        {
            get { return _contract.IsExpanded; }
            set { _contract.IsExpanded = value; }
        }

        public bool IsVisible
        {
            get { return _contract.IsVisible; }
            set { _contract.IsVisible = value; }
        }

        public Action<IApplication> Execute
        {
            get { return _execute ?? (_execute = application => _contract.Execute(IApplicationAddInAdapter.ViewToContractAdapter(application))); }
            set
            {
                if (_execute != value)
                {
                    _execute = value;
                    _contract.Execute = application => value(IApplicationAddInAdapter.ContractToViewAdapter(application));
                }
            }
        }

        private Action<IApplication> _execute;

        public FrameworkElement Visual
        {
            get { return FrameworkElementAdapters.ContractToViewAdapter(_contract.Visual); }
        }

        internal Contracts.IGadget GetSource()
        {
            return _contract;
        }
    }
} ;