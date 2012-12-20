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
[assembly: AssemblyCopyright("Copyright © Alex F. Sherman & Codeplex community 2011-2012")]

[assembly: SecurityRules(SecurityRuleSet.Level2, SkipVerificationInFullTrust = true)]

[assembly: ComVisible(false)]
[assembly: CLSCompliant(false)]

[assembly: BootstrapperApplication(typeof(App))]

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]

[assembly: NeutralResourcesLanguage("en-us", UltimateResourceFallbackLocation.MainAssembly)]

#if NETFX4
[assembly: AssemblyVersion("1.5.348.0")]
[assembly: AssemblyFileVersion("1.5.348.0")]
#elif NETFX45
[assembly: AssemblyVersion("1.5.362.0")]
[assembly: AssemblyFileVersion("1.5.362.0")]
#endif
[assembly: AssemblyInformationalVersion("1.5 EAP")]
