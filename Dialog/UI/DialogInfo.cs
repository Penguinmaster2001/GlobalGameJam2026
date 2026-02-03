
using System.Collections.Generic;
using System.Linq;
using Godot;



namespace Interactions.UI;




public class DialogInfo
{
    public Godot.Collections.Dictionary<string, Texture2D> CharacterTextures;
    public List<Response> Responses;
    public Godot.Collections.Array<string> Text;



    public DialogInfo(Dictionary<string, Texture2D> characterTextures, IEnumerable<Response> responses, string[] text)
    {
        CharacterTextures = [];
        foreach (var character in characterTextures)
        {
            CharacterTextures.Add(character.Key, character.Value);
        }
        Responses = [.. responses];
        Text = [.. text];
    }
}
