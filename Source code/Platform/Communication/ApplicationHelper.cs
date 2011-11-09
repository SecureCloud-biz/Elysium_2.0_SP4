using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Reflection;
using System.Security;
using System.Threading;
using Elysium.Theme.Controls;

namespace Elysium.Platform.Communication
{
    internal static class ApplicationHelper
    {
        internal static bool Register(string path, string type)
        {
            AssemblyName assemblyName;
            try
            {
                Contract.Assume(path != null);
                assemblyName = AssemblyName.GetAssemblyName(path);
            }
            catch (ArgumentNullException)
            {
                ToastNotification.Show(Resources.Application.RegistrationFailed, Resources.Default.PathIsNotSpecified);
                return false;
            }
            catch (ArgumentException)
            {
                ToastNotification.Show(Resources.Application.RegistrationFailed, string.Format(Resources.Default.InvalidPath, path));
                return false;
            }
            catch (FileNotFoundException)
            {
                ToastNotification.Show(Resources.Application.RegistrationFailed, string.Format(Resources.Default.FileNotFound, path));
                return false;
            }
            catch (BadImageFormatException)
            {
                ToastNotification.Show(Resources.Application.RegistrationFailed, string.Format(Resources.Default.FileIsNotAssembly, path));
                return false;
            }
            catch (Exception)
            {
                ToastNotification.Show(Resources.Application.RegistrationFailed);
                return false;
            }
            AppDomain sandbox = null;
            try
            {
                sandbox = Security.Helper.CreateSandbox();
                sandbox.SetupInformation.ApplicationBase = Path.GetDirectoryName(path);
                var assembly = sandbox.Load(assemblyName);
                if (!assembly.IsDefined(typeof(SecurityTransparentAttribute), false))
                {
                    ToastNotification.Show(Resources.Application.RegistrationFailed,
                                           string.Format(Resources.Default.AssemblyIsNotSecurityTransparent, assemblyName.Name));
                }
                var typeInfo = assembly.GetType(type);
                if (typeInfo == null)
                {
                    ToastNotification.Show(Resources.Application.RegistrationFailed, string.Format(Resources.Default.TypeNotFound, assemblyName.Name, type));
                    return false;
                }
                if (typeInfo.BaseType != typeof(Application))
                {
                    ToastNotification.Show(Resources.Application.RegistrationFailed, string.Format(Resources.Default.InvalidType, assemblyName.Name, type));
                    return false;
                }
                var appInfo = assembly.GetType("App");
                if (appInfo == null)
                {
                    ToastNotification.Show(Resources.Application.RegistrationFailed, string.Format(Resources.Default.TypeNotFound, assemblyName.Name, "App"));
                    return false;
                }
                if (appInfo.BaseType != typeof(System.Windows.Application))
                {
                    ToastNotification.Show(Resources.Application.RegistrationFailed, string.Format(Resources.Default.InvalidType, assemblyName.Name, "App"));
                    return false;
                }
            }
            catch (ArgumentException)
            {
                ToastNotification.Show(Resources.Application.RegistrationFailed, string.Format(Resources.Default.InvalidTypeName, type));
                return false;
            }
            catch (Exception)
            {
                ToastNotification.Show(Resources.Application.RegistrationFailed);
                return false;
            }
            finally
            {
                if (sandbox != null)
                    try
                    {
                        AppDomain.Unload(sandbox);
                    }
                    catch (Exception)
                    {
                        ToastNotification.Show(Resources.Default.CannotUnloadDomain, sandbox.FriendlyName);
                    }
            }

            var id = Guid.NewGuid();
            var application = new Models.Application { ID = id, Assembly = path, Type = type };
            Settings.Default.Applications.Add(id, application);
            return true;
        }

        internal static bool Load(Guid id, out Application proxy)
        {
            proxy = null;
            ViewModels.Application application;
            try
            {
                application = ViewModels.Locator.Applications[id];
            }
            catch (KeyNotFoundException)
            {
                ToastNotification.Show(string.Format(Resources.Application.InitializationFailed, id), Resources.Application.NotFound);
                return false;
            }
            catch (Exception)
            {
                ToastNotification.Show(string.Format(Resources.Application.InitializationFailed, id));
                return false;
            }
            try
            {
                application.Domain = Security.ApplicationHelper.CreateDomain(application.Assembly);
            }
            catch (Exception)
            {
                ToastNotification.Show(string.Format(Resources.Application.InitializationFailed, id), Resources.Default.DomainInitializationFailed);
                return false;
            }
            try
            {
                proxy = (Application)application.Domain.CreateInstance(AssemblyName.GetAssemblyName(application.Assembly).Name, application.Type).Unwrap();
                proxy.Execute = () => Execute(id);
                proxy.Close = () => Close(id);
                return true;
            }
            catch (Exception)
            {
                ToastNotification.Show(string.Format(Resources.Application.InitializationFailed, id), Resources.Default.ProxyTypeInitializationFailed);
                return false;
            }
        }

        internal static void Execute(Guid id)
        {
            var application = ViewModels.Locator.Applications[id];
            var domain = application.Domain;
            var thread = new Thread(() => domain.ExecuteAssembly(application.Assembly));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            application.Thread = thread;
        }

        internal static void Close(Guid id)
        {
            var application = ViewModels.Locator.Applications[id];
            var thread = application.Thread;
            thread.Abort();
            application.Thread = null;
        }

        internal static void Unload(Guid id)
        {
            var domain = ViewModels.Locator.Applications[id].Domain;
            try
            {
                if (domain != null)
                    AppDomain.Unload(domain);
            }
            catch (Exception)
            {
                ToastNotification.Show(Resources.Default.CannotUnloadDomain, domain.FriendlyName);
            }
        }

        internal static bool Unregister(Guid id)
        {
            return Settings.Default.Applications.Remove(id);
        }
    }
} ;