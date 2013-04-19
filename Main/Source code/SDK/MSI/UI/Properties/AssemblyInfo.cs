using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;

using Elysium.SDK.MSI.UI;

using Microsoft.Tools.WindowsInstallerXml.Bootstrapper;

[assembly: AssemblyTitle("UI.dll")]
[assembly: AssemblyDescription("Elysium SDK installer UI")]
[assembly: AssemblyProduct("Elysium SDK")]
[assembly: AssemblyCopyright("Copyright © Alex F. Sherman & Codeplex community 2011-2013")]

[assembly: SecurityRules(SecurityRuleSet.Level2, SkipVerificationInFullTrust = true)]

[assembly: ComVisible(false)]
[assembly: CLSCompliant(false)]

[assembly: BootstrapperApplication(typeof(App))]

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]

[assembly: NeutralResourcesLanguage("en-us", UltimateResourceFallbackLocation.MainAssembly)]

#if NETFX4
[assembly: AssemblyVersion("2.0.359.0")]
[assembly: AssemblyFileVersion("2.0.359.0")]
#elif NETFX45
[assembly: AssemblyVersion("2.0.368.0")]
[assembly: AssemblyFileVersion("2.0.368.0")]
#endif
[assembly: AssemblyInformationalVersion("2.0 RTM")]
