
using Godot;



namespace Interactions.UI;



public class MockDialogUi : IDialogUi
{
    public void End()
    {
        GD.Print("done!!!");
    }

    public void Show(DialogInfo dialog)
    {
        GD.Print(dialog.Text);
    }
}
