
using System.Collections.Generic;
using System.Linq;
using Godot;



namespace Interactions.UI;




public class DialogInfo
{
    public Godot.Collections.Dictionary<string, Texture2D> CharacterTextures;
    public Godot.Collections.Array<Godot.Collections.Array<string>> Responses;
    public Godot.Collections.Array<string> Text;



    public DialogInfo(Dictionary<string, Texture2D> characterTextures, List<string[]> responses, string[] text)
    {
        CharacterTextures = [];
        foreach (var character in characterTextures)
        {
            CharacterTextures.Add(character.Key, character.Value);
        }
        Responses = [.. responses.Select<string[], Godot.Collections.Array<string>>(r => [.. r])];
        Text = [.. text];
    }
}
