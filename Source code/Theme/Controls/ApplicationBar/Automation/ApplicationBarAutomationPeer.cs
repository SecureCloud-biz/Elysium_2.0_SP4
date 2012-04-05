using System.Diagnostics.Contracts;
using System.Windows.Automation.Peers;

namespace Elysium.Controls.Automation
{
    public class ApplicationBarAutomationPeer : FrameworkElementAutomationPeer
    {
        public ApplicationBarAutomationPeer(ApplicationBar owner)
            : base(owner)
        {
        }

        protected override string GetClassNameCore()
        {
            Contract.Ensures(Contract.Result<string>() == "ApplicationBar");
            return "ApplicationBar";
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            Contract.Ensures(Contract.Result<AutomationControlType>() == AutomationControlType.Menu);
            return AutomationControlType.Menu;
        }
    }
} ;