using UnityEditor;
using UnityEngine;

namespace BugReproduction.Editor
{
    [CustomPropertyDrawer(typeof(SpW2))]
    public class SpW2Drawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            var prop = property.FindPropertyRelative("a");

            EditorGUI.PropertyField(position, prop, GUIContent.none);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("a"));
        }
    }
}