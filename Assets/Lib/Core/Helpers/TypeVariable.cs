using System;
using System.Linq;
using UnityEngine;

namespace MolecularLib
{
    [Serializable]
    public class TypeVariable : ISerializationCallbackReceiver
    {
        [SerializeField] private string typeName;
        [SerializeField] private string assemblyName;

        private Type type;

        public Type Type { get => type; set => type = value; }

        public void OnAfterDeserialize()
        {
            if (string.IsNullOrEmpty(assemblyName) || string.IsNullOrEmpty(typeName)) return;
            Type = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(assembly => assembly.GetName().Name == assemblyName)?.GetType(typeName);
        }

        public void OnBeforeSerialize()
        {
            typeName = Type?.FullName;
            assemblyName = Type?.Assembly.GetName().Name;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class TypeVariableBaseTypeAttribute : PropertyAttribute
    {
        private readonly Type type;

        public TypeVariableBaseTypeAttribute(Type type)
        {
            this.type = type;
        }

        public Type Type => type;
    }
}