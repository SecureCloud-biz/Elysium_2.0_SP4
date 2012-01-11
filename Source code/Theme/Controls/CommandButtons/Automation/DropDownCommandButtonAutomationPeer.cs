using System;
using System.Runtime.CompilerServices;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace Elysium.Theme.Controls.Automation
{
    public class DropDownCommandButtonAutomationPeer : ButtonBaseAutomationPeer, IExpandCollapseProvider
    {
        public DropDownCommandButtonAutomationPeer(DropDownCommandButton owner)
            : base(owner)
        {
        }

        protected override string GetClassNameCore()
        {
            return "Button";
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Button;
        }

        public override object GetPattern(PatternInterface patternInterface)
        {
            return patternInterface == PatternInterface.ExpandCollapse ? this : base.GetPattern(patternInterface);
        }

        void IExpandCollapseProvider.Expand()
        {
            if (!IsEnabled())
            {
                throw new ElementNotEnabledException();
            }
            var owner = (DropDownCommandButton)Owner;
            if (!owner.HasSubmenu)
            {
                throw new InvalidOperationException("Operation can't be perform");
            }
            owner.IsDropDownOpen = true;
        }

        void IExpandCollapseProvider.Collapse()
        {
            if (!IsEnabled())
            {
                throw new ElementNotEnabledException();
            }
            var owner = (DropDownCommandButton)Owner;
            if (!owner.HasSubmenu)
            {
                throw new InvalidOperationException("Operation can't be perform");
            }
            owner.IsDropDownOpen = false;
        }

        ExpandCollapseState IExpandCollapseProvider.ExpandCollapseState
        {
            get
            {
                var owner = (DropDownCommandButton)Owner;
                return !owner.HasSubmenu ? ExpandCollapseState.LeafNode : (owner.IsDropDownOpen ? ExpandCollapseState.Expanded : ExpandCollapseState.Collapsed);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RaiseExpandCollapseStatePropertyChangedEvent(ExpandCollapseState oldValue, ExpandCollapseState newValue)
        {
            if (oldValue != newValue)
            {
                RaisePropertyChangedEvent(ExpandCollapsePatternIdentifiers.ExpandCollapseStateProperty, oldValue, newValue);
            }
        }
    }
} ;