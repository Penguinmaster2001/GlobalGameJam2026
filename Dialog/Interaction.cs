
using System.Collections.Generic;
using System.Linq;



namespace Interactions;



public class Interaction
{
    public int InteractionId;
    public Requirements Requirements;
    public int Repetitions;
    public Dictionary<int, Dialog> Dialogs;



    public Interaction(int interactionId, Requirements requirements, int repetitions, List<Dialog> dialogs)
    {
        InteractionId = interactionId;
        Requirements = requirements;
        Repetitions = repetitions;
        Dialogs = dialogs.ToDictionary(d => d.DialogId);
    }



    public record InteractionData(int InteractionId, Requirements Requirements, int Repetitions, List<Dialog.DialogData> Dialogs)
    {
        public Interaction IntoInteraction() => new(InteractionId, Requirements, Repetitions, [.. Dialogs.Select(d => d.IntoDialog())]);
    }
}
