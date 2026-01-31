
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;



namespace Interactions;



public class Dialog
{
    public Dictionary<int, Response> Responses;
    public List<DialogAction> Actions;



    [JsonConstructor]
    public Dialog(List<Response> responses, List<DialogAction> actions)
    {
        Responses = responses.ToDictionary(r => r.ResponseId);
        Actions = actions;
    }
}
