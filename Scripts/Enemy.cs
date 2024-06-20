using Godot;
using System;

public partial class Enemy : PathFollow2D
{
    private int _healthpoints;
    private float _speed;

    private gameManager gameManager;

    public override void _Ready()
    {
        gameManager = GetNode<gameManager>("/root/Game");
    }

    public override void _Process(double delta)
    {
        ProgressRatio += _speed * (float)delta * 0.05f;
        if (ProgressRatio == 1) // reached the end of the path
            gameManager.EndEnemy(this);
    }

    public void Initialize(Texture2D sprite, int healthpoints, float speed)
    {
        GetNode<Sprite2D>("Sprite2D").Texture = sprite;
        _healthpoints = healthpoints;
        _speed = speed;
    }

    /* way to damage
    private void OnClicked(Node viewport, InputEvent @event, int shapeIdx)
    {
        if (@event is InputEventMouseButton e && e.ButtonIndex == MouseButton.Left && e.Pressed)
        {
            _healthpoints--;
            if (_healthpoints == 0)
                gameManager.EndEnemy(this);
        }
    }
    */
}
