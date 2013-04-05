using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using Microsoft.Windows.Design.Metadata;

#if VISUALSTUDIO2010
[assembly: AssemblyTitle("Elysium.Design.10.0.dll")]
#elif VISUALSTUDIO2012
[assembly: AssemblyTitle("Elysium.Design.11.0.dll")]
#endif
[assembly: AssemblyDescription("Design-time resources for Elysium")]
[assembly: AssemblyProduct("Elysium")]
[assembly: AssemblyCopyright("Copyright © Alex F. Sherman & Codeplex community 2011-2013")]

[assembly: SecurityRules(SecurityRuleSet.Level2)]

[assembly: ComVisible(false)]
[assembly: CLSCompliant(false)]

[assembly: ProvideMetadata(typeof(Elysium.Design.Metadata))]

[assembly: ThemeInfo(ResourceDictionaryLocation.SourceAssembly, ResourceDictionaryLocation.None)]

[assembly: NeutralResourcesLanguage("en-us")]

#if NETFX4
[assembly: AssemblyVersion("2.0.105.0")]
[assembly: AssemblyFileVersion("2.0.105.0")]
#elif NETFX45
[assembly: AssemblyVersion("2.0.18.0")]
[assembly: AssemblyFileVersion("2.0.18.0")]
#endif
[assembly: AssemblyInformationalVersion("2.0 RTM")]
