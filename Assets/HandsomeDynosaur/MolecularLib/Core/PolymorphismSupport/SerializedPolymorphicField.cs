/*  Copyright 2022 Gabriel Pasquale Rodrigues Scavone
*
*  Licensed under the Apache License, Version 2.0 (the "License");
*  you may not use this file except in compliance with the License.
*  You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
*  Unless required by applicable law or agreed to in writing, software
*  distributed under the License is distributed on an "AS IS" BASIS,
*  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
*  See the License for the specific language governing permissions and
*  limitations under the License.
*/

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