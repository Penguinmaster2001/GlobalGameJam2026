
using System.Text.Json.Serialization;



namespace Interactions;



public enum DialogActionTypes
{
    None,
    ChangeSuspicion,
    GiveMask,
    SetObjective,
    GiveItem,
    TriggerEvent,
    EndGame,
}



public class DialogAction
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DialogActionTypes ActionType { get; set; } = DialogActionTypes.None;
    public int Value { get; set; } = 0;
}
