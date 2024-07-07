using Godot;
using System;

public partial class enemy_3d : CharacterBody3D
{
    [Export] private NavigationAgent3D navigationAgent;

    private int _healthpoints;
    private float speed;
    private float accel;
    private float deltaTime;

    private Vector3 direction;

    private Node3D target;
    private gameManager gameManager;

    public override void _Ready()
    {
        gameManager = GetNode<gameManager>("/root/Root");
    }

    public override void _Process(double delta)
    {
        navigationAgent.TargetPosition = target.Position;
        direction = navigationAgent.GetNextPathPosition().Normalized();
        Velocity = Velocity.Lerp(direction * speed, accel * (float)delta);

    }

    public void Initialize(Texture3D sprite, int healthpoints, float _speed)
    {
        //GetNode<Sprite3D>("Sprite3D").sprite = sprite;
        _healthpoints = healthpoints;
        speed = _speed;
    }
}
