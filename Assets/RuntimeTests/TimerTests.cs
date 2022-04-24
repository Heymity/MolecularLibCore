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

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Timer = MolecularLib.Timers.Timer;

namespace MolecularLibTests
{
    public class TimerTests
    {
        [UnityTest]
        public IEnumerator TimerCoroutineTest()
        {
            var timer = Timer.Create(3f, () => Debug.Log("Finished"));
            yield return new WaitForSeconds(3f);
            Assert.True(timer.HasFinished);
        }
        
        [UnityTest]
        public IEnumerator TimerRepeatCoroutineTest()
        {
            var finishedTimes = 0;
            var timer = Timer.Create(3f, () =>
            {
                finishedTimes++;
                Debug.Log($"Finished Repeat normal for the {finishedTimes} time");
            }, true);
            for (var i = 0; i <= 2; i++)
            {
                yield return new WaitForSeconds(3f);
                Debug.Log("Complete now");
            }
            timer.StopTimer();

            Debug.Log($"Asserting normal repeat {finishedTimes}");
            Assert.AreEqual(3, finishedTimes);
        }
        
        [UnityTest]
        public IEnumerator TimerRepeatAbruptlyStopTest()
        {
            var finishedTimes = 0;
            var timer = Timer.Create(3f, () =>
            {
                finishedTimes++;
                Debug.Log($"Finished for the {finishedTimes} time");
            }, true);
            for (var i = 0; i <= 2; i++)
            {
                if (i == 1)
                {
                    yield return new WaitForSeconds(1.5f);
                    timer.StopTimer();
                    yield return new WaitForSeconds(4f);
                    break;
                }
                yield return new WaitForSeconds(3f);
            }
            
            Debug.Log($"Asserting {finishedTimes}");
            Assert.AreEqual(1, finishedTimes);
        }
    }
}