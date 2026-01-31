
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;



namespace Interactions;



public class Interaction
{
    public int InteractionId;
    public string Npc;
    public Dictionary<int, Dialog> Dialogs;




    [JsonConstructor]
    public Interaction(int interactionId, string npc, List<Dialog> dialogs)
    {
        InteractionId = interactionId;
        Npc = npc;
        Dialogs = dialogs.ToDictionary(d => d.DialogId);
    }
}
