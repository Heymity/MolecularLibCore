using System;
using System.Collections.Generic;
using System.Linq;
using Molecular;
using UnityEngine;

namespace MolecularLib.Testing
{
    public class Tests : MonoBehaviour
    {
        //TODO somewhere in the editor (including OnBefore and After (De)Serialize) code of these vars there is a HUGE performance hog.
        [SerializeField] private Tag tagTest;

        [SerializeField] private Range<double> doubleRange;
        [SerializeField] private Range<float> genericFloatRange;
        [SerializeField, MinMaxRange(-30.6345f, 24.34634f)] private Range floatRange;
        [SerializeField, MinMaxRange(-30, 20)] private RangeInteger intRange;
        [SerializeField] private RangeVector2 vec2Range;
        [SerializeField] private RangeVector3 vec3Range;

        [SerializeField, TypeVariableBaseType(typeof(MonoBehaviour))] private TypeVariable type;

        [SerializeField] private TestArgInstantiable prefab;

        [SerializeField] private SerializableDictionary<string, int> stringToInt;
        [SerializeField] private SerializableDictionary<HideFlags, Color> flagsToColor;
        [SerializeField] private SerializableDictionary<string, TestStruct> myStructs;
        [SerializeField] private SerializableDictionary<TestStruct, string> myStructsOpposite;
        [SerializeField] private SerializableDictionary<TestStruct, TestStruct> myStructsBoth;

        [ContextMenu("Test1ArgInstantiate")]
        private void Test1ArgInstantiate()
        {
            Molecular.Instantiate(prefab, false);
        }
        
        [ContextMenu("Test4ArgInstantiate")]
        private void Test4ArgInstantiate()
        {
            Molecular.Instantiate(prefab, 4.5f, 3, "Hi I am a string", gameObject);
        }

        private void Start()
        {
            stringToInt.Add("Hello World", 234);
            stringToInt.Select(kvp => kvp.Key + ": " + kvp.Value).ToList().ForEach(Debug.Log);
        }
    }

    [Serializable]
    public struct TestStruct
    {
        public int MyInt;
        public bool MyBool;
    }
    
    public struct TestStruct2
    {
        public int MyInt;
        public bool MyBool;
    }
}