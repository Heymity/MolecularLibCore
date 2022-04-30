﻿/*  Copyright 2022 Gabriel Pasquale Rodrigues Scavone
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
using MolecularLib.Helpers;
using UnityEngine;

namespace MolecularLib.Demo
{
    [CreateAssetMenu(fileName = "Volatile SO", menuName = "New Volatile SO", order = 0)]
    public class TestVolatileScriptableObject : VolatileScriptableObject<TestVolatileScriptableObject.Data>
    {
        // Here is a quick way of accessing the data. Can be done in other ways too.
        public Data VolatileData
        {
            get => Value;
            set => Value = value;
        }
        
        // Can be done like this too.
        public string MyString
        {
            get => Value.myString;
            set => Value.myString = value;
        }
        public int MyInt
        {
            get => Value.myInt;
            set => Value.myInt = value;
        }
        // ...etc...
        
        public static implicit operator Data(TestVolatileScriptableObject obj)
        {
            return obj.VolatileData;
        }
        
        // Here you will put all the data you want to be volatile
        [Serializable]
        public class Data
        { 
            [TextArea] public string myString;
            public MonoBehaviour myBehaviour;
            public int myInt;
            public float myFloat;
            public List<string> myList;
            public Optional<SerializableDictionary<int, string>> myOptionalDictionary;
            public ScriptableObject myScriptableObject;
        } 
    }
}