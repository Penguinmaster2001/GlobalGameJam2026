
using System.Collections.Generic;
using System.Linq;



namespace Interactions;



public class Dialog
{
    public int DialogId;
    public Dictionary<int, Response> Responses;
    public List<DialogAction> Actions;
    public string Text;



    public Dialog(int dialogId, string text, List<Response> responses, List<DialogAction> actions)
    {
        DialogId = dialogId;
        Text = text;
        Responses = responses.ToDictionary(r => r.ResponseId);
        Actions = actions;
    }



    public Dialog(DialogData data) : this(data.DialogId, data.Text, data.Responses, data.Actions) { }



    public record DialogData(int DialogId, string Text, List<Response> Responses, List<DialogAction> Actions)
    {
        public Dialog IntoDialog() => new(this);
    }
}
