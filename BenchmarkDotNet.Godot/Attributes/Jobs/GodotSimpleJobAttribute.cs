using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Godot.Extensions;
using BenchmarkDotNet.Jobs;

namespace BenchmarkDotNet.Godot.Attributes.Jobs;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = true)]
public class GodotSimpleJobAttribute : SimpleJobAttribute
{
    private const int DefaultValue = -1;

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