using BenchmarkDotNet.Attributes;

namespace BenchmarkDotNet.Godot.Attributes
{
    /// <summary>
    /// Marks method to be executed before each benchmark iteration. This should NOT be used for microbenchmarks - please see the docs.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class GodotIterationSetupAttribute : IterationSetupAttribute, ISynchronizationContext
    {
        public bool SynchronizationContext { get; set; }
        public GodotIterationSetupAttribute(bool synchronizationContext = true)
        {
            SynchronizationContext = synchronizationContext;
        }
    }
}