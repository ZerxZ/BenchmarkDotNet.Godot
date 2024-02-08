using System.ComponentModel;
using System.Reflection;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Godot.Configs;
using BenchmarkDotNet.Godot.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Godot;

namespace BenchmarkDotNet.Godot.Running;

public static partial class GodotBenchmarkRunner
{
    public static bool EditorMode = true;
    private static void SetGodotConfig(ref IConfig? config, bool isDebug = false)
    {
        config ??= isDebug ? GodotConfig.DebugInProcessConfig : GodotConfig.DefaultConfig;
        if (!config.Options.HasFlag(ConfigOptions.DisableOptimizationsValidator))
        {
            config = config.WithOptions(ConfigOptions.DisableOptimizationsValidator);
        }

    }
    private static void Callable(this Action<Summary, string>? onCallback, Summary[] summaries)
    {

        var logger = new AccumulationLogger();
        foreach (var summary in summaries)
        {
            BBCodeExporter.Default.ExportToLog(summary, logger);
            var log = logger.GetLog();
            onCallback?.Invoke(summary, log);
            if (EditorMode)
            {
                GD.PrintRich(log);
            }

            logger.ClearLog();
        }
    }
    private static void Callable(this Action<string>? onCallback, Summary summary)
    {
        var logger = new AccumulationLogger();
        BBCodeExporter.Default.ExportToLog(summary, logger);
        var log = logger.GetLog();
        onCallback?.Invoke(log);
        if (EditorMode)
        {
            GD.PrintRich(log);
        }
        logger.ClearLog();
    }

    

}