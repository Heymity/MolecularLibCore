using UnityEngine;

namespace MolecularLib.AutoAssign
{
    public class AutoAssignMonoBehaviour : MonoBehaviour
    {
        protected void Awake()
        {
            this.AutoAssign();
        }
    }
}