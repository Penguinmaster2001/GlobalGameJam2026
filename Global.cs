
using System.Collections.Generic;
using System.IO;
using Godot;
using Interactions;
using Interactions.UI;
using Objectives;



public partial class Global : Node2D
{
    public static Global Instance { get; set; }



    [Export]
    public required TopDownCharacter PlayerBody { get; set; }


    [Export]
    public required Sprite2D Jumpscare;



    public Player Player { get; set; }
    public InteractionDirector Director { get; set; }



    [Export]
    public required MaskUi MaskUi { get; set; }



    [Export]
    public required DialogUiWrapper DialogUi { get; set; }



    [Export]
    public required ProgressBar SusUi { get; set; }



    [Export]
    public required ObjectiveManager ObjectiveManager { get; set; }



    public int SuspicionLevel;



    public InteractionDatabase Interactions;



    public NpcTextureDatabase NpcTextures { get; set; }



    public override void _Ready()
    {
        Instance = this;

        Player = new(PlayerBody);

        var interactions = DialogManager.LoadDialogs("res://Resources/Story/story.json");
        Interactions = new(interactions);

        var texturePath = "res://Resources/Textures/Characters";
        var textures = new Dictionary<string, Texture2D>();
        foreach (var file in DirAccess.Open(texturePath).GetFiles())
        {
            var name = Path.GetFileNameWithoutExtension(file);
            if (name.Contains('.')) continue;
            GD.Print(name);
            var texture = GD.Load(Path.Combine(texturePath, file));
            if (texture is Texture2D validTexture)
            {
                textures.Add(name, validTexture);
            }
        }
        NpcTextures = new(textures);

        Director = new(DialogUi, NpcTextures, Player);

        MaskUi.AddMasks(Mask.Masks.Values);



        if (Interactions.Query(0, out var interaction))
        {
            Director.StartInteraction(interaction);
        }
    }
}
