using System.Collections;
using System.Collections.Generic;
using MolecularLib.AutoAssign;
using MolecularLib.Demo;
using UnityEngine;
using UnityEngine.Serialization;

public class TestAutoAssign : AutoAssignMonoBehaviour
{
    [GetComponent] public Rigidbody2D Rigidbody2D;
    [GetComponent] public Rigidbody2D Rigidbody2DProp { get; set; }

    [GetComponents(typeof(BoxCollider2D))] public List<Collider2D> Colliders;
    [GetComponents(typeof(BoxCollider2D))] public List<Collider2D> CollidersProp { get; set; }
        
    [GetComponentInChildren] public Collider2D Collider;
    [GetComponentInChildren] public Collider2D ColliderProp { get; set; }
        
    [GetComponentsInChildren] public List<Transform> TransformList;
    [GetComponentsInChildren] public List<Transform> TransformListProp { get; set; }
        
    [Find("AutoAssign")] public GameObject AutoAssignGo;
    [Find("AutoAssign")] public GameObject AutoAssignGoProp { get; set; }
        
    [FindWithTag("Player")] public GameObject Obj;
    [FindWithTag("Player")] public GameObject ObjProp { get; set; }
        
    [FindGameObjectsWithTag("GameController")] public GameObject[] GameControllers;
    [FindGameObjectsWithTag("GameController")] public GameObject[] GameControllersProp { get; set; }
    
    [FindObjectOfType(typeof(TestAutoAssign))] public TestAutoAssign FindAutoAssign;
    [FindObjectOfType(typeof(TestAutoAssign))] public TestAutoAssign FindAutoAssignProp { get; set; }
        
    [FindObjectsOfType(typeof(TestAutoAssign))] public TestAutoAssign[] FindAutoAssigns;
    [FindObjectsOfType(typeof(TestAutoAssign))] public TestAutoAssign[] FindAutoAssignsProp { get; set; }
        
    [LoadResource("AutoAssign")] public GameObject GameObject;
    [LoadResource("AutoAssign")] public GameObject GameObjectProp { get; set; }
}
