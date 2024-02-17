using BenchmarkDotNet.Attributes;

namespace BenchmarkDotNet.Godot.Attributes
{
    /// <summary>
    /// Marks method to be executed after each benchmark iteration. This should NOT be used for microbenchmarks - please see the docs.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class GodotIterationCleanupAttribute : IterationCleanupAttribute, ISynchronizationContext
    {
        public bool SynchronizationContext { get; set; }
        public GodotIterationCleanupAttribute(bool synchronizationContext = true)
        {
            SynchronizationContext = synchronizationContext;
        }
    }
}