using System;

namespace MolecularLib.AutoAssign
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class FindObjectOfTypeAttribute : Attribute
    {
        public readonly Type Type; 
        
        public FindObjectOfTypeAttribute(Type type)
        {
            Type = type;
        }
    }
}