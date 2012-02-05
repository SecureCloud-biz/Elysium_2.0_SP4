using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

using JetBrains.Annotations;

namespace Elysium.Theme.Controls
{
    [PublicAPI]
    public class Submenu : MenuBase
    {
        static Submenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Submenu), new FrameworkPropertyMetadata(typeof(Submenu)));
            IsTabStopProperty.OverrideMetadata(typeof(Submenu), new FrameworkPropertyMetadata(false));
            KeyboardNavigation.TabNavigationProperty.OverrideMetadata(typeof(Submenu), new FrameworkPropertyMetadata(KeyboardNavigationMode.Cycle));
            KeyboardNavigation.ControlTabNavigationProperty.OverrideMetadata(typeof(Submenu), new FrameworkPropertyMetadata(KeyboardNavigationMode.Contained));
            KeyboardNavigation.DirectionalNavigationProperty.OverrideMetadata(typeof(Submenu), new FrameworkPropertyMetadata(KeyboardNavigationMode.Contained));
        }
    }
} ;