using System;
using System.Threading;
using System.Windows;

using Elysium.Theme;

using JetBrains.Annotations;

namespace Elysium.Platform
{
    public sealed partial class App
    {
        private const string MutexName = "elysium";

        [UsedImplicitly]
        private static Mutex _mutex;

        private static bool IsSingleInstance()
        {
            bool isSingleInstance;
            _mutex = new Mutex(false, MutexName, out isSingleInstance);
            return isSingleInstance;
        }

        public App()
        {
            // Is application full-trusted?
            if (!AppDomain.CurrentDomain.IsFullyTrusted)
            {
                MessageBox.Show(Platform.Resources.Messages.NotFullyTrusted);
                Shutdown();
            }

            if (!IsSingleInstance())
            {
                Shutdown(0x480);
            }

            switch (Settings.Default.Theme)
            {
                case ThemeType.Dark:
                    ThemeManager.Instance.Dark(Settings.Default.AccentColor);
                    break;
                default:
                    ThemeManager.Instance.Light(Settings.Default.AccentColor);
                    break;
            }

            InitializeComponent();

            Views.Locator.Main.Show();
        }
    }
} ;