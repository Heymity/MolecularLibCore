using System;
using System.Collections.Generic;
using MolecularLib.Helpers;
using UnityEngine;

namespace MolecularLib.Demo
{
    [CreateAssetMenu(fileName = "Volatile SO", menuName = "New Volatile SO", order = 0)]
    public class TestVolatileScriptableObject : VolatileScriptableObject<TestVolatileScriptableObject.Data>
    {
        public Data VolatileData
        {
            get => Value;
            set => Value = value;
        }
        
        public static implicit operator Data(TestVolatileScriptableObject obj)
        {
            return obj.VolatileData;
        }
        
        [Serializable]
        public class Data
        { 
            public MonoBehaviour myBehaviour;
            public int myInt;
            public float myFloat;
            public string myString;
            public List<string> myList;
            public Optional<SerializableDictionary<int, string>> myOptionalDictionary;
            public ScriptableObject myScriptableObject;
        } 
    }
}