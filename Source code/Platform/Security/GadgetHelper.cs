using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace Elysium.Platform.Security
{
    internal static class GadgetHelper
    {
        public static AppDomain CreateDomain(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");
            Contract.Ensures(Contract.Result<AppDomain>() != null);
            Contract.EndContractBlock();

            var domainName = AssemblyName.GetAssemblyName(path).FullName;
            var domainSetup = new AppDomainSetup { ApplicationBase = Path.GetDirectoryName(path) };
            var permissions = new PermissionSet(PermissionState.None);
            permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution | SecurityPermissionFlag.UnmanagedCode));
            permissions.AddPermission(new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, Path.GetDirectoryName(path)));
            permissions.AddPermission(new UIPermission(UIPermissionWindow.AllWindows, UIPermissionClipboard.AllClipboard));
            var domain = AppDomain.CreateDomain(domainName, null, domainSetup, permissions);

            Contract.Assume(domain != null);

            return domain;
        }
    }
} ;