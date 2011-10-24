using System.Reflection;
using System.Windows;
using System.Windows.Media;
using Elysium.Platform.Properties;
using Elysium.Theme.WPF;

namespace Elysium.Platform
{
    public sealed partial class App
    {
        public App()
        {
            InitializeComponent();

            if (Interop.Windows.IsWindowsXP)
            {
                MessageBox.Show(Platform.Properties.Resources.XPError);
                Shutdown(0x47E); // 0x47E - This version of Windows not supported
            }

            Core.Parameters.Instance.PropertyChanged += (sender, e) =>
                                                            {
                                                                if (e.PropertyName.Equals("AccentColor"))
                                                                {
                                                                    Settings.Default.AccentColor = Core.Parameters.Instance.AccentColor;
                                                                    Settings.Default.Save();
                                                                }
                                                                if (e.PropertyName.Equals("IsDarkTheme"))
                                                                {
                                                                    Settings.Default.IsDarkTheme = Core.Parameters.Instance.IsDarkTheme;
                                                                    Settings.Default.Save();
                                                                }
                                                                var accentValue = Core.Parameters.Instance.AccentColor;
                                                                var accentColor = Color.FromArgb((byte)(accentValue >> 24),
                                                                                                 (byte)(accentValue >> 16),
                                                                                                 (byte)(accentValue >> 8),
                                                                                                 (byte)(accentValue >> 0));
                                                                if (Core.Parameters.Instance.IsDarkTheme)
                                                                    ThemeManager.Instance.Dark(accentColor);
                                                                else ThemeManager.Instance.Light(accentColor);
                                                            };

            Core.Parameters.Instance.AccentColor = Settings.Default.AccentColor;
            Core.Parameters.Instance.IsDarkTheme = Settings.Default.IsDarkTheme;

            foreach (Assembly assembly in Settings.Default.CompositionAssemblies)
            {
                Core.Composer.Instance.LoadAssembly(assembly);
            }

            Core.Composer.Instance.AssemblyStatus += assembly => Settings.Default.CompositionAssemblies.Contains(assembly)
                                                                     ? Core.Composer.RegistrationStatus.Registered
                                                                     : Core.Composer.RegistrationStatus.Unregistered;
            Core.Composer.Instance.AssemblyRegistered += assembly =>
                                                             {
                                                                 Settings.Default.CompositionAssemblies.Add(assembly);
                                                                 Core.Composer.Instance.LoadAssembly(assembly);
                                                             };
            Core.Composer.Instance.AssemblyUnregistered += assembly =>
                                                               {
                                                                   Settings.Default.CompositionAssemblies.Remove(assembly);
                                                                   Core.Composer.Instance.UnloadAssembly(assembly);
                                                               };
        }
    }
} ;