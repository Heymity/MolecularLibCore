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

namespace MolecularInternal
{
    public class Error
    {
        public static readonly Error NoError = null;

        public string ErrorMessage { get; }

        public bool Success => this == NoError;
        
        public Error(string errorMsg)
        {
            ErrorMessage = errorMsg;
        }

        public static implicit operator bool(Error e) => !e?.Success ?? false;
    }

    public static class ErrorExtensionMethods
    {
        public static bool Failed<T>(this (T, Error error) returnValue) => returnValue.error;

        public static bool Failed<T>(this (T value, Error error) returnValue, out T value, out Error err)
        {
            var (val, error) = returnValue;
            value = val;
            err = error;
            return error;
        }
        
        public static bool Failed<T>(this (T value, Error error) returnValue, out T value)
        {
            var (val, error) = returnValue;
            value = val;
            return error;
        }

        public static bool Failed<T1, T2>(this (T1, T2, Error error) returnValue) => returnValue.error;
        
        public static bool Failed<T1, T2>(this (T1 value1, T2 value2, Error error) returnValue, out T1 value1, out T2 value2)
        {
            var (val1, val2, error) = returnValue;
            value1 = val1;
            value2 = val2;
            return error;
        }
    }
}