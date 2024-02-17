using System.Reflection;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Godot.Attributes;
using BenchmarkDotNet.Godot.Helper;
using BenchmarkDotNet.Portability;
using Godot;
using GodotTask.Tasks;

namespace BenchmarkDotNet.Godot.Toolchains.InGodotProcess.NoEmit;

public static partial class BenchmarkActionFactory
{
    public static bool GetSynchronizationContext(this MethodInfo method)
    {

        foreach (var attribute in method.GetCustomAttributes(true))
        {
            if (attribute is ISynchronizationContext synchronizationContext)
            {
                GD.Print(synchronizationContext.GetType().FullName);
                return synchronizationContext?.SynchronizationContext ?? false;
            }
        }
        return false;
    }

    public class BenchmarkActionVoid : BenchmarkActionBase
    {
        private readonly Action callback;
        private readonly Action unrolledCallback;
        public           bool   SynchronizationContext;
        public readonly  Action originCallback;
        public           bool   GlobalCleanup  { get; set; }
        public           bool   isNode         { get; set; }
        public           void   SyncCallback() =>
            
            GodotHelper.SynchronizationContext(originCallback);
        private          object Instance       { get; set; }
        public BenchmarkActionVoid(object instance, MethodInfo method, int unrollFactor)
        {
            Instance = instance;
            originCallback = CreateWorkloadOrOverhead<Action>(instance, method, OverheadStatic, OverheadInstance);
            SynchronizationContext = method?.GetSynchronizationContext() ?? false;
            callback = SynchronizationContext ?SyncCallback  :originCallback ;
            GlobalCleanup = method.HasAttribute<GlobalCleanupAttribute>();
            isNode = instance is Node;
            InvokeSingle = callback;

            unrolledCallback = Unroll(callback, unrollFactor);
            InvokeUnroll = WorkloadActionUnroll;
            InvokeNoUnroll = WorkloadActionNoUnroll;
        }
        private void OnInvokeSingle()
        {
            callback();
            if (isNode && GlobalCleanup)
            {
                GodotHelper.SynchronizationContext((Node)Instance, node => node?.QueueFree());
            }
        }
        private static void OverheadStatic()
        {
        }
        private void OverheadInstance()
        {
        }

        [MethodImpl(CodeGenHelper.AggressiveOptimizationOption)]
        private void WorkloadActionUnroll(long repeatCount)
        {
            for (long i = 0; i < repeatCount; i++)
                unrolledCallback();
        }

        [MethodImpl(CodeGenHelper.AggressiveOptimizationOption)]
        private void WorkloadActionNoUnroll(long repeatCount)
        {
            for (long i = 0; i < repeatCount; i++)
                callback();
        }
    }

    public class BenchmarkAction<T> : BenchmarkActionBase
    {
        private readonly Func<T> callback;
        private readonly Func<T> unrolledCallback;
        private          T       result;


        public BenchmarkAction(object instance, MethodInfo method, int unrollFactor)
        {
            callback = CreateWorkloadOrOverhead<Func<T>>(instance, method, OverheadStatic, OverheadInstance);
            InvokeSingle = InvokeSingleHardcoded;

            unrolledCallback = Unroll(callback, unrollFactor);
            InvokeUnroll = WorkloadActionUnroll;
            InvokeNoUnroll = WorkloadActionNoUnroll;
        }

        private static T OverheadStatic()   => default;
        private        T OverheadInstance() => default;

        private void InvokeSingleHardcoded() => result = callback();

        [MethodImpl(CodeGenHelper.AggressiveOptimizationOption)]
        private void WorkloadActionUnroll(long repeatCount)
        {
            for (long i = 0; i < repeatCount; i++)
                result = unrolledCallback();
        }

        [MethodImpl(CodeGenHelper.AggressiveOptimizationOption)]
        private void WorkloadActionNoUnroll(long repeatCount)
        {
            for (long i = 0; i < repeatCount; i++)
                result = callback();
        }

        public override object LastRunResult => result;
    }

    public class BenchmarkActionTask : BenchmarkActionBase
    {
        private readonly Func<Task> startTaskCallback;
        private readonly Action     callback;
        private readonly Action     unrolledCallback;

        public BenchmarkActionTask(object instance, MethodInfo method, int unrollFactor)
        {
            bool isIdle = method == null;
            if (!isIdle)
            {
                startTaskCallback = CreateWorkload<Func<Task>>(instance, method);
                callback = ExecuteBlocking;
            }
            else
            {
                callback = Overhead;
            }

            InvokeSingle = callback;

            unrolledCallback = Unroll(callback, unrollFactor);
            InvokeUnroll = WorkloadActionUnroll;
            InvokeNoUnroll = WorkloadActionNoUnroll;

        }

        // must be kept in sync with VoidDeclarationsProvider.IdleImplementation
        private void Overhead()
        {
        }

        // must be kept in sync with TaskDeclarationsProvider.TargetMethodDelegate
        private void ExecuteBlocking() => startTaskCallback.Invoke().GetAwaiter().GetResult();

        [MethodImpl(CodeGenHelper.AggressiveOptimizationOption)]
        private void WorkloadActionUnroll(long repeatCount)
        {
            for (long i = 0; i < repeatCount; i++)
                unrolledCallback();
        }

        [MethodImpl(CodeGenHelper.AggressiveOptimizationOption)]
        private void WorkloadActionNoUnroll(long repeatCount)
        {
            for (long i = 0; i < repeatCount; i++)
                callback();
        }
    }

    public class BenchmarkActionTask<T> : BenchmarkActionBase
    {
        private readonly Func<Task<T>> startTaskCallback;
        private readonly Func<T>       callback;
        private readonly Func<T>       unrolledCallback;
        private          T             result;

        public BenchmarkActionTask(object instance, MethodInfo method, int unrollFactor)
        {
            bool isOverhead = method == null;
            if (!isOverhead)
            {
                startTaskCallback = CreateWorkload<Func<Task<T>>>(instance, method);
                callback = ExecuteBlocking;
            }
            else
            {
                callback = Overhead;
            }

            InvokeSingle = InvokeSingleHardcoded;

            unrolledCallback = Unroll(callback, unrollFactor);
            InvokeUnroll = WorkloadActionUnroll;
            InvokeNoUnroll = WorkloadActionNoUnroll;
        }

        private T Overhead() => default;

        // must be kept in sync with GenericTaskDeclarationsProvider.TargetMethodDelegate
        private T ExecuteBlocking() => startTaskCallback().GetAwaiter().GetResult();

        private void InvokeSingleHardcoded() => result = callback();

        [MethodImpl(CodeGenHelper.AggressiveOptimizationOption)]
        private void WorkloadActionUnroll(long repeatCount)
        {
            for (long i = 0; i < repeatCount; i++)
                result = unrolledCallback();
        }

        [MethodImpl(CodeGenHelper.AggressiveOptimizationOption)]
        private void WorkloadActionNoUnroll(long repeatCount)
        {
            for (long i = 0; i < repeatCount; i++)
                result = callback();
        }

        public override object LastRunResult => result;
    }

    public class BenchmarkActionValueTask<T> : BenchmarkActionBase
    {
        private readonly Func<ValueTask<T>> startTaskCallback;
        private readonly Func<T>            callback;
        private readonly Func<T>            unrolledCallback;
        private          T                  result;

        public BenchmarkActionValueTask(object instance, MethodInfo method, int unrollFactor)
        {
            bool isOverhead = method == null;
            if (!isOverhead)
            {
                startTaskCallback = CreateWorkload<Func<ValueTask<T>>>(instance, method);
                callback = ExecuteBlocking;
            }
            else
            {
                callback = Overhead;
            }

            InvokeSingle = InvokeSingleHardcoded;


            unrolledCallback = Unroll(callback, unrollFactor);
            InvokeUnroll = WorkloadActionUnroll;
            InvokeNoUnroll = WorkloadActionNoUnroll;
        }

        private T Overhead() => default;

        // must be kept in sync with GenericTaskDeclarationsProvider.TargetMethodDelegate
        private T ExecuteBlocking() => startTaskCallback().GetAwaiter().GetResult();

        private void InvokeSingleHardcoded() => result = callback();

        [MethodImpl(CodeGenHelper.AggressiveOptimizationOption)]
        private void WorkloadActionUnroll(long repeatCount)
        {
            for (long i = 0; i < repeatCount; i++)
                result = unrolledCallback();
        }

        [MethodImpl(CodeGenHelper.AggressiveOptimizationOption)]
        private void WorkloadActionNoUnroll(long repeatCount)
        {
            for (long i = 0; i < repeatCount; i++)
                result = callback();
        }

        public override object LastRunResult => result;
    }


}