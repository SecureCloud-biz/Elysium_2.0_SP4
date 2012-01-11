using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Windows.Media;

namespace Elysium.Platform.Communication
{
    public class Info : MarshalByRefObject
    {
        public Info(string name, IEnumerable<string> author, IEnumerable<Uri> license, string description = null, ImageSource image = null, Uri link = null)
        {
            Name = name;
            Description = description;
            License = license;
            Author = author;
            Image = image;
            Link = link;
        }

        public string Name
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return _name;
            }
            private set { _name = value; }
        }

        private string _name;

        public string Description { get; private set; }

        public IEnumerable<string> Author
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
                Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<string>>(), element => !string.IsNullOrEmpty(element)));
                return _author;
            }
            private set { _author = value; }
        }

        private IEnumerable<string> _author;

        public IEnumerable<Uri> License
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<Uri>>() != null);
                Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<Uri>>(), element => element != null));
                return _license;
            }
            private set { _license = value; }
        }

        private IEnumerable<Uri> _license;

        public ImageSource Image { get; private set; }

        public Uri Link { get; private set; }
    }
} ;