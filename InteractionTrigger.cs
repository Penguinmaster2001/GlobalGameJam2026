
using System.Linq;
using Godot;
using Godot.Collections;
using Interactions;



public partial class InteractionTrigger : Area2D
{
    [Export]
    public bool Enabled = true;

    [Export]
    public bool DetectClick = true;

    [Export]
    public bool DetectCollide = true;

    [Export]
    public Array<DialogActionResource> Actions = [];

    [Export]
    public Array<int> InteractionId = [];

    [Export]
    public int CurrentInteraction = 0;

    public DialogAction[] DialogActions = [];



    public override void _Ready()
    {
        DialogActions = [.. Actions.Select(a => a.ToDialogAction())];
        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExit;
        InputEvent += OnInput;
    }



    private void OnInput(Node viewport, InputEvent inputEvent, long shapeIdx)
    {
        if (Enabled
            && DetectClick
            && inputEvent is InputEventMouseButton mouseInput
            && mouseInput.Pressed)
        {
            Global.Instance.Player.OnInteraction(this);
        }
    }



    private void OnBodyEntered(Node2D body)
    {
        if (Enabled && DetectCollide && body == Global.Instance.PlayerBody)
        {
            Global.Instance.Player.OnInteraction(this);
        }
    }



    private void OnBodyExit(Node2D body)
    {
        if (body == Global.Instance.PlayerBody)
        {
            Global.Instance.Player.OnColliderInteractionLeave(this);
        }
    }
}
