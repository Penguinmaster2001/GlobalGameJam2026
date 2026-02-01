
using System.Collections.Generic;
using Godot;



namespace Interactions.UI;




public class DialogInfo
{
    public Godot.Collections.Dictionary<string, Texture2D> CharacterTextures;
    public List<string[]> Responses;
    public string[] Text;



    public DialogInfo(Dictionary<string, Texture2D> characterTextures, List<string[]> responses, string[] text)
    {
        CharacterTextures = [];
        foreach (var character in characterTextures)
        {
            CharacterTextures.Add(character.Key, character.Value);
        }
        Responses = responses;
        Text = text;
    }
}
