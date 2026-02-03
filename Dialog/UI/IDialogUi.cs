
using System;



namespace Interactions.UI;



public interface IDialogUi
{
    event Action? DialogFinished;


    
    public void Show(DialogInfo dialog, InteractionDirector director);



    public void End();
}
