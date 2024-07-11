using Godot;
using System;
using System.Diagnostics;

public partial class enemy_3d : CharacterBody3D
{
    [Export] private NavigationAgent3D navigationAgent;
    [Export] private Node3D target;

    private int _healthpoints;
    private float speed = 1;
    private float accel = 10;

    private Vector3 direction;

    private GameManagerThreeD gameManager;

    public override void _Ready()
    {
        gameManager = GetNode<GameManagerThreeD>("/root/Root");
    }

    public override void _Process(double delta)
    {
        navigationAgent.TargetPosition = target.GlobalPosition;
        direction = navigationAgent.GetNextPathPosition() - GlobalPosition;
        direction = direction.Normalized();
        Velocity = Velocity.Lerp(direction * speed, accel * (float)delta);

        MoveAndSlide();
    }

    public void Initialize(Texture3D sprite, int healthpoints, float _speed)
    {
        //GetNode<Sprite3D>("Sprite3D").sprite = sprite;
        _healthpoints = healthpoints;
        speed = _speed;
    }
}
