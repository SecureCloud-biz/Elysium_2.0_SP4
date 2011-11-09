using System.AddIn.Contract;
using System.Diagnostics.Contracts;

namespace Elysium.Platform.Communication.Contracts
{
    [ContractClassFor(typeof(Gadget))]
    internal abstract class GadgetContract : Gadget
    {
        public override Info Info
        {
            get
            {
                Contract.Ensures(Contract.Result<Info>() != null);
                return default(Info);
            }
        }

        public override GadgetCallback Callback
        {
            get
            {
                Contract.Ensures(Contract.Result<GadgetCallback>() != null);
                return default(GadgetCallback);
            }
        }

        public override INativeHandleContract Visual
        {
            get
            {
                Contract.Ensures(Contract.Result<INativeHandleContract>() != null);
                return default(INativeHandleContract);
            }
        }
    }
} ;