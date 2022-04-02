using MolecularLib.Helpers;
using UnityEditor;
using UnityEngine;

namespace MolecularLib
{
    [CustomPropertyDrawer(typeof(Optional<>))]
    public class OptionalPropertyDrawer : PropertyDrawer
    {
        private const float ToggleSize = 18f;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            property.serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            
            var valueProperty = property.FindPropertyRelative("value");
            var useValueProperty = property.FindPropertyRelative("useValue");
            
            EditorGUI.BeginDisabledGroup(!useValueProperty.boolValue);
            var valueRect = new Rect(position.x, position.y, position.width - ToggleSize - 3f, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(valueRect, valueProperty, label);
            EditorGUI.EndDisabledGroup();
            
            var useValueRect = new Rect(valueRect.xMax + 3f, position.y, ToggleSize, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(useValueRect, useValueProperty, GUIContent.none);

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();
            
            EditorGUI.EndProperty();
        }
    }
}