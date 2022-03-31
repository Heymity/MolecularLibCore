using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Molecular
{
    [System.Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
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
    }
}
