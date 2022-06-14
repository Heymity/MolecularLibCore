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

using System.Collections.Generic;
using UnityEngine;

namespace MolecularLib.Timers
{
    public class TimerManager : AutoSingleton<TimerManager>
    {
        private readonly List<Timer> _timers = new List<Timer>();

        private void Awake()
        {
            gameObject.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(this);
        }

        internal void AddTimer(Timer timer)
        {
            _timers.Add(timer);

            StartCoroutine(timer.TimerCoroutine);
            
            timer.OnComplete += () =>
            {
                if (timer.Repeat)
                {
                    timer.RestartTimer();
                    StartCoroutine(timer.TimerCoroutine);
                    return;
                }
                
                _timers.Remove(timer);
            };
        }
    
        public bool HasTimer(Timer timer)
        {
            return _timers.Contains(timer);
        }
        
        public void RemoveTimer(Timer timer)
        {
            StopCoroutine(timer.TimerCoroutine);
            _timers.Remove(timer);
        }
    }
}