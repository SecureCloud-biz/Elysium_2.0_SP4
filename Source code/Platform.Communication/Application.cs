using System;
using System.AddIn.Contract;
using System.Diagnostics.Contracts;

namespace Elysium.Platform.Communication
{
    public delegate void ApplicationExecutionDelegate();

    [ContractClass(typeof(Contracts.ApplicationContract))]
    public abstract class Application : MarshalByRefObject
    {
        public abstract Info Info { get; }

        public abstract ApplicationCallback Callback { get; }

        public abstract bool IsAttachable { get; }

        public ApplicationExecutionDelegate Execute;

        public ApplicationExecutionDelegate Close;

        public abstract INativeHandleContract Visual { get; }
    }
} ;