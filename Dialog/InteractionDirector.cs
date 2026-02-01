
using System;
using System.Collections.Generic;
using System.Linq;
using Interactions.UI;



namespace Interactions;



public class InteractionDirector
{
    private readonly IDialogUi _dialogUi;
    private readonly NpcTextureDatabase _npcTextureDatabase;
    private readonly Player _player;
    private readonly Dictionary<DialogActionTypes, Action<int>> _dialogActionHandlers;


    public InteractionDirector(IDialogUi dialogUi, NpcTextureDatabase npcTextureDatabase, Player player)
    {
        _dialogUi = dialogUi;
        _npcTextureDatabase = npcTextureDatabase;
        _player = player;

        _dialogActionHandlers = new() {
            {DialogActionTypes.ChangeSuspicion, i => _player.SuspicionLevel -= i },
            {DialogActionTypes.GiveMask, i => {_player.GiveMask(i); Global.Instance.MaskUi.EnableMask(i); } },
        };
    }



    public void StartInteraction(Interaction interaction)
    {
        var info = AssembleDialogInfo(interaction, 0);
        _dialogUi.Show(info);
    }



    private void SendDialog(Interaction interaction, int dialogId)
    {
        var dialog = interaction.Dialogs[dialogId];
        foreach (var action in dialog.Actions)
        {
            _dialogActionHandlers[action.ActionType](action.Value);
        }
        var info = AssembleDialogInfo(interaction, dialogId);
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
