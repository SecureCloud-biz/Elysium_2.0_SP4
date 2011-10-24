using System.Collections.Generic;

namespace Elysium.Core.Groups
{
    public interface IPage
    {
        object PagePresenter { get; set; }
        
        string Name { get; }

        string Description { get; }
    }
} ;