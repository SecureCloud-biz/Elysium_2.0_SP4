using System.Windows;
using System.Windows.Controls.Primitives;

namespace Elysium.Theme.Controls
{
    public class CommandButton : ButtonBase
    {
        static CommandButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CommandButton), new FrameworkPropertyMetadata(typeof(CommandButton)));
        }
    }
}
