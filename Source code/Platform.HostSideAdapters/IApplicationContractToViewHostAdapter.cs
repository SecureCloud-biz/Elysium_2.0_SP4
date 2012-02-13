using System;
using System.AddIn.Pipeline;
using System.Windows;
using System.Windows.Media;

using Elysium.Platform.Contracts.Helpers;
using Elysium.Theme;

namespace Elysium.Platform.HostSideAdapters
{
    [HostAdapter]
    public class IApplicationContractToViewHostAdapter : IApplication
    {
        private readonly Contracts.IApplication _contract;
        private ContractHandle _handle;

        public IApplicationContractToViewHostAdapter(Contracts.IApplication contract)
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

        public bool IsAttachable
        {
            get { return _contract.IsAttachable; }
        }

        public bool IsAttached
        {
            get { return _contract.IsAttached; }
            set { _contract.IsAttached = value; }
        }

        public bool IsVisible
        {
            get { return _contract.IsVisible; }
            set { _contract.IsVisible = value; }
        }

        public void Suspend()
        {
            _contract.Suspend();
        }

        public void Resume()
        {
            _contract.Resume();
        }

        public Action Close
        {
            get { return _contract.Close; }
            set { _contract.Close = value; }
        }

        public FrameworkElement Visual
        {
            get { return FrameworkElementAdapters.ContractToViewAdapter(_contract.Visual); }
        }

        internal Contracts.IApplication GetSource()
        {
            return _contract;
        }
    }
} ;