
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



    public override void _Ready()
    {
        Ui.Connect("dialogue_finished", Callable.From(OnDialogFinished));
        Ui.Visible = false;
    }



    public void Show(DialogInfo dialog, InteractionDirector director)
    {
        Ui.Call("start_dialogue", dialog.Text, dialog.CharacterTextures);
        Choices.Show(dialog.Responses, director);
        Ui.Visible = true;
    }



    private void OnDialogFinished()
    {
        DialogFinished?.Invoke();
        // Ui.Call("hide_children");
        // GD.Print("dialog done");
    }



    public void End()
    {
        Ui.Visible = false;
    }
}
