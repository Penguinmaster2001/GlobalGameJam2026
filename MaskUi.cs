
using System.Collections.Generic;
using System.Linq;
using Godot;



public partial class MaskUi : Control
{
    [Export]
    public required GridContainer _maskContainer;


    [Export]
    private Player? _player;



    public void AddMasks(List<Mask> masks)
    {
        foreach (var mask in masks.OrderBy(m => m.Level))
        {
            var maskButton = new Button
            {
                Icon = mask.Texture
            };
            maskButton.ButtonDown += () => ActivateMask(mask.Level);
            _maskContainer.AddChild(maskButton);
        }
    }



    public void ActivateMask(int level)
    {
        _player!.SetCurrentMask(level);
    }
}
