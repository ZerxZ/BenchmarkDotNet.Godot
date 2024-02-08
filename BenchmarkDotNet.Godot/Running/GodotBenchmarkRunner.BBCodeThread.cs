using System.Reflection;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;

namespace BenchmarkDotNet.Godot.Running;

public partial class GodotBenchmarkRunner
{
    public static Summary RunWithBBCodeThread<T>(IConfig? config = null, string[]? args = null, Action<string>? onCallback = null, bool isDebug = false, Action<Summary>? onFinish = null, CancellationToken cancellationToken = default)
    {
        
        var summary = Run<T>(config, args, isDebug);
        onCallback?.Callable(summary);
        return summary;
    }


    public static Summary RunWithBBCodeThread(Type type, IConfig? config = null, string[]? args = null, Action<string>? onCallback = null, bool isDebug = false, Action<Summary>? onFinish = null, CancellationToken cancellationToken = default)
    {

        var summary = Run(type, config, args, isDebug);
        onCallback?.Callable(summary);
        return summary;
    }


    public static Summary[] RunWithBBCodeThread(Type[] types, IConfig? config = null, string[]? args = null, Action<Summary, string>? onCallback = null, bool isDebug = false, Action<Summary>? onFinish = null, CancellationToken cancellationToken = default)
    {

        var summaries = Run(types, config, args, isDebug);
        onCallback?.Callable(summaries);
        return summaries;
    }


    public static Summary RunWithBBCodeThread(Type type, MethodInfo[] methods, IConfig? config = null, Action<string>? onCallback = null, bool isDebug = false, Action<Summary>? onFinish = null, CancellationToken cancellationToken = default)
    {

        var summary = Run(type, methods, config, isDebug);
        onCallback?.Callable(summary);
        return summary;
    }

    public static Summary[] RunWithBBCodeThread(Assembly assembly, IConfig? config = null, string[]? args = null, Action<Summary, string>? onCallback = null, bool isDebug = false, Action<Summary>? onFinish = null, CancellationToken cancellationToken = default)
    {

        var summaries = Run(assembly, config, args, isDebug);
        onCallback?.Callable(summaries);
        return summaries;
    }
}