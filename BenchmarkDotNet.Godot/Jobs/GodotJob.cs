using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

namespace BenchmarkDotNet.Godot.Jobs;

public static class GodotJob
{
    public static readonly Job GodotDry = CreateJob(nameof(GodotDry), RunMode.Dry);

    public static readonly Job GodotShortRun    = CreateJob(nameof(GodotShortRun),    RunMode.Short);
    public static readonly Job GodotMediumRun   = CreateJob(nameof(GodotMediumRun),   RunMode.Medium);
    public static readonly Job GodotLongRun     = CreateJob(nameof(GodotLongRun),     RunMode.Long);
    public static readonly Job GodotVeryLongRun = CreateJob(nameof(GodotVeryLongRun), RunMode.VeryLong);
    private static         Job CreateJob(string id, RunMode runMode) => new Job(id, runMode).WithToolchain(InProcessEmitToolchain.Instance).Freeze();
}