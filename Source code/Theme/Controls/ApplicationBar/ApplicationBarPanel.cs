using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Elysium.Theme.Controls
{
    public class ApplicationBarPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            var infinitySize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            var desiredSize = new Size(0, 0);
            foreach (var child in InternalChildren.Cast<UIElement>())
            {
                if (child != null)
                {
                    child.Measure(infinitySize);
                    Contract.Assume(child.DesiredSize.Width >= 0.0);
                    Contract.Assume(child.DesiredSize.Height >= 0.0);
                    desiredSize.Width += child.DesiredSize.Width;
                    desiredSize.Height = Math.Max(desiredSize.Height, child.DesiredSize.Height);
                }
            }
            foreach (var child in InternalChildren.Cast<UIElement>())
            {
                if (child != null)
                {
                    child.Measure(desiredSize);
                }
            }
            return desiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var leftFilledWidth = 0.0;
            var rightFilledWidth = 0.0;
            foreach (var child in InternalChildren.Cast<UIElement>())
            {
                if (child != null)
                {
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
            }
            return finalSize;
        }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(InternalChildren != null);
        }
    }
} ;