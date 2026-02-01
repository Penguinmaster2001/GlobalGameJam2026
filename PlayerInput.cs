
using Godot;



public class PlayerInput : IInput
{
    public Vector2 GetInput() => Input.GetVector("left", "right", "up", "down");
}
