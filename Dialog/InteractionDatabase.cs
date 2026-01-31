
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;



namespace Interactions;




public class InteractionDatabase
{
    private readonly Dictionary<int, Interaction> _interactions;



    public InteractionDatabase(IEnumerable<Interaction> interactions)
    {
        _interactions = interactions.ToDictionary(i => i.InteractionId);
    }




    public bool Query(int interactionId, [NotNullWhen(true)] out Interaction? interaction)
        => _interactions.TryGetValue(interactionId, out interaction);
}
