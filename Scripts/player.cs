using Godot;

public partial class player : CharacterBody2D
{
    [Export]
    public int Speed { get; set; } = 100;
    [Export]
    private NodePath HUDPath;
    [Export]
    private NodePath MiningTimerPath;
    
    private bool IsMining = false;
    private Timer MiningTimer;
    private int CoalAmount = 0;
    private HUD HudNode;
    private Vector2 _target;

    public override void _Ready()
    {
        MiningTimer = GetNode<Timer>(MiningTimerPath);
        HudNode = GetNode<HUD>(HUDPath);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("Move"))
        {
            _target = GetGlobalMousePosition();
            SetIsMining(false);
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

    public void SetIsMining(bool isMining)
    {
        IsMining = isMining;
        if(IsMining)
        {
            MiningTimer.Start();
        } else {
            MiningTimer.Stop();
        }
    }

    public void _on_mining_timer_timeout()
    {
        CoalAmount++;
        HudNode.UpdateCoal(CoalAmount);
    }
}
