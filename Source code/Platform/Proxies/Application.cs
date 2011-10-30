using System;
using System.Diagnostics.Contracts;
using System.Net.Security;
using System.ServiceModel;
using Elysium.Platform.Properties;
using Elysium.Theme.WPF.Extensions;

namespace Elysium.Platform.Proxies
{
    [ServiceContract(
        Namespace = "urn:Elysium:Platform/Applications",
        CallbackContract = typeof(IApplicationNotification),
        ProtectionLevel = ProtectionLevel.EncryptAndSign,
        SessionMode = SessionMode.Required)]
    public class Application
    {
        /*[OperationContract]
        public Guid Register(bool isAttachable)
        {
            var id = Guid.NewGuid();

            var application = new Entities.Application { IsAttachable = isAttachable };

            Settings.Default.Applications.Add(id, application);

            return id;
        }

        [OperationContract]
        public void Unregister(Guid id)
        {
            if (!Settings.Default.Applications.ContainsKey(id))
                throw new ArgumentException(ApplicationErrors.IsNotRegistered, "id");
            Contract.EndContractBlock();

            Settings.Default.Applications.Remove(id);
        }

        [OperationContract(IsOneWay = true)]
        public void Initialize(Guid id, bool isAttached)
        {
            if (!Settings.Default.Applications.ContainsKey(id))
                throw new ArgumentException(ApplicationErrors.IsNotRegistered, "id");
            Contract.EndContractBlock();

            var application = Settings.Default.Applications[id];
            application.IsAttached = isAttached;

            var callback = OperationContext.Current.GetCallbackChannel<IApplicationNotification>();

            Settings.Default.PropertyChanged += (s, e) =>
                                                    {
                                                        if (e.PropertyName.Equals(Settings.Default.AccentColor.GetName()))
                                                            callback.AccentColorChanged(Settings.Default.AccentColor);
                                                        else if (e.PropertyName.Equals(Settings.Default.Theme.GetName()))
                                                            callback.ThemeChanged(Settings.Default.Theme);
                                                    };

            application.PropertyChanged += (s, e) =>
                                               {
                                                   if (e.PropertyName.Equals(application.Page.GetName()))
                                                       callback.PageChanged(application.Page);
                                                   else if (application.IsAttachable && e.PropertyName.Equals(application.IsAttached.GetName()))
                                                       callback.AttachmentChanged(application.IsAttached);
                                                   else if (e.PropertyName.Equals(application.IsVisible))
                                                       callback.VisibilityChanged(application.IsVisible);
                                               };
        }*/
    }
} ;