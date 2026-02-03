
using System;



namespace Interactions.UI;



public interface IDialogUi
{
    event Action? DialogFinished;



    event Action<Response>? ChoseResponse;


    
    public void ShowDialog(DialogInfo dialog);



    void ShowResponses(DialogInfo dialog);



    public void End();
}
