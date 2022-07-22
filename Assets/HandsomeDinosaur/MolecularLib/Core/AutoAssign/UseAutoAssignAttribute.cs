using System;

namespace MolecularLib.AutoAssign
{
    public class UseAutoAssignAttribute : Attribute
    {
        //public readonly AutoAssignAt DefaultAutoAssignMoment;
        
        public UseAutoAssignAttribute(/*AutoAssignAt defaultAutoAssignMoment = AutoAssignAt.Awake*/)
        {
            //this.DefaultAutoAssignMoment = defaultAutoAssignMoment;
        }
    }
}