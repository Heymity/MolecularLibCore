using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MolecularLib
{
    public class VolatileScriptableObject<T> : ScriptableObject
    {
        [SerializeField] protected T editorSavedValue;

        [SerializeField] private T runtimeValue;
        protected T Value
        {
            get
            {
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
            runtimeValue = editorSavedValue;
        }
    }
}
