using BenchmarkDotNet.Attributes;

namespace BenchmarkDotNet.Godot.Attributes
{
    /// <summary>
    /// Marks method to be executed after all benchmark iterations.
    /// <remarks>It's going to be executed only once, after all benchmark runs.</remarks>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class GodotGlobalCleanupAttribute : GlobalCleanupAttribute, ISynchronizationContext
    {
        public bool SynchronizationContext { get; set; }
        public GodotGlobalCleanupAttribute(bool synchronizationContext = true)
        {
            SynchronizationContext = synchronizationContext;
        }
    }
}