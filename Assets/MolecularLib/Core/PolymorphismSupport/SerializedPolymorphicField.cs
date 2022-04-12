using System;
using MolecularLib.Helpers;

namespace MolecularLib.PolymorphismSupport
{
    [Serializable]
    public class SerializedPolymorphicField
    {
        public string name;
        public TypeVariable fieldType;
    }
}