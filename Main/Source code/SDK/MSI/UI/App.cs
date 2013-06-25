using System;
using System.Diagnostics;
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

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        private static void Debug()
        {
            Dispatcher.CurrentDispatcher.Thread.Join(TimeSpan.FromSeconds(20));
        }

        protected override void Run()
        {
            Debug();

            Engine.Log(LogLevel.Verbose, "Running Elysium SDK for .NET Framework 4 setup");

            Current = this;
            Dispatcher = Dispatcher.CurrentDispatcher;

            Locator.MainViewModel = new MainViewModel();
            Locator.MainViewModel.Refresh();
            Locator.MainViewModel.ParseCommandLine();

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