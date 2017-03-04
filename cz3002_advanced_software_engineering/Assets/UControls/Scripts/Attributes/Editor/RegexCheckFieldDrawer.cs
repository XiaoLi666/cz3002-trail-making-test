using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Ultralpha.Editor
{
    [CustomPropertyDrawer(typeof (RegexCheckFieldAttribute))]
    public class RegexCheckFieldDrawer : PropertyDrawer
    {
        private float height = EditorGUIUtility.singleLineHeight;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUIStyle style =
#if UNITY_5
                EditorStyles.helpBox;
#else
                EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).FindStyle("HelpBox");
#endif

            RegexCheckFieldAttribute atr = (RegexCheckFieldAttribute) attribute;
            position.y += height;
            position.height = EditorGUI.GetPropertyHeight(property, label);
            EditorGUI.PropertyField(position, property, label);
            string pattern = atr.pattern;
            if (string.IsNullOrEmpty(pattern) || property.propertyType != SerializedPropertyType.String ||
                Regex.IsMatch(property.stringValue, pattern, RegexOptions.IgnoreCase))
            {
                height = 0;
                return;
            }
            if (atr.forceClear)
                property.stringValue = null;
            position = EditorGUI.IndentedRect(position);
            GUIContent gui = new GUIContent(atr.warning ?? "Input does not match the pattern: " + pattern);
            height = style.CalcHeight(gui, position.width);
            position.y -= height;
            position.height = height;
            EditorGUI.HelpBox(position, gui.text, MessageType.Error);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return height + EditorGUI.GetPropertyHeight(property, label);
        }
    }
}