using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ultralpha
{
    [RequireComponent(typeof (CanvasGroup), typeof (LayoutGroup))]
    public class ListView : MonoBehaviour
    {
        [SerializeField] private ListItem _listItemPrefab;
        [SerializeField] private GameObject _header;

        /// <summary>
        /// Enable or disable reordering
        /// </summary>
        public bool reorderable;

        /// <summary>
        /// Enable or disable selection
        /// </summary>
        public bool selectable;

        /// <summary>
        /// ListItem Background
        /// </summary>
        public Color background = Color.white;

        /// <summary>
        /// ListItem Foreground
        /// </summary>
        public Color foreground = Color.black;

        [SerializeField] private Color _selectionMask = Color.black;
        private IEnumerable _dataSource;
        private ListItem _draggingItem;
        private GameObject _placeHolder;

        /// <summary>
        /// Binding datasource
        /// </summary>
        public IEnumerable DataSource
        {
            get { return _dataSource; }
            set
            {
                if (!Equals(_dataSource, value))
                {
                    _dataSource = value;
                    UpdateView();
                    if (_dataSource is IObservable)
                        (_dataSource as IObservable).StateChangedHandler += UpdateView;
                }
            }
        }

        /// <summary>
        /// Selected ListItem
        /// </summary>
        public ListItem SelectedItem { get; private set; }

        /// <summary>
        /// ListItem prefab
        /// </summary>
        public ListItem ListItemPrefab
        {
            get { return _listItemPrefab; }
            set
            {
                _listItemPrefab = value;
                UpdateView();
            }
        }

        /// <summary>
        /// ListView header
        /// </summary>
        public GameObject Header
        {
            get { return _header; }
            set
            {
                if (_header != null)
                    Destroy(_header);

                _header = value;

                if (_header != null)
                {
                    _header.transform.SetParent(transform, false);
                    _header.transform.SetAsFirstSibling();
                }
            }
        }

        /// <summary>
        /// Selected ListItem color
        /// </summary>
        public Color SelectionMask
        {
            get { return _selectionMask; }
            set
            {
                _selectionMask = value;
                UpdateView();
            }
        }

        #region Events

        [SerializeField] private ListViewPointerUEvent _itemClicked;
        [SerializeField] private ListViewUItemEvent _itemSelected;
        [SerializeField] private ListViewUItemEvent _itemDeselected;
        [SerializeField] private ListViewPointerUEvent _itemBeginDrag;
        [SerializeField] private ListViewPointerUEvent _itemDragging;
        [SerializeField] private ListViewPointerUEvent _itemEndDrag;
        [SerializeField] private ListViewUEvent _selectionChanged;

        public event ListViewPointerEvent ItemClicked;
        public event ListViewItemEvent ItemSelected;
        public event ListViewItemEvent ItemDeselected;
        public event ListViewPointerEvent ItemBeginDrag;
        public event ListViewPointerEvent ItemDragging;
        public event ListViewPointerEvent ItemEndDrag;
        public event ListViewEvent SelectionChanged;

        protected virtual void OnSelectionChanged()
        {
            if (_selectionChanged != null) _selectionChanged.Invoke();
            ListViewEvent handler = SelectionChanged;
            if (handler != null) handler();
        }

        protected virtual void OnItemClicked(ListItem item, PointerEventData eventdata)
        {
            if (_itemClicked != null) _itemClicked.Invoke(item, eventdata);
            ListViewPointerEvent handler = ItemClicked;
            if (handler != null) handler(item, eventdata);
        }

        protected virtual void OnItemSelected(ListItem item)
        {
            if (_itemSelected != null) _itemSelected.Invoke(item);
            ListViewItemEvent handler = ItemSelected;
            if (handler != null) handler(item);
        }

        protected virtual void OnItemDeselected(ListItem item)
        {
            if (_itemDeselected != null) _itemDeselected.Invoke(item);
            ListViewItemEvent handler = ItemDeselected;
            if (handler != null) handler(item);
        }

        protected virtual void OnItemBeginDrag(ListItem item, PointerEventData eventdata)
        {
            if (reorderable)
            {
                _placeHolder = item.CreatePlaceHolder();
                item.transform.SetParent(transform.parent, false);
                item.transform.SetAsLastSibling();
                item.GetComponent<CanvasGroup>().blocksRaycasts = false;
                _draggingItem = item;
            }

            if (_itemBeginDrag != null) _itemBeginDrag.Invoke(item, eventdata);
            ListViewPointerEvent handler = ItemBeginDrag;
            if (handler != null) handler(item, eventdata);
        }

        protected virtual void OnItemDragging(ListItem item, PointerEventData eventdata)
        {
            if (reorderable && _draggingItem && eventdata.pointerCurrentRaycast.gameObject)
            {
                ListItem obj = eventdata.pointerCurrentRaycast.gameObject.GetComponentInParent<ListItem>();
                if (obj && obj.transform.IsChildOf(transform))
                {
                    _placeHolder.transform.SetParent(transform, false);
                    _placeHolder.transform.SetSiblingIndex(obj.transform.GetSiblingIndex());
                }
                RectTransform rect = _draggingItem.GetComponent<RectTransform>();
                rect.position = new Vector3(_placeHolder.transform.position.x, eventdata.position.y, rect.position.z);
            }

            if (_itemDragging != null) _itemDragging.Invoke(item, eventdata);
            ListViewPointerEvent handler = ItemDragging;
            if (handler != null) handler(item, eventdata);
        }

        protected virtual void OnItemEndDrag(ListItem item, PointerEventData eventdata)
        {
            if (reorderable && _draggingItem)
            {
                _draggingItem.transform.SetParent(transform, false);
                _draggingItem.transform.SetSiblingIndex(_placeHolder.transform.GetSiblingIndex());
                Destroy(_placeHolder);

                _draggingItem.GetComponent<CanvasGroup>().blocksRaycasts = true;
                _draggingItem = null;
            }

            if (_itemEndDrag != null) _itemEndDrag.Invoke(item, eventdata);
            ListViewPointerEvent handler = ItemEndDrag;
            if (handler != null) handler(item, eventdata);
        }

        protected virtual void OnToggle(bool value)
        {
            if (value)
            {
                foreach (Toggle toggle in GetComponent<ToggleGroup>().ActiveToggles())
                {
                    if (toggle.isOn)
                    {
                        ListItem item = toggle.GetComponent<ListItem>();
                        ListItem selected = SelectedItem;
                        SelectedItem = item;
                        if (selected != item)
                            OnSelectionChanged();
                        break;
                    }
                }
            }
        }

        #endregion

        protected virtual void UpdateView()
        {
            foreach (ListItem item in GetComponentsInChildren<ListItem>())
            {
                if (item != _listItemPrefab)
                    Destroy(item.gameObject);
            }

            if (DataSource == null)
                return;

            if (_listItemPrefab)
            {
                foreach (object data in DataSource)
                {
#if UNITY_5
                    ListItem item = Instantiate(_listItemPrefab);
#else
                    ListItem item = ((GameObject) Instantiate(_listItemPrefab.gameObject)).GetComponent<ListItem>();
#endif

                    AddItem(item, data);
                }
            }
            else
            {
                foreach (object data in DataSource)
                {
                    ListItem item = UITool.CreateDefaultListItem();
                    if (data is IEnumerable || data.GetType().Namespace == typeof (object).Namespace)
                    {
                        GameObject col = UITool.CreateDefaultColumn(background,foreground);
                        col.transform.SetParent(item.transform, false);
                    }
                    else
                    {
                        IEnumerable<MemberInfo> members =
                            data.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance)
                                .Where(m => m.MemberType == MemberTypes.Field || m.MemberType == MemberTypes.Property);
                        foreach (
                            MemberInfo member in members.Where(m => !Attribute.IsDefined(m, typeof (ExcludeBinding))))
                        {
                            GameObject col = UITool.CreateDefaultColumn(background, foreground);
                            col.GetComponent<PathBinding>().Path = member.Name;
                            col.transform.SetParent(item.transform, false);
                        }
                    }
                    AddItem(item, data);
                }
            }
        }

        protected virtual void AddItem(ListItem item, object data)
        {
            item.transform.SetParent(transform, false);
            item.Clicked += eventData => OnItemClicked(item, eventData);
            item.Selected += () => OnItemSelected(item);
            item.Deselected += () => OnItemDeselected(item);
            item.BeginDrag += eventData => OnItemBeginDrag(item, eventData);
            item.Dragging += eventData => OnItemDragging(item, eventData);
            item.EndDrag += eventData => OnItemEndDrag(item, eventData);
            if (selectable)
            {
                Toggle toggle = item.gameObject.AddComponent<Toggle>();
                toggle.hideFlags = HideFlags.HideInInspector;
                ToggleGroup group = GetComponent<ToggleGroup>() ?? gameObject.AddComponent<ToggleGroup>();
                group.hideFlags = HideFlags.HideInInspector;
                toggle.group = group;
                Image mask = item.GetComponent<RectTransform>().AddColorMask(_selectionMask);
                mask.gameObject.hideFlags = HideFlags.HideInHierarchy;
                toggle.graphic = mask;
                toggle.transition = Selectable.Transition.None;
                toggle.onValueChanged.AddListener(OnToggle);
            }
            item.Data = data;
        }

        [ContextMenu("Refresh ListView")]
        public void Refresh()
        {
            UpdateView();
        }
    }
}