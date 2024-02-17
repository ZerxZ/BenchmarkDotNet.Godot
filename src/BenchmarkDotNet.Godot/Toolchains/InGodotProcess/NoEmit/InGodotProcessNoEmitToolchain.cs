using BenchmarkDotNet.Characteristics;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains;
using BenchmarkDotNet.Toolchains.InProcess;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using BenchmarkDotNet.Validators;

namespace BenchmarkDotNet.Godot.Toolchains.InGodotProcess.NoEmit;

public class InGodotProcessNoEmitToolchain : IToolchain
{
    /// <summary>The default toolchain instance.</summary>
    public static readonly IToolchain Instance = new InGodotProcessNoEmitToolchain(true);

    /// <summary>The toolchain instance without output logging.</summary>
    public static readonly IToolchain DontLogOutput = new InGodotProcessNoEmitToolchain(false);

    /// <summary>Initializes a new instance of the <see cref="InProcessNoEmitToolchain" /> class.</summary>
    /// <param name="logOutput"><c>true</c> if the output should be logged.</param>
    public InGodotProcessNoEmitToolchain(bool logOutput) : this(
        InGodotProcessNoEmitExecutor.DefaultTimeout,
        logOutput)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="InProcessNoEmitToolchain" /> class.</summary>
    /// <param name="timeout">Timeout for the run.</param>
    /// <param name="logOutput"><c>true</c> if the output should be logged.</param>
    public InGodotProcessNoEmitToolchain(TimeSpan timeout, bool logOutput)
    {
        Generator = new InProcessNoEmitGenerator();
        Builder = new InProcessNoEmitBuilder();
        Executor = new InGodotProcessNoEmitExecutor(timeout, logOutput);
    }

    public IEnumerable<ValidationError> Validate(BenchmarkCase benchmarkCase, IResolver resolver) =>
        InGodotProcessValidator.Validate(benchmarkCase);

    /// <summary>Name of the toolchain.</summary>
    /// <value>The name of the toolchain.</value>
    public string Name => nameof(InGodotProcessNoEmitToolchain);

    /// <summary>The generator.</summary>
    /// <value>The generator.</value>
    public IGenerator Generator { get; }

    /// <summary>The builder.</summary>
    /// <value>The builder.</value>
    public IBuilder Builder { get; }

    /// <summary>The executor.</summary>
    /// <value>The executor.</value>
    public IExecutor Executor { get; }

    public bool IsInProcess => true;

    /// <summary>Returns a <see cref="string" /> that represents this instance.</summary>
    /// <returns>A <see cref="string" /> that represents this instance.</returns>
    public override string ToString() => GetType().Name;
}