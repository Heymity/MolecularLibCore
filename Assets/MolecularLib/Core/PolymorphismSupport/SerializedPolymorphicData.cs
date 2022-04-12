using System;
using System.Collections.Generic;
using UnityEngine;

namespace MolecularLib.PolymorphismSupport
{
    [Serializable]
    public class SerializedPolymorphicData : ISerializationCallbackReceiver
    {
        public List<SerializedPolymorphicField> Fields { get; private set; }
        [SerializeField] private string serializedData;

        public void OnBeforeSerialize()
        {
            serializedData = JsonUtility.ToJson(Fields);
        }

        public void OnAfterDeserialize()
        {
            Fields = JsonUtility.FromJson<List<SerializedPolymorphicField>>(serializedData);
        }
    }
}