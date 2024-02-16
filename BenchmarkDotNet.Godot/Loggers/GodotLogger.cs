using System.Text;
using BenchmarkDotNet.Loggers;
using Godot;

namespace BenchmarkDotNet.Godot.Loggers;

/// <summary>
/// A logger that outputs benchmark results in Godot's rich text format.
/// </summary>
public class GodotLogger : ILogger
{
    private readonly       StringBuilder _sb          = new StringBuilder();
    private const          string        DefaultColor = "gray";
    public static readonly ILogger       Default      = new GodotLogger();

    /// <summary>
    /// Writes a log message of a specific kind.
    /// </summary>
    /// <param name="logKind">The kind of the log message.</param>
    /// <param name="text">The text of the log message.</param>
    public void Write(LogKind logKind, string text) => Write(logKind, _sb.Append, text);

    /// <summary>
    /// Writes a new line.
    /// </summary>
    public void WriteLine() => GD.Print();


    /// <summary>
    /// Writes a log message of a specific kind and a new line.
    /// </summary>
    /// <param name="logKind">The kind of the log message.</param>
    /// <param name="text">The text of the log message.</param>
    public void WriteLine(LogKind logKind, string text) => Write(logKind, _sb.AppendLine, text);

    /// <summary>
    /// Flushes the logger.
    /// </summary>
    public void Flush()
    {
        GD.PrintRich(_sb.ToString());
        _sb.Clear();
    }

    private static void Write(LogKind logKind, Func<string, StringBuilder> write, string text)
    {
        var color = GetColor(logKind);
        write($"[color={color}]{text}[/color]");
    }

    private static string GetColor(LogKind logKind) =>
        ColorScheme.GetValueOrDefault(logKind, DefaultColor);

    /// <summary>
    /// A dictionary that maps log kinds to color names.
    /// </summary>
    private static readonly Dictionary<LogKind, string> ColorScheme =
        new Dictionary<LogKind, string>
        {
            { LogKind.Default, nameof(Colors.Gray).ToLower() },
            { LogKind.Help, nameof(Colors.Green).ToLower() },
            { LogKind.Header, nameof(Colors.Magenta).ToLower() },
            { LogKind.Result, nameof(Colors.Blue).ToLower() },
            { LogKind.Statistic, nameof(Colors.Cyan).ToLower() },
            { LogKind.Info, nameof(Colors.Yellow).ToLower() },
            { LogKind.Error, nameof(Colors.Red).ToLower() },
            { LogKind.Hint, nameof(Colors.Blue).ToLower() }
        };

    /// <summary>
    /// Gets the ID of the logger.
    /// </summary>
    public string Id => nameof(GodotLogger);

    /// <summary>
    /// Gets the priority of the logger.
    /// </summary>
    public int Priority => 0;
}