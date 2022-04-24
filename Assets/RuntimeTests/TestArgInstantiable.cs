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

using MolecularLib;
using UnityEngine;

namespace MolecularLibTests
{
    public class TestArgInstantiable : MonoBehaviour, IArgsInstantiable<bool>, IArgsInstantiable<float, int, string, GameObject>
    {
        public bool boolArg;
        public float floatArg;
        public int intArg;
        public string stringArg;
        public GameObject gameObjectArg;
        
        public void Initialize(bool arg1)
        {
            Debug.Log($"I was instantiated with args: args1: {arg1}");
            boolArg = arg1;
        }

        public void Initialize(float arg1, int arg2, string arg3, GameObject arg4)
        {
            Debug.Log($"I was instantiated with args: args1: {arg1} args2: {arg2} args3: {arg3} args4: {arg4}");
            floatArg = arg1;
            intArg = arg2;
            stringArg = arg3;
            gameObjectArg = arg4;
        }
    }
}