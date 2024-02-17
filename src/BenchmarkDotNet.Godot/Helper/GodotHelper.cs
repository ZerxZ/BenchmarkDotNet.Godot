using Godot;

namespace BenchmarkDotNet.Godot.Helper;

public static class GodotHelper
{
    public static Window Root => ((SceneTree)Engine.GetMainLoop()).Root;
    public static void SynchronizationContext(Action action)
    {
        Dispatcher.SynchronizationContext?.Send(_ =>
        {
            action();
        }, null);
    }

    public static void SynchronizationContext<TNode>(TNode node, Action<TNode> action) where TNode : Node
    {


        Dispatcher.SynchronizationContext?.Send(_ =>
        {
            action(node);
        }, null);
    }

}