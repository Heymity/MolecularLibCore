using MolecularLib.PolymorphismSupport;
using UnityEditor;
using UnityEngine;

namespace MolecularLib.Core.Editor
{
    [CustomPropertyDrawer(typeof(PolymorphicVariable<>))]
    public class PolymorphicVariableDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();
            
            var typeProp = property.FindPropertyRelative("selectedPolymorphicType");

            var foldoutRect = position;
            foldoutRect.width = EditorGUIUtility.labelWidth;
            property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(foldoutRect, property.isExpanded, "");
            
            var typeSelectionRect = EditorGUI.PrefixLabel(position, label);
            typeSelectionRect.height = EditorGUIUtility.singleLineHeight;
            
            EditorGUIUtility.labelWidth = 90;
            EditorGUI.PropertyField(typeSelectionRect, typeProp, new GUIContent("Instance Type"));
            
            EditorGUI.EndFoldoutHeaderGroup();
            
            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(property.serializedObject.targetObject);
            }
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return property.isExpanded ? 400 : base.GetPropertyHeight(property, label);
        }
    }
}