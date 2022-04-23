using System;
using System.Collections.Generic;
using MolecularLib.Helpers;
using MolecularLib.PolymorphismSupport;
using MolecularLib.Testing;
using UnityEngine;

namespace MolecularLib.Demo
{
    public class DemoDrawersScript : MonoBehaviour
    {
        [Header("Sprite, Tag and Type variable editor drawers")] 
        [SerializeField] private Sprite sprite;
        [SerializeField] private Tag tagTest;
        [SerializeField, TypeVariableBaseType(typeof(MonoBehaviour))] private TypeVariable type;
        [Space] 
        [Header("Range variables and editor drawers")] 
        [SerializeField] private Range<double> doubleRange;
        [SerializeField] private Range<float> genericFloatRange;
        [SerializeField] private Range floatRange1;
        [SerializeField, MinMaxRange(-30.6345f, 24.34634f)] private Range floatRange;
        [SerializeField, MinMaxRange(-30, 20)] private RangeInteger intRange;
        [SerializeField] private RangeVector2 vec2Range;
        [SerializeField] private RangeVector3 vec3Range;
        [Space] 
        [Header("Serializable Dictionary examples")] 
        [SerializeField] private SerializableDictionary<string, int> stringToInt;
        [SerializeField] private SerializableDictionary<HideFlags, Color> flagsToColor;
        [SerializeField] private SerializableDictionary<string, TestStruct> myStructs;
        [SerializeField] private SerializableDictionary<TestStruct, string> myStructsOpposite;
        [SerializeField] private SerializableDictionary<TestStruct, TestStruct> myStructsBoth;
        //TODO this is not working
        [SerializeField] private SerializableDictionary<string, Sprite> stringToSprite;
        [Space] 
        [Header("Optional variable examples")] 
        [SerializeField] private Optional<string> myOptionalString;
        [SerializeField] private Optional<List<string>> myList;
        [SerializeField] private Optional<SerializableDictionary<string, string>> myOptionalDictionary;
        //TODO Fix this: [SerializeField] private Optional<Range> myOptionalRange;
        [Space] 
        [Header("Polymorphic variable examples")] 
        [SerializeField] private PolymorphicVariable<Base> myPolymorphicVariable;

        public PolymorphicVariable<Base> PolymorphicVariable => myPolymorphicVariable;

        [ContextMenu("Test Polymorphic Variable")]
        public void TestPoly()
        {
            if (myPolymorphicVariable.As<A>(out var asA))
                Debug.Log($"As A | aClassInt: {asA.aClassInt}");
            else if (myPolymorphicVariable.As<B>(out var asB))
                Debug.Log($"As B | bClassInt: {asB.bClassInt}");
            else if (myPolymorphicVariable.As<C>(out var asC))
                Debug.Log($"As C | cClassFloat: {asC.cClassFloat}");
            else
                Debug.Log($"As Base | myBaseString: {myPolymorphicVariable.Value.myBaseString}");

            myPolymorphicVariable.Value.myBaseString = "Hey, I changed it!";
        }
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
        //public Range bClassRange;
        public int bClassInt;
    }

    [Serializable]
    public class C : B
    {
        public float cClassFloat;
        //public TestVolatileScriptableObject cClassScriptableObject;
    }
}