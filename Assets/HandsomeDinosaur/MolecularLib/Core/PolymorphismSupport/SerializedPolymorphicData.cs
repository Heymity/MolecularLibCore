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
using System.Reflection;

namespace MolecularLib.PolymorphismSupport
{
    [Serializable]
    public class SerializedPolymorphicData
    {
        private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
        
        public List<SerializedPolymorphicField> fields;

        public SerializedPolymorphicData()
        {
            fields = new List<SerializedPolymorphicField>();
        }

        public T SetValuesTo<T>(T target, Type targetType)
        {
            foreach (var field in fields)
            {
                var fieldInfo = targetType.GetField(field.fieldName, Flags);
                if (fieldInfo == null)
                    continue;

                try
                {
                    fieldInfo.SetValue(target, field.DeserializedValue);
                }
                catch(ArgumentException){ /*This means the type of the field was changed, should not throw*/ }
            }

            return target;
        }
    }
}