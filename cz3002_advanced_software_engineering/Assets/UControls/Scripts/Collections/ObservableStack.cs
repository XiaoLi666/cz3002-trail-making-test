using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Ultralpha
{
    public class ObservableStack<T> : Stack<T>, IObservable
    {
        public ObservableStack()
        {
        }

        public ObservableStack(int capacity)
            : base(capacity)
        {
        }

        public ObservableStack(IEnumerable<T> collection)
            : base(collection)
        {
        }

        ~ObservableStack()
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

        public event Action<T> PopHandler;

        protected virtual void OnPopHandler(T obj)
        {
            TryUnregister(obj);
            Action<T> handler = PopHandler;
            if (handler != null) handler(obj);
            OnStateChangedHandler();
        }

        public event Action<T> PushHandler;

        protected virtual void OnPushHandler(T obj)
        {
            TryRegister(obj);
            Action<T> handler = PushHandler;
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

        public new T Pop()
        {
            T item = base.Pop();
            OnPopHandler(item);
            return item;
        }

        public new void Push(T item)
        {
            base.Push(item);
            OnPushHandler(item);
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