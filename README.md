# BenchmarkDotNet.Godot

BenchmarkDotNet.Godot allows developers to easily conduct performance testing and benchmarking within the Godot engine, enabling them to assess the efficiency of their code and identify potential performance bottlenecks.

# Dependency

[GDTask.Nuget](https://github.com/Delsin-Yu/GDTask.Nuget)

[BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet)

# Installation via Nuget

For .Net CLI

```
dotnet add package BenchmarkDotNetGodot
```

For Package Manager Console:

```
NuGet\Install-Package BenchmarkDotNetGodot
```

# Basic API usage

```csharp
using Godot;
using System.Linq;
using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Godot.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using GodotTask.Tasks;
public partial class BenchmarkRun : Node
{

    public override async void _Ready()
    {
        var logger = new AccumulationLogger();
        await GDTask.RunOnThreadPool(() =>
        {
            var config =DefaultConfig.Instance.WithOptions(ConfigOptions.DisableOptimizationsValidator);
#if DEBUG
            config = new DebugInProcessConfig().WithOptions(ConfigOptions.DisableOptimizationsValidator);
#endif
            var summary = BenchmarkRunner.Run<BenchmarkGodot>(config);
            BBCodeExporter.Default.ExportToLog(summary, logger);
            ConclusionHelper.Print(logger,
                summary.BenchmarksCases
                    .SelectMany(benchmark => benchmark.Config.GetCompositeAnalyser().Analyse(summary))
                    .Distinct()
                    .ToList());
        });
        var text = logger.GetLog();
        GD.PrintRich(text);
    }
}
```

```csharp
using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Godot.Attributes.Jobs;
using BenchmarkDotNet.Order;
using Godot;


[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[MemoryDiagnoser, GodotSimpleJob]
public class BenchmarkGodot
{
    private static Window _root;
    public static  Window Root => _root ??= ((SceneTree)Engine.GetMainLoop()).Root;

    [Benchmark]
    public void TestNode()
    {
        SyncTest(_TestNode);
    }
    public static List<Node> NodeList = new List<Node>();

    public void _TestNode()
    {
        var root = Root;

        for (int i = 0; i < 1000; i++)
        {
            var node = new Node();
            NodeList.Add(node);
            root.AddChild(node);
        }
        foreach (var node in NodeList)
        {
            node.QueueFree();
        }
        NodeList.Clear();

    }
    public static void SyncTest(Action action)
    {
        Dispatcher.SynchronizationContext?.Send(_ =>
        {
            action();
        }, null);
    }
}
```

# Api

| BenchmarkDotNet Type | BenchmarkDotNet.Godot Type |
| -------------------- | -------------------------- |
| `DryJob`             | `GodotDryJob`              |
| `SimpleJob`          | `GodotSimpleJob`           |
| `ShortRunJob`        | `GodotShortRunJob`         |
| `MediumRunJob`       | `GodotMediumRunJob`        |
| `LongRunJob`         | `GodotLongRunJob`          |
| `VeryLongRunJob`     | `GodotVeryLongRunJob`      |
