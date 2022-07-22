using System;

namespace MolecularLib.AutoAssign
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class FindObjectsOfTypeAttribute : Attribute
    {
        public readonly Type Type;

        public FindObjectsOfTypeAttribute(Type type)
        {
            Type = type;
        }
    }
}