using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.ComponentModel.Composition;

namespace Elysium.Platform.Collections
{
    [DebuggerDisplay("Count = {Count}")]
    [ComVisible(false)]
    [Serializable]
    internal class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary,
                                                      INotifyCollectionChanged, INotifyPropertyChanged,
                                                      ISerializable, IDeserializationCallback,
                                                      IXmlSerializable
    {
        #region Monitor

        [Serializable]
        private class Monitor : IDisposable
        {
            private int _busyCount;

            public bool Busy
            {
                get { return _busyCount > 0; }
            }

            public void Enter()
            {
                ++_busyCount;
            }

            public void Dispose()
            {
                --_busyCount;
            }
        }

        private readonly Monitor _monitor = new Monitor();

        protected IDisposable BlockReentrancy()
        {
            _monitor.Enter();
            return _monitor;
        }

        protected void CheckReentrancy()
        {
            if (_monitor.Busy && _collectionChanged != null && _collectionChanged.GetInvocationList().Length > 1)
                throw new InvalidOperationException(Resources.Default.ObservableDictionaryReentrancyNotAllowed);
        }

        #endregion

        #region Dictionary

        protected Dictionary<TKey, TValue> Dictionary
        {
            get { return _dictionary; }
        }

        private readonly Dictionary<TKey, TValue> _dictionary;

        #endregion

        #region Constructors

        public ObservableDictionary()
        {
            _dictionary = new Dictionary<TKey, TValue>();
        }

        public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
        {
            _dictionary = new Dictionary<TKey, TValue>(dictionary);
        }

        public ObservableDictionary(IEqualityComparer<TKey> comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(comparer);
        }

        public ObservableDictionary(int capacity)
        {
            _dictionary = new Dictionary<TKey, TValue>(capacity);
        }

        public ObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(dictionary, comparer);
        }

        public ObservableDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(capacity, comparer);
        }

        #endregion

        #region Implementation of IEnumerable and IEnumerable<KeyValuePair<TKey, TValue>>

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<TKey, TValue>>)_dictionary).GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection and ICollection<KeyValuePair<TKey, TValue>>

        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)_dictionary).CopyTo(array, index);
        }

        int ICollection.Count
        {
            [Pure]
            get { return ((ICollection)_dictionary).Count; }
        }

        object ICollection.SyncRoot
        {
            get { return ((ICollection)_dictionary).SyncRoot; }
        }

        bool ICollection.IsSynchronized
        {
            get { return ((ICollection)_dictionary).IsSynchronized; }
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            CheckReentrancy();
            ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Add(item);
            OnPropertiesChanged();
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            CheckReentrancy();
            ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Clear();
            OnPropertiesChanged();
            OnCollectionReset();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Contains(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            CheckReentrancy();
            var result = ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Remove(item);
            if (result)
            {
                OnPropertiesChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Remove, item);
            }
            return result;
        }

        int ICollection<KeyValuePair<TKey, TValue>>.Count
        {
            [Pure]
            get { return ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Count; }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get { return ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).IsReadOnly; }
        }

        #endregion

        #region Implementation of IDictionary

        bool IDictionary.Contains(object key)
        {
            return ((IDictionary)_dictionary).Contains(key);
        }

        void IDictionary.Add(object key, object value)
        {
            CheckReentrancy();
            var itemKey = key;
            var itemValue = value;
            ((IDictionary)_dictionary).Add(key, value);
            OnPropertiesChanged();
            OnCollectionChanged(NotifyCollectionChangedAction.Add, new DictionaryEntry(itemKey, itemValue));
        }

        public void Clear()
        {
            CheckReentrancy();
            ((IDictionary)_dictionary).Clear();
            OnPropertiesChanged();
            OnCollectionReset();
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary)_dictionary).GetEnumerator();
        }

        void IDictionary.Remove(object key)
        {
            CheckReentrancy();
            var itemKey = key;
            var itemValue = ((IDictionary)_dictionary)[key];
            ((IDictionary)_dictionary).Remove(key);
            OnPropertiesChanged();
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, new DictionaryEntry(itemKey, itemValue));
        }

        object IDictionary.this[object key]
        {
            get { return ((IDictionary)_dictionary)[key]; }
            set
            {
                CheckReentrancy();
                var oldItem = new DictionaryEntry(key, ((IDictionary)_dictionary)[key]);
                var newItem = new DictionaryEntry(key, value);
                ((IDictionary)_dictionary)[key] = value;
                OnPropertiesChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Replace, newItem, oldItem);
            }
        }

        ICollection IDictionary.Keys
        {
            get { return ((IDictionary)_dictionary).Keys; }
        }

        ICollection IDictionary.Values
        {
            get { return ((IDictionary)_dictionary).Values; }
        }

        bool IDictionary.IsReadOnly
        {
            get { return ((IDictionary)_dictionary).IsReadOnly; }
        }

        bool IDictionary.IsFixedSize
        {
            get { return ((IDictionary)_dictionary).IsFixedSize; }
        }

        #endregion

        #region Class members

        public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public IEqualityComparer<TKey> Comparer
        {
            get { return _dictionary.Comparer; }
        }

        [Pure]
        public bool ContainsKey(TKey key)
        {
            Contract.Ensures(!Contract.Result<bool>() || Count > 0);
            return _dictionary.ContainsKey(key);
        }

        [Pure]
        public bool ContainsValue(TValue value)
        {
            Contract.Ensures(!Contract.Result<bool>() || Count > 0);
            return _dictionary.ContainsValue(value);
        }

        public void Add(TKey key, TValue value)
        {
            CheckReentrancy();
            _dictionary.Add(key, value);
            OnPropertiesChanged();
            OnCollectionChanged(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value));
        }

        public TValue this[TKey key]
        {
            get { return _dictionary[key]; }
            set
            {
                CheckReentrancy();
                var oldItem = new KeyValuePair<TKey, TValue>(key, _dictionary[key]);
                var newItem = new KeyValuePair<TKey, TValue>(key, value);
                _dictionary[key] = value;
                OnPropertiesChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Replace, newItem, oldItem);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            Contract.Ensures(Contract.Result<bool>() == ContainsKey(key));
            var result = _dictionary.TryGetValue(key, out value);
            return result;
        }

        public bool Remove(TKey key)
        {
            CheckReentrancy();
            var itemKey = key;
            var itemValue = _dictionary[key];
            var result = _dictionary.Remove(key);
            OnPropertiesChanged();
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, new KeyValuePair<TKey, TValue>(itemKey, itemValue));
            return result;
        }

        public int Count
        {
            [Pure]
            get { return _dictionary.Count; }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                Contract.Ensures(Contract.Result<ICollection<TKey>>() != null);
                return _dictionary.Keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                Contract.Ensures(Contract.Result<ICollection<TValue>>() != null);
                return _dictionary.Values;
            }
        }

        #endregion

        #region Notifications

        [NonSerialized]
        private NotifyCollectionChangedEventHandler _collectionChanged;

        [NonSerialized]
        private PropertyChangedEventHandler _propertyChanged;

        event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
        {
            add { _collectionChanged += value; }
            remove { _collectionChanged -= value; }
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { _propertyChanged += value; }
            remove { _propertyChanged -= value; }
        }

        public virtual event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                var changedEventHandler = _collectionChanged;
                NotifyCollectionChangedEventHandler comparand;
                do
                {
                    comparand = changedEventHandler;
                    changedEventHandler = Interlocked.CompareExchange(ref _collectionChanged, comparand + value, comparand);
                } while (changedEventHandler != comparand);
            }
            remove
            {
                var changedEventHandler = _collectionChanged;
                NotifyCollectionChangedEventHandler comparand;
                do
                {
                    comparand = changedEventHandler;
                    changedEventHandler = Interlocked.CompareExchange(ref _collectionChanged, comparand - value, comparand);
                } while (changedEventHandler != comparand);
            }
        }

        protected virtual event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                var changedEventHandler = _propertyChanged;
                PropertyChangedEventHandler comparand;
                do
                {
                    comparand = changedEventHandler;
                    changedEventHandler = Interlocked.CompareExchange(ref _propertyChanged, comparand + value, comparand);
                } while (changedEventHandler != comparand);
            }
            remove
            {
                var changedEventHandler = _propertyChanged;
                PropertyChangedEventHandler comparand;
                do
                {
                    comparand = changedEventHandler;
                    changedEventHandler = Interlocked.CompareExchange(ref _propertyChanged, comparand - value, comparand);
                } while (changedEventHandler != comparand);
            }
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (_collectionChanged == null)
                return;
            using (BlockReentrancy())
                _collectionChanged(this, e);
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action, object changedItem)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, changedItem));
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action, object newItem, object oldItem)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem));
        }

        protected virtual void OnCollectionReset()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (_propertyChanged == null)
                return;
            _propertyChanged(this, e);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertiesChanged()
        {
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
            OnPropertyChanged("Keys");
            OnPropertyChanged("Values");
        }

        #endregion

        #region Serialization

        [SecurityCritical]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _dictionary.GetObjectData(info, context);
        }

        [SecuritySafeCritical]
        public virtual void OnDeserialization(object sender)
        {
            _dictionary.OnDeserialization(sender);
        }

        #endregion

        #region Implementation of IXmlSerializable

        public XmlSchema GetSchema()
        {
            return null;
        }

        [SecurityCritical]
        public void ReadXml(XmlReader reader)
        {
            var wasEmpty = reader.IsEmptyElement;
            reader.Read();
            if (wasEmpty) return;

            var keySerializer = new XmlSerializer(typeof(TKey));
            var valueSerializer = new XmlSerializer(typeof(TValue));

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");

                reader.ReadStartElement("key");
                var key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement("value");
                var value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();

                Add(key, value);

                reader.ReadEndElement();

                reader.MoveToContent();
            }
        }

        [SecurityCritical]
        public void WriteXml(XmlWriter writer)
        {
            var keySerializer = new XmlSerializer(typeof(TKey));
            var valueSerializer = new XmlSerializer(typeof(TValue));

            foreach (var key in Keys)
            {
                writer.WriteStartElement("item");

                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement("value");
                valueSerializer.Serialize(writer, this[key]);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        #endregion
    }
} ;