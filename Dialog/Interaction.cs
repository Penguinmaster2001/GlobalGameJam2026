
using System.Collections.Generic;
using System.Linq;



namespace Interactions;



public class Interaction
{
    public int InteractionId;
    public string Npc;
    public Dictionary<int, Dialog> Dialogs;



    public Interaction(int interactionId, string npc, List<Dialog> dialogs)
    {
        InteractionId = interactionId;
        Npc = npc;
        Dialogs = dialogs.ToDictionary(d => d.DialogId);
    }




    public Interaction(InteractionData data) : this(data.InteractionId, data.Npc, [.. data.Dialogs.Select(d => d.IntoDialog())]) { }



    public record InteractionData(int InteractionId, string Npc, List<Dialog.DialogData> Dialogs)
    {
        public Interaction IntoInteraction() => new(this);
    }
}
