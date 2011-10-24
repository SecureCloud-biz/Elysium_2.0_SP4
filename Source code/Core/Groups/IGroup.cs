namespace Elysium.Core.Groups
{
    public interface IGroup
    {
        string Name { get; }

        string Description { get; }

        IPage Page { get; set; }
    }
} ;