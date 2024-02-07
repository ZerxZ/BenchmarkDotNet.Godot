using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Characteristics;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

namespace BenchmarkDotNet.Godot.Extensions;

internal static class JobAttributeExtensions
{
    internal static void ChangeConfigToInProcessEmitToolchain(this JobConfigBaseAttribute jobConfig)
    {
        var type  = typeof(CharacteristicObject);
        var field = type.GetField("frozen", BindingFlags.NonPublic | BindingFlags.Instance);
        var owner = type.GetProperty("Owner", BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var job in jobConfig.Config.GetJobs())
        {
            var nowOwner = owner?.GetValue(job) as CharacteristicObject;
            if (nowOwner is not null)
            {
                field?.SetValue(job, false);
            }
            field?.SetValue(job, false);
            job.Infrastructure.Toolchain = InProcessEmitToolchain.Instance;
            if (nowOwner is not null)
            {
                nowOwner.Freeze();
            }
            job.Freeze();
        }
    }
}