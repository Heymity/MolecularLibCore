using UnityEngine;

namespace Molecular
{
    public static class PlayStatus
    {
        public static bool IsPlaying { get; set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
            IsPlaying = true;
            Application.quitting += () => IsPlaying = false;
        }
    }
}