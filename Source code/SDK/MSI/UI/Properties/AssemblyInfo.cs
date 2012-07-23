using System.Reflection;
using System.Runtime.InteropServices;

using Elysium.SDK.MSI.UI;

using Microsoft.Tools.WindowsInstallerXml.Bootstrapper;

[assembly: AssemblyTitle("Elysium: SDK Installer UI assembly")]
[assembly: AssemblyDescription("WPF Metro-style theme installer ui")]
[assembly: AssemblyProduct("Elysium")]
[assembly: AssemblyCopyright("Copyright © Alex F. Sherman & Codeplex Community 2011-2012")]

[assembly: ComVisible(false)]

[assembly: BootstrapperApplication(typeof(App))]

[assembly: AssemblyVersion("1.5.172.0")]
[assembly: AssemblyFileVersion("1.5.172.0")]
[assembly: AssemblyInformationalVersion("1.5")]