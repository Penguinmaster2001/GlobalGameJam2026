
using Godot;
using Interactions;



public partial class Global : Node2D
{
    public static Global Instance { get; set; }



    [Export]
    public required TopDownCharacter PlayerBody { get; set; }



    public Player Player { get; set; }



    [Export]
    public required MaskUi MaskUi { get; set; }



    public int SuspicionLevel;



    public InteractionDatabase Interactions;



    public override void _Ready()
    {
        Instance = this;

        Player = new(PlayerBody);

        var interactions = DialogManager.LoadDialogs("res://Resources/Dialog/");
        Interactions = new(interactions);

        MaskUi.AddMasks(Mask.Masks.Values);
    }
}
