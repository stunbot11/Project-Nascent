using Godot;
using System;

public partial class CursorManager : Node
{
	Texture2D defaultCursor;
	Texture2D grabCursor;

	public override void _Ready()
	{
		defaultCursor = ResourceLoader.Load<Texture2D>("res://3DVersion/Assets/hand_point.png");
		grabCursor = ResourceLoader.Load<Texture2D>("res://3DVersion/Assets/hand_closed.png");

		Input.SetCustomMouseCursor(defaultCursor);
	}

    public override void _Input(InputEvent @event)
    {
        if(@event is InputEventMouseButton e) {
			if(e.Pressed) Input.SetCustomMouseCursor(grabCursor);
			else Input.SetCustomMouseCursor(defaultCursor);
		}
    }
}
