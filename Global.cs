
using Godot;
using Interactions;



public partial class Global : Node2D
{
    public static Global Instance { get; private set; }



    [Export]
    public Player Player { get; private set; }



    public int SuspicionLevel;



    public InteractionDatabase Interactions;



    public override void _Ready()
    {
        Instance = this;

        var interactions = DialogManager.LoadDialogs("res://Resources/Dialog/");
        Interactions = new(interactions);
    }
}
