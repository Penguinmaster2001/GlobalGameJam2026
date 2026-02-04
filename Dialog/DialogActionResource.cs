
using Godot;



namespace Interactions;



[Tool]
[GlobalClass]
public partial class DialogActionResource : Resource
{
    [Export]
    public DialogActionTypes ActionType { get; set; } = DialogActionTypes.None;

    [Export]
    public string Value { get; set; } = string.Empty;

    [Export]
    public int Repetitions { get; set; } = 1;



    public DialogAction ToDialogAction()
        => new() { ActionType = ActionType, Value = Value, Repetitions = Repetitions };
}
