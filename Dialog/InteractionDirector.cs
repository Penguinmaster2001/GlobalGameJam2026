
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
    private DialogInfo? _currentDialog = null;
    public List<int> InteractionHistory { get; }



    public InteractionDirector(IDialogUi dialogUi, NpcTextureDatabase npcTextureDatabase, Player player)
    {
        InteractionHistory = [];

        _dialogUi = dialogUi;
        _dialogUi.ChoseResponse += OnChoseResponse;
        _dialogUi.DialogFinished += OnDialogFinished;

        _npcTextureDatabase = npcTextureDatabase;
        _player = player;

        _dialogActionHandlers = new() {
            {
                DialogActionTypes.None,
                i => { }
            },
            {
                DialogActionTypes.ChangeSuspicion,
                i => _player.SuspicionLevel += int.Parse(i)
            },
            {
                DialogActionTypes.GiveMask,
                i => {
                    var level = int.Parse(i);
                    _player.GiveMask(level);
                    Global.Instance.MaskUi.EnableMask(level);
                }
            },
            {
                DialogActionTypes.SetObjective,
                i => Global.Instance.ObjectiveManager.CurrentObjective = i
            },
        };
    }



    private void OnDialogFinished()
    {
        if (_currentDialog is DialogInfo dialogInfo)
        {
            _dialogUi.ShowResponses(dialogInfo);
        }
    }



    public void DoAction(DialogAction dialogAction, bool checkRepetitions = true)
    {
        // If repetitions < 0, unlimited repeats
        if (checkRepetitions && dialogAction.Repetitions != 0)
        {
            _dialogActionHandlers.GetValueOrDefault(dialogAction.ActionType,
                (s) => GD.Print($"No action set. Value: {s}"))(dialogAction.Value);
            dialogAction.Repetitions--;
        }
    }



    public int TryInteractions(IEnumerable<int> interactionIds)
    {
        var i = 0;
        foreach (var interactionId in interactionIds)
        {
            if (StartInteraction(interactionId)) return i;
            i++;
        }
        return -1;
    }



    public int TryInteractions(IEnumerable<Interaction> interactions)
    {
        var i = 0;
        foreach (var interaction in interactions)
        {
            if (StartInteraction(interaction)) return i;
            i++;
        }
        return -1;
    }



    public bool StartInteraction(int interactionId)
        => Global.Instance.Interactions.Query(interactionId, out var interaction)
            && StartInteraction(interaction);



    public bool StartInteraction(Interaction interaction)
    {
        if (interaction.Repetitions == 0 || !CheckRequirements(interaction.Requirements)) return false;

        _currentInteraction = interaction;
        SendDialog(interaction, 0);
        InteractionHistory.Add(interaction.InteractionId);
        return true;
    }



    public void OnChoseResponse(Response response)
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
        var dialogInfo = AssembleDialogInfo(interaction, dialogId);
        _currentDialog = dialogInfo;
        _dialogUi.ShowDialog(_currentDialog);
    }



    private DialogInfo AssembleDialogInfo(Interaction interaction, int dialogId)
    {
        var dialog = interaction.Dialogs[dialogId];
        var filteredResponses = FilterResponses(dialog.Responses.Values);
        if (filteredResponses.Count <= 0)
        {
            filteredResponses = [Response.Continue];
        }
        return new DialogInfo(_npcTextureDatabase.Textures, filteredResponses, dialog.Text);
    }



    private List<Response> FilterResponses(IEnumerable<Response> responses)
        => [.. responses.Where(r => CheckRequirements(r.Requirements))];



    private bool CheckRequirements(Requirements requirements)
        => requirements.MinSuspicion <= _player.SuspicionLevel && _player.SuspicionLevel <= requirements.MaxSuspicion
        && (requirements.Mask.Count <= 0 || requirements.Mask.Contains(_player.CurrentMask.Level))
        && (requirements.CurrentObjective.Count <= 0 || requirements.CurrentObjective.Any(o => string.Compare(o, Global.Instance.ObjectiveManager.CurrentObjective, StringComparison.OrdinalIgnoreCase) == 0))
        && (requirements.PreviousInteractions.Count <= 0 || !InteractionHistory.Except(requirements.PreviousInteractions).Any())
        && (requirements.CompletedObjectives.Count <= 0 || !Global.Instance.ObjectiveManager.CompletedObjectives.Except(requirements.CompletedObjectives, StringComparer.OrdinalIgnoreCase).Any());
}
