
using System.Collections.Generic;
using System.Linq;
using Interactions.UI;



namespace Interactions;



public class InteractionDirector
{
    private readonly IDialogUi _dialogUi;
    private readonly NpcTextureDatabase _npcTextureDatabase;
    private readonly Player _player;



    public InteractionDirector(IDialogUi dialogUi, NpcTextureDatabase npcTextureDatabase, Player player)
    {
        _dialogUi = dialogUi;
        _npcTextureDatabase = npcTextureDatabase;
        _player = player;
    }



    public void StartInteraction(Interaction interaction)
    {
        var info = AssembleDialogInfo(interaction, 0);
        _dialogUi.Show(info);
    }



    private DialogInfo AssembleDialogInfo(Interaction interaction, int dialogId)
    {
        var dialog = interaction.Dialogs[dialogId];
        var filteredResponses = FilterResponses(dialog.Responses.Values);
        return new DialogInfo(_npcTextureDatabase.Query([interaction.Npc, "player"]), filteredResponses, dialog.Text);
    }



    private List<Response> FilterResponses(IEnumerable<Response> responses)
        => [.. responses.Where(r => r.RequiredMasks.Contains(_player.CurrentMask.Level)
            && r.SuspicionThreshold >= _player.SuspicionLevel)];

}
