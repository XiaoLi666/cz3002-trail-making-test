using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ultralpha
{
    [RequireComponent(typeof (CanvasGroup), typeof (LayoutElement))]
    public class ListItem : MonoBehaviour, IPointerClickHandler, ISelectHandler, IDeselectHandler, IBeginDragHandler,
        IDragHandler, IEndDragHandler
    {
        private object _data;

        public object Data
        {
            get { return _data; }
            set
            {
                if (!Equals(_data, value))
                {
                    _data = value;
                    UpdateView();
                }
            }
        }

        protected virtual void UpdateView()
        {
            foreach (PathBinding binding in GetComponentsInChildren<PathBinding>())
            {
                binding.DataSource = _data;
            }
        }

        #region Events

        [SerializeField] private ListItemPointerUEvent _clicked;
        [SerializeField] private ListItemUEvent _selected;
        [SerializeField] private ListItemUEvent _deselected;
        [SerializeField] private ListItemPointerUEvent _beginDrag;
        [SerializeField] private ListItemPointerUEvent _dragging;
        [SerializeField] private ListItemPointerUEvent _endDrag;

        public event ListItemPointerEvent Clicked;
        public event ListItemEvent Selected;
        public event ListItemEvent Deselected;
        public event ListItemPointerEvent BeginDrag;
        public event ListItemPointerEvent Dragging;
        public event ListItemPointerEvent EndDrag;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
            if (_clicked != null) _clicked.Invoke(eventData);
            ListItemPointerEvent handler = Clicked;
            if (handler != null) handler(eventData);
        }

        void ISelectHandler.OnSelect(BaseEventData eventData)
        {
            if (_selected != null) _selected.Invoke();
            ListItemEvent handler = Selected;
            if (handler != null) handler();
        }

        void IDeselectHandler.OnDeselect(BaseEventData eventData)
        {
            if (_deselected != null) _deselected.Invoke();
            ListItemEvent handler = Deselected;
            if (handler != null) handler();
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (_dragging != null) _dragging.Invoke(eventData);
            ListItemPointerEvent handler = Dragging;
            if (handler != null) handler(eventData);
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            if (_beginDrag != null) _beginDrag.Invoke(eventData);
            ListItemPointerEvent handler = BeginDrag;
            if (handler != null) handler(eventData);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            if (_endDrag != null) _endDrag.Invoke(eventData);
            ListItemPointerEvent handler = EndDrag;
            if (handler != null) handler(eventData);
        }

        #endregion
    }
}