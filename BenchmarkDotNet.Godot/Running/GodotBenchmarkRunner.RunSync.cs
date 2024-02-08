using System.Reflection;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace BenchmarkDotNet.Godot.Running;

/// <summary>
/// A partial class that provides methods to run benchmarks in Godot.
/// </summary>
public partial class GodotBenchmarkRunner
{
    /// <summary>
    /// Runs a benchmark for a specific type.
    /// </summary>
    /// <typeparam name="T">The type to benchmark.</typeparam>
    /// <param name="config">The configuration for the benchmark.</param>
    /// <param name="args">The arguments for the benchmark.</param>
    /// <param name="isDebug">A flag indicating whether the benchmark is in debug mode.</param>
    /// <returns>A summary of the benchmark results.</returns>
    public static Summary Run<T>(IConfig? config = null, string[]? args = null, bool isDebug = false)
    {
        SetGodotConfig(ref config, isDebug);
        return BenchmarkRunner.Run<T>(config, args);
    }

    /// <summary>
    /// Runs a benchmark for a specific type.
    /// </summary>
    /// <param name="type">The type to benchmark.</param>
    /// <param name="config">The configuration for the benchmark.</param>
    /// <param name="args">The arguments for the benchmark.</param>
    /// <param name="isDebug">A flag indicating whether the benchmark is in debug mode.</param>
    /// <returns>A summary of the benchmark results.</returns>
    public static Summary Run(Type type, IConfig? config = null, string[]? args = null, bool isDebug = false)
    {
        SetGodotConfig(ref config, isDebug);
        return BenchmarkRunner.Run(type, config, args);
    }

    /// <summary>
    /// Runs benchmarks for multiple types.
    /// </summary>
    /// <param name="types">The types to benchmark.</param>
    /// <param name="config">The configuration for the benchmarks.</param>
    /// <param name="args">The arguments for the benchmarks.</param>
    /// <param name="isDebug">A flag indicating whether the benchmarks are in debug mode.</param>
    /// <returns>An array of summaries of the benchmark results.</returns>
    public static Summary[] Run(Type[] types, IConfig? config = null, string[]? args = null, bool isDebug = false)
    {
        SetGodotConfig(ref config, isDebug);
        return BenchmarkRunner.Run(types, config, args);
    }

    /// <summary>
    /// Runs a benchmark for a specific type and methods.
    /// </summary>
    /// <param name="type">The type to benchmark.</param>
    /// <param name="methods">The methods to benchmark.</param>
    /// <param name="config">The configuration for the benchmark.</param>
    /// <param name="isDebug">A flag indicating whether the benchmark is in debug mode.</param>
    /// <returns>A summary of the benchmark results.</returns>
    public static Summary Run(Type type, MethodInfo[] methods, IConfig? config = null, bool isDebug = false)
    {
        SetGodotConfig(ref config, isDebug);
        return BenchmarkRunner.Run(type, methods, config);
    }

    /// <summary>
    /// Runs benchmarks for all types in a specific assembly.
    /// </summary>
    /// <param name="assembly">The assembly to benchmark.</param>
    /// <param name="config">The configuration for the benchmarks.</param>
    /// <param name="args">The arguments for the benchmarks.</param>
    /// <param name="isDebug">A flag indicating whether the benchmarks are in debug mode.</param>
    /// <returns>An array of summaries of the benchmark results.</returns>
    public static Summary[] Run(Assembly assembly, IConfig? config = null, string[]? args = null, bool isDebug = false)
    {
        SetGodotConfig(ref config, isDebug);
        return BenchmarkRunner.Run(assembly, config, args);
    }
}