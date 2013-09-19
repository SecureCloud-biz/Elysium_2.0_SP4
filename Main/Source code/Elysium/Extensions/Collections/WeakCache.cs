using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace Elysium.Extensions
{
    [SecurityCritical]
    internal sealed class WeakCache<T>
    {
        private List<WeakReference> _storage;

        public WeakCache() : this(0)
        {
        }

        public WeakCache(int capacity)
        {
            _storage = new List<WeakReference>(capacity);
        } 

        public void Add(T value)
        {
            ValidationHelper.NotNull(value, "value");
            Collect();
            if (!_storage.AsParallel().Any(reference => Equals(reference.Target, value)))
            {
                _storage.Add(new WeakReference(value));
            }
        }

        public void Remove(T value)
        {
            ValidationHelper.NotNull(value, "value");
            Collect();
            foreach (var reference in _storage.AsParallel().Where(reference => Equals(reference.Target, value)))
            {
                _storage.Remove(reference);
            }
        }

        public void Iterate(Action<T> action)
        {
            Collect();
            foreach (T item in _storage.Select(reference => reference.Target))
            {
                action(item);
            }
        }

        public void Collect()
        {
            GC.Collect();
            foreach (var reference in _storage.AsParallel().Where(reference => !reference.IsAlive))
            {
                _storage.Remove(reference);
            }
        }
    }
}