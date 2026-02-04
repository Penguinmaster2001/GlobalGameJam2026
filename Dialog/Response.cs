
namespace Interactions;



public class Response
{
    public int ResponseId;
    public Requirements Requirements;
    public string Text;
    public int NextDialogId;



    public Response(int responseId, Requirements requirements, string text, int nextDialogId)
    {
        ResponseId = responseId;
        Requirements = requirements;
        Text = text;
        NextDialogId = nextDialogId;
    }



    public static Response Continue => new(-1, Requirements.None, "Continue.", -1);
}