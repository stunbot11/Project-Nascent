using Godot;
using System;

public partial class PlayerThreeD : CharacterBody3D
{
	private NavigationAgent3D navigationAgent;
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	[Export] public Vector3 velocity;
	[Export] private float speed = 2.0f;

    public override void _Ready()
    {
        navigationAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");
    }

    public override void _PhysicsProcess(double delta)
    {
        velocity = Velocity;

		if(!IsOnFloor()) {
			velocity.Y -= gravity * (float)delta;
		}

		if (!navigationAgent.IsNavigationFinished()) {
			Vector3 next = navigationAgent.GetNextPathPosition();
			// LookAt(next, Vector3.Up);
			Vector3 dir = (next - GlobalPosition).Normalized();
			velocity.X = dir.X * speed;
			velocity.Z = dir.Z * speed;
		} else {
			velocity.X = 0f;
			velocity.Z = 0f;
		}

		Velocity = velocity;
		MoveAndSlide();
    }

    public void SetTargetPosition(Vector3 position) {
		var map = GetWorld3D().NavigationMap;
		var p = NavigationServer3D.MapGetClosestPoint(map, position);
		navigationAgent.TargetPosition = p;
	}
}
