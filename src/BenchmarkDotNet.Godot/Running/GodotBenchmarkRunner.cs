using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Godot.Configs;
using BenchmarkDotNet.Godot.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using Godot;

namespace BenchmarkDotNet.Godot.Running;

public static partial class GodotBenchmarkRunner
{
    public static readonly  bool               EditorMode = OS.HasFeature("editor");
    private static readonly AccumulationLogger Logger     = new AccumulationLogger();
    private static void SetGodotConfig(ref IConfig? config, bool isDebug = false)
    {
        config ??= isDebug ? GodotConfig.DebugInProcessConfig : GodotConfig.DefaultConfig;
        // if (!config.Options.HasFlag(ConfigOptions.DisableOptimizationsValidator))
        // {
        //     config = config.WithOptions(ConfigOptions.DisableOptimizationsValidator);
        // }
    }
    private static void Callable(Action<Summary, string>? onCallback, Summary[] summaries)
    {

        var logger = Logger;
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
    private static void Callable(Action<string>? onCallback, Summary summary)
    {
        var logger = Logger;
        BBCodeExporter.Default.ExportToLog(summary, logger);
        var log = Logger.GetLog();
        onCallback?.Invoke(log);
        if (EditorMode)
        {
            GD.PrintRich(log);
        }
        logger.ClearLog();
    }
}