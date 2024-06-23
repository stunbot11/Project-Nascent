using Godot;
using System;
using System.Numerics;

public partial class AerialCamera : Node3D
{
	private const float DragSpeed = 0.01f;
	private Camera3D cam;
	private float screenRatio;
	private bool dragging;
	private Godot.Vector3 rightVec, forwardVec;

	public override void _Ready()
	{
		cam = GetNode<Camera3D>("Camera3D");
		Godot.Vector2 screenSize = GetViewport().GetVisibleRect().Size;
		screenRatio = screenSize.Y / screenSize.X;
		GetMoveVectors();
	}

	private void GetMoveVectors() {
		Godot.Vector3 offset = cam.GlobalPosition - GlobalPosition;
		rightVec = cam.Transform.Basis.X;
		forwardVec = new Godot.Vector3(offset.X, 0, offset.Z).Normalized();
	}

    public override void _Input(InputEvent @event) {
		if(@event is InputEventMouseButton e) {
			if(e.ButtonIndex == MouseButton.WheelUp || e.ButtonIndex == MouseButton.WheelDown) {
				if(dragging) return;
				float newZoomSize = cam.Size + 0.3f * (e.ButtonIndex == MouseButton.WheelUp ? -1f : 1f);
				cam.Size = Mathf.Clamp(newZoomSize, 3f, 15f);
			} else {
				dragging = e.Pressed;
			}
		} else if(@event is InputEventMouseMotion m && dragging) {
			GlobalPosition += 
				rightVec * -m.Relative.X * DragSpeed + 
				forwardVec * -m.Relative.Y * DragSpeed / screenRatio;
		}
    }
}
