using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Characteristics;
using BenchmarkDotNet.Godot.Toolchains.InGodotProcess.NoEmit;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;

namespace BenchmarkDotNet.Godot.Extensions;

public static class JobAttributeExtensions
{
    public static void ChangeConfigToInProcessNoEmitToolchain(this JobConfigBaseAttribute jobConfig)
    {

        var type  = typeof(CharacteristicObject);
        var field = type.GetField("frozen", BindingFlags.NonPublic | BindingFlags.Instance);
        var owner = type.GetProperty("Owner", BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var job in jobConfig.Config.GetJobs())
        {
            var infrastructure = job.Infrastructure;
            if (infrastructure.Toolchain is InGodotProcessNoEmitToolchain)
            {
                continue;
            }
            var nowOwner = owner?.GetValue(job) as CharacteristicObject;
            if (nowOwner is not null)
            {
                field?.SetValue(nowOwner, false);
            }
            field?.SetValue(job, false);
            infrastructure.Toolchain = InGodotProcessNoEmitToolchain.Instance;
            nowOwner?.Freeze();
            job.Freeze();
        }
    }
}