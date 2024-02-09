using BenchmarkDotNet.Loggers;
using Godot;

namespace BenchmarkDotNet.Godot.Loggers;

/// <summary>
/// A logger that outputs benchmark results in Godot's rich text format.
/// </summary>
public class GodotLogger : ILogger
{
    private const string DefaultColor = nameof(Colors.Gray);
    public static readonly ILogger Default = new GodotLogger();

    /// <summary>
    /// Writes a log message of a specific kind.
    /// </summary>
    /// <param name="logKind">The kind of the log message.</param>
    /// <param name="text">The text of the log message.</param>
    public void Write(LogKind logKind, string text) => Write(logKind, GD.PrintRich, text);

    /// <summary>
    /// Writes a new line.
    /// </summary>
    public void WriteLine() => GD.Print();

    private static void PrintRich(string text) => GD.PrintRich(text + "\n");

    /// <summary>
    /// Writes a log message of a specific kind and a new line.
    /// </summary>
    /// <param name="logKind">The kind of the log message.</param>
    /// <param name="text">The text of the log message.</param>
    public void WriteLine(LogKind logKind, string text) => Write(logKind, PrintRich, text);

    /// <summary>
    /// Flushes the logger. This method is not implemented and does nothing.
    /// </summary>
    public void Flush()
    {
        // Do nothing
    }

    private static void Write(LogKind logKind, Action<string> write, string text)
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
            { LogKind.Default, nameof(Colors.Gray) },
            { LogKind.Help, nameof(Colors.DarkGreen) },
            { LogKind.Header, nameof(Colors.Magenta) },
            { LogKind.Result, nameof(Colors.DarkCyan) },
            { LogKind.Statistic, nameof(Colors.Cyan) },
            { LogKind.Info, nameof(Colors.Yellow) },
            { LogKind.Error, nameof(Colors.Red) },
            { LogKind.Hint, nameof(Colors.DarkCyan) }
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