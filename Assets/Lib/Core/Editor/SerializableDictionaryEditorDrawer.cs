using System.Collections;
using System.Collections.Generic;
using Molecular;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace MolecularEditor
{
    [CustomPropertyDrawer(typeof(SerializableDictionary<,>))]
    public class SerializableDictionaryEditorDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 40f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var keysProp = property.FindPropertyRelative("keys");
            var valuesProp = property.FindPropertyRelative("values");

            var reorderableList = new ReorderableList(property.serializedObject, keysProp);
            
            reorderableList.DoList(position);
        }
    }
}