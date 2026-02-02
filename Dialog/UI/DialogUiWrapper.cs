
using Godot;



namespace Interactions.UI;



public partial class DialogUiWrapper : Control, IDialogUi
{
    [Export]
    public required Node2D Ui { get; set; }



    public override void _Ready()
    {
        Ui.Connect("dialogue_finished", Callable.From(OnDialogFinished));
        Ui.Visible = false;
    }



    public void Show(DialogInfo dialog)
    {
        Ui.Call("start_dialogue", dialog.Text, dialog.CharacterTextures, dialog.Responses);
    }



    private void OnDialogFinished()
    {
        Ui.Call("hide_children");
        GD.Print("dialog done");
    }



    public void End()
    {
        Ui.Call("hide_children");
    }
}
