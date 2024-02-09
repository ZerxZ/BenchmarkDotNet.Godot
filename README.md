# BenchmarkDotNet.Godot

BenchmarkDotNet.Godot allows developers to easily conduct performance testing and benchmarking within the Godot engine, enabling them to assess the efficiency of their code and identify potential performance bottlenecks.

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
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Godot.Attributes.Jobs;
using BenchmarkDotNet.Godot.Running;
using BenchmarkDotNet.Order;

public partial class BenchmarkRun : Node
{

    public override void _Ready()
    {
        GodotBenchmarkRunner.RunWithBBCodeThread<BenchmarkTest>();
    }
}

[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[MemoryDiagnoser, GodotSimpleJob]
public  class BenchmarkTest
{
    [Params(1000, 10000)]
    public int N;

    private int[] data;

    [GlobalSetup]
    public void Setup()
    {
        data = new int[N];
    }

    [Benchmark]
    public int Sum()
    {
        return data.Sum();
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
