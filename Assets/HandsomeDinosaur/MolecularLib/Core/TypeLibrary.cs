using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MolecularLib
{
    public static class TypeLibrary
    {
        public static IEnumerable<Type> AllAssembliesTypes { get; private set; }
        public static IEnumerable<Type> AllNonUnityAssembliesTypes { get; private set; }
        
        public static IDictionary<string, Assembly> AllAssemblies { get; private set; }

        /*public static IEnumerable<Type> AllAssembliesTypes
        {
            get => throw new Exception("The USE_TYPE_LIBRARY precompiler symbol is disabled, therefore this feature is no enabled. To enable it add USE_TYPE_LIBRARY to the script define symbols in unity project settings");
            set => throw new Exception("The USE_TYPE_LIBRARY precompiler symbol is disabled, therefore this feature is no enabled. To enable it add USE_TYPE_LIBRARY to the script define symbols in unity project settings");
        }
        public static IEnumerable<Type> AllNonUnityAssembliesTypes
        {
            get => throw new Exception("The USE_TYPE_LIBRARY precompiler symbol is disabled, therefore this feature is no enabled. To enable it add USE_TYPE_LIBRARY to the script define symbols in unity project settings");
            set => throw new Exception("The USE_TYPE_LIBRARY precompiler symbol is disabled, therefore this feature is no enabled. To enable it add USE_TYPE_LIBRARY to the script define symbols in unity project settings");
        }*/
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Bootstrap()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            
            AllAssembliesTypes = assemblies
                .Select(a => a.GetTypes())
                .Aggregate(new List<Type>() as IEnumerable<Type>,(a, s) => a.Concat(s));

            AllNonUnityAssembliesTypes = assemblies
                .Where(a => !IsAssemblyFromUnity(a.FullName))
                .Select(a => a.GetTypes())
                .Aggregate(new List<Type>() as IEnumerable<Type>,(a, s) => a.Concat(s));

            AllAssemblies = AllAssembliesTypes
                .Select(t => (Assembly: t.Assembly, Name: t.Assembly.GetName().Name))
                .Distinct<(Assembly a, string name)>(new AssemblyTypeEqualityComparer())
                .ToDictionary(a => a.name, a => a.a);
            
            /*PlayerAssembliesTypes = CompilationPipeline
                .GetAssemblies(AssembliesType.Player).Select(a => a.)
                .Aggregate(new List<Type>() as IEnumerable<Type>,(a, s) => a.Concat(s));*/

            bool IsAssemblyFromUnity(string assemblyName)
            {
                return assemblyName.Split('.')[0] == "Unity"
                       || assemblyName.Contains("UnityEngine")
                       || assemblyName.Contains("UnityEditor")
                       || assemblyName.Contains("UnityEngineInternal")
                       || assemblyName.Contains("UnityEditorInternal");
            }
        }
        
        #if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
        public static void BootstrapEditor()
        {
            Bootstrap();
        }
        #endif
    }
    
    internal class AssemblyTypeEqualityComparer : IEqualityComparer<(Assembly a, string name)>
    {
        public bool Equals((Assembly a, string name) x, (Assembly a, string name) y)
        {
            return x.name == y.name;
        }

        public int GetHashCode((Assembly a, string name) obj)
        {
            return obj.name.GetHashCode();
        }
    }
}
