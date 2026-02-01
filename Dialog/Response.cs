
using System.Collections.Generic;
using System.Text.Json.Serialization;



namespace Interactions;



public class Response
{
    public int ResponseId;
    public int SuspicionThreshold;
    public List<int> RequiredMasks;
    public string[] Text;
    public int NextDialogId;



    [JsonConstructor]
    public Response(int responseId, int suspicionThreshold, List<int> requiredMasks, string[] text, int nextDialogId)
    {
        ResponseId = responseId;
        SuspicionThreshold = suspicionThreshold;
        RequiredMasks = requiredMasks;
        Text = text;
        NextDialogId = nextDialogId;
    }
}