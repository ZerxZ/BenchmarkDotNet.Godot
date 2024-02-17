using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;

namespace BenchmarkDotNet.Godot.Exporters;

/// <summary>
/// Represents a custom exporter for exporting benchmark results in BBCode format.
/// </summary>
public class BBCodeExporter : ExporterBase
{
    /// <summary>
    /// Gets the file extension for the exported file.
    /// </summary>
    protected override string FileExtension => "bbcode";

    /// <summary>
    /// Gets the default instance of the BBCodeExporter.
    /// </summary>
    public static readonly IExporter Default = new BBCodeExporter();

    /// <summary>
    /// Exports the benchmark results to a log.
    /// </summary>
    /// <param name="summary">The summary of the benchmark results.</param>
    /// <param name="logger">The logger to use for exporting the results.</param>
    public override void ExportToLog(Summary summary, ILogger logger) => PrintAll(summary, logger);

    /// <summary>
    /// Prints all benchmark results to the logger.
    /// </summary>
    /// <param name="summary">The summary of the benchmark results.</param>
    /// <param name="logger">The logger to use for printing the results.</param>
    public static void PrintAll(Summary summary, ILogger logger)
    {
        logger.WriteLine($"[center][font_size=24][b]{summary.Title}[/b][/font_size][/center]");
        logger.WriteLine();
        logger.WriteLine("[b]Host Environment Information:[/b]");
        foreach (string infoLine in summary.HostEnvironmentInfo.ToFormattedString())
        {
            logger.WriteLine(infoLine);
        }
        logger.WriteLine(summary.AllRuntimes);
        logger.WriteLine();
        PrintTable(summary.Table, logger);
    }

    /// <summary>
    /// Prints the summary table to the logger.
    /// </summary>
    /// <param name="summaryTable">The summary table to print.</param>
    /// <param name="logger">The logger to use for printing the table.</param>
    private static void PrintTable(SummaryTable summaryTable, ILogger logger)
    {
        if (summaryTable.FullContent.Length == 0)
        {
            logger.WriteLineError("[code]There are no benchmarks found[/code]");
            return;
        }
        summaryTable.PrintCommonColumns(logger);
        logger.WriteLine();
        logger.WriteLine($"[center][table={summaryTable.Columns.Count(column => column.NeedToShow)}]");
        summaryTable.PrintLine(summaryTable.FullHeader, logger, "[cell bg=white border=black][center][color=black]", "[/color][/center][/cell]");
        foreach (var line in summaryTable.FullContent)
        {
            summaryTable.PrintLine(line, logger, "[cell bg=black border=white][fill]  ", "  [/fill][/cell]");
        }
        logger.WriteLine("[/table][/center]");
    }
}