using System;
using System.IO;
using System.Xml.Serialization;
using MolecularLib.Helpers;
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

        private static string SerializeData(Type fieldType, object value)
        {
            using var writer = new StringWriter();
            if (value is IPolymorphicSerializationOverride inter)
            {
                inter.Serialize(writer);
                return writer.ToString();
            }
            
            var serializer = new XmlSerializer(fieldType);
            serializer.Serialize(writer, value!);

            return writer.ToString();
        }
        
        private static object DeserializeData(Type fieldType, string serializedValue)
        {
            var inter = fieldType.GetInterface("IPolymorphicSerializationOverride");
            if (inter != null)
            {
                var deserialized = Activator.CreateInstance(fieldType);
                (deserialized as IPolymorphicSerializationOverride).Deserialize(serializedValue);
                return deserialized;
            }
            
            using var reader = new StringReader(serializedValue);
            var serializer = new XmlSerializer(fieldType);
            return serializer.Deserialize(reader);
        }
        
        public void OnBeforeSerialize()
        {
            if (DeserializedValue is null) return;

            var valueToSerialize = DeserializedValue;
            
            fieldType.Type = valueToSerialize.GetType();

            serializedValue = SerializeData(fieldType.Type, valueToSerialize!);
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
                
                DeserializedValue = DeserializeData(fieldType.Type, serializedValue);
            }
            catch (ArgumentNullException)
            {
                DeserializedValue = null;
            }
        }
        
        private static Object FindObjectFromInstanceID(int iid)
        {
            return (Object)typeof(Object)
                .GetMethod("FindObjectFromInstanceID", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                .Invoke(null, new object[] { iid });
        }
    }
}
