using System;
using System.Linq;
using System.Reflection;
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
            
            var typeNameProp = property.FindPropertyRelative("typeName");
            var assemblyNameProp = property.FindPropertyRelative("assemblyName");

            EditorGUI.BeginChangeCheck();
            
            var type = GetType(assemblyNameProp.stringValue, typeNameProp.stringValue);
            
            Type selectedType;
            var typeAtt = fieldInfo.GetCustomAttributes(typeof(TypeVariableBaseTypeAttribute), true);
            if (typeAtt.Any())
            {
                var baseType = (typeAtt.FirstOrDefault() as TypeVariableBaseTypeAttribute)?.Type;

                selectedType = EditorHelper.TypeField(position, label.text, type, baseType);
            }
            else
            {
                selectedType = EditorHelper.TypeField<MonoBehaviour>(position, label.text, type);
            }

            assemblyNameProp.stringValue = selectedType.Assembly.GetName().Name;
            typeNameProp.stringValue = selectedType.FullName;
            
            if (!EditorGUI.EndChangeCheck()) return;

            property.serializedObject.ApplyModifiedProperties();

            EditorUtility.SetDirty(property.serializedObject.targetObject);
        }

        private Assembly _cachedAssembly;
        private Type _cachedType;
        private string _lastAssemblyName;
        private string _lastTypeName;
        private Type GetType(string assemblyName, string typeName)
        {
            if (assemblyName != "" && assemblyName == _lastAssemblyName)
            {
                //Debug.Log("Cached");
                if (_lastTypeName != "" && _lastTypeName == typeName)
                {
                    return _cachedType;
                }

                var type = _cachedAssembly.GetType(typeName);
                if (type != null)
                {
                    _cachedType = type;
                    _lastTypeName = typeName;
                    return type;
                }
            }
            //Debug.Log($"Not Cached | {_lastAssemblyName} | {_lastTypeName} | {assemblyName} | {typeName}");
            _lastAssemblyName = assemblyName;
            _lastTypeName = typeName;

            if (!TypeLibrary.AllAssemblies!.TryGetValue(assemblyName, out var assembly))
                return TypeLibrary.AllAssemblies.FirstOrDefault().Value.GetType(typeName) ??
                       TypeLibrary.AllAssemblies.FirstOrDefault().Value.GetTypes().FirstOrDefault();
            
            _cachedAssembly = assembly;
            _cachedType = assembly.GetType(typeName);
            return _cachedType;

        }
    }
}