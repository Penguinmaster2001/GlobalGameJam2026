
using System.Collections.Generic;
using System.Text.Json.Serialization;



namespace Interactions;



public class Interaction
{
    public int InteractionId;
    public string Npc;
    public List<Dialog> Dialogs;




    [JsonConstructor]
    public Interaction(int interactionId, string npc, List<Dialog> dialogs)
    {
        InteractionId = interactionId;
        Npc = npc;
        Dialogs = dialogs;
    }
}
