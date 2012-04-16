using System;
using System.Diagnostics.Contracts;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

using Elysium.Controls.Primitives;

using JetBrains.Annotations;

namespace Elysium.Controls.Automation
{
    public class ProgressBarAutomationPeer : RangeBaseAutomationPeer, IRangeValueProvider
    {
        public ProgressBarAutomationPeer([NotNull] ProgressBarBase owner) : base(owner)
        {
            // BUG in CodeContracts: UIElementAutomationPeer's constructor throw ArgumentNullException if owner equals null
            Contract.Assume(Owner != null);
        }

        protected override string GetClassNameCore()
        {
            Contract.Ensures(Contract.Result<string>() == "ProgressBar");
            return "ProgressBar";
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            Contract.Ensures(Contract.Result<AutomationControlType>() == AutomationControlType.ProgressBar);
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

        public void SetValue(double value)
        {
            throw new InvalidOperationException("Progress bar is read-only");
        }

        public bool IsReadOnly
        {
            get
            {
                Contract.Ensures(Contract.Result<bool>());
                return true;
            }
        }

        public double LargeChange
        {
            get
            {
                Contract.Ensures(double.IsNaN(Contract.Result<double>()));
                return double.NaN;
            }
        }

        public double SmallChange
        {
            get
            {
                Contract.Ensures(double.IsNaN(Contract.Result<double>()));
                return double.NaN;
            }
        }

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private void Invariants()
        {
            // BUG in CodeContracts: Owner can't be null
            Contract.Invariant(Owner != null);
        }
    }
} ;