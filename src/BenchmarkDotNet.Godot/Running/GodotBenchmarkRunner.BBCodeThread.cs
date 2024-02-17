using System.Reflection;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;

namespace BenchmarkDotNet.Godot.Running;

public partial class GodotBenchmarkRunner
{
    /// <summary>
    /// Runs a benchmark for a specific type in a separate thread and provides a callback with the results in BBCode format.
    /// </summary>
    /// <typeparam name="T">The type to benchmark.</typeparam>
    /// <param name="config">The configuration for the benchmark.</param>
    /// <param name="args">The arguments for the benchmark.</param>
    /// <param name="onCallback">The callback to invoke with the results in BBCode format.</param>
    /// <param name="isDebug">A flag indicating whether the benchmark is in debug mode.</param>
    /// <param name="onFinish">The callback to invoke when the benchmark finishes.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the benchmark.</param>
    /// <returns>A boolean indicating whether the benchmark was queued successfully.</returns>
    public static bool RunWithBBCodeThread<T>(IConfig? config = null, string[]? args = null, Action<string>? onCallback = null, bool isDebug = false, Action<Summary>? onFinish = null, CancellationToken cancellationToken = default)
    {
        // Check if the operation has been cancelled
        if (cancellationToken.IsCancellationRequested)
        {
            return false;
        }
        // Queue the benchmark to run in a separate thread
        return ThreadPool.QueueUserWorkItem(_ =>
        {
            // Check again if the operation has been cancelled
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
            // Run the benchmark and get the summary
            var summary = RunWithBBCode<T>(config, args, onCallback, isDebug);
            // Invoke the callback when the benchmark finishes
            onFinish?.Invoke(summary);
        });
    }
    /// <summary>
    /// Runs a benchmark for a specific type in a separate thread and provides a callback with the results in BBCode format.
    /// </summary>
    /// <param name="type">The type to benchmark.</param>
    /// <param name="config">The configuration for the benchmark.</param>
    /// <param name="args">The arguments for the benchmark.</param>
    /// <param name="onCallback">The callback to invoke with the results in BBCode format.</param>
    /// <param name="isDebug">A flag indicating whether the benchmark is in debug mode.</param>
    /// <param name="onFinish">The callback to invoke when the benchmark finishes.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the benchmark.</param>
    /// <returns>A boolean indicating whether the benchmark was queued successfully.</returns>
    public static bool RunWithBBCodeThread(Type type, IConfig? config = null, string[]? args = null, Action<string>? onCallback = null, bool isDebug = false, Action<Summary>? onFinish = null, CancellationToken cancellationToken = default)
    {
        // Check if the operation has been cancelled
        if (cancellationToken.IsCancellationRequested)
        {
            return false;
        }
        // Queue the benchmark to run in a separate thread
        return ThreadPool.QueueUserWorkItem(_ =>
        {
            // Check again if the operation has been cancelled
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
            // Run the benchmark and get the summary
            var summary = RunWithBBCode(type, config, args, onCallback, isDebug);
            // Invoke the callback when the benchmark finishes
            onFinish?.Invoke(summary);
        });
    }


    /// <summary>
    /// Runs a benchmark for a specific type and methods in a separate thread and provides a callback with the results in BBCode format.
    /// </summary>
    /// <param name="type">The type to benchmark.</param>
    /// <param name="methods">The methods to benchmark.</param>
    /// <param name="config">The configuration for the benchmark.</param>
    /// <param name="onCallback">The callback to invoke with the results in BBCode format.</param>
    /// <param name="isDebug">A flag indicating whether the benchmark is in debug mode.</param>
    /// <param name="onFinish">The callback to invoke when the benchmark finishes.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the benchmark.</param>
    /// <returns>A boolean indicating whether the benchmark was queued successfully.</returns>
    public static bool RunWithBBCodeThread(Type type, MethodInfo[] methods, IConfig? config = null, Action<string>? onCallback = null, bool isDebug = false, Action<Summary>? onFinish = null, CancellationToken cancellationToken = default)
    {
        // Check if the operation has been cancelled
        if (cancellationToken.IsCancellationRequested)
        {
            return false;
        }
        // Queue the benchmark to run in a separate thread
        return ThreadPool.QueueUserWorkItem(_ =>
        {
            // Check again if the operation has been cancelled
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
            // Run the benchmark and get the summary
            var summary = RunWithBBCode(type, methods, config, onCallback, isDebug);
            // Invoke the callback when the benchmark finishes
            onFinish?.Invoke(summary);
        });
    }
    /// <summary>
    /// Runs benchmarks for multiple types in a separate thread and provides a callback with the results in BBCode format.
    /// </summary>
    /// <param name="types">The types to benchmark.</param>
    /// <param name="config">The configuration for the benchmarks.</param>
    /// <param name="args">The arguments for the benchmarks.</param>
    /// <param name="onCallback">The callback to invoke with the results in BBCode format.</param>
    /// <param name="isDebug">A flag indicating whether the benchmarks are in debug mode.</param>
    /// <param name="onFinish">The callback to invoke when the benchmarks finish.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the benchmarks.</param>
    /// <returns>A boolean indicating whether the benchmarks were queued successfully.</returns>
    public static bool RunWithBBCodeThread(Type[] types, IConfig? config = null, string[]? args = null, Action<Summary, string>? onCallback = null, bool isDebug = false, Action<Summary[]>? onFinish = null, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return false;
        }
        return ThreadPool.QueueUserWorkItem(_ =>
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
            var summaries = RunWithBBCode(types, config, args, onCallback, isDebug);
            onFinish?.Invoke(summaries);
        });
    }

    /// <summary>
    /// Runs benchmarks for an assembly in a separate thread and provides a callback with the results in BBCode format.
    /// </summary>
    /// <param name="assembly">The assembly to benchmark.</param>
    /// <param name="config">The configuration for the benchmarks.</param>
    /// <param name="args">The arguments for the benchmarks.</param>
    /// <param name="onCallback">The callback to invoke with the results in BBCode format.</param>
    /// <param name="isDebug">A flag indicating whether the benchmarks are in debug mode.</param>
    /// <param name="onFinish">The callback to invoke when the benchmarks finish.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the benchmarks.</param>
    /// <returns>A boolean indicating whether the benchmarks were queued successfully.</returns>
    public static bool RunWithBBCodeThread(Assembly assembly, IConfig? config = null, string[]? args = null, Action<Summary, string>? onCallback = null, bool isDebug = false, Action<Summary[]>? onFinish = null, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return false;
        }
        return ThreadPool.QueueUserWorkItem(_ =>
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
            var summaries = RunWithBBCode(assembly, config, args, onCallback, isDebug);
            onFinish?.Invoke(summaries);
        });
    }

}