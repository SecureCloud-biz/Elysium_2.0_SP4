using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Elysium.Theme.Controls
{
    public class CommandButton : Button
    {
        static CommandButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CommandButton), new FrameworkPropertyMetadata(typeof(CommandButton)));
        }
    }

    public class RepeatCommandButton : RepeatButton
    {
        static RepeatCommandButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RepeatCommandButton), new FrameworkPropertyMetadata(typeof(RepeatCommandButton)));
        }
    }

    public class ToggleCommandButton : ToggleButton
    {
        static ToggleCommandButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleCommandButton), new FrameworkPropertyMetadata(typeof(ToggleCommandButton)));
        }
    }
} ;