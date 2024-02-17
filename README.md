# BenchmarkDotNet.Godot
![NuGet Version](https://img.shields.io/nuget/v/BenchmarkDotNetGodot?label=BenchmarkDotNetGodot)
![NuGet Downloads](https://img.shields.io/nuget/dt/BenchmarkDotNetGodot)

![NuGet Version](https://img.shields.io/nuget/v/BenchmarkDotNetGodot.GDTask?label=BenchmarkDotNetGodot.GDTask) 
![NuGet Downloads](https://img.shields.io/nuget/dt/BenchmarkDotNetGodot.GDtask)

BenchmarkDotNet.Godot allows developers to easily conduct performance testing and benchmarking within the Godot engine,
enabling them to assess the efficiency of their code and identify potential performance bottlenecks.

# Dependencies

[GDTask.Nuget](https://github.com/Delsin-Yu/GDTask.Nuget)

[BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet)

# Installation via Nuget

For .Net CLI

```
dotnet add package BenchmarkDotNetGodot
dotnet add package BenchmarkDotNet
dotnet add package GDTask
```

For Package Manager Console:

```
NuGet\Install-Package BenchmarkDotNetGodot
NuGet\Install-Package BenchmarkDotNet
NuGet\Install-Package GDTask
```

For Package Reference:

```xml
<ItemGroup>
    <PackageReference Include="BenchmarkDotNetGodot" Version="*" />
    <PackageReference Include="BenchmarkDotNet" Version="*" />
    <PackageReference Include="GDTask" Version="*" />
</ItemGroup>
```

# Basic API usage

```csharp
using Godot;
using BenchmarkDotNet.Godot.Running;

public partial class BenchmarkRun : Node
{

    public override void _Ready()
    {
        GodotBenchmarkRunner.RunWithBBCodeAsync<BenchmarkTest>(isDebug:true);
    }
}
------
using Godot;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Godot.Attributes;
using BenchmarkDotNet.Godot.Attributes.Jobs;
using BenchmarkDotNet.Order;

[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[MemoryDiagnoser, GodotSimpleJob]
public partial class BenchmarkTest : Node
{
    [GodotBenchmark]
    public void TestNode()
    {
        var Parent = new Node();
        AddChild(Parent);
        for (int i = 0; i < 1000; i++)
        {
            var node = new Node();
            Parent.AddChild(node);
        }
        Parent.QueueFree();

    }
    [Benchmark]
    public void TestNodeCallDeferred()
    {
        var Parent = new Node();
        CallDeferred(MethodName.AddChild, Parent);
        for (int i = 0; i < 1000; i++)
        {
            var node = new Node();
            Parent.CallDeferred(MethodName.AddChild, node);
        }
        Parent.CallDeferred(Godot.Node.MethodName.QueueFree);
    }
}
```

# Api

| BenchmarkDotNet Type | BenchmarkDotNet.Godot Type |
|----------------------|----------------------------|
| `DryJob`             | `GodotDryJob`              |
| `SimpleJob`          | `GodotSimpleJob`           |
| `ShortRunJob`        | `GodotShortRunJob`         |
| `MediumRunJob`       | `GodotMediumRunJob`        |
| `LongRunJob`         | `GodotLongRunJob`          |
| `VeryLongRunJob`     | `GodotVeryLongRunJob`      |
