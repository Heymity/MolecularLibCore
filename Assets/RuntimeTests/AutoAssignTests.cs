/*  Copyright 2022 Gabriel Pasquale Rodrigues Scavone
*
*  Licensed under the Apache License, Version 2.0 (the "License");
*  you may not use this file except in compliance with the License.
*  You may obtain taa copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
*  Unless required by applicable law or agreed to in writing, software
*  distributed under the License is distributed on an "AS IS" BASIS,
*  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
*  See the License for the specific language governing permissions and
*  limitations under the License.
*/

using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MolecularLibTests
{
    public class AutoAssignTests
    {
        private TestAutoAssign taa;
        private TestAutoAssign prefab;

        [UnityTest]
        public IEnumerator GetComponentTest()
        {
            if (!Application.isPlaying) yield break;

            var rb = taa.GetComponent<Rigidbody2D>();

            Assert.AreEqual(rb, taa.Rigidbody2D);
            Assert.AreEqual(rb, taa.Rigidbody2DProp); 
            
            yield return null;
        }

        [UnityTest]
        public IEnumerator GetComponentsTest()
        {
            if (!Application.isPlaying) yield break;

            var colliders = taa.GetComponents<BoxCollider2D>().Select(b => (Collider2D)b).ToList();

            Assert.AreEqual(colliders, taa.Colliders);
            Assert.AreEqual(colliders, taa.CollidersProp);

            yield return null;
        }

        [UnityTest]
        public IEnumerator GetComponentInChild()
        {
            if (!Application.isPlaying) yield break;

            var coll2d = taa.GetComponentInChildren<Collider2D>();

            Assert.AreEqual(coll2d, taa.Collider);
            Assert.AreEqual(coll2d, taa.ColliderProp);

            yield return null;
        }

        [UnityTest]
        public IEnumerator GetComponentsInChild()
        {
            if (!Application.isPlaying) yield break;

            var transforms = taa.GetComponentsInChildren<Transform>();

            Assert.AreEqual(transforms, taa.TransformList);
            Assert.AreEqual(transforms, taa.TransformListProp);

            yield return null;
        }

        [UnityTest]
        public IEnumerator Find()
        {
            if (!Application.isPlaying) yield break;

            var go = GameObject.Find("AutoAssign");

            Assert.AreEqual(go, taa.AutoAssignGo);
            Assert.AreEqual(go, taa.AutoAssignGoProp);

            yield return null;
        }

        [UnityTest]
        public IEnumerator FindWithTag()
        {
            if (!Application.isPlaying) yield break;

            var p = GameObject.FindWithTag("Player");

            Assert.AreEqual(p, taa.Obj);
            Assert.AreEqual(p, taa.ObjProp);

            yield return null;
        }

        [UnityTest]
        public IEnumerator FindGOsWithTag()
        {
            if (!Application.isPlaying) yield break;

            var gos = GameObject.FindGameObjectsWithTag("GameController");

            Assert.AreEqual(gos, taa.GameControllers);
            Assert.AreEqual(gos, taa.GameControllersProp);

            yield return null;
        }

        [UnityTest]
        public IEnumerator FindObjOfType()
        {
            if (!Application.isPlaying) yield break;

            var autoAssignObj = Object.FindObjectOfType(typeof(TestAutoAssign));

            Assert.AreEqual(autoAssignObj, taa.FindAutoAssign);
            Assert.AreEqual(autoAssignObj, taa.FindAutoAssignProp);

            yield return null;
        }

        [UnityTest]
        public IEnumerator FindObjsOfType()
        {
            if (!Application.isPlaying) yield break;

            var autoAssignObjs = Object.FindObjectsOfType(typeof(TestAutoAssign));

            Assert.AreEqual(autoAssignObjs, taa.FindAutoAssigns);
            Assert.AreEqual(autoAssignObjs, taa.FindAutoAssignsProp);

            yield return null;

        }

        [UnityTest]
        public IEnumerator ResourcesLoad()
        {
            if (!Application.isPlaying) yield break;

            var gameObject = Resources.Load<GameObject>("AutoAssign");

            Assert.AreEqual(gameObject, taa.GameObject);
            Assert.AreEqual(gameObject, taa.GameObjectProp);

            yield return null;
        }

        [OneTimeSetUp]
        public void Setup()
        {
            prefab = Resources.Load<TestAutoAssign>("AutoAssign");
            taa = Object.Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
