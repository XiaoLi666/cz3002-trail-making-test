using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Ultralpha
{
    [Serializable]
    public class ObservableList<T> : List<T>, IObservable
    {
        public ObservableList()
        {
        }

        public ObservableList(int capacity)
            : base(capacity)
        {
        }

        public ObservableList(IEnumerable<T> collection)
            : base(collection)
        {
        }

        ~ObservableList()
        {
            foreach (T item in this)
            {
                TryUnregister(item);
            }
        }

        #region Observable Handler

        public event Action StateChangedHandler;

        protected virtual void OnStateChangedHandler()
        {
            Action handler = StateChangedHandler;
            if (handler != null) handler();
        }

        public event Action<T> AddHandler;

        protected virtual void OnAddHandler(T obj)
        {
            TryRegister(obj);
            Action<T> handler = AddHandler;
            if (handler != null) handler(obj);
            OnStateChangedHandler();
        }

        public event Action<T> RemoveHandler;

        protected virtual void OnRemoveHandler(T obj)
        {
            TryUnregister(obj);
            Action<T> handler = RemoveHandler;
            if (handler != null) handler(obj);
            OnStateChangedHandler();
        }

        public event Action<T> ItemChangedHandler;

        protected virtual void OnItemChangedHandler(T obj)
        {
            Action<T> handler = ItemChangedHandler;
            if (handler != null) handler(obj);
            OnStateChangedHandler();
        }

        #endregion

        public new void Add(T item)
        {
            base.Add(item);
            OnAddHandler(item);
        }

        public new void AddRange(IEnumerable<T> collection)
        {
            foreach (T item in collection)
            {
                Add(item);
            }
        }

        public new void Insert(int index, T item)
        {
            base.Insert(index, item);
            OnAddHandler(item);
        }

        public new void InsertRange(int index, IEnumerable<T> collection)
        {
            foreach (T item in collection)
            {
                Insert(index, item);
                index++;
            }
        }

        public new bool Remove(T item)
        {
            bool result = base.Remove(item);
            if (result) OnRemoveHandler(item);
            return result;
        }


        public new void RemoveAt(int index)
        {
            T item = base[index];
            base.RemoveAt(index);
            OnRemoveHandler(item);
        }

        public new int RemoveAll(Predicate<T> match)
        {
            int count = 0;
            for (int i = Count - 1; i >= 0; i--)
            {
                if (match(this[i]))
                {
                    RemoveAt(i);
                    count++;
                }
            }
            return count;
        }

        public new void RemoveRange(int index, int count)
        {
            for (int i = index + count - 1; i >= 0; i--)
            {
                RemoveAt(i);
            }
        }

        public new void Clear()
        {
            foreach (T item in this)
            {
                TryUnregister(item);
            }
            base.Clear();
            OnStateChangedHandler();
        }

        public new T this[int index]
        {
            get { return base[index]; }
            set
            {
                base[index] = value;
                OnItemChangedHandler(value);
            }
        }

        private void TryRegister(T item)
        {
            if (item is INotifyPropertyChanged)
            {
                (item as INotifyPropertyChanged).PropertyChanged += OnPropertyChanged;
            }
        }

        private void TryUnregister(T item)
        {
            if (item is INotifyPropertyChanged)
            {
                (item as INotifyPropertyChanged).PropertyChanged -= OnPropertyChanged;
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            OnItemChangedHandler((T) sender);
        }
    }
}