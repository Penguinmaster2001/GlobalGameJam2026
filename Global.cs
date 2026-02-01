
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using Interactions;
using Interactions.UI;



public partial class Global : Node2D
{
    public static Global Instance { get; set; }



    [Export]
    public required TopDownCharacter PlayerBody { get; set; }



    public Player Player { get; set; }
    public InteractionDirector Director { get; set; }



    [Export]
    public required MaskUi MaskUi { get; set; }



    [Export]
    public required DialogUiWrapper DialogUi { get; set; }



    public int SuspicionLevel;



    public InteractionDatabase Interactions;



    public NpcTextureDatabase NpcTextures { get; set; }



    public override void _Ready()
    {
        Instance = this;

        Player = new(PlayerBody);

        var interactions = DialogManager.LoadDialogs("res://Resources/Dialog/");
        Interactions = new(interactions);

        var textures = new Dictionary<string, Texture2D>();
        NpcTextures = new(textures);

        Director = new(DialogUi, NpcTextures, Player);

        MaskUi.AddMasks(Mask.Masks.Values);
    }
}
