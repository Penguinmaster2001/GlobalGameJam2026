
using System.Collections.Generic;
using Godot;



namespace Objectives;



public partial class ObjectiveManager : Control
{
    [Export]
    public required RichTextLabel ObjectiveLabel;



    public readonly List<string> CompletedObjectives = [];



    private string _currentObjective = string.Empty;
    public string CurrentObjective
    {
        get => _currentObjective;
        set
        {
            CompletedObjectives.Add(_currentObjective);
            ObjectiveLabel.Text = value;
            _currentObjective = value;
        }
    }
}
