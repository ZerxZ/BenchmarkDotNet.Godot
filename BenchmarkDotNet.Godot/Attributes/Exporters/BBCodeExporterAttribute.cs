using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Godot.Exporters;

namespace BenchmarkDotNet.Godot.Attributes.Exporters;

public class BBCodeExporterAttribute:ExporterConfigBaseAttribute
{
    public BBCodeExporterAttribute() : base(BBCodeExporter.Default)
    {
    }
}