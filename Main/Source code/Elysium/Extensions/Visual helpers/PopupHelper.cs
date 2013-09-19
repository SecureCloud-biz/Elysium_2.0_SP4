using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

using Elysium.Controls;

using JetBrains.Annotations;

namespace Elysium.Extensions
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [DebuggerNonUserCode]
    [System.Diagnostics.Contracts.Pure]
    public static class PopupHelper
    {
        [Pure]
        public static bool IsMousePhysicallyOver(UIElement element)
        {
            if (element == null)
            {
                return false;
            }
            var position = Mouse.GetPosition(element);
            return position.X >= 0.0 && position.Y >= 0.0 && position.X <= element.RenderSize.Width && position.Y <= element.RenderSize.Height;
        }

        [Pure]
        public static bool IsInRootPopup(DependencyObject element)
        {
            if (IsMousePhysicallyOver(element as UIElement))
            {
                return true;
            }

            // Check if is drop down control
            var dropDown = element as DropDownCommandButton;
            if (dropDown != null && dropDown.IsDropDownOpen && dropDown.Popup != null && dropDown.Popup.Child != null && IsMousePhysicallyOver(dropDown.Popup.Child))
            {
                return true;
            }

            // Check if is context menu
            var menu = element as ContextMenu;
            if (menu != null && menu.IsOpen && IsMousePhysicallyOver(menu))
            {
                return true;
            }

            // Check if is menu item
            var menuItem = element as MenuItem;
            if (menuItem != null && menuItem.IsSubmenuOpen && IsMousePhysicallyOver(menuItem.SubmenuPopup().Child))
            {
                return true;
            }

            // Check if is Popup
            var popup = element as Popup;
            if (popup != null && popup.IsOpen && IsMousePhysicallyOver(popup.Child))
            {
                return true;
            }

            // Check childs
            var children = LogicalTreeHelper.GetChildren(element);

            // we are only interested in children of type FrameworkElement
            return children.OfType<FrameworkElement>().Any(child => child.IsVisible && IsInRootPopup(child));
        }

        public static Popup SubmenuPopup(this MenuItem menuItem)
        {
            return (Popup)menuItem.Template.FindName("PART_Popup", menuItem);
        }
    }
}