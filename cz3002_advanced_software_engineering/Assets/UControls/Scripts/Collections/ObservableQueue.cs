using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Ultralpha
{
    public class ObservableQueue<T> : Queue<T>, IObservable
    {
        public ObservableQueue()
        {
        }

        public ObservableQueue(int capacity)
            : base(capacity)
        {
        }

        public ObservableQueue(IEnumerable<T> collection)
            : base(collection)
        {
        }

        ~ObservableQueue()
        {
            foreach (T obj in this)
            {
                TryUnregister(obj);
            }
        }

        #region Observable Handler

        public event Action StateChangedHandler;

        protected virtual void OnStateChangedHandler()
        {
            Action handler = StateChangedHandler;
            if (handler != null) handler();
        }

        public event Action<T> EnqueHandler;

        protected virtual void OnEnqueHandler(T obj)
        {
            TryRegister(obj);
            Action<T> handler = EnqueHandler;
            if (handler != null) handler(obj);
            OnStateChangedHandler();
        }

        public event Action<T> DequeHandler;

        protected virtual void OnDequeHandler(T obj)
        {
            TryUnregister(obj);
            Action<T> handler = DequeHandler;
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

        public new T Dequeue()
        {
            T item = base.Dequeue();
            OnDequeHandler(item);
            return item;
        }

        public new void Enqueue(T item)
        {
            base.Enqueue(item);
            OnEnqueHandler(item);
        }

        public new void Clear()
        {
            base.Clear();
            OnStateChangedHandler();
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