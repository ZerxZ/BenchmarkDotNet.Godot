using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Godot.Exporters;

namespace BenchmarkDotNet.Godot.Attributes.Exporters;

/// <summary>
/// This attribute is used to export benchmark results to BBCode format.
/// </summary>
public class BBCodeExporterAttribute:ExporterConfigBaseAttribute
{
    /// <summary>
    /// This attribute is used to export benchmark results to BBCode format.
    /// </summary>
    public BBCodeExporterAttribute() : base(BBCodeExporter.Default)
    {
    }
}