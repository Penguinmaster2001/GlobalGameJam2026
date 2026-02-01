
using System.Collections.Generic;
using System.Linq;
using Godot;



public partial class Player : CharacterBody2D
{
    [Export]
    private float _moveSpeed = 400.0f;

    [Export]
    private float _rotateSpeed = 0.5f;

    [Export]
    private float _acceleration = 0.2f;

    [Export]
    private float _currentDirection = 0.0f;

    [Export]
    private Node2D? _playerBase;



    private WalkInteraction? _currentInteraction = null;

    public Mask CurrentMask { get; set; } = Mask.Masks["none"];
    public List<Mask> PlayerMasks = [Mask.Masks["none"], Mask.Masks["low"], Mask.Masks["high"]];
    public int SuspicionLevel = 0;



    public void GetInput()
    {
        var speed = 0.0f;
        var inputDir = _currentDirection;
        var moveInput = Input.GetVector("left", "right", "up", "down");

        if (moveInput.LengthSquared() > 0.01f)
        {
            inputDir = Mathf.Atan2(moveInput.Y, moveInput.X);
            speed = _moveSpeed;
        }

        _currentDirection = Mathf.LerpAngle(_currentDirection, inputDir, _rotateSpeed);
        _playerBase!.Rotation = _currentDirection;

        var (s, c) = Mathf.SinCos(_currentDirection);

        Velocity = Velocity.Lerp(new Vector2(c, s) * speed, _acceleration);
    }



    public override void _PhysicsProcess(double delta)
    {
        GetInput();
        if (MoveAndSlide())
        {
            var collision = GetLastSlideCollision().GetCollider();
        }
        else
        {
            _currentInteraction = null;
        }
    }



    public void OnWalkInteraction(WalkInteraction walkInteraction)
    {
        if (walkInteraction != _currentInteraction)
        {
            _currentInteraction = walkInteraction;
            GD.Print(walkInteraction.InteractionName);

            if (Global.Instance.Interactions.Query(walkInteraction.InteractionId, out var interaction))
            {
                GD.Print(interaction.Dialogs[0].Text);
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
        GD.Print($"level: {level}, New mask: {CurrentMask.Level}");
    }
}
