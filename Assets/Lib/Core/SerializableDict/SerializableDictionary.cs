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

        public void OnBeforeSerialize()
        {
            keys ??= new List<TKey>();
            values ??= new List<TValue>();
            
            if (keys.Count == Keys.Count && values.Count == Keys.Count)
            {
                for (var i = 0; i < keys.Count; i++)
                {
                    if (Equals(keys[i], Keys.ElementAt(i)) && Equals(values[i], Values.ElementAt(i))) continue;
                    
                    keys = Keys.ToList();
                    values = Values.ToList();
                    break;
                }
            }
            
            Clear();
        }

        public void OnAfterDeserialize()
        {
            Clear();

            Debug.Log($"AfterDeserialize {keys.Count} {values.Count}");
            if (keys.Count != values.Count)
            {
                Debug.LogWarning("The key and value array sizes are not the same");
                return;
            }
            for (var i = 0; i < keys.Count; i++)
            {
                Add(keys[i], values[i]);
            }
        }
    }
}
