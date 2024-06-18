using Godot;
using System;

public partial class gameManager : Node2D
{
	private RadialMenu radialMenu;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		radialMenu = GetNode<RadialMenu>("RadialMenu");
		radialMenu.Hide();
		GD.Print(radialMenu);
	}

	public override void _Input(InputEvent @event)
    {
		if (@event.IsActionPressed("Open Tower Menu"))
		{
			// Show the radial menu at the mouse position
			// GD.Print("Open tower menu");
			// GD.Print(GetViewport().GetMousePosition(), GetGlobalMousePosition());
			radialMenu.SetPosition(GetGlobalMousePosition());
			radialMenu.Show();

		}
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
