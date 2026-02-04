
using System.Collections.Generic;
using System.Linq;
using Godot;



public class Player : IInput
{
    private InteractionTrigger? _currentInteraction = null;

    public Mask CurrentMask { get; set; } = Mask.Masks["none"];
    public List<Mask> PlayerMasks = [Mask.Masks["none"]];
    private int _suspicionLevel = 0;
    public int SuspicionLevel
    {
        get => _suspicionLevel; set
        {
            _suspicionLevel = value;
            Global.Instance.SusUi.Set("health", value);
        }
    }

    private readonly TopDownCharacter _playerCharacter;



    public Player(TopDownCharacter playerCharacter)
    {
        _playerCharacter = playerCharacter;
        _playerCharacter.Input = this;
    }



    public Vector2 GetInput() => Input.GetVector("left", "right", "up", "down");



    public void OnInteraction(InteractionTrigger walkInteraction)
    {
        if (walkInteraction != _currentInteraction)
        {
            _currentInteraction = walkInteraction;
            foreach (var action in walkInteraction.DialogActions)
            {
                Global.Instance.Director.DoAction(action);
            }
            walkInteraction.CurrentInteraction = Global.Instance.Director.TryInteractions(_currentInteraction.InteractionId, walkInteraction.CurrentInteraction) + 1;
        }
    }



    public void GiveMask(int level)
    {
        if (PlayerMasks.Any(m => m.Level == level)) return;

        PlayerMasks.Add(Mask.Masks[Mask.MaskNames[level]]);
    }



    public void SetCurrentMask(int level)
    {
        CurrentMask = PlayerMasks.Find(m => m.Level == level) ?? Mask.Masks["none"];
        _playerCharacter.CurrentMask = CurrentMask;
        GD.Print($"level: {level}, New mask: {CurrentMask.Level}");
    }



    internal void OnColliderInteractionLeave(InteractionTrigger walkInteraction)
    {
        if (walkInteraction == _currentInteraction)
        {
            _currentInteraction = null;
        }
    }
}
