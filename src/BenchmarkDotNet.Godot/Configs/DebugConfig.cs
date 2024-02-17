using System.Globalization;
using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.EventProcessors;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Filters;
using BenchmarkDotNet.Godot.Loggers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using BenchmarkDotNet.Validators;
using Godot;
using Environment = System.Environment;

namespace BenchmarkDotNet.Godot.Configs;

/// <summary>
/// Represents a base configuration for debugging benchmarks.
/// </summary>
public abstract class DebugConfig : IConfig
{
    private readonly static Conclusion[] emptyConclusion = Array.Empty<Conclusion>();

    /// <summary>
    /// Gets the jobs for the configuration.
    /// </summary>
    /// <returns>The jobs for the configuration.</returns>
    public abstract IEnumerable<Job> GetJobs();

    /// <summary>
    /// Gets the validators for the configuration.
    /// </summary>
    /// <returns>The validators for the configuration.</returns>
    public IEnumerable<IValidator> GetValidators() => Array.Empty<IValidator>();

    /// <summary>
    /// Gets the column providers for the configuration.
    /// </summary>
    /// <returns>The column providers for the configuration.</returns>
    public IEnumerable<IColumnProvider> GetColumnProviders() => DefaultColumnProviders.Instance;

    /// <summary>
    /// Gets the exporters for the configuration.
    /// </summary>
    /// <returns>The exporters for the configuration.</returns>
    public IEnumerable<IExporter> GetExporters()
    {
        yield return CsvExporter.Default;
        yield return MarkdownExporter.GitHub;
        yield return HtmlExporter.Default;
        yield return JsonExporter.FullCompressed;
    }

    /// <summary>
    /// Gets the loggers for the configuration.
    /// </summary>
    /// <returns>The loggers for the configuration.</returns>
    public IEnumerable<ILogger> GetLoggers()
    {
        if (OS.HasFeature("editor"))
        {
            yield return GodotLogger.Default;
        }
        else
        {
            yield return ConsoleLogger.Default;
        }
    }

    /// <summary>
    /// Gets the diagnosers for the configuration.
    /// </summary>
    /// <returns>The diagnosers for the configuration.</returns>
    public IEnumerable<IDiagnoser> GetDiagnosers() => Array.Empty<IDiagnoser>();

    /// <summary>
    /// Gets the analysers for the configuration.
    /// </summary>
    /// <returns>The analysers for the configuration.</returns>
    public IEnumerable<IAnalyser> GetAnalysers() => Array.Empty<IAnalyser>();

    /// <summary>
    /// Gets the hardware counters for the configuration.
    /// </summary>
    /// <returns>The hardware counters for the configuration.</returns>
    public IEnumerable<HardwareCounter> GetHardwareCounters() => Array.Empty<HardwareCounter>();

    /// <summary>
    /// Gets the event processors for the configuration.
    /// </summary>
    /// <returns>The event processors for the configuration.</returns>
    public IEnumerable<EventProcessor> GetEventProcessors() => Array.Empty<EventProcessor>();

    /// <summary>
    /// Gets the filters for the configuration.
    /// </summary>
    /// <returns>The filters for the configuration.</returns>
    public IEnumerable<IFilter> GetFilters() => Array.Empty<IFilter>();

    /// <summary>
    /// Gets the column hiding rules for the configuration.
    /// </summary>
    /// <returns>The column hiding rules for the configuration.</returns>
    public IEnumerable<IColumnHidingRule> GetColumnHidingRules() => Array.Empty<IColumnHidingRule>();

    /// <summary>
    /// Gets the orderer for the configuration.
    /// </summary>
    public IOrderer Orderer => DefaultOrderer.Instance;

    /// <summary>
    /// Gets the category discoverer for the configuration.
    /// </summary>
    public ICategoryDiscoverer? CategoryDiscoverer => DefaultCategoryDiscoverer.Instance;

    /// <summary>
    /// Gets the summary style for the configuration.
    /// </summary>
    public SummaryStyle SummaryStyle => SummaryStyle.Default;

    /// <summary>
    /// Gets the union rule for the configuration.
    /// </summary>
    public ConfigUnionRule UnionRule => ConfigUnionRule.Union;

    /// <summary>
    /// Gets the build timeout for the configuration.
    /// </summary>
    public TimeSpan BuildTimeout => DefaultConfig.Instance.BuildTimeout;

    /// <summary>
    /// Gets the artifacts path for the configuration.
    /// </summary>
    public string ArtifactsPath
    {
        get
        {
            var root = OS.GetName() == "Android" ? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) : Directory.GetCurrentDirectory();
            return Path.Combine(root, "BenchmarkDotNet.Artifacts");
        }
    }

    /// <summary>
    /// Gets the culture info for the configuration.
    /// </summary>
    public CultureInfo CultureInfo => null;

    /// <summary>
    /// Gets the logical group rules for the configuration.
    /// </summary>
    /// <returns>The logical group rules for the configuration.</returns>
    public IEnumerable<BenchmarkLogicalGroupRule> GetLogicalGroupRules() => Array.Empty<BenchmarkLogicalGroupRule>();

    /// <summary>
    /// Gets the options for the configuration.
    /// </summary>
    public ConfigOptions Options => ConfigOptions.KeepBenchmarkFiles | ConfigOptions.DisableOptimizationsValidator;

    /// <summary>
    /// Gets the config analysis conclusion for the configuration.
    /// </summary>
    public IReadOnlyList<Conclusion> ConfigAnalysisConclusion => emptyConclusion;
}