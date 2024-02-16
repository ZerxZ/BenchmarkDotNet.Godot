using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Godot.Helper;
using Godot;

namespace BenchmarkDotNet.Godot.Benchmark;

public class GodotBenchmark<TNode> where TNode : Node
{
    public Window RootWindow { get; private set; }
    public virtual void GlobalSetup()
    {
    }
    public virtual void IterationSetup()
    {
    }
    public virtual void IterationCleanup()
    {
    }
    public virtual void GlobalCleanup()
    {
    }
    public TNode Node { get; private set; }
    public void SyncCallback(Action<Node> action)
    {
        GodotHelper.SynchronizationContext(Node, action);
    }

    [GlobalSetup]
    public void __GlobalSetup()
    {
        RootWindow = GodotHelper.Root;
        GlobalSetup();
    }

    [IterationSetup]
    public void __IterationSetup()
    {
        Node = Activator.CreateInstance<TNode>();
        RootWindow.CallDeferred(global::Godot.Node.MethodName.AddChild, Node);
        IterationSetup();
    }

    [IterationCleanup]
    public void __IterationCleanup()
    {
   
        Node.CallDeferred(global::Godot.Node.MethodName.QueueFree);
        IterationCleanup();
    }

    [GlobalCleanup]
    public void __GlobalCleanup()
    {
        GlobalCleanup();
    }
}

public class GodotBenchmark : GodotBenchmark<Node>
{

}