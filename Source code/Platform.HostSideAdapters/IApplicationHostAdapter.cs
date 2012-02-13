using System.Runtime.Remoting;

namespace Elysium.Platform.HostSideAdapters
{
    public class IApplicationHostAdapter
    {
        internal static IApplication ContractToViewAdapter(Contracts.IApplication contract)
        {
            if (contract == null)
            {
                return null;
            }
            return (RemotingServices.IsObjectOutOfAppDomain(contract) != true) && contract.GetType() == typeof(IApplicationViewToContractHostAdapter)
                       ? ((IApplicationViewToContractHostAdapter)(contract)).GetSourceView()
                       : new IApplicationContractToViewHostAdapter(contract);
        }

        internal static Contracts.IApplication ViewToContractAdapter(IApplication view)
        {
            if (view == null)
            {
                return null;
            }
            return view.GetType() == typeof(IApplicationContractToViewHostAdapter)
                       ? ((IApplicationContractToViewHostAdapter)(view)).GetSource()
                       : new IApplicationViewToContractHostAdapter(view);
        }
    }
} ;