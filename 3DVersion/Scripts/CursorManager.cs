using Godot;
using System;

public partial class CursorManager : Node
{
    Texture2D defaultCursor;
    Texture2D grabCursor;
    Texture2D zoomInCursor;
    Texture2D zoomOutCursor;

    public override void _Ready()
    {
        defaultCursor = ResourceLoader.Load<Texture2D>("res://3DVersion/Assets/hand_point.png");
        grabCursor = ResourceLoader.Load<Texture2D>("res://3DVersion/Assets/hand_closed.png");
        zoomInCursor = ResourceLoader.Load<Texture2D>("res://3DVersion/Assets/zoom_in.png");
        zoomOutCursor = ResourceLoader.Load<Texture2D>("res://3DVersion/Assets/zoom_out.png");

        Input.SetCustomMouseCursor(defaultCursor);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton e)
        {
            if (e.ButtonIndex == MouseButton.WheelUp) Input.SetCustomMouseCursor(zoomInCursor);
            else if (e.ButtonIndex == MouseButton.WheelDown) Input.SetCustomMouseCursor(zoomOutCursor);
            else if (e.Pressed) Input.SetCustomMouseCursor(grabCursor);
            else Input.SetCustomMouseCursor(defaultCursor);
        }
    }
}