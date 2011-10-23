namespace Elysium.Core.Plugins
{
    public interface IApplication : IEmbeddedApplication
    {
        bool IsAttached { get; set; }
    }
} ;