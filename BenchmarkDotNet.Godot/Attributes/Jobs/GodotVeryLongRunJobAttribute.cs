using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Godot.Extensions;
using BenchmarkDotNet.Jobs;

namespace BenchmarkDotNet.Godot.Attributes.Jobs;

/// <summary>
/// Represents a custom attribute for defining a very long run job configuration for running benchmarks in Godot.
/// This attribute is used to decorate benchmark methods.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = true)]
public class GodotVeryLongRunJobAttribute : VeryLongRunJobAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GodotVeryLongRunJobAttribute"/> class.
    /// This constructor creates a job with the default configuration.
    /// </summary>
    public GodotVeryLongRunJobAttribute()
    {
        this.ChangeConfigToInProcessEmitToolchain();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GodotVeryLongRunJobAttribute"/> class with the specified runtime moniker.
    /// This constructor creates a job that targets the specified framework.
    /// </summary>
    /// <param name="runtimeMoniker">The target framework to test.</param>
    public GodotVeryLongRunJobAttribute(RuntimeMoniker runtimeMoniker)
        : base(runtimeMoniker)
    {
        this.ChangeConfigToInProcessEmitToolchain();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GodotVeryLongRunJobAttribute"/> class with the specified runtime moniker, JIT, and platform.
    /// This constructor creates a job that targets the specified framework, JIT, and platform.
    /// </summary>
    /// <param name="runtimeMoniker">The target framework to test.</param>
    /// <param name="jit">The JIT to test.</param>
    /// <param name="platform">The platform to test.</param>
    public GodotVeryLongRunJobAttribute(RuntimeMoniker runtimeMoniker, Jit jit, Platform platform)
        : base(runtimeMoniker, jit, platform)
    {
        this.ChangeConfigToInProcessEmitToolchain();
    }
}