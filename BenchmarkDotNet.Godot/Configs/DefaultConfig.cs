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
using BenchmarkDotNet.Validators;
using Godot;
using Environment = System.Environment;

namespace BenchmarkDotNet.Godot.Configs;

/// <summary>
/// Default configuration for running benchmarks in Godot.
/// </summary>
public class DefaultConfig : IConfig
{
    public static readonly  IConfig      Instance        = new DefaultConfig();
    private readonly static Conclusion[] emptyConclusion = Array.Empty<Conclusion>();

    private DefaultConfig()
    {
    }

    /// <summary>
    /// Gets the column providers for the benchmark.
    /// </summary>
    /// <returns>The column providers for the benchmark.</returns>
    public IEnumerable<IColumnProvider> GetColumnProviders() => DefaultColumnProviders.Instance;

    /// <summary>
    /// Gets the exporters for the benchmark.
    /// </summary>
    /// <returns>The exporters for the benchmark.</returns>
    public IEnumerable<IExporter> GetExporters()
    {
        yield return CsvExporter.Default;
        yield return MarkdownExporter.GitHub;
        yield return HtmlExporter.Default;
        yield return JsonExporter.FullCompressed;
    }

    /// <summary>
    /// Gets the loggers for the benchmark.
    /// </summary>
    /// <returns>The loggers for the benchmark.</returns>
    public IEnumerable<ILogger> GetLoggers()
    {
        yield return GodotLogger.Default;
    }

    /// <summary>
    /// Gets the analysers for the benchmark.
    /// </summary>
    /// <returns>The analysers for the benchmark.</returns>
    public IEnumerable<IAnalyser> GetAnalysers()
    {
        yield return EnvironmentAnalyser.Default;
        yield return OutliersAnalyser.Default;
        yield return MinIterationTimeAnalyser.Default;
        yield return MultimodalDistributionAnalyzer.Default;
        yield return RuntimeErrorAnalyser.Default;
        yield return ZeroMeasurementAnalyser.Default;
        yield return BaselineCustomAnalyzer.Default;
        yield return HideColumnsAnalyser.Default;
    }

    /// <summary>
    /// Gets the validators for the benchmark.
    /// </summary>
    /// <returns>The validators for the benchmark.</returns>
    public IEnumerable<IValidator> GetValidators()
    {
        yield return BaselineValidator.FailOnError;
        yield return SetupCleanupValidator.FailOnError;
#if !DEBUG
        yield return JitOptimizationsValidator.FailOnError;
#endif
        yield return RunModeValidator.FailOnError;
        yield return GenericBenchmarksValidator.DontFailOnError;
        yield return DeferredExecutionValidator.FailOnError;
        yield return ParamsAllValuesValidator.FailOnError;
        yield return ParamsValidator.FailOnError;
    }

    public IOrderer             Orderer            => null;
    public ICategoryDiscoverer? CategoryDiscoverer => null;

    public ConfigUnionRule UnionRule => ConfigUnionRule.Union;

    public CultureInfo CultureInfo => null;

    public ConfigOptions Options => ConfigOptions.Default;

    public SummaryStyle SummaryStyle => SummaryStyle.Default;

    public TimeSpan BuildTimeout => TimeSpan.FromSeconds(120);

    /// <summary>
    /// Gets the path to the artifacts of the benchmark.
    /// </summary>
    public string ArtifactsPath
    {
        get
        {
            var root = OS.GetName() == "Android" ? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) : Directory.GetCurrentDirectory();
            return Path.Combine(root, "BenchmarkDotNet.Artifacts");
        }
    }

    public IReadOnlyList<Conclusion> ConfigAnalysisConclusion => emptyConclusion;

    public IEnumerable<Job> GetJobs() => Array.Empty<Job>();

    public IEnumerable<BenchmarkLogicalGroupRule> GetLogicalGroupRules() => Array.Empty<BenchmarkLogicalGroupRule>();

    public IEnumerable<IDiagnoser> GetDiagnosers() => Array.Empty<IDiagnoser>();

    public IEnumerable<HardwareCounter> GetHardwareCounters() => Array.Empty<HardwareCounter>();

    public IEnumerable<IFilter> GetFilters() => Array.Empty<IFilter>();

    public IEnumerable<EventProcessor> GetEventProcessors() => Array.Empty<EventProcessor>();

    public IEnumerable<IColumnHidingRule> GetColumnHidingRules() => Array.Empty<IColumnHidingRule>();
}