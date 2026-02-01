
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



    public static readonly ReadOnlyDictionary<string, Mask> Masks = new(new Dictionary<string, Mask>()
    {
        {"none", new Mask(0, "res://Resources/Textures/Masks/None.png")}
    });



    public static readonly ReadOnlyDictionary<int, string> MaskNames = new(new Dictionary<int, string>()
    {
        {0, "none"}
    });
}
