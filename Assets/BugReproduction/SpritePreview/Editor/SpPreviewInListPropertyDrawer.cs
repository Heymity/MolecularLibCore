using UnityEditor;
using UnityEngine;

namespace BugReproduction.Editor
{
    [CustomPropertyDrawer(typeof(SpWrapper))]
    public class SpPreviewInListPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("testSprite"));
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var testStr = property.FindPropertyRelative("testStr");
            var testSp = property.FindPropertyRelative("testSprite");

            EditorGUI.BeginProperty(position, label, property);

            var fieldRect = position;
            fieldRect.width /= 4;
            fieldRect.height = EditorGUIUtility.singleLineHeight;
            
            EditorGUI.PropertyField(fieldRect, testStr,GUIContent.none);
            fieldRect.x += fieldRect.width;
            fieldRect.width -= 10;
            EditorGUI.PropertyField(fieldRect, testStr,GUIContent.none);
            fieldRect.x += fieldRect.width + 10;
            EditorGUI.PropertyField(fieldRect, testSp, GUIContent.none);
            fieldRect.x += fieldRect.width + 20;
            EditorGUI.PropertyField(fieldRect, testSp, GUIContent.none);
            
            EditorGUI.EndProperty();
        }
    }
}
