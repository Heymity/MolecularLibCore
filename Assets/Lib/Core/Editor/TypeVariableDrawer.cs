using System.Linq;
using MolecularLib;
using UnityEditor;
using UnityEngine;

namespace MolecularEditor
{
    [CustomPropertyDrawer(typeof(TypeVariable))]
    public class TypeVariableDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.serializedObject.Update();

            var typeVar = this.GetSerializedValue<TypeVariable>(property);

            EditorGUI.BeginChangeCheck();

            var typeAtt = fieldInfo.GetCustomAttributes(typeof(TypeVariableBaseTypeAttribute), true);
            if (typeAtt.Any())
            {
                var baseType = (typeAtt.FirstOrDefault() as TypeVariableBaseTypeAttribute)?.Type;
                typeVar.Type = EditorHelper.TypeField(position, label.text, typeVar.Type, baseType);
            }
            else
            {
                typeVar.Type = EditorHelper.TypeField<MonoBehaviour>(position, label.text, typeVar.Type);
            }

            if (!EditorGUI.EndChangeCheck()) return;
            
            property.serializedObject.ApplyModifiedProperties();

            EditorUtility.SetDirty(property.serializedObject.targetObject);
        }
    }
}