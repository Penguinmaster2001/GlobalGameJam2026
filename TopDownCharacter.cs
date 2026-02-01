
using Godot;



public partial class TopDownCharacter : CharacterBody2D
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

    [Export]
    private AnimationPlayer? _animation;

    [Export]
    private Sprite2D? _maskSprite;



    public IInput? Input;



    private static readonly string[] _animationDirections = ["right", "down", "left", "up"];
    private static readonly int[] _maskDirections = [1, 0, 3, 2];



    private WalkInteraction? _currentInteraction = null;

    public Mask CurrentMask { get; set; } = Mask.Masks["none"];



    public void GetInput()
    {
        var speed = 0.0f;
        var animationState = "stand";
        var inputDir = _currentDirection;
        var moveInput = Input!.GetInput();

        if (moveInput.LengthSquared() > 0.01f)
        {
            animationState = "walk";
            inputDir = Mathf.Atan2(moveInput.Y, moveInput.X);
            speed = _moveSpeed;
        }

        _currentDirection = Mathf.PosMod(Mathf.LerpAngle(_currentDirection, inputDir, _rotateSpeed), Mathf.Tau);
        var displayDir = Mathf.FloorToInt(Mathf.PosMod(0.5 + (4.0 * _currentDirection / Mathf.Tau), 4.0));
        var animationDir = _animationDirections[displayDir];
        var maskDir = _maskDirections[displayDir];
        _animation!.Play($"{animationState}_{animationDir}");
        _maskSprite!.FrameCoords = new(maskDir, CurrentMask.Level);

        var (s, c) = Mathf.SinCos(_currentDirection);

        Velocity = Velocity.Lerp(new Vector2(c, s) * speed, _acceleration);
    }



    public override void _PhysicsProcess(double delta)
    {
        GetInput();
        MoveAndSlide();
    }
}

