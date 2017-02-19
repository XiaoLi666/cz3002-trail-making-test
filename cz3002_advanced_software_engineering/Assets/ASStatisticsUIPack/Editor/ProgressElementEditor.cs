#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ASStatisticsUIPack
{
    [CustomEditor(typeof(ProgressElement), true)]
    [CanEditMultipleObjects]
    public class ProgressElementEditor : Editor
    {
        private bool rectTransformToggle = true;
        private bool colorToggle = true;
        private bool optionsToggle = false;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            rectTransformToggle = EditorGUILayout.Foldout(rectTransformToggle, "Transforms");
            if (rectTransformToggle)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("frontRect"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("backgroundRect"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("indicatorRect"));
            }

            colorToggle = EditorGUILayout.Foldout(colorToggle, "Colors");
            if (colorToggle)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("primaryColor"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("secondaryColor"));
            }
            EditorGUILayout.Slider(serializedObject.FindProperty("value"), 0f, 1f);

            optionsToggle = EditorGUILayout.Foldout(optionsToggle, "Options");
            if (optionsToggle)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("AnimateAtStart"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("showIndicator"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("showBackgroundBar"));
                EditorGUI.BeginChangeCheck();
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("OnValueChange"));

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
