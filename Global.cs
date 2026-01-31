
using Godot;



public partial class Global : Node2D
{
    public static Global Instance { get; private set; }




    [Export]
    public Player Player { get; private set; }



    public override void _Ready()
    {
        Instance = this;
    }
}
