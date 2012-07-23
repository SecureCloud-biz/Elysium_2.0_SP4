using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

[assembly: AssemblyTitle("Elysium: Notifications assembly")]
[assembly: AssemblyDescription("Elysium Notifications")]
[assembly: AssemblyProduct("Elysium")]
[assembly: AssemblyCopyright("Copyright © Alex F. Sherman & Codeplex Community 2011-2012")]

[assembly: SecurityRules(SecurityRuleSet.Level2)]
[assembly: AllowPartiallyTrustedCallers]
[assembly: InternalsVisibleTo("Elysium.Notifications.Server, PublicKey=0024000004800000940000000602000000240000525341310004000001000100cd87cb3804debcaa" +
                                                                      "7edc1def98e42610bdbb17e423711a88429dc54feb574284165edd131e30a88193284d73db2720b3" +
                                                                      "7c080b59e27ff3fae0ba2f05b7828a51625b20946f37e260a4c42fc437927550f0237f56b8050104" +
                                                                      "025dfe07d52cb1f0ff6281f3a06b096a3c8b2923726880c5f029ae1e42f8abbff578e516d8b549f9")]

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]

[assembly: NeutralResourcesLanguage("en-us")]

[assembly: AssemblyVersion("1.5.19.0")]
[assembly: AssemblyFileVersion("1.5.19.0")]
[assembly: AssemblyInformationalVersion("1.5")]