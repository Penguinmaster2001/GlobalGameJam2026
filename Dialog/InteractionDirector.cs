
using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Interactions.UI;



namespace Interactions;



public class InteractionDirector
{
    private readonly IDialogUi _dialogUi;
    private readonly NpcTextureDatabase _npcTextureDatabase;
    private readonly Player _player;
    private readonly Dictionary<DialogActionTypes, Action<string>> _dialogActionHandlers;
    private Interaction? _currentInteraction = null;



    public InteractionDirector(IDialogUi dialogUi, NpcTextureDatabase npcTextureDatabase, Player player)
    {
        _dialogUi = dialogUi;
        _npcTextureDatabase = npcTextureDatabase;
        _player = player;

        _dialogActionHandlers = new() {
            {DialogActionTypes.None, i => { } },
            {DialogActionTypes.ChangeSuspicion, i => _player.SuspicionLevel += int.Parse(i) },
            {DialogActionTypes.GiveMask, i => {_player.GiveMask(int.Parse(i)); Global.Instance.MaskUi.EnableMask(int.Parse(i)); } },
            {DialogActionTypes.SetObjective, i => Global.Instance.ObjectiveManager.CurrentObjective = i },
            {DialogActionTypes.TriggerEvent, i => Global.Instance.Jumpscare.Visible = true },
        };
    }



    public void DoAction(DialogAction dialogAction)
    {
        _dialogActionHandlers[dialogAction.ActionType](dialogAction.Value);
    }



    public void StartInteraction(Interaction interaction)
    {
        _currentInteraction = interaction;
        SendDialog(interaction, 0);
    }



    public void ChoseResponse(Response response)
    {
        if (_currentInteraction is Interaction interaction)
        {
            GD.Print(response.Text);
            GD.Print(response.NextDialogId);
            SendDialog(interaction, response.NextDialogId);
        }
    }



    private void SendDialog(Interaction interaction, int dialogId)
    {
        if (!interaction.Dialogs.TryGetValue(dialogId, out var dialog))
        {
            _dialogUi.End();
            _currentInteraction = null;
            return;
        }
        foreach (var action in dialog.Actions)
        {
            GD.Print($"{action.ActionType}, {action.Value}");
            DoAction(action);
        }
        var info = AssembleDialogInfo(interaction, dialogId);
        _dialogUi.Show(info, this);
    }



    private DialogInfo AssembleDialogInfo(Interaction interaction, int dialogId)
    {
        var dialog = interaction.Dialogs[dialogId];
        var filteredResponses = FilterResponses(dialog.Responses.Values);
        if (filteredResponses.Count <= 0)
        {
            filteredResponses = [new(0, 100, [], ["Continue"], -1)];
        }
        return new DialogInfo(_npcTextureDatabase.Query([interaction.Npc, "player"]), filteredResponses, dialog.Text);
    }



    private List<Response> FilterResponses(IEnumerable<Response> responses)
        => [.. responses.Where(r => r.RequiredMasks.Contains(_player.CurrentMask.Level)
            && r.SuspicionThreshold >= _player.SuspicionLevel)];



    private string[] AppendCharacterName(IEnumerable<string> text, string name)
        => [.. text.Select(t => t.StartsWith($"{name.ToLower()}:") ? t : $"{name.ToLower()}: {t}")];
}
