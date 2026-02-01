
using System.Collections.Generic;
using System.Linq;
using Godot;



public class Player : IInput
{
    private WalkInteraction? _currentInteraction = null;

    public Mask CurrentMask { get; set; } = Mask.Masks["none"];
    public List<Mask> PlayerMasks = [Mask.Masks["none"]];
    public int SuspicionLevel = 0;

    private readonly TopDownCharacter _playerCharacter;



    public Player(TopDownCharacter playerCharacter)
    {
        _playerCharacter = playerCharacter;
        _playerCharacter.Input = this;
    }



    public Vector2 GetInput() => Input.GetVector("left", "right", "up", "down");



    public void OnWalkInteraction(WalkInteraction walkInteraction)
    {
        if (walkInteraction != _currentInteraction)
        {
            _currentInteraction = walkInteraction;
            GD.Print(walkInteraction.InteractionName);

            if (Global.Instance.Interactions.Query(walkInteraction.InteractionId, out var interaction))
            {
                Global.Instance.Director.StartInteraction(interaction);
            }
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
}
