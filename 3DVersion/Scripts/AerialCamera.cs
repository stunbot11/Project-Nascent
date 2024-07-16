using Godot;
using System;
using System.Numerics;

public partial class AerialCamera : Node3D
{
	private const float DragSpeed = 0.01f;
	private Camera3D cam;
	private bool isZoomingIn = false;
	private bool isZoomingOut = false;
	private float screenRatio;
	private bool isDragging = false;
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
		if (@event.IsActionPressed("Camera Zoom In")) {
			if(@event is InputEventMouseButton) {
				float newZoomSize = cam.Size + 0.3f * -1f;
				cam.Size = Mathf.Clamp(newZoomSize, 10f, 50f);
			} else {
				isZoomingIn = true;
			}
		}

		if (@event.IsActionReleased("Camera Zoom In")) {
			isZoomingIn = false;
		}

		if (@event.IsActionPressed("Camera Zoom Out")) {
			if(@event is InputEventMouseButton) {
				float newZoomSize = cam.Size + 0.3f * 1f;
				cam.Size = Mathf.Clamp(newZoomSize, 10f, 50f);
			} else {
				isZoomingOut = true;
			}
		}

		if (@event.IsActionReleased("Camera Zoom Out")) {
			isZoomingOut = false;
		}

		if (@event.IsActionPressed("Camera Pan")) {
			isDragging = true;
			if (@event is InputEventJoypadMotion eventJoypadMotion) {
				// GD.Print("Joypad Motion: ", eventJoypadMotion.Axis, eventJoypadMotion.AxisValue);
				if (eventJoypadMotion.AxisValue >= 1) {
					GD.Print("Above 1: le sigh ", eventJoypadMotion.AxisValue);
					GlobalPosition += rightVec * -eventJoypadMotion.AxisValue * DragSpeed + forwardVec * -eventJoypadMotion.AxisValue * DragSpeed / screenRatio;
				}
			}

			if  (@event is InputEventKey eventKey) {
				GD.Print("Keyboard: ", eventKey.Keycode);
			}

			if (@event is InputEventMouseMotion eventMouseMotion) {
				GD.Print("Mouse Motion: ", eventMouseMotion);
				GlobalPosition += rightVec * -eventMouseMotion.Relative.X * DragSpeed + forwardVec * -eventMouseMotion.Relative.Y * DragSpeed / screenRatio;
			}
			// GlobalPosition += rightVec * 10 * DragSpeed + forwardVec * 10 * DragSpeed / screenRatio;
		}

		if (@event.IsActionReleased("Camera Pan")) {
			isDragging = false;
			if (@event is InputEventJoypadMotion eventJoypadMotion) {
				GD.Print("Joypad Motion: ", eventJoypadMotion.Axis, eventJoypadMotion.AxisValue);
				// GlobalPosition += rightVec * 
			}

			if  (@event is InputEventKey eventKey) {
				GD.Print("Keyboard: ", eventKey.Keycode);
			}

			if (@event is InputEventMouseButton eventMouseButton) {
				GD.Print("Mouse Button: ", eventMouseButton);
			}
			GlobalPosition += rightVec * 10 * DragSpeed + forwardVec * 10 * DragSpeed / screenRatio;
		}
		// if(@event is InputEventMouseButton e) {
		// } else if(@event is InputEventMouseMotion m && dragging) {
		// 	GlobalPosition += 
		// 		rightVec * -m.Relative.X * DragSpeed + 
		// 		forwardVec * -m.Relative.Y * DragSpeed / screenRatio;
		// }
    }

    public override void _PhysicsProcess(double delta) {
        if(isZoomingIn) {
			float newZoomSize = cam.Size + 0.3f * -1f;
			cam.Size = Mathf.Clamp(newZoomSize, 10f, 50f);
		}

        if(isZoomingOut) {
			float newZoomSize = cam.Size + 0.3f * 1f;
			cam.Size = Mathf.Clamp(newZoomSize, 10f, 50f);
		}

		// if(isDragging) {

		// }
    }
}
