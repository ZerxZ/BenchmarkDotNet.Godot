using System.Reflection;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace BenchmarkDotNet.Godot.Running;

/// <summary>
/// A partial class that provides methods to run benchmarks in Godot on a separate thread.
/// </summary>
public partial class GodotBenchmarkRunner
{
    /// <summary>
    /// Runs a benchmark for a specific type on a separate thread.
    /// </summary>
    /// <typeparam name="T">The type to benchmark.</typeparam>
    /// <param name="config">The configuration for the benchmark.</param>
    /// <param name="args">The arguments for the benchmark.</param>
    /// <param name="isDebug">A flag indicating whether the benchmark is in debug mode.</param>
    /// <param name="onFinish">The action to invoke when the benchmark finishes.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the benchmark.</param>
    /// <returns>A boolean indicating whether the benchmark was queued successfully.</returns>
    public static async Task RunAsync<T>(IConfig? config = null, string[]? args = null, bool isDebug = false, Action<Summary>? onFinish = null, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }
        await Task.Run(() =>
        {

            var summary = Run<T>(config, args, isDebug);
            onFinish?.Invoke(summary);
        },cancellationToken);
    }

    /// <summary>
    /// Runs a benchmark for a specific type on a separate thread.
    /// </summary>
    /// <param name="type">The type to benchmark.</param>
    /// <param name="config">The configuration for the benchmark.</param>
    /// <param name="args">The arguments for the benchmark.</param>
    /// <param name="isDebug">A flag indicating whether the benchmark is in debug mode.</param>
    /// <param name="onFinish">The action to invoke when the benchmark finishes.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the benchmark.</param>
    /// <returns>A boolean indicating whether the benchmark was queued successfully.</returns>
    public static async Task RunAsync(Type type, IConfig? config = null, string[]? args = null, bool isDebug = false, Action<Summary>? onFinish = null, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }
        await Task.Run(() =>
        {

            var summary = Run(type, config, args, isDebug);
            onFinish?.Invoke(summary);
        }, cancellationToken);
    }

    /// <summary>
    /// Runs benchmarks for multiple types on a separate thread.
    /// </summary>
    /// <param name="types">The types to benchmark.</param>
    /// <param name="config">The configuration for the benchmarks.</param>
    /// <param name="args">The arguments for the benchmarks.</param>
    /// <param name="isDebug">A flag indicating whether the benchmarks are in debug mode.</param>
    /// <param name="onFinish">The action to invoke when the benchmarks finish.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the benchmarks.</param>
    /// <returns>A boolean indicating whether the benchmarks were queued successfully.</returns>
    public static async Task RunAsync(Type[] types, IConfig? config = null, string[]? args = null, bool isDebug = false, Action<Summary[]>? onFinish = null, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }
        await Task.Run(() =>
        {

            var summary = Run(types, config, args, isDebug);
            onFinish?.Invoke(summary);
        }, cancellationToken);
    }

    /// <summary>
    /// Runs a benchmark for a specific type and methods on a separate thread.
    /// </summary>
    /// <param name="type">The type to benchmark.</param>
    /// <param name="methods">The methods to benchmark.</param>
    /// <param name="config">The configuration for the benchmark.</param>
    /// <param name="isDebug">A flag indicating whether the benchmark is in debug mode.</param>
    /// <param name="onFinish">The action to invoke when the benchmark finishes.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the benchmark.</param>
    /// <returns>A boolean indicating whether the benchmark was queued successfully.</returns>
    public static async Task RunAsync(Type type, MethodInfo[] methods, IConfig? config = null, bool isDebug = false, Action<Summary>? onFinish = null, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }
        await Task.Run(() =>
        {

            var summary = Run(type, methods, config, isDebug);
            onFinish?.Invoke(summary);
        }, cancellationToken);
    }

    /// <summary>
    /// Runs benchmarks for all types in a specific assembly on a separate thread.
    /// </summary>
    /// <param name="assembly">The assembly to benchmark.</param>
    /// <param name="config">The configuration for the benchmarks.</param>
    /// <param name="args">The arguments for the benchmarks.</param>
    /// <param name="isDebug">A flag indicating whether the benchmarks are in debug mode.</param>
    /// <param name="onFinish">The action to invoke when the benchmarks finish.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the benchmarks.</param>
    /// <returns>A boolean indicating whether the benchmarks were queued successfully.</returns>
    public static async Task RunAsync(Assembly assembly, IConfig? config = null, string[]? args = null, bool isDebug = false, Action<Summary[]>? onFinish = null, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }
        await Task.Run(() =>
        {

            var summary = Run(assembly, config, args, isDebug);
            onFinish?.Invoke(summary);
        }, cancellationToken);
    }
}