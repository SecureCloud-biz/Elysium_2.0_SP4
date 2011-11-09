using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Markup;

[assembly: AssemblyTitle("Elysium: Theme assembly")]
[assembly: AssemblyDescription("WPF Metro-style theme")]
[assembly: AssemblyProduct("Elysium theme")]
[assembly: AssemblyCopyright("Copyright © Alex F. Sherman & Codeplex Community 2011")]

[assembly: SecurityRules(SecurityRuleSet.Level2)]
[assembly: AllowPartiallyTrustedCallers]

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]

[assembly: XmlnsDefinition("http://schemas.codeplex.com/elysium/theme", "Elysium.Theme")]
[assembly: XmlnsDefinition("http://schemas.codeplex.com/elysium/theme", "Elysium.Theme.Controls")]
[assembly: XmlnsDefinition("http://schemas.codeplex.com/elysium/theme", "Elysium.Theme.Converters")]
[assembly: XmlnsPrefix("http://schemas.codeplex.com/elysium/theme", "metro")]

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None,
    ResourceDictionaryLocation.SourceAssembly
)]

[assembly: AssemblyVersion("1.1.34.0")]
[assembly: AssemblyFileVersion("1.1.34.0")]
