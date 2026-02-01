
using Godot;



namespace Objectives;



public partial class ObjectiveManager : Control
{
    [Export]
    public required RichTextLabel ObjectiveLabel;



    private string _currentObjective = string.Empty;
    public string CurrentObjective
    {
        get => _currentObjective; set
        {
            ObjectiveLabel.Text = value;
            _currentObjective = value;
        }
    }
}
