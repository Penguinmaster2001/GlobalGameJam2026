
using System.Text.Json.Serialization;



namespace Interactions;



public enum DialogActionTypes
{
    ChangeSuspicion,
    GiveMask,
    EndGame,
}



public class DialogAction
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DialogActionTypes ActionType { get; set; }
    public int Value { get; set; }
}
