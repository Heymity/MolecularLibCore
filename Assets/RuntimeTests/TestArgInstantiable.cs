using UnityEngine;

namespace MolecularLib
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