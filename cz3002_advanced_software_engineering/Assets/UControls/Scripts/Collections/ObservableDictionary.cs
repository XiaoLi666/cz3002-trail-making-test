using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Ultralpha
{
    public class ObservableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IObservable
    {
        public ObservableDictionary()
        {
        }

        public ObservableDictionary(int capacity)
            : base(capacity)
        {
        }

        public ObservableDictionary(int capacity, IEqualityComparer<TKey> comparer)
            : base(capacity, comparer)
        {
        }

        public ObservableDictionary(IEqualityComparer<TKey> comparer)
            : base(comparer)
        {
        }

        public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        {
        }

        public ObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
            : base(dictionary, comparer)
        {
        }

        protected ObservableDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        ~ObservableDictionary()
        {
            foreach (TValue value in Values)
            {
                TryUnregister(value);
            }
        }

        #region Observable Handler

        public event Action StateChangedHandler;

        protected virtual void OnStateChangedHandler()
        {
            Action handler = StateChangedHandler;
            if (handler != null) handler();
        }

        public event Action<TKey, TValue> AddHandler;

        protected virtual void OnAddHandler(TKey key, TValue value)
        {
            TryRegister(value);
            Action<TKey, TValue> handler = AddHandler;
            if (handler != null) handler(key, value);
            OnStateChangedHandler();
        }

        public event Action<TKey, TValue> RemoveHandler;

        protected virtual void OnRemoveHandler(TKey key, TValue value)
        {
            TryUnregister(value);
            Action<TKey, TValue> handler = RemoveHandler;
            if (handler != null) handler(key, value);
            OnStateChangedHandler();
        }

        public event Action<TKey, TValue> ItemChangedHandler;

        protected virtual void OnItemChangedHandler(TKey key, TValue value)
        {
            Action<TKey, TValue> handler = ItemChangedHandler;
            if (handler != null) handler(key, value);
            OnStateChangedHandler();
        }

        #endregion

        public new void Add(TKey key, TValue value)
        {
            base.Add(key, value);
            OnAddHandler(key, value);
        }

        public new void Clear()
        {
            foreach (TValue value in Values)
            {
                TryUnregister(value);
            }
            base.Clear();
            OnStateChangedHandler();
        }

        public new bool Remove(TKey key)
        {
            TValue value = base[key];
            bool result = base.Remove(key);
            if (result) OnRemoveHandler(key, value);
            return result;
        }

        public new TValue this[TKey key]
        {
            get { return base[key]; }
            set
            {
                base[key] = value;
                OnItemChangedHandler(key, value);
            }
        }

        private void TryRegister(TValue value)
        {
            if (value is INotifyPropertyChanged)
            {
                (value as INotifyPropertyChanged).PropertyChanged += OnPropertyChanged;
            }
        }

        private void TryUnregister(TValue value)
        {
            if (value is INotifyPropertyChanged)
            {
                (value as INotifyPropertyChanged).PropertyChanged -= OnPropertyChanged;
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                if (ReferenceEquals(pair.Value, sender))
                    OnItemChangedHandler(pair.Key, pair.Value);
            }
        }
    }
}