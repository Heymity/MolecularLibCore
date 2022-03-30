using UnityEngine;

namespace MolecularLib
{
    //TODO Research the possibility of using source generators
    //TODO Add (original, parent) signature
    public static partial class Molecular
    {
        /// <summary>
        /// Instantiates a Object that derives from IArgsInstantiable and calls the initialize function
        /// </summary>
        public static T Instantiate<T>(T original) where T : Object, IArgsInstantiable
        {
            var instantiatedObj = Object.Instantiate(original);

            if (instantiatedObj is IArgsInstantiable argsInstantiable)
                argsInstantiable.Initialize();

            return instantiatedObj;
        }
        
        /// <summary>
        /// Instantiates a Object that derives from IArgsInstantiable with 1 argument and calls the initialize function
        /// </summary>
        public static T Instantiate<T, TArg1>(T original, TArg1 arg1) where T : Object, IArgsInstantiable<TArg1>
        {
            var instantiatedObj = Object.Instantiate(original);

            if (instantiatedObj is IArgsInstantiable<TArg1> argsInstantiable)
                argsInstantiable.Initialize(arg1);

            return instantiatedObj;
        }
        
        /// <summary>
        /// Instantiates a Object that derives from IArgsInstantiable with 2 argument and calls the initialize function
        /// </summary>
        public static T Instantiate<T, TArg1, TArg2>(T original, TArg1 arg1, TArg2 arg2) where T : Object, IArgsInstantiable<TArg1, TArg2>
        {
            var instantiatedObj = Object.Instantiate(original);

            if (instantiatedObj is IArgsInstantiable<TArg1, TArg2> argsInstantiable)
                argsInstantiable.Initialize(arg1, arg2);

            return instantiatedObj;
        }
        
        /// <summary>
        /// Instantiates a Object that derives from IArgsInstantiable with 3 argument and calls the initialize function
        /// </summary>
        public static T Instantiate<T, TArg1, TArg2, TArg3>(T original, TArg1 arg1, TArg2 arg2, TArg3 arg3) where T : Object, IArgsInstantiable<TArg1, TArg2, TArg3>
        {
            var instantiatedObj = Object.Instantiate(original);

            if (instantiatedObj is IArgsInstantiable<TArg1, TArg2, TArg3> argsInstantiable)
                argsInstantiable.Initialize(arg1, arg2, arg3);

            return instantiatedObj;
        }
        
        /// <summary>
        /// Instantiates a Object that derives from IArgsInstantiable with 4 argument and calls the initialize function
        /// </summary>
        public static T Instantiate<T, TArg1, TArg2, TArg3, TArg4>(T original, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4) where T : Object, IArgsInstantiable<TArg1, TArg2, TArg3, TArg4>
        {
            var instantiatedObj = Object.Instantiate(original);

            if (instantiatedObj is IArgsInstantiable<TArg1, TArg2, TArg3, TArg4> argsInstantiable)
                argsInstantiable.Initialize(arg1, arg2, arg3, arg4);

            return instantiatedObj;
        }
        
        /// <summary>
        /// Instantiates a Object that derives from IArgsInstantiable with 5 argument and calls the initialize function
        /// </summary>
        public static T Instantiate<T, TArg1, TArg2, TArg3, TArg4, TArg5>(T original, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5) where T : Object, IArgsInstantiable<TArg1, TArg2, TArg3, TArg4, TArg5>
        {
            var instantiatedObj = Object.Instantiate(original);

            if (instantiatedObj is IArgsInstantiable<TArg1, TArg2, TArg3, TArg4, TArg5> argsInstantiable)
                argsInstantiable.Initialize(arg1, arg2, arg3, arg4, arg5);

            return instantiatedObj;
        }
        
        /// <summary>
        /// Instantiates a Object that derives from IArgsInstantiable with 6 argument and calls the initialize function
        /// </summary>
        public static T Instantiate<T, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(T original, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6) where T : Object, IArgsInstantiable<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>
        {
            var instantiatedObj = Object.Instantiate(original);

            if (instantiatedObj is IArgsInstantiable<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> argsInstantiable)
                argsInstantiable.Initialize(arg1, arg2, arg3, arg4, arg5, arg6);

            return instantiatedObj;
        }
        
        /// <summary>
        /// Instantiates a Object that derives from IArgsInstantiable with 7 argument and calls the initialize function
        /// </summary>
        public static T Instantiate<T, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(T original, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7) where T : Object, IArgsInstantiable<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>
        {
            var instantiatedObj = Object.Instantiate(original);

            if (instantiatedObj is IArgsInstantiable<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> argsInstantiable)
                argsInstantiable.Initialize(arg1, arg2, arg3, arg4, arg5, arg6, arg7);

            return instantiatedObj;
        }
        
        /// <summary>
        /// Instantiates a Object that derives from IArgsInstantiable with 8 argument and calls the initialize function
        /// </summary>
        public static T Instantiate<T, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(T original, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8) where T : Object, IArgsInstantiable<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>
        {
            var instantiatedObj = Object.Instantiate(original);

            if (instantiatedObj is IArgsInstantiable<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> argsInstantiable)
                argsInstantiable.Initialize(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);

            return instantiatedObj;
        }
    }
}