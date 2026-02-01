
namespace Interactions;



public enum DialogActionTypes
{
    ChangeSuspicion,
    GiveMask,
    EndGame,
}



public class DialogAction
{
    public DialogActionTypes ActionType { get; set; }
    public int Value { get; set; }
}
