
using System.Collections.Generic;
using Godot;
using Interactions;



public partial class ChoicesContainer : Control
{
    [Export]
    public required PackedScene OptionButtonScene;

    [Export]
    public required BoxContainer OptionBox;



    private readonly List<Button> _currentButtons = [];



    public void Show(List<Response> responses, InteractionDirector director)
    {
        GD.Print("Show responses");
        foreach (var button in _currentButtons)
        {
            button.Visible = false;
            OptionBox.RemoveChild(button);
            button.QueueFree();
        }
        _currentButtons.Clear();

        foreach (var response in responses)
        {
            var button = OptionButtonScene.Instantiate<Button>();
            button.Text = response.Text[0];
            button.ButtonUp += () => { GD.Print("hello"); director.ChoseResponse(response); };
            _currentButtons.Add(button);
            OptionBox.CallDeferred("add_child", button);
        }
    }
}
