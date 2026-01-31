
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;




public class InteractionHistory
{
    private readonly Dictionary<string, Interaction> _interactions = [];



    public bool Query(string character, [NotNullWhen(true)] out Interaction? interaction)
        => _interactions.TryGetValue(character, out interaction);



    public void Add(Interaction interaction) => _interactions.Add(interaction.CharacterName, interaction);
}
