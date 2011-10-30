using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Windows;
using Elysium.Platform.Properties;
using Elysium.Theme.WPF;

namespace Elysium.Platform
{
    public sealed partial class App
    {
        public App()
        {
            // Checking OS version
            if (Interop.Windows.IsWindowsXP)
            {
                MessageBox.Show(Platform.Properties.Resources.XPError);
                Shutdown(0x47E); // 0x47E - This version of Windows not supported
            }

            // Is application full-trusted?
            if (!AppDomain.CurrentDomain.IsFullyTrusted)
            {
                MessageBox.Show(Platform.Properties.Resources.PartiallyTrusted);
                Shutdown();
            }

            // Set theme
            if (Settings.Default.Theme == Theme.WPF.Theme.Dark)
                ThemeManager.Instance.Dark(Settings.Default.AccentColor);
            else ThemeManager.Instance.Light(Settings.Default.AccentColor);

            InitializeComponent();

            // Hosting gadgets
            var gadgetsHost = new ServiceHost(typeof(Proxies.Gadget), new Uri("http://localhost:8000/Elysium/Platform/Gadgets"));
            try
            {
                gadgetsHost.AddServiceEndpoint(typeof(Proxies.Gadget), new NetNamedPipeBinding(NetNamedPipeSecurityMode.Transport),
                                               "net.pipe://localhost/Elysium/Platform/Gadgets");
                gadgetsHost.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
                gadgetsHost.Open();

                Exit += (s, e) => gadgetsHost.Close();
            }
            catch (CommunicationException e)
            {
                MessageBox.Show(Platform.Properties.GadgetErrors.HostingFailed + Environment.NewLine + Environment.NewLine + e);
                gadgetsHost.Abort();
                Shutdown();
            }

            // Hosting applications
            var applicationsHost = new ServiceHost(typeof(Proxies.Application), new Uri("http://localhost:8000/Elysium/Platform/Applications"));
            try
            {
                applicationsHost.AddServiceEndpoint(typeof(Proxies.Application), new NetNamedPipeBinding(NetNamedPipeSecurityMode.Transport),
                                                    "net.pipe://localhost/Elysium/Platform/Applications");
                applicationsHost.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
                applicationsHost.Open();

                Exit += (s, e) => applicationsHost.Close();
            }
            catch (CommunicationException e)
            {
                MessageBox.Show(Platform.Properties.ApplicationErrors.HostingFailed + Environment.NewLine + Environment.NewLine + e);
                applicationsHost.Abort();
                Shutdown();
            }
        }
    }
} ;