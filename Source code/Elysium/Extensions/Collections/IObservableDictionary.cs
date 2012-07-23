using System.Collections.Specialized;
using System.ComponentModel;

using JetBrains.Annotations;

// ReSharper disable CheckNamespace

namespace System.Collections.Generic
{
    [PublicAPI]
    public interface IObservableDictionary<TKey, TValue> :
        ICollection<ObservableKeyValuePair<TKey, TValue>>,
        IEnumerable<ObservableKeyValuePair<TKey, TValue>>,
        IEnumerable,
        INotifyCollectionChanged,
        INotifyPropertyChanged
    {
        [PublicAPI]
        TValue this[TKey key] { get; set; }

        [PublicAPI]
        ICollection<TKey> Keys { get; }

        [PublicAPI]
        ICollection<TValue> Values { get; }

        [PublicAPI]
        bool ContainsKey(TKey key);

        [PublicAPI]
        void Add(TKey key, TValue value);

        [PublicAPI]
        bool Remove(TKey key);

        [PublicAPI]
        bool TryGetValue(TKey key, out TValue value);
    }
} ;

// ReSharper restore CheckNamespace