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

namespace MolecularLib.Helpers
{
    [Serializable]
    public class Optional<T>
    {
        [SerializeField] private T value;
        [SerializeField] private bool useValue;

        public Optional(T value, bool useValue)
        {
            Value = value;
            UseValue = useValue;
        }

        public T Value
        {
            get => value;
            set => this.value = value;
        }

        public bool UseValue
        {
            get => useValue;
            set => useValue = value;
        }

        public bool HasValue => Value != null;

        public static implicit operator Optional<T>(T value) => new Optional<T>(value, true);

        public static implicit operator T(Optional<T> optional) => optional.Value;
        public static implicit operator bool(Optional<T> optional) => optional.UseValue;
    }
}