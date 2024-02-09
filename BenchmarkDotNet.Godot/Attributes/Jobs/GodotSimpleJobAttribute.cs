using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Godot.Extensions;
using BenchmarkDotNet.Jobs;

namespace BenchmarkDotNet.Godot.Attributes.Jobs;


/// <summary>
/// Represents a custom attribute for defining a simple job configuration for running benchmarks in Godot.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = true)]
public class GodotSimpleJobAttribute : SimpleJobAttribute
{
    private const int DefaultValue = -1;

    /// <summary>
    /// Initializes a new instance of the <see cref="GodotSimpleJobAttribute"/> class with the specified parameters.
    /// </summary>
    /// <param name="launchCount">The number of times the benchmark process will be launched.</param>
    /// <param name="warmupCount">The number of warmup iterations to perform.</param>
    /// <param name="iterationCount">The number of benchmark iterations to perform.</param>
    /// <param name="invocationCount">The number of benchmark method invocations per iteration.</param>
    /// <param name="id">The id of the job.</param>
    /// <param name="baseline">A value indicating whether the job should be used as a baseline.</param>
    public GodotSimpleJobAttribute(
        int     launchCount     = DefaultValue,
        int     warmupCount     = DefaultValue,
        int     iterationCount  = DefaultValue,
        int     invocationCount = DefaultValue,
        string? id              = null,
        bool    baseline        = false
    ) : base(launchCount, warmupCount, iterationCount, invocationCount, id, baseline)
    {
        this.ChangeConfigToInProcessEmitToolchain();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GodotSimpleJobAttribute"/> class with the specified parameters.
    /// </summary>
    /// <param name="runStrategy">The run strategy of the job.</param>
    /// <param name="launchCount">The number of times the benchmark process will be launched.</param>
    /// <param name="warmupCount">The number of warmup iterations to perform.</param>
    /// <param name="iterationCount">The number of benchmark iterations to perform.</param>
    /// <param name="invocationCount">The number of benchmark method invocations per iteration.</param>
    /// <param name="id">The id of the job.</param>
    /// <param name="baseline">A value indicating whether the job should be used as a baseline.</param>
    public GodotSimpleJobAttribute(
        RunStrategy runStrategy,
        int         launchCount     = DefaultValue,
        int         warmupCount     = DefaultValue,
        int         iterationCount  = DefaultValue,
        int         invocationCount = DefaultValue,
        string?     id              = null,
        bool        baseline        = false
    ) : base(runStrategy, launchCount, warmupCount, iterationCount, invocationCount, id, baseline)
    {
        this.ChangeConfigToInProcessEmitToolchain();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GodotSimpleJobAttribute"/> class with the specified parameters.
    /// </summary>
    /// <param name="runtimeMoniker">The runtime to use for the job.</param>
    /// <param name="launchCount">The number of times the benchmark process will be launched.</param>
    /// <param name="warmupCount">The number of warmup iterations to perform.</param>
    /// <param name="iterationCount">The number of benchmark iterations to perform.</param>
    /// <param name="invocationCount">The number of benchmark method invocations per iteration.</param>
    /// <param name="id">The id of the job.</param>
    /// <param name="baseline">A value indicating whether the job should be used as a baseline.</param>
    public GodotSimpleJobAttribute(
        RuntimeMoniker runtimeMoniker,
        int            launchCount     = DefaultValue,
        int            warmupCount     = DefaultValue,
        int            iterationCount  = DefaultValue,
        int            invocationCount = DefaultValue,
        string?        id              = null,
        bool           baseline        = false
    ) : base(runtimeMoniker, launchCount, warmupCount, iterationCount, invocationCount, id, baseline)
    {
        this.ChangeConfigToInProcessEmitToolchain();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GodotSimpleJobAttribute"/> class with the specified parameters.
    /// </summary>
    /// <param name="runStrategy">The run strategy of the job.</param>
    /// <param name="runtimeMoniker">The runtime to use for the job.</param>
    /// <param name="launchCount">The number of times the benchmark process will be launched.</param>
    /// <param name="warmupCount">The number of warmup iterations to perform.</param>
    /// <param name="iterationCount">The number of benchmark iterations to perform.</param>
    /// <param name="invocationCount">The number of benchmark method invocations per iteration.</param>
    /// <param name="id">The id of the job.</param>
    /// <param name="baseline">A value indicating whether the job should be used as a baseline.</param>
    public GodotSimpleJobAttribute(
        RunStrategy    runStrategy,
        RuntimeMoniker runtimeMoniker,
        int            launchCount     = DefaultValue,
        int            warmupCount     = DefaultValue,
        int            iterationCount  = DefaultValue,
        int            invocationCount = DefaultValue,
        string?        id              = null,
        bool           baseline        = false
    ) : base(runStrategy, runtimeMoniker, launchCount, warmupCount, iterationCount, invocationCount, id, baseline)
    {
        this.ChangeConfigToInProcessEmitToolchain();
    }
}