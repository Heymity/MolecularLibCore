using System;
using MolecularLib.AutoAssign;
using UnityEngine;

namespace MolecularLib.Demo
{
    [UseAutoAssign]
    public class DemoAutoAssign : MonoBehaviour
    {
        [GetComponent] 
        private Rigidbody2D _rigidbody2D;

        [GetComponent] 
        private Rigidbody2D _rigidbody2DProp { get; set; }

        [ContextMenu("Test")]
        public void Test()
        {
            Debug.Log(_rigidbody2D.name);
            Debug.Log(_rigidbody2DProp.name);
        }

        private void Awake()
        {
            AutoAssignController.AutoAssign(this);
            GetComponent<Rigidbody2D>();
        }
    }
}