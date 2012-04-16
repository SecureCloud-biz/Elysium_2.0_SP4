using System.Diagnostics.Contracts;
using System.Windows.Automation.Peers;

using Elysium.Controls.Primitives;

using JetBrains.Annotations;

namespace Elysium.Controls.Automation
{
    [PublicAPI]
    public class CommandButtonBaseAutomationPeer : ButtonBaseAutomationPeer
    {
        [PublicAPI]
        public CommandButtonBaseAutomationPeer([NotNull] CommandButtonBase owner) : base(owner)
        {
            // NOTE: Lack of contracts: UIElementAutomationPeer's constructor throw ArgumentNullException if owner equals null
            Contract.Assume(Owner != null);
        }

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private void Invariants()
        {
            Contract.Invariant(Owner != null);
        }

        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            Contract.Ensures(Contract.Result<AutomationControlType>() == AutomationControlType.Button);
            return AutomationControlType.Button;
        }
    }
} ;