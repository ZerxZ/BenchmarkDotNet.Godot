using BenchmarkDotNet.Godot.Toolchains.InGodotProcess.NoEmit;
using BenchmarkDotNet.Jobs;

namespace BenchmarkDotNet.Godot.Configs;

/// <summary>
/// config which allows to debug benchmarks running it in the same process
/// </summary>
public class DebugInProcessConfig : DebugConfig
{
    public static readonly DebugInProcessConfig Instance = new DebugInProcessConfig();
    public override IEnumerable<Job> GetJobs()
        => new[]
        {
            Job.Default
                .WithToolchain(
                    new InGodotProcessNoEmitToolchain(
                        TimeSpan.FromHours(1), // 1h should be enough to debug the benchmark
                        true))
        };
}