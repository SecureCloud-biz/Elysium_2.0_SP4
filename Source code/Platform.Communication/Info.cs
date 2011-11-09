using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Windows.Media;

namespace Elysium.Platform.Communication
{
    [ContractClass(typeof(Contracts.InfoContract))]
    public abstract class Info : MarshalByRefObject
    {
        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract IEnumerable<string> Author { get; }

        public abstract IEnumerable<Uri> License { get; }

        public abstract ImageSource Image { get; }

        public abstract Uri Link { get; }
    }
} ;