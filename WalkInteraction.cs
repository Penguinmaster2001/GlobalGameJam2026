
using System;
using System.Linq;
using Godot;
using Godot.Collections;
using Interactions;



public partial class WalkInteraction : Area2D
{
    [Export]
    public bool MultiUse = false;

    [Export]
    public bool Valid = true;

    [Export]
    public Array<DialogActionTypes> ActionTypes = [];

    [Export]
    public Array<int> ActionValues = [];

    [Export]
    public int InteractionId = -1;

    public DialogAction[] DialogAction = [];



    public override void _Ready()
    {
        DialogAction = [.. ActionTypes.Zip(ActionValues).Select(v => new DialogAction() { ActionType = v.First, Value = v.Second })];
        BodyEntered += OnBodyEntered;
    }



    private void OnBodyEntered(Node2D body)
    {
        if (Valid && body == Global.Instance.PlayerBody)
        {
            Global.Instance.Player.OnWalkInteraction(this);
        }
    }
}
