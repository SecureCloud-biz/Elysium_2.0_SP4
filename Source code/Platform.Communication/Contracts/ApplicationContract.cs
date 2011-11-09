using System.AddIn.Contract;
using System.Diagnostics.Contracts;

namespace Elysium.Platform.Communication.Contracts
{
    [ContractClassFor(typeof(Application))]
    internal abstract class ApplicationContract : Application
    {
        public override Info Info
        {
            get
            {
                Contract.Ensures(Contract.Result<Info>() != null);
                return default(Info);
            }
        }

        public override ApplicationCallback Callback
        {
            get
            {
                Contract.Ensures(Contract.Result<ApplicationCallback>() != null);
                return default(ApplicationCallback);
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