using System.Runtime.Remoting;

namespace Elysium.Platform.HostSideAdapters
{
    internal static class IGadgetHostAdapter
    {
        internal static IGadget ContractToViewAdapter(Contracts.IGadget contract)
        {
            if (contract == null)
            {
                return null;
            }
            return (RemotingServices.IsObjectOutOfAppDomain(contract) != true) && contract.GetType() == typeof(IGadgetViewToContractHostAdapter)
                       ? ((IGadgetViewToContractHostAdapter)(contract)).GetSource()
                       : new IGadgetContractToViewHostAdapter(contract);
        }

        internal static Contracts.IGadget ViewToContractAdapter(IGadget view)
        {
            if (view == null)
            {
                return null;
            }
            return view.GetType() == typeof(IGadgetContractToViewHostAdapter)
                       ? ((IGadgetContractToViewHostAdapter)(view)).GetSource()
                       : new IGadgetViewToContractHostAdapter(view);
        }
    }
} ;