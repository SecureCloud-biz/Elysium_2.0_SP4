using System;
using System.AddIn.Pipeline;
using System.Windows;
using System.Windows.Media;

using Elysium.Platform.Contracts.Helpers;
using Elysium.Theme;

namespace Elysium.Platform
{
    [AddInBase]
    public interface IGadget
    {
        Info Info { get; }

        Color AccentColor { get; set; }

        ThemeType Theme { get; set; }

        bool IsExpandable { get; }

        bool IsExpanded { get; set; }

        bool IsVisible { get; set; }

        Action<IApplication> Execute { get; set; }

        FrameworkElement Visual { get; }
    }
} ;