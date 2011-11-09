using System;
using System.AddIn.Contract;
using System.Diagnostics.Contracts;

namespace Elysium.Platform.Communication
{
    [ContractClass(typeof(Contracts.GadgetContract))]
    public abstract class Gadget : MarshalByRefObject
    {
        public abstract Info Info { get; }

        public abstract GadgetCallback Callback { get; }

        public abstract bool IsExpandable { get; }

        public abstract INativeHandleContract Visual { get; }
    }
} ;