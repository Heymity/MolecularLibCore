using System;
using MolecularLib.Helpers;
using UnityEngine;

namespace MolecularLib.PolymorphismSupport
{
    [Serializable]
    public class PolymorphicVariable<TBase>
    {
        [SerializeField] private TypeVariable<TBase> selectedPolymorphicType;
        [SerializeField] private SerializedPolymorphicData polymorphicData;
    }
}