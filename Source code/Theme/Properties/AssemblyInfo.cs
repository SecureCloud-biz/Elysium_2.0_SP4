using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Markup;

[assembly: AssemblyTitle("Elysium: Theme assembly")]
[assembly: AssemblyDescription("WPF Metro-style theme")]
[assembly: AssemblyProduct("Elysium Theme")]
[assembly: AssemblyCopyright("Copyright © Alex F. Sherman & Codeplex Community 2011-2012")]

[assembly: SecurityRules(SecurityRuleSet.Level2)]
[assembly: AllowPartiallyTrustedCallers]

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]

[assembly: XmlnsDefinition("http://schemas.codeplex.com/elysium/theme", "Elysium.Theme")]
[assembly: XmlnsDefinition("http://schemas.codeplex.com/elysium/theme", "Elysium.Theme.Controls")]
[assembly: XmlnsDefinition("http://schemas.codeplex.com/elysium/theme", "Elysium.Theme.Converters")]
[assembly: XmlnsPrefix("http://schemas.codeplex.com/elysium/theme", "metro")]

[assembly: XmlnsDefinition("http://schemas.codeplex.com/elysium/mvvm", "Elysium.Theme.Commands")]
[assembly: XmlnsDefinition("http://schemas.codeplex.com/elysium/mvvm", "Elysium.Theme.ViewModels")]
[assembly: XmlnsPrefix("http://schemas.codeplex.com/elysium/mvvm", "mvvm")]

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None,
    ResourceDictionaryLocation.SourceAssembly
)]

[assembly: NeutralResourcesLanguage("en-us")]

[assembly: AssemblyVersion("1.2.257.1")]
[assembly: AssemblyFileVersion("1.2.257.1")]
