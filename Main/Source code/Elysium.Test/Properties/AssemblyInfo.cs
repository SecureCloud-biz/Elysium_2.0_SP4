using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;

[assembly: AssemblyTitle("Elysium.Test.exe")]
[assembly: AssemblyDescription("Test WPF Metro style application")]
[assembly: AssemblyProduct("Elysium")]
[assembly: AssemblyCopyright("Copyright © Alex F. Sherman & Codeplex community 2011-2012")]

[assembly: SecurityRules(SecurityRuleSet.Level2)]

[assembly: ComVisible(false)]
[assembly: CLSCompliant(false)]

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]

[assembly: NeutralResourcesLanguage("en-us")]

#if NETFX4
[assembly: AssemblyVersion("1.5.276.0")]
[assembly: AssemblyFileVersion("1.5.276.0")]
#elif NETFX45
[assembly: AssemblyVersion("1.5.290.0")]
[assembly: AssemblyFileVersion("1.5.290.0")]
#endif
[assembly: AssemblyInformationalVersion("1.5 EAP")]
