using System.Runtime.Remoting;

namespace Elysium.Platform.AddInSideAdapters
{
    public class IApplicationAddInAdapter
    {
        internal static IApplication ContractToViewAdapter(Contracts.IApplication contract)
        {
            if (contract == null)
            {
                return null;
            }
            return (RemotingServices.IsObjectOutOfAppDomain(contract) != true) && contract.GetType() == typeof(IApplicationViewToContractAddInAdapter)
                       ? ((IApplicationViewToContractAddInAdapter)(contract)).GetSource()
                       : new IApplicationContractToViewAddInAdapter(contract);
        }

        internal static Contracts.IApplication ViewToContractAdapter(IApplication view)
        {
            if (view == null)
            {
                return null;
            }
            return view.GetType() == typeof(IApplicationContractToViewAddInAdapter)
                       ? ((IApplicationContractToViewAddInAdapter)(view)).GetSource()
                       : new IApplicationViewToContractAddInAdapter(view);
        }
    }
} ;