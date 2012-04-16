using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using JetBrains.Annotations;

namespace Elysium.Controls
{
    [PublicAPI]
    public class ApplicationBarPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            var infinitySize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            var desiredSize = new Size(0, 0);
            foreach (var child in InternalChildren.Cast<UIElement>().Where(child => child != null))
            {
                // NOTE: Code Contracts doesn't support closures
                Contract.Assume(child != null);
                child.Measure(infinitySize);
                // BUG in Code Contracts: DesiredSize.Width must be is equal to or greater than zero
                Contract.Assume(child.DesiredSize.Width >= 0d);
                desiredSize.Width += child.DesiredSize.Width;
                desiredSize.Height = Math.Max(desiredSize.Height, child.DesiredSize.Height);
            }
            foreach (var child in InternalChildren.Cast<UIElement>().Where(child => child != null))
            {
                // NOTE: Code Contracts doesn't support closures
                Contract.Assume(child != null);
                child.Measure(desiredSize);
            }
            return desiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var leftFilledWidth = 0d;
            var rightFilledWidth = 0d;
            foreach (var child in InternalChildren.Cast<UIElement>().Where(child => child != null))
            {
                // NOTE: Code Contracts doesn't support closures
                Contract.Assume(child != null);
                var isRightDocked = ApplicationBar.GetDock(child) == ApplicationBarDock.Right;
                var x = !isRightDocked ? leftFilledWidth : finalSize.Width - rightFilledWidth - child.DesiredSize.Width;

                if (!isRightDocked)
                {
                    leftFilledWidth += child.DesiredSize.Width;
                }
                else
                {
                    rightFilledWidth += child.DesiredSize.Width;
                }

                child.Arrange(leftFilledWidth + rightFilledWidth <= finalSize.Width
                                  ? new Rect(new Point(x, 0), child.DesiredSize)
                                  : new Rect(new Point(0, 0), new Size(0, 0)));
            }
            return finalSize;
        }
    }
} ;