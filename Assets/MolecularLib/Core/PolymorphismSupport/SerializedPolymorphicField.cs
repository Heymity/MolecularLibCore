using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using MolecularLib.Helpers;
using UnityEngine;

namespace MolecularLib.PolymorphismSupport
{
    [Serializable]
    public class SerializedPolymorphicField : ISerializationCallbackReceiver
    {
        public string fieldName;
        public TypeVariable fieldType;
        [TextArea] public string serializedValue; 

        public object DeserializedValue { get; set; }

        public void OnBeforeSerialize()
        {
            var serializer = new XmlSerializer(fieldType.Type);
            using var writer = new StringWriter();
            serializer.Serialize(writer, DeserializedValue);
            
            serializedValue = writer.ToString();
        }

        public void OnAfterDeserialize()
        {
            try
            {
                var serializer = new XmlSerializer(fieldType.Type);
                using var reader = new StringReader(serializedValue);
                DeserializedValue = serializer.Deserialize(reader);
            }
            catch (ArgumentNullException)
            {
                DeserializedValue = null;
            }
        }
    }
}