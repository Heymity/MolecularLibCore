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

using MolecularLib.Timers;
using UnityEngine;

namespace MolecularLib.Testing
{
    public class TimersTest : MonoBehaviour
    {
        private TimerReference _timerReference;
        private Timer _timer;

        [ContextMenu("Timer Tests/TestTimerAsync")]
        private void TestTimerAsync()
        {
            Timer.TimerAsync(5, () => Debug.Log("Timer (5s) Async Finished"));
        }
        
        [ContextMenu("Timer Tests/TestTimerReferenceAsync")]
        private void TestTimerReferenceAsync()
        {
            _timerReference = Timer.TimerAsyncReference(6);
            _timerReference.OnFinish = () => Debug.Log("Timer Async (6s) Reference Finished");
        }
        
        [ContextMenu("Timer Tests/TestTimerReference REPEAT Async")]
        private void TestTimerReferenceRepeatAsync()
        {
            _timerReference = Timer.TimerAsyncReference(2, true);
            _timerReference.OnFinish = () => Debug.Log("Timer Async (2s) Reference Repeat Finished");
        }
        
        [ContextMenu("Timer Tests/Stop repeat")]
        private void TestTimerReferenceStopRepeatAsync()
        {
            _timerReference.StopOnNextCycle();
        }
        
        [ContextMenu("Timer Tests/Instance Timer Test")]
        private void InstanceTimerTest()
        {
            _timer = Timer.Create(5, () => Debug.Log("Timer (5s) Instance Finished"));
        }

        private void Update()
        {
            if (_timerReference is { HasFinished: false })
            {
                Debug.Log($"Async timer reference elapsed seconds: {_timerReference.ElapsedSeconds}");
            }
            
            if (_timer is { HasFinished: false })
            {
                Debug.Log($"Instance timer elapsed seconds: {_timer.ElapsedSeconds}");
            }
        }
    }
}