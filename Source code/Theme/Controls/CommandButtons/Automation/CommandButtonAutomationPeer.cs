using System.Diagnostics.Contracts;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

using JetBrains.Annotations;

namespace Elysium.Controls.Automation
{
    public class CommandButtonAutomationPeer : ButtonBaseAutomationPeer, IInvokeProvider
    {
        public CommandButtonAutomationPeer([NotNull] CommandButton owner) : base(owner)
        {
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
            return patternInterface == PatternInterface.Invoke ? this : base.GetPattern(patternInterface);
        }

        public void Invoke()
        {
            if (!IsEnabled())
            {
                throw new ElementNotEnabledException();
            }

            Dispatcher.BeginInvoke(DispatcherPriority.Input, new DispatcherOperationCallback(
                                                                 delegate
                                                                 {
                                                                     ((CommandButton)Owner).OnClickInternal();
                                                                     return null;
                                                                 }), null);
        }
    }
} ;