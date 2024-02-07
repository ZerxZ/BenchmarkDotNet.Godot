﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Godot.Extensions;
using BenchmarkDotNet.Jobs;

namespace BenchmarkDotNet.Godot.Attributes.Jobs;

public class GodotVeryLongRunJobAttribute : VeryLongRunJobAttribute
{
    public GodotVeryLongRunJobAttribute() 
    {
        this.ChangeConfigToInProcessEmitToolchain();
    }

    /// <summary>
    /// defines a new VeryLongRun Job that targets specified Framework
    /// </summary>
    /// <param name="runtimeMoniker">Target Framework to test.</param>
    public GodotVeryLongRunJobAttribute(RuntimeMoniker runtimeMoniker)
        : base(runtimeMoniker)
    {
        this.ChangeConfigToInProcessEmitToolchain();
    }

    /// <summary>
    /// defines a new VeryLongRun Job that targets specified Framework, JIT and Platform
    /// </summary>
    /// <param name="runtimeMoniker">Target Framework to test.</param>
    /// <param name="jit">Jit to test.</param>
    /// <param name="platform">Platform to test.</param>
    public GodotVeryLongRunJobAttribute(RuntimeMoniker runtimeMoniker, Jit jit, Platform platform)
        : base(runtimeMoniker, jit, platform)
    {
        this.ChangeConfigToInProcessEmitToolchain();
    }
}