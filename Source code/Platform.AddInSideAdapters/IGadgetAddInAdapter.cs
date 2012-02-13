using System.Runtime.Remoting;

namespace Elysium.Platform.AddInSideAdapters
{
    public class IGadgetAddInAdapter
    {
        internal static IGadget ContractToViewAdapter(Contracts.IGadget contract)
        {
            if (contract == null)
            {
                return null;
            }
            return (RemotingServices.IsObjectOutOfAppDomain(contract) != true) && contract.GetType() == typeof(IGadgetViewToContractAddInAdapter)
                       ? ((IGadgetViewToContractAddInAdapter)(contract)).GetSource()
                       : new IGadgetContractToViewAddInAdapter(contract);
        }

        internal static Contracts.IGadget ViewToContractAdapter(IGadget view)
        {
            if (view == null)
            {
                return null;
            }
            return view.GetType() == typeof(IGadgetContractToViewAddInAdapter)
                       ? ((IGadgetContractToViewAddInAdapter)(view)).GetSource()
                       : new IGadgetViewToContractAddInAdapter(view);
        }
    }
} ;