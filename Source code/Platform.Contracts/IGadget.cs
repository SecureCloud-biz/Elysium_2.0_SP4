using System;
using System.AddIn.Contract;
using System.AddIn.Pipeline;
using System.Windows.Media;

using Elysium.Platform.Contracts.Helpers;
using Elysium.Theme;

namespace Elysium.Platform.Contracts
{
    [AddInContract]
    public interface IGadget : IContract
    {
        Info Info { get; }

        Color AccentColor { get; set; }

        ThemeType Theme { get; set; }

        bool IsExpandable { get; }

        bool IsExpanded { get; set; }

        bool IsVisible { get; set; }

        Action<IApplication> Execute { get; set; }

        INativeHandleContract Visual { get; }
    }
} ;