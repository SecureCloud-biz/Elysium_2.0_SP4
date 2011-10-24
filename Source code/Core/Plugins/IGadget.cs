using System;
using Elysium.Core.Groups;

namespace Elysium.Core.Plugins
{
    public interface IGadget
    {
        object TilePresenter { get; }

        string Name { get; }

        string Description { get; }

        Uri Icon { get; }

        string Author { get; }

        Uri License { get; }

        Version Version { get; }

        IGroup Group { get; set; }

        int Column { get; set; }

        int ColumnSpan { get; set; }

        int Row { get; set; }

        bool IsEnabled { get; set; }
    }
} ;