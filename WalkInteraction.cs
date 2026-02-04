
using System.Linq;
using Godot;
using Godot.Collections;
using Interactions;



public partial class WalkInteraction : Area2D
{
    [Export]
    public bool Enabled = true;

    [Export]
    public Array<DialogActionResource> Actions = [];

    [Export]
    public Array<int> InteractionId = [];

    public DialogAction[] DialogActions = [];



    public override void _Ready()
    {
        DialogActions = [.. Actions.Select(a => a.ToDialogAction())];
        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExit;
    }



    private void OnBodyEntered(Node2D body)
    {
        if (Enabled && body == Global.Instance.PlayerBody)
        {
            Global.Instance.Player.OnWalkInteraction(this);
        }
    }



    private void OnBodyExit(Node2D body)
    {
        if (body == Global.Instance.PlayerBody)
        {
            Global.Instance.Player.OnWalkLeave(this);
        }
    }
}
