using System.ComponentModel;
using System.Text;

using JetBrains.Annotations;

// ReSharper disable CheckNamespace

namespace System.Collections.Generic
{
    [PublicAPI]
    [Serializable]
    public sealed class ObservableKeyValuePair<TKey, TValue> : INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region Private fields

        private readonly TKey _key;
        private TValue _value;

        internal TValue PreviewValue;

        #endregion

        #region Constructor

        [PublicAPI]
        public ObservableKeyValuePair(TKey key, TValue value)
        {
            _key = key;
            _value = value;
        }

        #endregion

        #region Properties

        [PublicAPI]
        public TKey Key
        {
            get { return _key; }
        }

        [PublicAPI]
        public TValue Value
        {
            get { return _value; }
            set
            {
                if (!Equals(_value, value))
                {
                    var observableChangingValue = _value as INotifyPropertyChanging;
                    if (observableChangingValue != null)
                    {
                        observableChangingValue.PropertyChanging -= OnValueChanging;
                    }
                    var observableChangedValue = _value as INotifyPropertyChanged;
                    if (observableChangedValue != null)
                    {
                        observableChangedValue.PropertyChanged -= OnValueChanged;
                    }
                    observableChangingValue = value as INotifyPropertyChanging;
                    if (observableChangingValue != null)
                    {
                        observableChangingValue.PropertyChanging += OnValueChanging;
                    }
                    observableChangedValue = value as INotifyPropertyChanged;
                    if (observableChangedValue != null)
                    {
                        observableChangedValue.PropertyChanged += OnValueChanged;
                    }
                    PreviewValue = value;
                    RaisePropertyChanging("Value");
                    _value = value;
                    RaisePropertyChanged("Value");
                }
            }
        }

        private void OnValueChanging(object sender, PropertyChangingEventArgs e)
        {
            RaisePropertyChanging("Value");
        }

        private void OnValueChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("Value");
        }

        #endregion

        #region Implementation of INotifyPropertyChanging

        public event PropertyChangingEventHandler PropertyChanging;

        private void RaisePropertyChanging(string propertyName)
        {
            var handler = PropertyChanging;
            if (handler != null)
            {
                handler(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanged

        [PublicAPI]
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj is ObservableKeyValuePair<TKey, TValue> && Equals((ObservableKeyValuePair<TKey, TValue>)obj);
        }

        private bool Equals(ObservableKeyValuePair<TKey, TValue> other)
        {
            return EqualityComparer<TKey>.Default.Equals(_key, other._key) && EqualityComparer<TValue>.Default.Equals(_value, other._value);
        }

        public override int GetHashCode()
        {
            unchecked
            {
// ReSharper disable NonReadonlyFieldInGetHashCode
                return (EqualityComparer<TKey>.Default.GetHashCode(_key) * 397) ^ EqualityComparer<TValue>.Default.GetHashCode(_value);
// ReSharper restore NonReadonlyFieldInGetHashCode
            }
        }

// ReSharper disable CompareNonConstrainedGenericWithNull

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('[');
            if (!typeof(TKey).IsValueType && Key != null)
                stringBuilder.Append(Key);
            stringBuilder.Append(", ");
            if (typeof(TValue).IsValueType && Value != null)
                stringBuilder.Append(Value);
            stringBuilder.Append(']');
            return stringBuilder.ToString();
        }

// ReSharper restore CompareNonConstrainedGenericWithNull

        #endregion
    }
} ;

// ReSharper restore CheckNamespace