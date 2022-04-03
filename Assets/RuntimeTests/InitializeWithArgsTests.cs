using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using MolecularLib;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InitializeWithArgsTests
{
    [UnityTest]
    public IEnumerator InitializeWithArgsPositionAndRotation1Arg()
    {
        if (!Application.isPlaying) yield break;
        
        var prefab = Resources.Load<TestArgInstantiable>("InstantiateTest");
        var posRotBoolTrue = Molecular.Instantiate(prefab, new Vector3(2, 3, 5), Quaternion.identity, true);

        Assert.AreEqual(new Vector3(2, 3, 5), posRotBoolTrue.transform.position);
        Assert.AreEqual(Quaternion.identity, posRotBoolTrue.transform.rotation);
        Assert.AreEqual(null, posRotBoolTrue.transform.parent);
        
        Assert.AreEqual(true, posRotBoolTrue.boolArg);
        Assert.AreEqual(default(float), posRotBoolTrue.floatArg);
        Assert.AreEqual(default(int), posRotBoolTrue.intArg);
        Assert.IsTrue(string.IsNullOrEmpty(posRotBoolTrue.stringArg));
        Assert.AreEqual(null, posRotBoolTrue.gameObjectArg);
    }

    [UnityTest]
    public IEnumerator InitializeWithArgsParent1Arg()
    {
        if (!Application.isPlaying) yield break;
        
        var prefab = Resources.Load<TestArgInstantiable>("InstantiateTest");

        var parentGo = new GameObject("TEST_ParentGo");
        
        var posBoolTrueTransform = Molecular.Instantiate(prefab, parentGo.transform, true);

        Assert.AreEqual(parentGo.name, posBoolTrueTransform.transform.parent.name);
        Assert.AreEqual(true, posBoolTrueTransform.boolArg);    
        
        Assert.AreEqual(default(float), posBoolTrueTransform.floatArg);
        Assert.AreEqual(default(int), posBoolTrueTransform.intArg);
        Assert.IsTrue(string.IsNullOrEmpty(posBoolTrueTransform.stringArg));
        Assert.AreEqual(null, posBoolTrueTransform.gameObjectArg);
        
        yield return new WaitForSeconds(5f);
    }
    
    [UnityTest]
    public IEnumerator InitializeWithArgs4Args()
    {
        if (!Application.isPlaying) yield break;
        
        var prefab = Resources.Load<TestArgInstantiable>("InstantiateTest");

        var targetGo = new GameObject("TEST_TargetGo");

        var floatIntStringGo = Molecular.Instantiate(prefab, 23.3f, 342, "I am a string", targetGo);

        Assert.AreEqual(false, floatIntStringGo.boolArg);
        Assert.AreEqual(targetGo, floatIntStringGo.gameObjectArg);
        Assert.AreEqual(23.3f, floatIntStringGo.floatArg);
        Assert.AreEqual(342, floatIntStringGo.intArg);
        Assert.AreEqual("I am a string", floatIntStringGo.stringArg);

        yield return null;
    }
}
