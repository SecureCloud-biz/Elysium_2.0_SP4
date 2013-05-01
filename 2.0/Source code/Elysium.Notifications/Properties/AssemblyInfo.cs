using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

[assembly: AssemblyTitle("Elysium.Notifications.dll")]
[assembly: AssemblyDescription("Metro style popup notifications for WPF")]
[assembly: AssemblyProduct("Elysium Notifications System")]
[assembly: AssemblyCopyright("Copyright © Aleksandr Vishnyakov & Codeplex community 2011-2013")]

[assembly: SecurityRules(SecurityRuleSet.Level2)]
[assembly: AllowPartiallyTrustedCallers]
[assembly: InternalsVisibleTo("Elysium.Notifications.Server, PublicKey=0024000004800000140100000602000000240000525341310008000001000100bd51c1fa60becf13e4774e3ba964c4eda307185816c6fdb50c355e5756df3f0976a9382b8480770edfcc1c7cb267cab6582ffb0900a52fd36935c445ec48b63f8d3a95e733e2e0e6b7d1c4938f93408f9ad330798fb073cae6fb4b3aa3601a90f5ff3954ab057c732262dad8bbefba75be014b187ae1bcceacbb4d375291a476c244df8d118d314df2b5137cb4d535c7a802274c59b224e5de91e4f725224b450737cb29a00783ddd9f148e6bbfcf4a322f48d98cf3e6f84536f45f8a61d269ca2fe7b5b18fdb52caaf9a2dcd2149e23cb1d23cb79bf22310f431ca5713d9d7c96db2655989645648bb030c1bdf1b1a3288d8c1481c85b71c49a810ae3fc86d2")]

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]

[assembly: NeutralResourcesLanguage("en-us")]

#if NETFX4
[assembly: AssemblyVersion("2.0.62.0")]
[assembly: AssemblyFileVersion("2.0.62.0")]
#elif NETFX45
[assembly: AssemblyVersion("2.0.65.0")]
[assembly: AssemblyFileVersion("2.0.65.0")]
#endif
[assembly: AssemblyInformationalVersion("2.0 RTM")]
