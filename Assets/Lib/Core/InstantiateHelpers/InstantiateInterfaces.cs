namespace MolecularLib
{
    public interface IArgsInstantiable
    {
        public void Initialize();
    }

    public interface IArgsInstantiable<in T>
    {
        public void Initialize(T arg1);
    }
    
    public interface IArgsInstantiable<in T1, in T2>
    {
        public void Initialize(T1 arg1, T2 arg2);
    }
    
    public interface IArgsInstantiable<in T1, in T2, in T3>
    {
        public void Initialize(T1 arg1, T2 arg2, T3 arg3);
    }
    
    public interface IArgsInstantiable<in T1, in T2, in T3, in T4>
    {
        public void Initialize(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }
    
    public interface IArgsInstantiable<in T1, in T2, in T3, in T4, in T5>
    {
        public void Initialize(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }
    
    public interface IArgsInstantiable<in T1, in T2, in T3, in T4, in T5, in T6>
    {
        public void Initialize(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
    }
    
    public interface IArgsInstantiable<in T1, in T2, in T3, in T4, in T5, in T6, in T7>
    {
        public void Initialize(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);
    }
    
    public interface IArgsInstantiable<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8>
    {
        public void Initialize(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);
    }
} 