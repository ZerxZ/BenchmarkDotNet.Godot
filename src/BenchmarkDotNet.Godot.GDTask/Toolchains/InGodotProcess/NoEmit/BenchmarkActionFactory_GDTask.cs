using System.Reflection;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Portability;
using GodotTask.Tasks;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using BenchmarkActionFactory = BenchmarkDotNet.Godot.Toolchains.InGodotProcess.NoEmit.BenchmarkActionFactory;

namespace BenchmarkDotNet.Godot.GDTask.Toolchains.InGodotProcess.NoEmit;

public static partial class BenchmarkActionFactoryGDtask
{
    static BenchmarkActionFactoryGDtask()
    {

        BenchmarkActionFactory.RegisterBenchmarkActionFactoryDelegate<GodotTask.Tasks.GDTask>(BenchmarkActionGDTaskFactory);
        BenchmarkActionFactory.RegisterBenchmarkActionFactoryTypeDelegate(typeof(GDTask<>), BenchmarkActionGDTaskGenericFactory);
    }
    private static BenchmarkAction BenchmarkActionGDTaskGenericFactory(Func<Type, object, MethodInfo, int, BenchmarkAction> create, Type argType, object instance, MethodInfo method, int unrollfactor)
    {
        return create(typeof(GDTask<>).MakeGenericType(argType), instance, method, unrollfactor);
    }
    private static BenchmarkAction BenchmarkActionGDTaskFactory(object instance, MethodInfo method, int unrollFactor)
    {
        return new BenchmarkActionGDTask(instance, method, unrollFactor);
    }

    public class BenchmarkActionGDTask : BenchmarkActionFactory.BenchmarkActionBase
    {
        private readonly Func<GodotTask.Tasks.GDTask> startTaskCallback;
        private readonly Action                       callback;
        private readonly Action                       unrolledCallback;

        public BenchmarkActionGDTask(object instance, MethodInfo method, int unrollFactor)
        {
            bool isIdle = method == null;
            if (!isIdle)
            {
                startTaskCallback = CreateWorkload<Func<GodotTask.Tasks.GDTask>>(instance, method);
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

    public class BenchmarkActionGDTask<T> : BenchmarkActionFactory.BenchmarkActionBase
    {
        private readonly Func<GDTask<T>> startTaskCallback;
        private readonly Func<T>         callback;
        private readonly Func<T>         unrolledCallback;
        private          T               result;

        public BenchmarkActionGDTask(object instance, MethodInfo method, int unrollFactor)
        {
            bool isOverhead = method == null;
            if (!isOverhead)
            {
                startTaskCallback = CreateWorkload<Func<GDTask<T>>>(instance, method);
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