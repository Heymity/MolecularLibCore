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

using MolecularLib.Helpers;
using UnityEngine;

namespace MolecularLib
{
    public class VolatileScriptableObject<TData> : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] protected TData runtimeValue;
        
        [SerializeField] protected TData editorSavedValue;
        protected TData Value
        {
            get
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                    return editorSavedValue;
#endif
                return runtimeValue;
            }
            set
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                    editorSavedValue = value;
#endif
                runtimeValue = value;
            }
        }

        private void OnValidate()
        {
            if (!Application.isPlaying)
                runtimeValue = DeepCopy(editorSavedValue);
        }

        public void OnBeforeSerialize()
        {
            
        }

        protected void CopyRuntimeValuesToEditorSaved()
        {
            editorSavedValue = DeepCopy(runtimeValue);
        }

        public void OnAfterDeserialize()
        {
            if (PlayStatus.IsPlaying) return;
            
            runtimeValue = DeepCopy(editorSavedValue);
        }
        
        private static TData DeepCopy(TData objToCopy)
        {
            var json = JsonUtility.ToJson(objToCopy);
            var obj = JsonUtility.FromJson<TData>(json);
            return obj;
        }
    }
}
