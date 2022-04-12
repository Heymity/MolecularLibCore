using System;
using MolecularLib.Helpers;
using UnityEngine;

namespace MolecularLib.PolymorphismSupport
{
    [Serializable]
    public class PolymorphicVariable<TBase> where TBase : class 
    {
        [SerializeField] private TypeVariable<TBase> selectedPolymorphicType;
        [SerializeField] private SerializedPolymorphicData polymorphicData;
        
        /*public static implicit operator TBase(PolymorphicVariable<TBase> variable)
        {
            return variable.GetValue();
        }*/
    }
}