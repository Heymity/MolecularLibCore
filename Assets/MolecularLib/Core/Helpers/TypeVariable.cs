using System;
using System.Linq;
using UnityEngine;

namespace MolecularLib.Helpers
{
    [Serializable]
    public class TypeVariable : ISerializationCallbackReceiver
    {
        [SerializeField] private string typeName;
        [SerializeField] private string assemblyName;

        private Type _type;

        public Type Type { get => _type; set => _type = value; }

        public void OnAfterDeserialize()
        {
            if (string.IsNullOrEmpty(assemblyName) || string.IsNullOrEmpty(typeName)) return;
#if UNITY_EDITOR
            if (TypeLibrary.AllAssemblies == null || TypeLibrary.AllAssemblies.Count == 0)
            {
                TypeLibrary.BootstrapEditor();
            }
#endif
            if (TypeLibrary.AllAssemblies != null && TypeLibrary.AllAssemblies.TryGetValue(assemblyName, out var assembly))
            {
                Type = assembly.GetType(typeName);
                return;
            }

            assembly = TypeLibrary.AllAssemblies?.First().Value;
            Type = assembly?.GetType(typeName);
        }

        public void OnBeforeSerialize()
        {
            typeName = Type?.FullName;
            assemblyName = Type?.Assembly.GetName().Name;
        }
    }

    [Serializable]
    public class TypeVariable<TBase> : TypeVariable
    {
        
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class TypeVariableBaseTypeAttribute : PropertyAttribute
    {
        public TypeVariableBaseTypeAttribute(Type type)
        {
            Type = type;
        }

        public Type Type { get; }
    }
}