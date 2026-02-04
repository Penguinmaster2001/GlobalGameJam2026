
using Godot;
using Godot.Collections;



public partial class Door : StaticBody2D
{
    [Export]
    public bool Open
    {
        get => _open;
        set
        {
            if (_open == value) return;

            _open = value;
            Collision.Disabled = _open;
            
            var newFrame = _open ? OpenFrame : ClosedFrame;
            foreach (var doorSprite in DoorSprites)
            {
                doorSprite.Frame = newFrame;
            }
        }
    }
    private bool _open;


    [Export]
    public required CollisionPolygon2D Collision;

    [Export]
    public required Array<Sprite2D> DoorSprites;
    
    [Export]
    public required int OpenFrame;
    
    [Export]
    public required int ClosedFrame;
}
