
using System.Collections.Generic;
using Godot;



namespace Interactions.UI;




public class DialogInfo
{
    public Dictionary<string, Texture2D> CharacterTextures;
    public List<Response> Responses;
    public string Text;



    public DialogInfo(Dictionary<string, Texture2D> characterTextures, List<Response> responses, string text)
    {
        CharacterTextures = characterTextures;
        Responses = responses;
        Text = text;
    }
}
