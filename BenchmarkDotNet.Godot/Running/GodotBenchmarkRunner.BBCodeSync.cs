using System.Reflection;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;

namespace BenchmarkDotNet.Godot.Running;

/// <summary>
/// A partial class that provides methods to run benchmarks in Godot with BBCode output.
/// </summary>
public static partial class GodotBenchmarkRunner
{
    /// <summary>
    /// Runs a benchmark for a specific type and provides a callback with the results in BBCode format.
    /// </summary>
    /// <typeparam name="T">The type to benchmark.</typeparam>
    /// <param name="config">The configuration for the benchmark.</param>
    /// <param name="args">The arguments for the benchmark.</param>
    /// <param name="onCallback">The callback to invoke with the results in BBCode format.</param>
    /// <param name="isDebug">A flag indicating whether the benchmark is in debug mode.</param>
    /// <returns>A summary of the benchmark results.</returns>
    public static Summary RunWithBBCode<T>(IConfig? config = null, string[]? args = null, Action<string>? onCallback = null, bool isDebug = false)
    {
        var summary = Run<T>(config, args, isDebug);
        onCallback ??= _ => { };
        onCallback.Callable(summary);
        return summary;
    }

    /// <summary>
    /// Runs a benchmark for a specific type and provides a callback with the results in BBCode format.
    /// </summary>
    /// <param name="type">The type to benchmark.</param>
    /// <param name="config">The configuration for the benchmark.</param>
    /// <param name="args">The arguments for the benchmark.</param>
    /// <param name="onCallback">The callback to invoke with the results in BBCode format.</param>
    /// <param name="isDebug">A flag indicating whether the benchmark is in debug mode.</param>
    /// <returns>A summary of the benchmark results.</returns>
    public static Summary RunWithBBCode(Type type, IConfig? config = null, string[]? args = null, Action<string>? onCallback = null, bool isDebug = false)
    {
        var summary = Run(type, config, args, isDebug);
        onCallback ??= _ => { };
        onCallback.Callable(summary);
        return summary;
    }

    /// <summary>
    /// Runs benchmarks for multiple types and provides a callback with the results in BBCode format.
    /// </summary>
    /// <param name="types">The types to benchmark.</param>
    /// <param name="config">The configuration for the benchmarks.</param>
    /// <param name="args">The arguments for the benchmarks.</param>
    /// <param name="onCallback">The callback to invoke with the results in BBCode format.</param>
    /// <param name="isDebug">A flag indicating whether the benchmarks are in debug mode.</param>
    /// <returns>An array of summaries of the benchmark results.</returns>
    public static Summary[] RunWithBBCode(Type[] types, IConfig? config = null, string[]? args = null, Action<Summary, string>? onCallback = null, bool isDebug = false)
    {
        var summaries = GodotBenchmarkRunner.Run(types, config, args, isDebug);
        onCallback ??= (_, _) => { };
        onCallback.Callable(summaries);
        return summaries;
    }

    /// <summary>
    /// Runs a benchmark for a specific type and methods, and provides a callback with the results in BBCode format.
    /// </summary>
    /// <param name="type">The type to benchmark.</param>
    /// <param name="methods">The methods to benchmark.</param>
    /// <param name="config">The configuration for the benchmark.</param>
    /// <param name="onCallback">The callback to invoke with the results in BBCode format.</param>
    /// <param name="isDebug">A flag indicating whether the benchmark is in debug mode.</param>
    /// <returns>A summary of the benchmark results.</returns>
    public static Summary RunWithBBCode(Type type, MethodInfo[] methods, IConfig? config = null, Action<string>? onCallback = null, bool isDebug = false)
    {
        var summary = Run(type, methods, config, isDebug);
        onCallback ??= _ => { };
        onCallback.Callable(summary);
        return summary;
    }
    /// <summary>
    /// Runs benchmarks for an assembly and provides a callback with the results in BBCode format.
    /// </summary>
    /// <param name="assembly">The assembly to benchmark.</param>
    /// <param name="config">The configuration for the benchmarks.</param>
    /// <param name="args">The arguments for the benchmarks.</param>
    /// <param name="onCallback">The callback to invoke with the results in BBCode format.</param>
    /// <param name="isDebug">A flag indicating whether the benchmarks are in debug mode.</param>
    /// <returns>An array of summaries of the benchmark results.</returns>
    public static Summary[] RunWithBBCode(Assembly assembly, IConfig? config = null, string[]? args = null, Action<Summary, string>? onCallback = null, bool isDebug = false)
    {

        var summaries = Run(assembly, config, args, isDebug);
        onCallback ??= (_, _) => { };
        onCallback.Callable(summaries);
        return summaries;
    }
}