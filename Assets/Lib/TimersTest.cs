using MolecularLib.Timers;
using UnityEngine;

namespace MolecularLib.Testing
{
    public class TimersTest : MonoBehaviour
    {
        private static Timer.TimerReference _timerReference;

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

        private void Update()
        {
            if (_timerReference is { HasFinished: false })
            {
                Debug.Log($"Async timer reference elapsed seconds: {_timerReference.ElapsedSeconds}");
            }
        }
    }
}