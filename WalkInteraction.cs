
using Godot;



public partial class WalkInteraction : Area2D
{
    [Export]
    public string InteractionName = string.Empty;

    [Export]
    public int InteractionId = -1;



    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }



    private void OnBodyEntered(Node2D body)
    {
        if (body == Global.Instance.PlayerBody)
        {
            Global.Instance.Player.OnWalkInteraction(this);
        }
    }
}
