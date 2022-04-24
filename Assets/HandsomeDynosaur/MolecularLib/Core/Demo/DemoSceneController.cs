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

using MolecularLib.Helpers;
using MolecularLib.PolymorphismSupport;
using MolecularLib.Timers;
using UnityEngine;
using UnityEngine.UI;

namespace MolecularLib.Demo
{
    public class DemoSceneController : MonoBehaviour
    {
        [Header("Timers")]
        [SerializeField] private InputField timerDelay;
        [SerializeField] private Text timerTimeText;

        [Header("Instantiate With Args")]
        [SerializeField] private InputField stringArgument;
        [SerializeField] private InstantiateWithArgsDemoObject instantiateWithArgsDemoObject;
        [SerializeField] private RangeVector3 instantiatePosRange;
        
        [Header("Polymorphic Variable")]
        [SerializeField] private DemoDrawersScript demoDrawersScript;
        [SerializeField] private Text polymorphicVariableText;
        
        private PolymorphicVariable<Base> PolymorphicVariable => demoDrawersScript.PolymorphicVariable;

        private TimerReference _timerAsync;
        private Timer _timer;

        public void AsyncTimer()
        {
            if (float.TryParse(timerDelay.text, out var delay))
            {
                _timerAsync = Timer.TimerAsyncReference(delay);
                _timerAsync.OnFinish += () => timerTimeText.text = "Timer finished";
                return;
            }
            
            timerTimeText.text = "Invalid delay provided";
        }

        public void CoroutineTimer()
        {
            if (float.TryParse(timerDelay.text, out var delay))
            {
                _timer = Timer.Create(delay, () => timerTimeText.text = "Timer finished");
            }
            
            timerTimeText.text = "Invalid delay provided";
        }
        
        public void InstantiateWithArgs()
        {
            Molecular.Instantiate(instantiateWithArgsDemoObject,  instantiatePosRange.Random(), Quaternion.identity, stringArgument.text);
        }
        
        public void TestPoly()
        {
            if (PolymorphicVariable.As<A>(out var asA))
                polymorphicVariableText.text = $"As A | aClassInt: {asA.aClassInt}";
            else if (PolymorphicVariable.As<B>(out var asB))
                polymorphicVariableText.text = $"As B | bClassInt: {asB.bClassInt}";
            else if (PolymorphicVariable.As<C>(out var asC))
                polymorphicVariableText.text = $"As C | cClassFloat: {asC.cClassFloat}";
            else
                polymorphicVariableText.text = $"As Base | myBaseString: {PolymorphicVariable.Value.myBaseString}";
        }
        
        public void Update()
        {
            var showTimerAsync = _timerAsync is {HasFinished: false};
            var showTimerCoroutine = _timer is {HasFinished: false};
            
            switch (showTimerAsync)
            {
                case true when showTimerCoroutine:
                    timerTimeText.text = $"Timer time: {_timer.ElapsedSeconds}s";
                    timerTimeText.text += $"\nAsync timer time: {_timerAsync.ElapsedSeconds}s";
                    break;
                case true:
                    timerTimeText.text = $"Async Timer time: {_timerAsync.ElapsedSeconds}s";
                    break;
                case false when showTimerCoroutine:
                    timerTimeText.text = $"Coroutine Timer time: {_timer.ElapsedSeconds}s";
                    break;
            }
        }
        
    }
}
