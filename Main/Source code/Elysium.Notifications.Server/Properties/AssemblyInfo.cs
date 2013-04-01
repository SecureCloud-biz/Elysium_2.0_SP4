using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

[assembly: AssemblyTitle("Elysium.Notifications.Server.exe")]
[assembly: AssemblyDescription("Elysium Notifications System Server")]
[assembly: AssemblyProduct("Elysium Notifications System")]
[assembly: AssemblyCopyright("Copyright © Alex F. Sherman & Codeplex community 2011-2013")]

[assembly: SecurityRules(SecurityRuleSet.Level2)]

[assembly: ComVisible(false)]
[assembly: CLSCompliant(false)]

#if NETFX4
[assembly: AssemblyVersion("1.5.30.0")]
[assembly: AssemblyFileVersion("1.5.30.0")]
#elif NETFX45
[assembly: AssemblyVersion("1.5.35.0")]
[assembly: AssemblyFileVersion("1.5.35.0")]
#endif
[assembly: AssemblyInformationalVersion("1.5 EAP")]
