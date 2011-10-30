using System;
using System.Diagnostics.Contracts;
using System.Net.Security;
using System.ServiceModel;
using Elysium.Platform.Properties;
using Elysium.Theme.WPF.Extensions;

namespace Elysium.Platform.Proxies
{
    [ServiceContract(
        Namespace = "urn:Elysium:Platform/Gadgets",
        CallbackContract = typeof(IGadgetNotification),
        ProtectionLevel = ProtectionLevel.EncryptAndSign,
        SessionMode = SessionMode.Required)]
    public class Gadget
    {
        /*[OperationContract]
        public Guid Register(bool isExpandable)
        {
            var id = Guid.NewGuid();

            var gadget = new Entities.Gadget { IsExpandable = isExpandable };

            Settings.Default.Gadgets.Add(id, gadget);

            return id;
        }

        [OperationContract]
        public void Unregister(Guid id)
        {
            if (!Settings.Default.Gadgets.ContainsKey(id))
                throw new ArgumentException(GadgetErrors.IsNotRegistered, "id");
            Contract.EndContractBlock();

            Settings.Default.Gadgets.Remove(id);
        }

        [OperationContract(IsOneWay = true)]
        public void Initialize(Guid id, bool isExpanded)
        {
            if (!Settings.Default.Gadgets.ContainsKey(id))
                throw new ArgumentException(GadgetErrors.IsNotRegistered, "id");
            Contract.EndContractBlock();

            var gadget = Settings.Default.Gadgets[id];
            gadget.IsExpanded = isExpanded;

            var callback = OperationContext.Current.GetCallbackChannel<IGadgetNotification>();

            Settings.Default.PropertyChanged += (s, e) =>
                                                    {
                                                        if (e.PropertyName.Equals(Settings.Default.AccentColor.GetName()))
                                                            callback.AccentColorChanged(Settings.Default.AccentColor);
                                                        else if (e.PropertyName.Equals(Settings.Default.Theme.GetName()))
                                                            callback.ThemeChanged(Settings.Default.Theme);
                                                    };

            gadget.PropertyChanged += (s, e) =>
                                          {
                                              if (e.PropertyName.Equals(gadget.Page.GetName()))
                                                  callback.PageChanged(gadget.Page);
                                              else if (e.PropertyName.Equals(gadget.Group.GetName()))
                                                  callback.GroupChanged(gadget.Group);
                                              else if (gadget.IsExpandable && e.PropertyName.Equals(gadget.IsExpanded.GetName()))
                                                  callback.SizeChanged(gadget.IsExpanded);
                                              else if (e.PropertyName.Equals(gadget.IsVisible))
                                                  callback.VisibilityChanged(gadget.IsVisible);
                                          };
        }*/
    }
} ;