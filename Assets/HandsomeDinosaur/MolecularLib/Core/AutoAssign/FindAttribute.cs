using System;

namespace MolecularLib.AutoAssign
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class FindAttribute : Attribute
    {
        public readonly string Name;
        
        public FindAttribute(string name)
        {
            Name = name;
        }
    }
}