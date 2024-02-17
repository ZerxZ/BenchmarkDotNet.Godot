using BenchmarkDotNet.Jobs;

namespace BenchmarkDotNet.Godot.Configs;

/// <summary>
/// config which allows to build benchmarks in Debug
/// </summary>
public class DebugBuildConfig : DebugConfig
{
    public override IEnumerable<Job> GetJobs()
        => new[]
        {
            Job.Default
                .WithCustomBuildConfiguration("Debug") // will do `-c Debug everywhere`
        };
}