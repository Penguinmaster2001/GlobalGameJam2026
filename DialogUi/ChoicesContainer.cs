
using System.Collections.Generic;
using Godot;
using Interactions;



public partial class ChoicesContainer : AspectRatioContainer
{
    [Export]
    public required PackedScene OptionButtonScene;
    [Export]
    public required BoxContainer OptionBox;





    public void Show(List<Response> responses, InteractionDirector director)
    {
        foreach (var child in OptionBox.GetChildren())
        {
            child.QueueFree();
        }

        foreach (var response in responses)
        {
            var button = OptionButtonScene.Instantiate<Button>();
            button.Text = response.Text[0];
            button.Pressed += () => director.ChoseResponse(response);
            OptionBox.AddChild(button);
        }
    }
}
