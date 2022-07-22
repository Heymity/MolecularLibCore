using System;

namespace MolecularLib.AutoAssign
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class GetComponentAttribute : Attribute
    {
        //public readonly AutoAssignAt OverrideAssignMoment; 
        
        public GetComponentAttribute(/*AutoAssignAt overrideAssignMoment = AutoAssignAt.None*/)
        {
            //OverrideAssignMoment = overrideAssignMoment;
        }
    }
}