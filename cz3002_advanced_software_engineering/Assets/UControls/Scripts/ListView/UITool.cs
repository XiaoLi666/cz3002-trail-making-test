using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Ultralpha
{
    public static class UITool
    {
        private static Font _defaultFont;

        public static Font DefaultFont
        {
            get
            {
#if UNITY_5
                return _defaultFont ??
                       (_defaultFont = Font.CreateDynamicFontFromOSFont("Arial", 14)) ??
                       (_defaultFont = Font.CreateDynamicFontFromOSFont(Font.GetOSInstalledFontNames(), 14));
#else
                var font =  Resources.Load<Font>("Arial");
                if(!font)
                    Debug.LogError("Font Arial not found");
                return font;
#endif
            }
            set
            {
                if (value == null && _defaultFont != null)
                    Object.Destroy(_defaultFont);
                _defaultFont = value;
            }
        }

        public static ListItem CreateDefaultListItem()
        {
            ListItem item = new GameObject("ListItem").AddComponent<ListItem>();
            item.GetComponent<LayoutElement>().preferredHeight = 35f;
            HorizontalLayoutGroup group = item.gameObject.AddComponent<HorizontalLayoutGroup>();
            group.spacing = 2f;
            group.childForceExpandHeight = false;
            return item;
        }

        public static GameObject CreateDefaultColumn(Color background, Color foreground)
        {
            GameObject column = new GameObject("Column");
            column.AddComponent<LayoutElement>().preferredHeight = 35f;
            GameObject image = new GameObject("Image", typeof (Image));
            image.GetComponent<Image>().color = background;
            image.transform.SetParent(column.transform, false);
            image.GetComponent<RectTransform>().SetStretch(Vector4.zero);
            Text text = new GameObject("Text").AddComponent<Text>();
            text.transform.SetParent(column.transform, false);
            text.transform.SetAsLastSibling();
            text.text = "Column";
            text.font = DefaultFont;
            text.color = foreground;
            text.alignment = TextAnchor.MiddleCenter;
            text.GetComponent<RectTransform>().SetStretch(Vector4.zero);
            column.AddComponent<PathBinding>().Text = text;
            return column;
        }

        public static GameObject CreateDefaultHeader(Color background, Color foreground, params string[] titles)
        {
            GameObject header = new GameObject("Header");
            header.AddComponent<LayoutElement>().preferredHeight = 35f;
            HorizontalLayoutGroup group = header.gameObject.AddComponent<HorizontalLayoutGroup>();
            group.spacing = 2f;
            group.childForceExpandHeight = false;
            foreach (string title in titles)
            {
                GameObject column = new GameObject("Column");
                column.AddComponent<LayoutElement>().preferredHeight = 100;
                Image image = new GameObject("Image").AddComponent<Image>();
                image.color = background;
                image.transform.SetParent(column.transform, false);
                image.GetComponent<RectTransform>().SetStretch(Vector4.zero);
                Text text = new GameObject("Text").AddComponent<Text>();
                text.transform.SetParent(column.transform, false);
                text.transform.SetAsLastSibling();
                text.text = "Column";
                text.font = DefaultFont;
                text.color = foreground;
                //text.color = background.InvertColor();
                text.alignment = TextAnchor.MiddleCenter;
                text.GetComponent<RectTransform>().SetStretch(Vector4.zero);
                text.text = title;
                column.transform.SetParent(header.transform, false);
            }
            return header;
        }

        public static GameObject CreateDefaultHeader(Color background, Color foreground, Type type)
        {
            IEnumerable<MemberInfo> members =
                type.GetMembers(BindingFlags.Public | BindingFlags.Instance)
                    .Where(m => m.MemberType == MemberTypes.Field || m.MemberType == MemberTypes.Property);
            GameObject header =
                CreateDefaultHeader(background, foreground,
                    members.Where(m => !Attribute.IsDefined(m, typeof (ExcludeBinding)))
                        .Select(m => m.Name)
                        .ToArray());
            return header;
        }

        public static ListView CreateDefaultListView()
        {
            ListView listview = new GameObject("ListView", typeof (VerticalLayoutGroup)).AddComponent<ListView>();
            VerticalLayoutGroup group = listview.GetComponent<VerticalLayoutGroup>();
            group.spacing = 2;
            group.childForceExpandHeight = false;
            listview.GetComponent<RectTransform>().SetStretch(Vector4.one*5);
            return listview;
        }

        public static GameObject CreatePlaceHolder(this ListItem listItem)
        {
            int index = listItem.transform.GetSiblingIndex();
            GameObject placeHolder = new GameObject("PlaceHolder", typeof (LayoutElement), typeof (CanvasGroup));
            placeHolder.GetComponent<LayoutElement>().preferredHeight =
                listItem.GetComponent<LayoutElement>().preferredHeight;
            placeHolder.GetComponent<CanvasGroup>().blocksRaycasts = false;
            placeHolder.transform.SetParent(listItem.transform.parent, false);
            placeHolder.transform.SetSiblingIndex(index);
            return placeHolder;
        }

        public static GameObject CreateDefaultProgressBar()
        {
            Slider progressbar = new GameObject("ProgressBar", typeof (Image)).AddComponent<Slider>();
            Image fill = new GameObject("Fill").AddComponent<Image>();
            fill.color = new Color32(125, 200, 255, 255);
            fill.transform.SetParent(progressbar.transform, false);
            RectTransform fillRect = fill.GetComponent<RectTransform>();
            fillRect.SetStretch(Vector4.zero);
            progressbar.fillRect = fillRect;
            Text text = new GameObject("Text").AddComponent<Text>();
            text.transform.SetParent(progressbar.transform, false);
            text.transform.SetAsLastSibling();
            text.font = DefaultFont;
            text.color = new Color32(50, 50, 50, 255);
            text.alignment = TextAnchor.MiddleCenter;
            text.GetComponent<RectTransform>().SetStretch(Vector4.zero);
            text.text = string.Empty;
            RectTransform rect = progressbar.GetComponent<RectTransform>();
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 180);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 20);
            progressbar.interactable = false;
            ColorBlock colors = ColorBlock.defaultColorBlock;
            colors.disabledColor = Color.white;
            progressbar.colors = colors;
            return progressbar.gameObject;
        }

        public static void CreateDefaultToolTip(Vector3 worldPosition, string content, float duration)
        {
            Canvas canvas = Object.FindObjectOfType<Canvas>();
            if (!canvas)
            {
                canvas =
                    new GameObject("Canvas", typeof (Canvas), typeof (CanvasScaler), typeof (GraphicRaycaster))
                        .GetComponent<Canvas>();
                canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            }
            Image image = new GameObject("Tooltip").AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 50);
            image.transform.SetParent(canvas.transform, false);
            image.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 80);
            image.transform.SetAsLastSibling();
            Text text = new GameObject("Text").AddComponent<Text>();
            text.color = Color.white;
            text.alignment = TextAnchor.MiddleCenter;
            text.font = DefaultFont;
            text.transform.SetParent(image.transform, false);
            text.GetComponent<RectTransform>().SetStretch(Vector4.zero);
            text.text = content;
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(),
                Input.mousePosition, canvas.worldCamera, out pos);
            image.transform.position = canvas.transform.TransformPoint(pos);
            Object.Destroy(image.gameObject, duration);
        }

        /// <summary>
        /// Set RectTransform stretch
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <param name="padding">Offset (left,right,bottom,top)</param>
        public static void SetStretch(this RectTransform rectTransform, Vector4 padding)
        {
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = new Vector2(padding.x, padding.z);
            rectTransform.offsetMax = -new Vector2(padding.y, padding.w);
        }

        /// <summary>
        /// Set all child image's color
        /// </summary>
        /// <param name="item"></param>
        /// <param name="color"></param>
        public static void SetColor(this RectTransform item, Color color)
        {
            foreach (Image image in item.gameObject.GetComponentsInChildren<Image>().Where(img => !img.sprite))
            {
                image.color = color;
            }
        }

        /// <summary>
        /// Add color mask
        /// </summary>
        /// <param name="item"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Image AddColorMask(this RectTransform item, Color color)
        {
            Image image = new GameObject("ColorMask").AddComponent<Image>();
            image.gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
            image.transform.SetParent(item, false);
            image.transform.SetAsLastSibling();
            image.GetComponent<RectTransform>().SetStretch(Vector4.zero);
            color.a = 0.5f;
            image.color = color;
            return image;
        }

        /// <summary>
        /// Remove color mask if any exists
        /// </summary>
        /// <param name="item"></param>
        public static void RemoveColorMask(this RectTransform item)
        {
            Transform mask = item.Find("ColorMask");
            if (mask)
                Object.Destroy(mask.gameObject);
        }

        /// <summary>
        /// Invert color(doesn't work for gray)
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color InvertColor(this Color color)
        {
            return new Color(1 - color.r, 1 - color.g, 1 - color.b);
        }
    }
}