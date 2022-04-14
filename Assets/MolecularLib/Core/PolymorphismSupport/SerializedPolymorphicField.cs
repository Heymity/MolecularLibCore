using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using MolecularLib.Helpers;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

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
            if (DeserializedValue is null) return;
            
            var valueToSerialize = DeserializedValue;

            fieldType.Type = valueToSerialize.GetType();
            
            var serializer = new XmlSerializer(fieldType.Type);
            using var writer = new StringWriter();
            serializer.Serialize(writer, valueToSerialize!);
            
            serializedValue = writer.ToString();
        }

        public void OnAfterDeserialize()
        {
            try
            {
                if (string.IsNullOrEmpty(serializedValue))
                {
                    DeserializedValue = null;
                    return;
                }
                
                var serializer = new XmlSerializer(fieldType.Type);
                using var reader = new StringReader(serializedValue);
                var deserialized = serializer.Deserialize(reader);

                DeserializedValue = deserialized;
            }
            catch (ArgumentNullException)
            {
                DeserializedValue = null;
            }
        }
    }
}