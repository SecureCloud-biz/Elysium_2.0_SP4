using System.Diagnostics.Contracts;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace Elysium.Theme.Controls.Automation
{
    public class CommandButtonAutomationPeer : ButtonBaseAutomationPeer, IInvokeProvider
    {
        public CommandButtonAutomationPeer(CommandButton owner)
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