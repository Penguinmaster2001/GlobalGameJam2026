
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
        };
    }



    public void DoAction(DialogAction dialogAction)
    {
        _dialogActionHandlers[dialogAction.ActionType](dialogAction.Value);
    }



    public void StartInteraction(Interaction interaction)
    {
        SendDialog(interaction, 0);
    }



    private void SendDialog(Interaction interaction, int dialogId)
    {
        var dialog = interaction.Dialogs[dialogId];
        foreach (var action in dialog.Actions)
        {
            GD.Print($"{action.ActionType}, {action.Value}");
            DoAction(action);
        }
        var info = AssembleDialogInfo(interaction, dialogId);
        _dialogUi.Show(info);
    }



    private DialogInfo AssembleDialogInfo(Interaction interaction, int dialogId)
    {
        var dialog = interaction.Dialogs[dialogId];
        var filteredResponses = FilterResponses(dialog.Responses.Values);
        return new DialogInfo(_npcTextureDatabase.Query([interaction.Npc, "player"]), filteredResponses, AppendCharacterName(dialog.Text, interaction.Npc));
    }



    private List<Response> FilterResponses(IEnumerable<Response> responses)
        => [.. responses.Where(r => r.RequiredMasks.Contains(_player.CurrentMask.Level)
            && r.SuspicionThreshold >= _player.SuspicionLevel).Select(r => { r.Text = AppendCharacterName(r.Text, "player");  return r;})];




    private string[] AppendCharacterName(IEnumerable<string> text, string name)
        => [.. text.Select(t => t.StartsWith($"{name}:") ? t : $"{name}: {text}")];
}
