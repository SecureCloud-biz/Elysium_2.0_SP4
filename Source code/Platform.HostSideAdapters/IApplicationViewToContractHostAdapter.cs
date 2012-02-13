using System;
using System.AddIn.Contract;
using System.AddIn.Pipeline;
using System.Windows.Media;

using Elysium.Platform.Contracts.Helpers;
using Elysium.Theme;

namespace Elysium.Platform.HostSideAdapters
{
    public class IApplicationViewToContractHostAdapter : ContractBase, Contracts.IApplication
    {
        private readonly IApplication _view;

        public IApplicationViewToContractHostAdapter(IApplication view)
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

        public bool IsAttachable
        {
            get { return _view.IsAttachable; }
        }

        public bool IsAttached
        {
            get { return _view.IsAttached; }
            set { _view.IsAttached = value; }
        }

        public bool IsVisible
        {
            get { return _view.IsVisible; }
            set { _view.IsVisible = value; }
        }

        public void Suspend()
        {
            _view.Suspend();
        }

        public void Resume()
        {
            _view.Resume();
        }

        public Action Close
        {
            get { return _view.Close; }
            set { _view.Close = value; }
        }

        public INativeHandleContract Visual
        {
            get { return FrameworkElementAdapters.ViewToContractAdapter(_view.Visual); }
        }

        internal IApplication GetSourceView()
        {
            return _view;
        }
    }
} ;