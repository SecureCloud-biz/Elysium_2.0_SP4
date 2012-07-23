using System;
using System.Windows.Interop;
using System.Windows.Threading;

using Elysium.SDK.MSI.UI.ViewModels;

using Microsoft.Tools.WindowsInstallerXml.Bootstrapper;

namespace Elysium.SDK.MSI.UI
{
    public class App : BootstrapperApplication
    {
        public static App Current { get; private set; }

        public Dispatcher Dispatcher { get; private set; }

        public int Result { get; set; }

        private MainView _mainView;
        private IntPtr _handle;

        internal void Apply()
        {
            Engine.Apply(_handle);
        }

        protected override void Run()
        {
            Engine.Log(LogLevel.Verbose, "Running Elysium SDK for .NET 4 Framework setup");

            Current = this;
            Dispatcher = Dispatcher.CurrentDispatcher;

            Locator.MainViewModel = new MainViewModel();
            Locator.MainViewModel.Refresh();

            if (Command.Display == Display.Passive || Command.Display == Display.Full)
            {
                Engine.Log(LogLevel.Verbose, "Creating a UI.");
                _mainView = new MainView();
                _handle = new WindowInteropHelper(_mainView).EnsureHandle();
                _mainView.Show();
            }

            Dispatcher.Run();

            Engine.Log(LogLevel.Verbose, "Stopping Elysium SDK for .NET 4 Framework setup");

            Engine.Quit(Result);
        }

        internal void Finish()
        {
            _mainView.Close();
        }
    }
} ;