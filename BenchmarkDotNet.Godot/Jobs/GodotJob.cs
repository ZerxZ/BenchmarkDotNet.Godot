using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

namespace BenchmarkDotNet.Godot.Jobs;

/// <summary>
/// Provides predefined Job configurations for running benchmarks in Godot.
/// </summary>
public static class GodotJob
{
    /// <summary>
    /// A Job configuration for a dry run in Godot.
    /// </summary>
    public static readonly Job GodotDry = CreateJob(nameof(GodotDry), RunMode.Dry);

    /// <summary>
    /// A Job configuration for a short run in Godot.
    /// </summary>
    public static readonly Job GodotShortRun = CreateJob(nameof(GodotShortRun), RunMode.Short);

    /// <summary>
    /// A Job configuration for a medium run in Godot.
    /// </summary>
    public static readonly Job GodotMediumRun = CreateJob(nameof(GodotMediumRun), RunMode.Medium);

    /// <summary>
    /// A Job configuration for a long run in Godot.
    /// </summary>
    public static readonly Job GodotLongRun = CreateJob(nameof(GodotLongRun), RunMode.Long);

    /// <summary>
    /// A Job configuration for a very long run in Godot.
    /// </summary>
    public static readonly Job GodotVeryLongRun = CreateJob(nameof(GodotVeryLongRun), RunMode.VeryLong);

    /// <summary>
    /// Creates a new Job with the specified id and run mode, using the InProcessEmitToolchain.
    /// </summary>
    /// <param name="id">The id of the Job.</param>
    /// <param name="runMode">The run mode of the Job.</param>
    /// <returns>A new Job with the specified id and run mode, using the InProcessEmitToolchain.</returns>
    private static Job CreateJob(string id, RunMode runMode) => new Job(id, runMode).WithToolchain(InProcessEmitToolchain.Instance).Freeze();
}