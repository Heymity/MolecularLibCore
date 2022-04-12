using System;
using System.Collections.Generic;
using MolecularLib.Helpers;
using MolecularLib.PolymorphismSupport;
using UnityEngine;

namespace MolecularLib.Testing
{
    public class Tests : MonoBehaviour
    {
        [SerializeField] private Sprite sprite; 
        [SerializeField] private Tag tagTest;
        [Space]
        [SerializeField] private Range<double> doubleRange;
        [SerializeField] private Range<float> genericFloatRange;
        [SerializeField] private Range floatRange1;
        [SerializeField, MinMaxRange(-30.6345f, 24.34634f)] private Range floatRange;
        [SerializeField, MinMaxRange(-30, 20)] private RangeInteger intRange;
        [SerializeField] private RangeVector2 vec2Range;
        [SerializeField] private RangeVector3 vec3Range;
        [Space]
        [SerializeField, TypeVariableBaseType(typeof(MonoBehaviour))] private TypeVariable type;
        [Space]
        [SerializeField] private SerializableDictionary<string, int> stringToInt;
        [SerializeField] private SerializableDictionary<HideFlags, Color> flagsToColor;
        [SerializeField] private SerializableDictionary<string, TestStruct> myStructs;
        [SerializeField] private SerializableDictionary<TestStruct, string> myStructsOpposite;
        [SerializeField] private SerializableDictionary<TestStruct, TestStruct> myStructsBoth;
        [SerializeField] private SerializableDictionary<string, Sprite> stringToSprite;
        [Space]
        [SerializeField] private Optional<string> myOptionalString;
        [SerializeField] private Optional<List<string>> myList;
        [SerializeField] private Optional<SerializableDictionary<string, string>> myOptionalDictionary;
        //TODO Fix this: [SerializeField] private Optional<Range> myOptionalRange;
        [Space]
        [SerializeField] private PolymorphicVariable<Base> myPolymorphicVariable;

        /*[ContextMenu("Test1ArgInstantiate")]
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
        }*/
    }

    [Serializable]
    public struct TestStruct
    {
        public int MyInt;
        public bool MyBool;
        public List<string> MyStringList;
    }

    [Serializable]
    public class Base
    {
        public string myBaseString;
    }

    [Serializable]
    public class A : Base
    {
        public int aClassInt;
    }

    [Serializable]
    public class B : Base
    {
        public Range bClassRange;
    }

    [Serializable]
    public class C : B
    {
        public float cClassFloat;
    }
}