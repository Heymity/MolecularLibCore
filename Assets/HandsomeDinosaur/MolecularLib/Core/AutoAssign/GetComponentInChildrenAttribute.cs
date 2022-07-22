using System;

namespace MolecularLib.AutoAssign
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class GetComponentInChildrenAttribute : Attribute
    {
        public GetComponentInChildrenAttribute()
        {
            
        }
    }
}