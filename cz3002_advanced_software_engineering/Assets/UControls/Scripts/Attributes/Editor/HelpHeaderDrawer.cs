using UnityEditor;
using UnityEngine;

namespace Ultralpha.Editor
{
    [CustomPropertyDrawer(typeof (HelpHeaderAttribute))]
    public class HelpHeaderDrawer : DecoratorDrawer
    {
        private float height = EditorGUIUtility.singleLineHeight;

        public override void OnGUI(Rect position)
        {
            GUIStyle style =
#if UNITY_5
                EditorStyles.helpBox;
#else
                EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).FindStyle("HelpBox");
#endif

            position = EditorGUI.IndentedRect(position);
            GUIContent gui = new GUIContent((attribute as HelpHeaderAttribute).content,
                (attribute as HelpHeaderAttribute).tooltip);
            height = style.CalcHeight(gui, position.width);
            EditorGUI.LabelField(position, gui, style);
        }

        public override float GetHeight()
        {
            return height;
        }
    }
}