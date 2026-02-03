
using System;
using Godot;



namespace Interactions.UI;



public partial class DialogUiWrapper : Control, IDialogUi
{
    [Export]
    public required Control Ui { get; set; }
 


    [Export]
    public required ChoicesContainer Choices { get; set; }



    public event Action? DialogFinished = null;



    public event Action<Response>? ChoseResponse = null;



    public override void _Ready()
    {
        Ui.Connect("dialogue_finished", Callable.From(OnDialogFinished));
        Choices.ChoseResponse += (response) => ChoseResponse?.Invoke(response);
        Choices.RemoveResponses();
        Ui.Visible = false;
    }



    public void ShowDialog(DialogInfo dialog)
    {
        Choices.RemoveResponses();
        Ui.Call("start_dialogue", dialog.Text, dialog.CharacterTextures);
        Ui.Visible = true;
    }



    public void ShowResponses(DialogInfo dialog)
    {
        Choices.Show(dialog.Responses);
    }



    private void OnDialogFinished()
    {
        DialogFinished?.Invoke();
    }



    public void End()
    {
        Ui.Visible = false;
        Choices.RemoveResponses();
    }
}
