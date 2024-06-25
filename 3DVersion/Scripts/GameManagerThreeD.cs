using Godot;
using System;

public partial class GameManagerThreeD : Node3D
{
	[Export] private PackedScene targetPointPrefab;
	private Camera3D cam;
	private bool clickedThisFrame = false;
	private Vector2 clickMousePos;
	private PlayerThreeD tempPlayer;

    public override void _Ready()
    {
        cam = GetNode<Camera3D>("CameraAnchor/Camera3D");
		tempPlayer = GetNode<PlayerThreeD>("TempCharacter");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton e && e.Pressed && e.ButtonIndex == MouseButton.Left) {
			clickedThisFrame = true;
			clickMousePos = e.Position;
		} else {
			clickedThisFrame = false;
		}
    }

    public override void _PhysicsProcess(double delta)
    {
        if (clickedThisFrame) {
			var from = cam.ProjectRayOrigin(clickMousePos);
			var to = from + cam.ProjectRayNormal(clickMousePos) * 1000f;

			var spaceState = GetWorld3D().DirectSpaceState;
			// Anything on Collision Layer 2 will be hit with this and be able to move character
			var query = PhysicsRayQueryParameters3D.Create(from, to, 2);
			var result = spaceState.IntersectRay(query);
			if(result.Count > 0) {
				Node3D p = targetPointPrefab.Instantiate<Node3D>();
				AddChild(p);
				p.GlobalPosition = result["position"].AsVector3();

				tempPlayer.SetTargetPosition(result["position"].AsVector3());
			}
		}
    }
}