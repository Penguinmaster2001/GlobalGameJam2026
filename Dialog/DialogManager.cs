
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Godot;



namespace Interactions;



public static class DialogManager
{
    private static readonly JsonSerializerOptions _interactionSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        IncludeFields = true,
    };




    public static List<Interaction> LoadDialogs(string path)
    {
        var interactions = new List<Interaction>();

        // foreach (var file in DirAccess.Open(path).GetFiles())
        // {
        //     var text = Godot.FileAccess.GetFileAsString(Path.Combine(path, file));
        //     GD.Print(Path.Combine(path, file));
        //     GD.Print(text);
        //     var interaction = JsonSerializer.Deserialize<Interaction.InteractionData>(text, _interactionSerializerOptions);
        //     if (interaction is Interaction.InteractionData validInteraction)
        //     {
        //         interactions.Add(validInteraction.IntoInteraction());
        //     }
        // }

        var data = JsonSerializer.Deserialize<List<Interaction.InteractionData>>(Godot.FileAccess.GetFileAsString(path), _interactionSerializerOptions)!;
        foreach (var interaction in data)
        {
            if (interaction is Interaction.InteractionData validInteraction)
            {
                interactions.Add(validInteraction.IntoInteraction());
            }
        }

        return interactions;
    }
}
