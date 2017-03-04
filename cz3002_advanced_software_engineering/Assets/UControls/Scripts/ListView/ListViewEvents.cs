using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Ultralpha
{
    [Serializable]
    public class ListViewPointerUEvent : UnityEvent<ListItem, PointerEventData>
    {
    }

    [Serializable]
    public class ListViewUItemEvent : UnityEvent<ListItem>
    {
    }

    [Serializable]
    public class ListViewUEvent : UnityEvent
    {
    }

    [Serializable]
    public class ListItemPointerUEvent : UnityEvent<PointerEventData>
    {
    }

    [Serializable]
    public class ListItemUEvent : UnityEvent
    {
    }

    public delegate void ListViewPointerEvent(ListItem item, PointerEventData eventData);

    public delegate void ListItemPointerEvent(PointerEventData eventData);

    public delegate void ListViewItemEvent(ListItem item);

    public delegate void ListViewEvent();

    public delegate void ListItemEvent();
}