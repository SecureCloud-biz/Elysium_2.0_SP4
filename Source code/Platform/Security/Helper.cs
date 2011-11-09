using System;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace Elysium.Platform.Security
{
    internal static class Helper
    {
        internal static AppDomain CreateSandbox()
        {
            Contract.Ensures(Contract.Result<AppDomain>() != null);

            var name = Assembly.GetExecutingAssembly().FullName + ": Sandbox " + Guid.NewGuid();
            var setup = new AppDomainSetup();
            var permissions = new PermissionSet(PermissionState.None);
            var sandbox = AppDomain.CreateDomain(name, null, setup, permissions);

            Contract.Assume(sandbox != null);

            return sandbox;
        }
    }
} ;