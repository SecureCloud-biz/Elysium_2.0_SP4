using System;
using System.Windows;
using Elysium.Theme;

namespace Elysium.Platform
{
    internal sealed partial class App
    {
        internal App()
        {
            // Is application full-trusted?
            if (!AppDomain.CurrentDomain.IsFullyTrusted)
            {
                MessageBox.Show(Platform.Resources.Default.PartiallyTrusted);
                Shutdown();
            }

            // Gets command line arguments
            var args = Environment.GetCommandLineArgs();

            var isSingleInstance = Communication.Helper.IsSingleInstance();
            var hasArguments = args.Length > 1;

            if (!isSingleInstance && !hasArguments)
                Shutdown(0x480);
            else
            {
                if (isSingleInstance)
                {
                    Communication.Helper.ExecuteServer();
                }
                if (hasArguments)
                {
                    Communication.Helper.ExecuteClient(args[1], args[2], args[3]);
                }
            }

            // Set theme
            if (Settings.Default.Theme == Theme.Theme.Dark)
                ThemeManager.Instance.Dark(Settings.Default.AccentColor);
            else ThemeManager.Instance.Light(Settings.Default.AccentColor);

            InitializeComponent();

            Views.Locator.Main.Show();
        }
    }
} ;