
using System;
using System.Collections.Generic;
using Godot;
using Interactions;



public partial class ChoicesContainer : Control
{
    [Export]
    public required PackedScene OptionButtonScene;

    [Export]
    public required BoxContainer OptionBox;



    public event Action<Response>? ChoseResponse = null;



    private readonly List<Button> _currentButtons = [];



    public void Show(List<Response> responses)
    {
        foreach (var response in responses)
        {
            var button = OptionButtonScene.Instantiate<Button>();
            button.Text = response.Text[0];
            button.ButtonUp += () => ChoseResponse?.Invoke(response);
            _currentButtons.Add(button);
            OptionBox.CallDeferred("add_child", button);
        }
        OptionBox.CallDeferred("show");
    }



    public void RemoveResponses()
    {
        OptionBox.Visible = false;
        foreach (var button in _currentButtons)
        {
            button.Visible = false;
            OptionBox.RemoveChild(button);
            button.QueueFree();
        }
        _currentButtons.Clear();
    }
}
