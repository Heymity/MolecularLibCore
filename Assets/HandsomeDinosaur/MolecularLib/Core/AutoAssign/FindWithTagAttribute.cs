using System;

namespace MolecularLib.AutoAssign
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class FindWithTagAttribute : Attribute
    {
        public readonly string Tag;   
        
        public FindWithTagAttribute(string tag)
        {
            Tag = tag;
        }
    }
}