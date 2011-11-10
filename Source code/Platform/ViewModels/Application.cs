using System;
using System.AddIn.Pipeline;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Windows;
using Elysium.Theme.ViewModels;

namespace Elysium.Platform.ViewModels
{
    internal sealed class Application : ViewModelBase
    {
        private readonly Models.Application _model;

        private readonly Communication.Application _proxy;

        internal Application(Models.Application model)
        {
            _model = model;
            if (!Communication.ApplicationHelper.Load(_model.ID, out _proxy))
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
                    throw new InvalidOperationException(Resources.Application.IsNotInitialized);

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

        internal Thread Thread
        {
            get
            {
                Contract.Ensures(Contract.Result<Thread>() != null);

                if (_thread == null)
                    throw new InvalidOperationException(string.Format(Resources.Application.IsNotRunning, _model.Assembly));

                return _thread;
            }
            set
            {
                _thread = value;
                OnPropertyChanged("Thread");
                OnPropertyChanged("Visual");
            }
        }

        private Thread _thread;

        internal Communication.Info Info
        {
            get
            {
                Contract.Ensures(Contract.Result<Communication.Info>() != null);

                if (_proxy == null)
                    throw new InvalidOperationException(Resources.Application.IsNotInitialized);

                return _proxy.Info;
            }
        }

        internal Communication.ApplicationCallback Callback
        {
            get
            {
                Contract.Ensures(Contract.Result<Communication.ApplicationCallback>() != null);

                if (_proxy == null)
                    throw new InvalidOperationException(Resources.Application.IsNotInitialized);

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
                        throw new InvalidOperationException(string.Format(Resources.Application.IsNotRunning, _model.Assembly)); 
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

        internal bool IsAttachable
        {
            get { return _proxy.IsAttachable; }
        }

        internal bool IsAttached
        {
            get { return _model.IsAttached; }
            set
            {
                _model.IsAttached = value;
                OnPropertyChanged("IsAttached");
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
                    Communication.ApplicationHelper.Unload(_model.ID);
                }

                Disposed = true;
            }
        }
    }
} ;