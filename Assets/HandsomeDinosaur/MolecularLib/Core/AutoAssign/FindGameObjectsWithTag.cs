using System;

namespace MolecularLib.AutoAssign
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class FindGameObjectsWithTag : Attribute
    {
        public readonly string Tag;
        
        public FindGameObjectsWithTag(string tag)
        {
            Tag = tag;
        }
    }
}