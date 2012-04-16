using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

using JetBrains.Annotations;

namespace Elysium.Controls.Automation
{
    public class ToggleCommandButtonAutomationPeer : ButtonBaseAutomationPeer, IToggleProvider
    {
        public ToggleCommandButtonAutomationPeer([NotNull] ToggleCommandButton owner) : base(owner)
        {
            // BUG in CodeContracts: UIElementAutomationPeer's constructor throw ArgumentNullException if owner equals null
            Contract.Assume(Owner != null);
        }

        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        protected override string GetClassNameCore()
        {
            Contract.Ensures(Contract.Result<string>() == "Button");
            return "Button";
        }

        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            Contract.Ensures(Contract.Result<AutomationControlType>() == AutomationControlType.Button);
            return AutomationControlType.Button;
        }

        public override object GetPattern(PatternInterface patternInterface)
        {
            return patternInterface == PatternInterface.Toggle ? this : base.GetPattern(patternInterface);
        }

        public void Toggle()
        {
            if (!IsEnabled())
            {
                throw new ElementNotEnabledException();
            }

            var owner = (ToggleCommandButton)Owner;
            owner.OnToggle();
        }

        public ToggleState ToggleState
        {
            get
            {
                var owner = (ToggleCommandButton)Owner;
                return ConvertToToggleState(owner.IsChecked);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RaiseToggleStatePropertyChangedEvent(bool? oldValue, bool? newValue)
        {
            if (oldValue != newValue)
            {
                RaisePropertyChangedEvent(TogglePatternIdentifiers.ToggleStateProperty, ConvertToToggleState(oldValue), ConvertToToggleState(newValue));
            }
        }

        private static ToggleState ConvertToToggleState(bool? value)
        {
            switch (value)
            {
                case (true):
                    return ToggleState.On;
                case (false):
                    return ToggleState.Off;
                default:
                    return ToggleState.Indeterminate;
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