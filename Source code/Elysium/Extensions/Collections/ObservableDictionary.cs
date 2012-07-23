using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

// ReSharper disable CheckNamespace

namespace System.Collections.Generic
{
    [Serializable]
    [DebuggerDisplay("Count = {Count}")]
    public class ObservableDictionary<TKey, TValue> :
        IObservableDictionary<TKey, TValue>,
        IDictionary<TKey, TValue>,
        IDictionary,
        ISerializable,
        IDeserializationCallback
    {
        #region Private fields

        private readonly Dictionary<TKey, TValue> _cache;
        private Monitor _monitor = new Monitor();

        #endregion

        #region Monitor

        private class Monitor : IDisposable
        {
            private int _busyCount;

            public bool IsBusy
            {
                get { return _busyCount > 0; }
            }

            public void Enter()
            {
                _busyCount++;
            }

            public void Dispose()
            {
                _busyCount--;
            }
        }

        #endregion

        #region Constructors

        public ObservableDictionary()
            : this(0, null)
        {
            Contract.Ensures(Count == 0);
        }

        public ObservableDictionary(int capacity)
            : this(capacity, null)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity");
            }
            Contract.Ensures(Count == 0);
            Contract.EndContractBlock();
        }

        public ObservableDictionary(IEqualityComparer<TKey> comparer)
            : this(0, comparer)
        {
            Contract.Ensures(Count == 0);
        }

        public ObservableDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity");
            }
            Contract.Ensures(Count == 0);
            Contract.EndContractBlock();
            _cache = new Dictionary<TKey, TValue>(capacity, comparer);
        }

        public ObservableDictionary(IDictionary<TKey, TValue> dictionary) :
            this(dictionary, null)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            Contract.Ensures(Count == dictionary.Count);
            Contract.EndContractBlock();
        }

        public ObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) :
            this(dictionary != null ? dictionary.Count : 0, comparer)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            Contract.Ensures(Count == dictionary.Count);
            Contract.EndContractBlock();
            foreach (var item in dictionary)
            {
                Add(item.Key, item.Value);
            }
        }

        #endregion

        #region Public methods

        public void Add(TKey key, TValue value)
        {
            CheckReentrancy();
// ReSharper disable CompareNonConstrainedGenericWithNull
            if (!typeof(TKey).IsValueType && key == null)
// ReSharper restore CompareNonConstrainedGenericWithNull
            {
                throw new ArgumentNullException("key");
            }
            Contract.Ensures(Count == Contract.OldValue(Count) + 1);
            Contract.EndContractBlock();
            var oldCount = Count;
            _cache.Add(key, value);
            // NOTE: Lack of Code Contracts
            Contract.Assume(Count == oldCount + 1);
            RaiseAdded(key, value, IndexOf(key));
        }

        private int IndexOf(TKey key)
        {
            var index = 0;
            foreach (var item in this)
            {
                if (Equals(item.Key, key))
                {
                    break;
                }
                index++;
            }
            if (index == Count)
            {
                index = -1;
            }
            return index;
        }

        public bool ContainsKey(TKey key)
        {
// ReSharper disable CompareNonConstrainedGenericWithNull
            if (!typeof(TKey).IsValueType && key == null)
// ReSharper restore CompareNonConstrainedGenericWithNull
            {
                throw new ArgumentNullException("key");
            }
            Contract.Ensures(!Contract.Result<bool>() || Count > 0);
            Contract.EndContractBlock();
            return _cache.ContainsKey(key);
        }

        public bool ContainsValue(TValue value)
        {
            Contract.Ensures(!Contract.Result<bool>() || Count > 0);
            return _cache.ContainsValue(value);
        }

        public void Clear()
        {
            CheckReentrancy();
            var items = new ObservableKeyValuePair<TKey, TValue>[Count];
            ((ICollection<ObservableKeyValuePair<TKey, TValue>>)this).CopyTo(items, 0);
            _cache.Clear();
            RaiseCleared(items);
        }

        public bool Remove(TKey key)
        {
            CheckReentrancy();
// ReSharper disable CompareNonConstrainedGenericWithNull
            if (!typeof(TKey).IsValueType && key == null)
// ReSharper restore CompareNonConstrainedGenericWithNull
            {
                throw new ArgumentNullException("key");
            }
            Contract.Ensures(!Contract.Result<bool>() || Count == Contract.OldValue(Count) - 1);
            Contract.EndContractBlock();
            var oldCount = Count;
            // NOTE: Lack of Code Contracts
            Contract.Assume(!Contract.Result<bool>() || Count == oldCount - 1);
            var value = _cache[key];
            var result = _cache.Remove(key);
            RaiseRemoved(key, value, IndexOf(key));
            return result;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
// ReSharper disable CompareNonConstrainedGenericWithNull
            if (!typeof(TKey).IsValueType && key == null)
// ReSharper restore CompareNonConstrainedGenericWithNull
            {
                throw new ArgumentNullException("key");
            }
            Contract.EndContractBlock();
            return _cache.TryGetValue(key, out value);
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion

        #region Public properties

        public IEqualityComparer<TKey> Comparer
        {
            get
            {
                Contract.Ensures(Contract.Result<IEqualityComparer<TKey>>() != null);
                return _cache.Comparer;
            }
        }

        public int Count
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return _cache.Count;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
// ReSharper disable CompareNonConstrainedGenericWithNull
                if (!typeof(TKey).IsValueType && key == null)
// ReSharper restore CompareNonConstrainedGenericWithNull
                {
                    throw new ArgumentNullException("key");
                }
                Contract.EndContractBlock();
                return _cache[key];
            }
            set
            {
                CheckReentrancy();
// ReSharper disable CompareNonConstrainedGenericWithNull
                if (!typeof(TKey).IsValueType && key == null)
// ReSharper restore CompareNonConstrainedGenericWithNull
                {
                    throw new ArgumentNullException("key");
                }
                Contract.EndContractBlock();
                var oldValue = _cache[key];
                _cache[key] = value;
                RaiseReplaced(key, oldValue, value, IndexOf(key));
            }
        }

        public Dictionary<TKey, TValue>.KeyCollection Keys
        {
            get
            {
                Contract.Ensures(Contract.Result<Dictionary<TKey, TValue>.KeyCollection>() != null);
                return _cache.Keys;
            }
        }

        public Dictionary<TKey, TValue>.ValueCollection Values
        {
            get
            {
                Contract.Ensures(Contract.Result<Dictionary<TKey, TValue>.ValueCollection>() != null);
                return _cache.Values;
            }
        }

        #endregion

        #region Implementation of IDictionary

        object IDictionary.this[object key]
        {
            get { return ((IDictionary)_cache)[key]; }
            set
            {
                CheckReentrancy();
                var oldValue = ((IDictionary)_cache)[key];
                ((IDictionary)_cache)[key] = value;
                RaiseReplaced((TKey)key, (TValue)oldValue, (TValue)value, IndexOf((TKey)key));
            }
        }

        ICollection<TKey> IObservableDictionary<TKey, TValue>.Keys
        {
            get { return ((IDictionary<TKey, TValue>)_cache).Keys; }
        }

        ICollection<TValue> IObservableDictionary<TKey, TValue>.Values
        {
            get { return ((IDictionary<TKey, TValue>)_cache).Values; }
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get { return ((IDictionary<TKey, TValue>)_cache).Keys; }
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get { return ((IDictionary<TKey, TValue>)_cache).Values; }
        }

        ICollection IDictionary.Keys
        {
            get { return ((IDictionary)_cache).Keys; }
        }

        ICollection IDictionary.Values
        {
            get { return ((IDictionary)_cache).Values; }
        }

        bool IDictionary.IsReadOnly
        {
            get { return false; }
        }

        bool IDictionary.IsFixedSize
        {
            get { return false; }
        }

        bool IDictionary.Contains(object key)
        {
            return ((IDictionary)_cache).Contains(key);
        }

        void IDictionary.Add(object key, object value)
        {
            CheckReentrancy();
            ((IDictionary)_cache).Add(key, value);
            RaiseAdded((TKey)key, (TValue)value, IndexOf((TKey)key));
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return new Enumerator(this);
        }

        void IDictionary.Remove(object key)
        {
            CheckReentrancy();
            var value = ((IDictionary)_cache)[key];
            ((IDictionary)_cache).Remove(key);
            RaiseRemoved((TKey)key, (TValue)value, IndexOf((TKey)key));
        }

        #endregion

        #region Implementation of ICollection

        bool ICollection<ObservableKeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get { return false; }
        }

        void ICollection<ObservableKeyValuePair<TKey, TValue>>.Add(ObservableKeyValuePair<TKey, TValue> item)
        {
            CheckReentrancy();
            Add(item.Key, item.Value);
            RaiseAdded(item, IndexOf(item.Key));
        }

        bool ICollection<ObservableKeyValuePair<TKey, TValue>>.Contains(ObservableKeyValuePair<TKey, TValue> item)
        {
            return ContainsKey(item.Key) && ContainsValue(item.Value);
        }

        bool ICollection<ObservableKeyValuePair<TKey, TValue>>.Remove(ObservableKeyValuePair<TKey, TValue> item)
        {
            CheckReentrancy();
            var result = ((ICollection<ObservableKeyValuePair<TKey, TValue>>)this).Contains(item) && Remove(item.Key);
            RaiseRemoved(item, IndexOf(item.Key));
            return result;
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get { return false; }
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            CheckReentrancy();
            ((ICollection<KeyValuePair<TKey, TValue>>)_cache).Add(item);
            RaiseAdded(item.Key, item.Value, IndexOf(item.Key));
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)_cache).Contains(item);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            CheckReentrancy();
            var result = ((ICollection<KeyValuePair<TKey, TValue>>)_cache).Remove(item);
            RaiseRemoved(item.Key, item.Value, IndexOf(item.Key));
            return result;
        }

        object ICollection.SyncRoot
        {
            get { return ((ICollection)_cache).SyncRoot; }
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        void ICollection<ObservableKeyValuePair<TKey, TValue>>.CopyTo(ObservableKeyValuePair<TKey, TValue>[] array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (array.Rank != 1)
            {
                throw new ArgumentException("Multi-demensional array is not supported", "array");
            }
            if (array.GetLowerBound(0) != 0)
            {
                throw new ArgumentException("Array must have non-zero lower bound", "array");
            }
            if (index < 0 || index > array.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (array.Length - index < Count)
            {
                throw new ArgumentException("Array length too small", "array");
            }
            foreach (var item in _cache)
            {
                array[index++] = new ObservableKeyValuePair<TKey, TValue>(item.Key, item.Value);
            }
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (array.Rank != 1)
            {
                throw new ArgumentException("Multi-demensional array is not supported", "array");
            }
            if (array.GetLowerBound(0) != 0)
            {
                throw new ArgumentException("Array must have non-zero lower bound", "array");
            }
            if (index < 0 || index > array.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (array.Length - index < Count)
            {
                throw new ArgumentException("Array length too small", "array");
            }
            foreach (var item in _cache)
            {
                array[index++] = new KeyValuePair<TKey, TValue>(item.Key, item.Value);
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (array.Rank != 1)
            {
                throw new ArgumentException("Multi-demensional array is not supported", "array");
            }
            if (array.GetLowerBound(0) != 0)
            {
                throw new ArgumentException("Array must have non-zero lower bound", "array");
            }
            if (index < 0 || index > array.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (array.Length - index < Count)
            {
                throw new ArgumentException("Array length too small", "array");
            }
            if (!(array is object[] || array is ObservableKeyValuePair<TKey, TValue>[] || array is KeyValuePair<TKey, TValue>[] || array is DictionaryEntry[]))
            {
                throw new ArgumentException("Invalid array type", "array");
            }
            if (array is ObservableKeyValuePair<TKey, TValue>[])
            {
                var observableKeyValuePairArray = array as ObservableKeyValuePair<TKey, TValue>[];
                foreach (var item in _cache)
                {
                    observableKeyValuePairArray[index++] = new ObservableKeyValuePair<TKey, TValue>(item.Key, item.Value);
                }
            }
            else if (array is KeyValuePair<TKey, TValue>[])
            {
                var keyValuePairArray = array as KeyValuePair<TKey, TValue>[];
                foreach (var item in _cache)
                {
                    keyValuePairArray[index++] = new KeyValuePair<TKey, TValue>(item.Key, item.Value);
                }
            }
            else if (array is DictionaryEntry[])
            {
                var dictionaryEntryArray = array as DictionaryEntry[];
                foreach (var item in _cache)
                {
                    dictionaryEntryArray[index++] = new DictionaryEntry(item.Key, item.Value);
                }
            }
            else
            {
                var objectArray = array as object[];
                try
                {
                    foreach (var item in _cache)
                    {
                        objectArray[index++] = new ObservableKeyValuePair<TKey, TValue>(item.Key, item.Value);
                    }
                }
                catch (Exception)
                {
                    throw new ArgumentException("Invalid array type", "array");
                }
            }
        }

        #endregion

        #region Implementation of IEnumerable

        IEnumerator<ObservableKeyValuePair<TKey, TValue>> IEnumerable<ObservableKeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion

        #region Enumerator

        [Serializable]
        public struct Enumerator : IEnumerator<ObservableKeyValuePair<TKey, TValue>>, IDictionaryEnumerator, IEnumerator<KeyValuePair<TKey, TValue>>
        {
            #region Private fields

            private readonly ObservableDictionary<TKey, TValue> _dictionary;
            private Dictionary<TKey, TValue>.Enumerator _cacheEnumerator;

            #endregion

            #region Constructors

            internal Enumerator(ObservableDictionary<TKey, TValue> dictionary)
            {
                _dictionary = dictionary;
                _cacheEnumerator = dictionary._cache.GetEnumerator();
            }

            #endregion

            #region Implementation of IDictionaryEnumerator

            object IDictionaryEnumerator.Key
            {
                get { return ((IDictionaryEnumerator)_cacheEnumerator).Key; }
            }

            object IDictionaryEnumerator.Value
            {
                get { return ((IDictionaryEnumerator)_cacheEnumerator).Value; }
            }

            DictionaryEntry IDictionaryEnumerator.Entry
            {
                get { return ((IDictionaryEnumerator)_cacheEnumerator).Entry; }
            }

            #endregion

            #region Implementation of IEnumerator

            public ObservableKeyValuePair<TKey, TValue> Current
            {
                get
                {
                    var current = _cacheEnumerator.Current;
                    var item = new ObservableKeyValuePair<TKey, TValue>(current.Key, current.Value);
                    item.PropertyChanging += _dictionary.RaiseItemChanging;
                    return item;
                }
            }

            KeyValuePair<TKey, TValue> IEnumerator<KeyValuePair<TKey, TValue>>.Current
            {
                get { return _cacheEnumerator.Current; }
            }

            object IEnumerator.Current
            {
                get
                {
                    var current = ((IEnumerator)_cacheEnumerator).Current;
                    if (current is DictionaryEntry)
                    {
                        return current;
                    }
                    var pair = (KeyValuePair<TKey, TValue>)current;
                    var item = new ObservableKeyValuePair<TKey, TValue>(pair.Key, pair.Value);
                    item.PropertyChanging += _dictionary.RaiseItemChanging;
                    return item;
                }
            }

            public bool MoveNext()
            {
                return _cacheEnumerator.MoveNext();
            }

            void IEnumerator.Reset()
            {
                ((IEnumerator)_cacheEnumerator).Reset();
            }

            #endregion

            #region Implementation of IDisposable

            public void Dispose()
            {
            }

            #endregion
        }

        #endregion

        #region Implementation of ISerializable

        [SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _cache.GetObjectData(info, context);
        }

        #endregion

        #region Implementation of IDeserializationCallback

        public void OnDeserialization(object sender)
        {
            _cache.OnDeserialization(sender);
        }

        #endregion

        #region Private methods

        protected IDisposable BlockReentrancy()
        {
            _monitor.Enter();
            return _monitor;
        }

        protected void CheckReentrancy()
        {
            if (_monitor.IsBusy && CollectionChanged != null && CollectionChanged.GetInvocationList().Length > 1)
            {
                throw new InvalidOperationException("Observable dictionary reentrancy is not allowed");
            }
        }

        #endregion

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Implementation of INotifyCollectionChanged

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected virtual void RaiseCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (CollectionChanged != null)
            {
                using (BlockReentrancy())
                {
                    CollectionChanged(this, args);
                }
            }
        }

        #endregion

        #region Raise events

        private void RaiseAdded(TKey key, TValue value, int index)
        {
            RaiseAdded(new ObservableKeyValuePair<TKey, TValue>(key, value), index);
        }

        private void RaiseAdded(ObservableKeyValuePair<TKey, TValue> item, int index)
        {
            RaiseCollectionChanged();
            item.PropertyChanging += RaiseItemChanging;
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        private void RaiseReplaced(TKey key, TValue oldValue, TValue newValue, int index)
        {
            RaiseItemChanged();
            var oldItem = new ObservableKeyValuePair<TKey, TValue>(key, oldValue);
            var newItem = new ObservableKeyValuePair<TKey, TValue>(key, newValue);
            newItem.PropertyChanging += RaiseItemChanging;
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newItem, oldItem, index));
        }

        private void RaiseRemoved(TKey key, TValue value, int index)
        {
            RaiseRemoved(new ObservableKeyValuePair<TKey, TValue>(key, value), index);
        }

        private void RaiseRemoved(ObservableKeyValuePair<TKey, TValue> item, int index)
        {
            RaiseCollectionChanged();
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
        }

        private void RaiseCleared(ObservableKeyValuePair<TKey, TValue>[] items)
        {
            RaiseCollectionChanged();
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, items));
        }

        private void RaiseItemChanging(object sender, PropertyChangingEventArgs e)
        {
            var item = (ObservableKeyValuePair<TKey, TValue>)sender;
            this[item.Key] = item.PreviewValue;
        }

        private void RaiseCollectionChanged()
        {
            RaisePropertyChanged("Count");
            RaisePropertyChanged("Item[]");
            RaisePropertyChanged("Keys");
            RaisePropertyChanged("Values");
        }

        private void RaiseItemChanged()
        {
            RaisePropertyChanged("Item[]");
            RaisePropertyChanged("Values");
        }

        #endregion
    }
} ;

// ReSharper restore CheckNamespace