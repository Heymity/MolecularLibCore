using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace MolecularInternal
{
    public static class Logger
    {
        [Conditional("MOLECULAR_VERBOSE")]
        public static void MolecularVerbose(string toLog)
        {
            Debug.Log(toLog);
        }
        
        [Conditional("VERBOSE")]
        public static void Verbose(string toLog)
        {
            Debug.Log(toLog);
        }
    }
}