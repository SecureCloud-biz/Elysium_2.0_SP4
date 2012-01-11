using System.Windows.Automation.Peers;

namespace Elysium.Theme.Controls.Automation
{
    public class ApplicationBarAutomationPeer : FrameworkElementAutomationPeer
    {
        public ApplicationBarAutomationPeer(ApplicationBar owner)
            : base(owner)
        {
        }

        protected override string GetClassNameCore()
        {
            return "ApplicationBar";
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Menu;
        }
    }
} ;