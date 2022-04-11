using System;
using System.Collections.Generic;
using MolecularLib.Helpers;
using UnityEngine;

namespace MolecularLib.Testing
{
    [CreateAssetMenu(fileName = "Volatile SO", menuName = "New Volatile SO", order = 0)]
    public class TestVolatileScriptableObject : VolatileScriptableObject<TestVolatileScriptableObject.Data>
    {
        [Serializable]
        public struct Data
        { 
            public MonoBehaviour myBehaviour;
            public int myInt;
            public float myFloat;
            public List<string> myList;
            public Optional<SerializableDictionary<int, string>> myOptionalDictionary;
            public ScriptableObject myScriptableObject;
        } 
    }
}