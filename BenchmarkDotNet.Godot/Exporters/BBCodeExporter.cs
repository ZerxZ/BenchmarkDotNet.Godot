using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;

namespace BenchmarkDotNet.Godot.Exporters;

public class BBCodeExporter : ExporterBase
{
    protected override     string    FileExtension => "bbcode";
    public static readonly IExporter Default = new BBCodeExporter();
    public override        void      ExportToLog(Summary summary, ILogger logger) => PrintAll(summary, logger);
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