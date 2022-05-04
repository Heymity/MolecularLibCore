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
using System.Timers;
using MolecularLib.Helpers;
using MolecularLib.PolymorphismSupport;
using UnityEngine;
using Timer = MolecularLib.Timers.Timer;

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
        [SerializeField] private Range floatRange;
        [SerializeField] private Range intRange;
        [SerializeField, MinMaxRange(-30.6345f, 24.34634f)] private Range minMaxFloatRange;
        [SerializeField, MinMaxRange(-30, 20)] private RangeInteger minMaxIntRange;
        [SerializeField] private RangeVector2 vec2Range;
        [SerializeField] private RangeVector3 vec3Range;
        [SerializeField] private RangeVector2Int vec2IntRange;
        [SerializeField] private RangeVector3Int vec3IntRange;
        [Space] 
        [Header("Serializable Dictionary examples")] 
        [SerializeField] private SerializableDictionary<string, int> stringToInt;
        [SerializeField] private SerializableDictionary<HideFlags, Color> flagsToColor;
        [SerializeField] private SerializableDictionary<string, TestStruct> myStructs;
        [SerializeField] private SerializableDictionary<TestStruct, string> myStructsOpposite;
        [SerializeField] private SerializableDictionary<TestStruct, TestStruct> myStructsBoth;
        [SerializeField] private SerializableDictionary<string, Sprite> stringToSprite;
        [Space] 
        [Header("Polymorphic variable examples")] 
        [SerializeField] private PolymorphicVariable<Base> myPolymorphicVariable;
        [Space] 
        [Header("Optional variable examples")] 
        [SerializeField] private Optional<string> myOptionalString;
        [SerializeField] private Optional<List<string>> myList;
        [SerializeField] private Optional<SerializableDictionary<string, string>> myOptionalDictionary;
        [SerializeField] private Optional<Range> myOptionalRange;
        
        public PolymorphicVariable<Base> PolymorphicVariable => myPolymorphicVariable;
        
// ReSharper disable Unity.InefficientPropertyAccess


        [ContextMenu("Demos")]
        public void Demos()
        {
            var timer = Timer.Create(5, () => Debug.Log("Timer finished"));
            Debug.Log(timer.ElapsedSeconds);
            
            
            Timer.TimerAsync(2, () => Debug.Log("TimerAsync finished"));

            
            var timerReference = Timer.TimerAsyncReference(3, repeat: true);
            timerReference.OnFinish += () => Debug.Log("TimerAsyncReference finished");
            
            floatRange.Random();

            var min = floatRange.Min;
            var max = floatRange.Max;

            var middle = floatRange.MidPoint;

            var patrolPos = vec3Range.Random();

            if (vec2Range.IsInRange(transform.position))
            {
                // Do something...
            }
                
            transform.position = transform.position.WithoutX().WithZ(1);

            var myVector = transform.position;
            var myTileMapPos = myVector.ToVec2Int();

            Color titleColor = "Hello World".ToColor();
            "This green string is in bold".Color(Color.green).Bold();
            "This string will be cut to fit the container".Ellipsis(30, "Label");
            
            var backgroundColor = ColorHelper.NormalizeToColor(28, 28, 28);
            var textColor = backgroundColor.TextForegroundColor();
            textColor.WithR(1f);
            var textColorHexString = textColor.ToHexString();
            
            if (myOptionalString.HasValue)
                Debug.Log(myOptionalString.Value);
            
            // Or simply (Using implicit operators)
            
            if (myOptionalString)
                Debug.Log(myOptionalString);
            
            if (myPolymorphicVariable.As<A>(out var asA))
                Debug.Log($"As A | aClassInt: {asA.aClassInt}");
            else if (myPolymorphicVariable.As<B>(out var asB))
                Debug.Log($"As B | bClassInt: {asB.bClassInt} | bClassRange: {asB.bClassRange.Min} - {asB.bClassRange.Max}");
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
        [SerializeField] private SerializableDictionary<string, int> aPrivateDictionary;
    }

    [Serializable]
    public class B : Base
    {
        public Range bClassRange;
        public int bClassInt;
    }

    [Serializable]
    public class C : B
    {
        public float cClassFloat;
        // Types derived from UnityEngine.Object aren't working yet, but in future versions it will; public TestVolatileScriptableObject cClassScriptableObject;
    }
}
