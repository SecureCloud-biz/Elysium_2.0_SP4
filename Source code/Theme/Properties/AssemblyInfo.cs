using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
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

[assembly: InternalsVisibleTo("Elysium.Platform.Host, PublicKey=0024000004800000940000000602000000240000525341310004000001000100737513285eb4030336b424fdf838f05b7548f48b05ec544d1cbe5c22767a215df696d29bb72d2db9f478f3ba2eaa806c30bd8ec2247c5f3bb003d441fdfb45ff0b48a3f72d4f4e2b3c996bb783e6aa4bad4c7be4dddae55df63ab3382cd7bb23fdb00c2aba39332600c882ad0134427e2b53c9a08d14bc4f1f99ea11c5e56ad5")]

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None,
    ResourceDictionaryLocation.SourceAssembly
)]

[assembly: NeutralResourcesLanguage("en-us")]

[assembly: AssemblyVersion("1.2.257.1")]
[assembly: AssemblyFileVersion("1.2.257.1")]
