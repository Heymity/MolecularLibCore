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
using UnityEngine;

namespace MolecularLib
{
    /// <summary>
    /// Derive from this class to have a Current (Singleton) property to your class. The Singleton will be assigned used the FindObjectOfType&lt;T&gt; method in a lazy way (will only call the method when used and then cache it for later use)
    /// </summary>
    /// <typeparam name="T">Your MonoBehaviour derived singleton Type</typeparam>
    public class AutoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _current;
        public static T Current => _current ??= FindObjectOfType<T>() ?? CreateSingleton();

        private static T CreateSingleton()
        {
            var singleton = new GameObject(typeof(T).Name);
            _current = singleton.AddComponent<T>();
            return _current;
        }
    }
}