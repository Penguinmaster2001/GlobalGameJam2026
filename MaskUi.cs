
using System.Collections.Generic;
using System.Linq;
using Godot;



public partial class MaskUi : Control
{
    [Export]
    public required GridContainer _maskContainer;



    private readonly Dictionary<int, TextureButton> _maskButtons = [];



    public void AddMasks(IEnumerable<Mask> masks)
    {
        _maskContainer.Columns = masks.Count();
        foreach (var mask in masks.OrderBy(m => m.Level))
        {
            var maskButton = new TextureButton
            {
                TexturePressed = mask.Texture,
                TextureDisabled = GD.Load<Texture2D>($"{Mask.BaseTexturePath}{mask.Level}_hidden.png"),
                TextureNormal = GD.Load<Texture2D>($"{Mask.BaseTexturePath}{mask.Level}_unselected.png"),
                SizeFlagsHorizontal = SizeFlags.ExpandFill,
                SizeFlagsVertical = SizeFlags.ExpandFill,
                IgnoreTextureSize = true,
                StretchMode = TextureButton.StretchModeEnum.KeepAspectCentered,
                ToggleMode = true,
                Disabled = true,
                ButtonPressed = false,
            };
            maskButton.Toggled += t => ActivateMask(t, mask.Level);
            _maskButtons.Add(mask.Level, maskButton);
            _maskContainer.AddChild(maskButton);
        }
        EnableMask(0);
        ActivateMask(false, -1);
    }



    public void ActivateMask(bool activate, int level)
    {
        if (!activate)
        {
            level = 0; // Set none mask
            _maskButtons[0].SetPressedNoSignal(true);
        }

        foreach (var (id, button) in _maskButtons)
        {
            if (id != level)
            {
                button.SetPressedNoSignal(false);
            }
        }
        Global.Instance.Player.SetCurrentMask(level);
    }



    public void EnableMask(int level)
    {
        GD.Print($"Enabling mask {level}");
        if (_maskButtons[level].Disabled)
        {
            _maskButtons[level].Disabled = false;
        }
    }
}
