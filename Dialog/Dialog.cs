
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;



namespace Interactions;



public class Dialog
{
    public int DialogId;
    public Dictionary<int, Response> Responses;
    public List<DialogAction> Actions;



    [JsonConstructor]
    public Dialog(int dialogId, List<Response> responses, List<DialogAction> actions)
    {
        DialogId = dialogId;
        Responses = responses.ToDictionary(r => r.ResponseId);
        Actions = actions;
    }
}
