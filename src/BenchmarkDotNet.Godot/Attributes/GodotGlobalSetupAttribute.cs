using BenchmarkDotNet.Attributes;

namespace BenchmarkDotNet.Godot.Attributes
{
    /// <summary>
    /// Marks method to be executed before all benchmark iterations.
    /// <remarks>It's going to be executed only once, just before warm up.</remarks>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class GodotGlobalSetupAttribute : GlobalSetupAttribute, ISynchronizationContext
    {
        public bool SynchronizationContext { get; set; }
        public GodotGlobalSetupAttribute(bool synchronizationContext = true)
        {
            SynchronizationContext = synchronizationContext;
        }
    }
}