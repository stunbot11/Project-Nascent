using Godot;

public partial class player : CharacterBody2D
{
    [Export]
    public int Speed { get; set; } = 100;

    private Vector2 _target;

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("Move"))
        {
            _target = GetGlobalMousePosition();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Velocity = Position.DirectionTo(_target) * Speed;
        LookAt(_target);
        if (Position.DistanceTo(_target) > 1)
        {
            MoveAndSlide();
        }
    }
}
