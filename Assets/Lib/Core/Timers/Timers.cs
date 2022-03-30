using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MolecularLib.Timers
{
    //TODO Handle editor leave playmode for async and timers being used outside of playmode
    public class Timer
    {
        #region Static Timers

        public class TimerReference
        {
            public bool Repeat { get; internal set; }
            public float StartTime { get; internal set; }
            public int DurationInMilliseconds { get; internal set; }
            public bool HasFinished => ElapsedMilliseconds >= DurationInMilliseconds;
            public Action OnFinish;
            public float ElapsedSeconds => Time.time - StartTime;
            public int ElapsedMilliseconds => (int)ElapsedSeconds * 1000;
            
            public void StopOnNextCycle() => Repeat = false;
        }

        /// <summary>
        /// Makes a timer using the await Task.Delay() method, not needing to create a MonoBehaviour
        /// </summary>
        /// <param name="seconds">The seconds to wait until call the callback</param>
        /// <param name="callback">The function to be called when the timer expires</param>
        public static async void TimerAsync(float seconds, Action callback)
        {
            await Task.Delay((int) (seconds * 1000));

            callback?.Invoke();
        }
        
        /// <summary>
        /// Makes a timer using the await Task.Delay() method, not needing to create a MonoBehaviour and returning a TimerReference, allowing to get the elapsed time, the Start Time, to make repeatable timers and reassign the callback
        /// </summary>
        /// <param name="seconds">The seconds to wait until call the callback</param>
        /// <param name="repeat">Whether the timer should repeat after conclusion or not</param>
        public static TimerReference TimerAsyncReference(float seconds, bool repeat = false)
        {
            var reference = new TimerReference
            {
                DurationInMilliseconds = (int) seconds * 1000,
                StartTime = Time.time,
                Repeat = repeat
            };

            TimerAsync(seconds, !reference.Repeat ? reference.OnFinish : HandleRepeat);
            
            return reference;

            void HandleRepeat()
            {
                if (!reference.Repeat) return;
                
                reference.OnFinish?.Invoke();
                reference.StartTime = Time.time;
                TimerAsync(seconds, HandleRepeat);
            }
        }

        #endregion
    }
}