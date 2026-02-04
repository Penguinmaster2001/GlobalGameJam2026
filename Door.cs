
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
    public string OpenDoorEvent = "open_doors";


    [Export]
    public string CloseDoorEvent = "close_doors";


    [Export]
    public required CollisionPolygon2D Collision;

    [Export]
    public required Array<Sprite2D> DoorSprites;

    [Export]
    public required int OpenFrame;

    [Export]
    public required int ClosedFrame;




    public override void _Ready()
    {
        Events.Subscribe("open_door", (v) =>
        {
            if (Events.EventStringComparer.Compare(v, OpenDoorEvent) == 0)
            {
                Open = true;
            }
        });

        Events.Subscribe("close_door", (v) =>
        {
            if (Events.EventStringComparer.Compare(v, OpenDoorEvent) == 0)
            {
                Open = false;
            }
        });
    }
}
