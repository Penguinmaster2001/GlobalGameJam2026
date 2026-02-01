
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Godot;



public class Mask
{
    public int Level { get; }
    public Texture2D Texture { get; }



    public Mask(int level, string texturePath)
    {
        Level = level;
        Texture = GD.Load<Texture2D>(texturePath);
    }



    public const string BaseTexturePath = "res://Resources/Textures/Masks/mask_";



    public static readonly ReadOnlyDictionary<string, Mask> Masks = new(new Dictionary<string, Mask>()
    {
        {"none", new Mask(0, "res://Resources/Textures/Masks/none.png")},
        {"low", new Mask(1, "res://Resources/Textures/Masks/mask_1.png")},
        {"normal", new Mask(2, "res://Resources/Textures/Masks/mask_2.png")},
        {"high", new Mask(3, "res://Resources/Textures/Masks/mask_3.png")},
    });



    public static readonly ReadOnlyDictionary<int, string> MaskNames = new(new Dictionary<int, string>()
    {
        {0, "none"},
        {1, "low"},
        {2, "normal"},
        {3, "high"},
    });
}
