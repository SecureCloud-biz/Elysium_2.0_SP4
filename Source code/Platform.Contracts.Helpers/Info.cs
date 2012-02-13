using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows.Media;

namespace Elysium.Platform.Contracts.Helpers
{
    using Contract = System.Diagnostics.Contracts.Contract;

    public class Info : MarshalByRefObject
    {
        public Info(string name, ReadOnlyCollection<string> author, ReadOnlyCollection<Uri> license,
                    string description = null, ImageSource image = null, Uri link = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (author == null)
            {
                throw new ArgumentNullException("author");
            }
            if (!Contract.ForAll(author.AsParallel(), x => !string.IsNullOrWhiteSpace(x)))
            {
                throw new ArgumentException("Items can't be null, empty or consists only of white-space characters.", "author");
            }
            if (license == null)
            {
                throw new ArgumentNullException("license");
            }
            if (!Contract.ForAll(license.AsParallel(), x => x != null))
            {
                throw new ArgumentException("Items can't be null.", "license");
            }
            Contract.EndContractBlock();
            Name = name;
            Description = description;
            Author = author;
            License = license;
            Image = image;
            Link = link;
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public ReadOnlyCollection<string> Author { get; private set; }

        public ReadOnlyCollection<Uri> License { get; private set; }

        public ImageSource Image { get; private set; }

        public Uri Link { get; private set; }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(!string.IsNullOrWhiteSpace(Name));
            Contract.Invariant(Author != null);
            Contract.Invariant(Contract.ForAll(Author.AsParallel(), x => !string.IsNullOrWhiteSpace(x)));
            Contract.Invariant(License != null);
            Contract.Invariant(Contract.ForAll(License.AsParallel(), x => x != null));
        }
    }
} ;