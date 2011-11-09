using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security;
using Elysium.Theme.Controls;

namespace Elysium.Platform.Communication
{
    internal static class GadgetHelper
    {
        internal static bool Register(string path, string type)
        {
            AssemblyName assemblyName;
            try
            {
                assemblyName = AssemblyName.GetAssemblyName(path);
            }
            catch (ArgumentException)
            {
                ToastNotification.Show(Resources.Gadget.RegistrationFailed, string.Format(Resources.Default.InvalidPath, path));
                return false;
            }
            catch (FileNotFoundException)
            {
                ToastNotification.Show(Resources.Gadget.RegistrationFailed, string.Format(Resources.Default.FileNotFound, path));
                return false;
            }
            catch (BadImageFormatException)
            {
                ToastNotification.Show(Resources.Gadget.RegistrationFailed, string.Format(Resources.Default.FileIsNotAssembly, path));
                return false;
            }
            catch (Exception)
            {
                ToastNotification.Show(Resources.Gadget.RegistrationFailed);
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
                    ToastNotification.Show(Resources.Gadget.RegistrationFailed,
                                           string.Format(Resources.Default.AssemblyIsNotSecurityTransparent, assemblyName.Name));
                }
                var typeInfo = assembly.GetType(type);
                if (typeInfo == null)
                {
                    ToastNotification.Show(Resources.Gadget.RegistrationFailed, string.Format(Resources.Default.TypeNotFound, type, assemblyName.Name));
                    return false;
                }
                if (typeInfo.BaseType != typeof(Gadget))
                {
                    ToastNotification.Show(Resources.Gadget.RegistrationFailed, string.Format(Resources.Default.InvalidType, assemblyName.Name, type));
                    return false;
                }
            }
            catch (ArgumentException)
            {
                ToastNotification.Show(Resources.Gadget.RegistrationFailed, string.Format(Resources.Default.InvalidTypeName, type));
                return false;
            }
            catch (Exception)
            {
                ToastNotification.Show(Resources.Gadget.RegistrationFailed);
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
            var gadget = new Models.Gadget { ID = id, Assembly = path, Type = type };
            Settings.Default.Gadgets.Add(id, gadget);
            return true;
        }

        internal static bool Load(Guid id, out Gadget proxy)
        {
            proxy = null;
            ViewModels.Gadget gadget;
            try
            {
                gadget = ViewModels.Locator.Gadgets[id];
            }
            catch (KeyNotFoundException)
            {
                ToastNotification.Show(string.Format(Resources.Gadget.InitializationFailed, id), Resources.Gadget.NotFound);
                return false;
            }
            catch (Exception)
            {
                ToastNotification.Show(string.Format(Resources.Gadget.InitializationFailed, id));
                return false;
            }
            try
            {
                gadget.Domain = Security.GadgetHelper.CreateDomain(gadget.Assembly);
            }
            catch (Exception)
            {
                ToastNotification.Show(string.Format(Resources.Gadget.InitializationFailed, id), Resources.Default.DomainInitializationFailed);
                return false;
            }
            try
            {
                proxy = (Gadget)gadget.Domain.CreateInstance(AssemblyName.GetAssemblyName(gadget.Assembly).Name, gadget.Type).Unwrap();
                return true;
            }
            catch (Exception)
            {
                ToastNotification.Show(string.Format(Resources.Gadget.InitializationFailed, id), Resources.Default.ProxyTypeInitializationFailed);
                return false;
            }
        }

        internal static void Unload(Guid id)
        {
            var domain = ViewModels.Locator.Gadgets[id].Domain;
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
            return Settings.Default.Gadgets.Remove(id);
        }
    }
} ;