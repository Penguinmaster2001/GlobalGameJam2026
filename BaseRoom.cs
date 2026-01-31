
using System.Collections.Generic;
using Godot;
using Godot.Collections;



public partial class BaseRoom : Control
{
    [Export]
    private TextureRect? _texture;
    


    [Export]
    private Array<RoomInteraction> _roomInteractionsArray = [];
    private List<RoomInteraction> _roomInteractions = [];



    public override void _Ready()
    {
        _roomInteractions = [.. _roomInteractionsArray];
    }
}
