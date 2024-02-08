using BenchmarkDotNet.Configs;

namespace BenchmarkDotNet.Godot.Configs;

/// <summary>
/// Configurations for Godot
/// </summary>
public static class GodotConfig
{
    /// <summary>
    /// Default Config for Godot
    /// </summary>
    public static readonly IConfig DefaultConfig = BenchmarkDotNet.Configs.DefaultConfig.Instance.WithOptions(ConfigOptions.DisableOptimizationsValidator);
    /// <summary>
    /// Debug InProcess Config for Godot
    /// </summary>
    public static readonly IConfig DebugInProcessConfig = new DebugInProcessConfig().WithOptions(ConfigOptions.DisableOptimizationsValidator);
}