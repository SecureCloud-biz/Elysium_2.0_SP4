namespace Elysium.Core.Plugins
{
    public interface IEmbeddedApplication
    {
        object ApplicationPresenter { get; }

        bool IsExpanded { get; set; }

        IGadget Gadget { get; }
    }
} ;