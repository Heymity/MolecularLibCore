using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

namespace MolecularLib.PolymorphismSupport
{
    [Serializable]
    public class SerializedPolymorphicData
    {
        public List<SerializedPolymorphicField> fields;

        public SerializedPolymorphicData()
        {
            fields = new List<SerializedPolymorphicField>();
        }
    }
}