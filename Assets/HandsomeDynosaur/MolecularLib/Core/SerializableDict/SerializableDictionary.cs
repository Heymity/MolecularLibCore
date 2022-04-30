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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using MolecularLib.PolymorphismSupport;
using UnityEngine;

namespace MolecularLib
{
    [System.Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver, IPolymorphicSerializationOverride
    {
        [SerializeField] private List<TKey> keys;
        [SerializeField] private List<TValue> values;

        public new void Add(TKey key, TValue value)
        {
            base.Add(key, value);
            
            values.Add(value);
            keys.Add(key);
        }
        
        public new void Remove(TKey key)
        {
            base.Remove(key);
            
            values.RemoveAt(keys.IndexOf(key));
            keys.Remove(key);
        }

        public new void Clear() 
        {
            base.Clear();
            
            keys.Clear();
            values.Clear();
        }
        
        public void OnBeforeSerialize()
        {
            keys ??= new List<TKey>();
            values ??= new List<TValue>();

            if (keys.Count != Keys.Count || values.Count != Keys.Count) return;
            for (var i = 0; i < keys.Count; i++)
            {
                if (Equals(keys[i], Keys.ElementAt(i)) && Equals(values[i], Values.ElementAt(i))) continue;
                    
                keys = Keys.ToList();
                values = Values.ToList();
                break;
            }
        }
        
        public void OnAfterDeserialize()
        {
            base.Clear();

            keys ??= new List<TKey>();
            values ??= new List<TValue>();
            
            if (keys.Count != values.Count)
            {
                Debug.LogWarning("The key and value array sizes are not the same");
                return;
            }
            for (var i = 0; i < keys.Count; i++)
            {
                if (ContainsKey(keys[i]))
                    continue;
                
                base.Add(keys[i], values[i]);
            }
        }

        public object Deserialize(string reader) { return JsonUtility.FromJson<SerializableDictionary<TKey, TValue>>(reader); }

        public void Serialize(StringWriter writer) { writer.Write(JsonUtility.ToJson(this)); }
    }
}
