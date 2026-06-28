using Godot;

namespace BallGame;

// We put inputs here so strings are not converted to StringNames every frame (increases performance)
public static class InputAction
{
    public static StringName Left { get; } = "left";
    public static StringName Right { get; } = "right";
    public static StringName Forwards { get; } = "forwards";
    public static StringName Backwards { get; } = "backwards";
    public static StringName Jump { get; } = "jump";
}
