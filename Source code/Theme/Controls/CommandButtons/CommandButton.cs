using System.Windows;
using System.Windows.Automation.Peers;

using Elysium.Theme.Controls.Automation;
using Elysium.Theme.Controls.Primitives;

namespace Elysium.Theme.Controls
{
    public class CommandButton : CommandButtonBase
    {
        static CommandButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CommandButton), new FrameworkPropertyMetadata(typeof(CommandButton)));
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new CommandButtonAutomationPeer(this);
        }

        protected override void OnClick()
        {
            if (AutomationPeer.ListenerExists(AutomationEvents.InvokePatternOnInvoked))
            {
                var peer = UIElementAutomationPeer.CreatePeerForElement(this);
                if (peer != null)
                {
                    peer.RaiseAutomationEvent(AutomationEvents.InvokePatternOnInvoked);
                }
            }
            base.OnClick();
        }

        internal void OnClickInternal()
        {
            OnClick();
        }
    }
} ;