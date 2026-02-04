
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
    public string Value { get; set; } = string.Empty;
    public int Repetitions { get; set; } = 1;
}
