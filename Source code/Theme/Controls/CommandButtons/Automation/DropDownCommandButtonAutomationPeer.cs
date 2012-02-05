using System;
using System.Diagnostics.Contracts;
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
            Contract.Assume(Owner != null);
        }

        protected override string GetClassNameCore()
        {
            Contract.Ensures(Contract.Result<string>() == "Button");
            return "Button";
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            Contract.Ensures(Contract.Result<AutomationControlType>() == AutomationControlType.Button);
            return AutomationControlType.Button;
        }

        public override object GetPattern(PatternInterface patternInterface)
        {
            return patternInterface == PatternInterface.ExpandCollapse ? this : base.GetPattern(patternInterface);
        }

        public void Expand()
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

        public void Collapse()
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

        public ExpandCollapseState ExpandCollapseState
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

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private void Invariants()
        {
            // NOTE: WPF doesn't declare contracts
            Contract.Invariant(Owner != null);
        }
    }
} ;