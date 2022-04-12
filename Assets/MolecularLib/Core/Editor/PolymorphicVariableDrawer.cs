using MolecularEditor;
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
            var typeProp = property.FindPropertyRelative("selectedPolymorphicType");

            EditorGUI.PropertyField(position, typeProp);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }
    }
}