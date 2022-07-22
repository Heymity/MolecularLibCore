using System.Text;
using MolecularLib.AutoAssign;
using UnityEngine;

namespace MolecularLib.Demo
{
    // Remember to add this attribute here on the class!
    [UseAutoAssign]
    public class DemoAutoAssign : AutoAssignMonoBehaviour
    {
        [GetComponent] private Rigidbody2D _rigidbody2D;
        [GetComponent] private Rigidbody2D _rigidbody2DProp { get; set; }

        [GetComponentInChildren] private Collider2D _collider;
        [GetComponentInChildren] private Collider2D _colliderProp { get; set; }

        [Find("DEMO DRAWERS")] private GameObject _drawers;
        [Find("DEMO DRAWERS")] private GameObject _drawersProp { get; set; }

        [FindWithTag("MainCamera")] private GameObject _camera;
        [FindWithTag("MainCamera")] private GameObject _cameraProp { get; set; }

        [FindGameObjectsWithTag("GameController")] private GameObject[] _gameControllers;
        [FindGameObjectsWithTag("GameController")] private GameObject[] _gameControllersProp { get; set; }

        [FindObjectOfType(typeof(DemoDrawersScript))] private DemoDrawersScript _drawersScript;
        [FindObjectOfType(typeof(DemoDrawersScript))] private DemoDrawersScript _drawersScriptProp { get; set; }

        [FindObjectsOfType(typeof(DemoDrawersScript))] private DemoDrawersScript[] _drawersScripts;
        [FindObjectsOfType(typeof(DemoDrawersScript))] private DemoDrawersScript[] _drawersScriptsProp { get; set; }

        /* If you can't derive from AutoAssignMonoBehaviour, you can just call the function below like that
        private void Awake()
        {
            this.AutoAssign();
        }*/
        
        
        
        [ContextMenu("Test")]
        public void Test()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Demo AutoAssign:");
            builder.AppendLine($"GetComponent: Field {_rigidbody2D} | Prop {_rigidbody2DProp}");
            builder.AppendLine($"GetComponentInChildren: Field {_collider} | Prop {_colliderProp}");
            builder.AppendLine($"Find: Field {_drawers} | Prop {_drawersProp}");
            builder.AppendLine($"FindWithTag: Field {_camera} | Prop {_cameraProp}");
            builder.AppendLine(
                $"FindGameObjectsWithTag: Field {_gameControllers} ({_gameControllers.Length}) | Prop {_gameControllersProp} ({_gameControllersProp.Length})");
            builder.AppendLine($"FindObjectOfType: Field {_drawersScript} | Prop {_drawersScriptProp}");
            builder.AppendLine(
                $"FindObjectsOfType: Field {_drawersScripts} ({_drawersScripts.Length}) | Prop {_drawersScriptsProp} ({_drawersScriptsProp.Length})");

            Debug.Log(builder.ToString());
        }
    }
}