using System;
using System.Threading;
using System.Windows.Interop;
using System.Windows.Threading;

using Elysium.SDK.MSI.UI.ViewModels;

using Microsoft.Tools.WindowsInstallerXml.Bootstrapper;

namespace Elysium.SDK.MSI.UI
{
    public class App : BootstrapperApplication
    {
        internal static App Current { get; private set; }

        internal Dispatcher Dispatcher { get; private set; }

        internal int Result { get; set; }

        private MainView _mainView;
        private IntPtr _handle;

        internal void Apply()
        {
            Engine.Apply(_handle);
        }

        protected override void Run()
        {
            Thread.CurrentThread.Join(TimeSpan.FromSeconds(20));
            Engine.Log(LogLevel.Verbose, "Running Elysium SDK for .NET Framework 4 setup");

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

            Engine.Log(LogLevel.Verbose, "Stopping Elysium SDK for .NET Framework 4 setup");

            Engine.Quit(Result);
        }

        internal void Finish()
        {
            _mainView.Close();
        }
    }
}