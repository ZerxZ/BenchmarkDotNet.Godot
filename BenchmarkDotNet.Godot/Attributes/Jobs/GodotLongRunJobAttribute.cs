using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Godot.Extensions;
using BenchmarkDotNet.Jobs;

namespace BenchmarkDotNet.Godot.Attributes.Jobs;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = true)]
public class GodotLongRunJobAttribute:  LongRunJobAttribute
{
    public GodotLongRunJobAttribute() : base()
    {
        this.ChangeConfigToInProcessEmitToolchain();
    }

    /// <summary>
    /// defines a new LongRun Job that targets specified Framework
    /// </summary>
    /// <param name="runtimeMoniker">Target Framework to test.</param>
    public GodotLongRunJobAttribute(RuntimeMoniker runtimeMoniker)
        : base(runtimeMoniker)
    {
        this.ChangeConfigToInProcessEmitToolchain();
    }

    /// <summary>
    /// defines a new LongRun Job that targets specified Framework, JIT and Platform
    /// </summary>
    /// <param name="runtimeMoniker">Target Framework to test.</param>
    /// <param name="jit">Jit to test.</param>
    /// <param name="platform">Platform to test.</param>
    public GodotLongRunJobAttribute(RuntimeMoniker runtimeMoniker, Jit jit, Platform platform)
        : base(runtimeMoniker, jit, platform)
    {
        this.ChangeConfigToInProcessEmitToolchain();
    }
}