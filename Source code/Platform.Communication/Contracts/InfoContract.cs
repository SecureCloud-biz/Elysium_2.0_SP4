using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Windows.Media;

namespace Elysium.Platform.Communication.Contracts
{
    [ContractClassFor(typeof(Info))]
    internal abstract class InfoContract : Info
    {
        public override string Name
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return default(string);
            }
        }

        public override string Description
        {
            get { return default(string); }
        }

        public override IEnumerable<string> Author
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
                Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<string>>(), element => !string.IsNullOrEmpty(element)));
                return default(IEnumerable<string>);
            }
        }

        public override IEnumerable<Uri> License
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<Uri>>() != null);
                Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<Uri>>(), element => element != null));
                return default(IEnumerable<Uri>);
            }
        }

        public override ImageSource Image
        {
            get { return default(ImageSource); }
        }

        public override Uri Link
        {
            get { return default(Uri); }
        }
    }
} ;