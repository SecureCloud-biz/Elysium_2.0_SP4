using System.Collections.Generic;
using System.Linq;
using Elysium.Extensions;

namespace System.Windows
{
    public class CachedResourceDictionary : ResourceDictionary
    {
        private static readonly Dictionary<Uri, WeakReference> Cache = new Dictionary<Uri, WeakReference>();

        public new Uri Source
        {
            get { return _source; }
            set
            {
                _source = value;
                if (DesignUtil.IsInDesignMode())
                {
                    base.Source = _source;
                }
                else
                {
                    ResourceDictionary resourceDictionary;
                    if (!Cache.SafeGet(_source, out resourceDictionary))
                    {
                        base.Source = _source;
                        Cache.SafeSet(_source, this);
                    }
                    else
                    {
                        MergedDictionaries.Add(resourceDictionary);
                    }
                }
            }
        }

        private Uri _source;
    }
}