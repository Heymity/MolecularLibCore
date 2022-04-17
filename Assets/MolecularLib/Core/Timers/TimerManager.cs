using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MolecularLib.Timers
{
    public class TimerManager : AutoSingleton<TimerManager>
    {
        private readonly List<Timer> _timers = new List<Timer>();

        private void Awake()
        {
            hideFlags = HideFlags.HideAndDontSave;
        }

        internal void AddTimer(Timer timer)
        {
            _timers.Add(timer);

            StartCoroutine(timer.TimerCoroutine);
            
            timer.OnComplete += () =>
            {
                if (timer.Repeat)
                {
                    StartCoroutine(timer.StartTimer());
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