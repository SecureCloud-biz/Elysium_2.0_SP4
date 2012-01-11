using System;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace Elysium.Platform.Security
{
    internal static class Helper
    {
        public static AppDomain CreateSandbox(string applicationBase = null, Pe)
        {
            Contract.Ensures(Contract.Result<AppDomain>() != null);

            var platform = Assembly.GetExecutingAssembly();
            var name = platform.FullName + ": Sandbox " + Guid.NewGuid();
            var setup = new AppDomainSetup { ApplicationBase = platform.Location };
            var permissions = new PermissionSet(PermissionState.None);
            permissions.AddPermission(new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, platform.Location));
            var sandbox = AppDomain.CreateDomain(name, null, setup, permissions);

            Contract.Assume(sandbox != null);

            return sandbox;
        }
    }
} ;