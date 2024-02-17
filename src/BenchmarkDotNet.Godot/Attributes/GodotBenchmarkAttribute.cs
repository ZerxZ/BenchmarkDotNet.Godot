using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Godot.Helper;

namespace BenchmarkDotNet.Godot.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class GodotBenchmarkAttribute : BenchmarkAttribute, ISynchronizationContext
{
    public bool SynchronizationContext { get; set; }

    public GodotBenchmarkAttribute(bool synchronizationContext = true, [CallerLineNumber] int sourceCodeLineNumber = 0, [CallerFilePath] string sourceCodeFile = "") : base(sourceCodeLineNumber, sourceCodeFile)
    {
        SynchronizationContext = synchronizationContext;
    }
}