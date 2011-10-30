using System.ServiceModel;
using System.Windows.Media;

namespace Elysium.Platform.Proxies
{
    public interface IApplicationNotification
    {
        [OperationContract(IsOneWay = true)]
        void AccentColorChanged(Color accentColor);

        [OperationContract(IsOneWay = true)]
        void ThemeChanged(Theme.WPF.Theme theme);

        [OperationContract(IsOneWay = true)]
        void PageChanged(string page);

        [OperationContract(IsOneWay = true)]
        void AttachmentChanged(bool isAttached);

        [OperationContract(IsOneWay = true)]
        void VisibilityChanged(bool isVisible);
    }
} ;