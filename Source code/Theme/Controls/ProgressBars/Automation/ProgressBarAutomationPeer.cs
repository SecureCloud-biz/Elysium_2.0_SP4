using System;
using System.Diagnostics.Contracts;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

using Elysium.Theme.Controls.Primitives;

namespace Elysium.Theme.Controls.Automation
{
    public class ProgressBarAutomationPeer : RangeBaseAutomationPeer, IRangeValueProvider
    {
        public ProgressBarAutomationPeer(ProgressBarBase owner)
            : base(owner)
        {
            Contract.Assume(Owner != null);
        }

        protected override string GetClassNameCore()
        {
            return "ProgressBar";
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.ProgressBar;
        }

        public override object GetPattern(PatternInterface patternInterface)
        {
            var state = ((ProgressBarBase)Owner).State;
            if (patternInterface == PatternInterface.RangeValue && state == ProgressBarState.Indeterminate)
            {
                return null;
            }

            return base.GetPattern(patternInterface);
        }

        void IRangeValueProvider.SetValue(double val)
        {
            throw new InvalidOperationException("ProgressBar is read-only");
        }

        bool IRangeValueProvider.IsReadOnly
        {
            get { return true; }
        }

        double IRangeValueProvider.LargeChange
        {
            get { return double.NaN; }
        }

        double IRangeValueProvider.SmallChange
        {
            get { return double.NaN; }
        }

        [ContractInvariantMethod]
        private void Invariants()
        {
            // NOTE: WPF doesn't declare contracts
            Contract.Invariant(Owner != null);
        }
    }
} ;