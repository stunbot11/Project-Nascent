using Godot;
using System;

public partial class RadialMenu : Control
{
	public void _on_tower_pressed()
	{
		GD.Print("Tower Pressed");
		Hide();
	}

	public void _on_close_button_pressed()
	{
		Hide();
	}
}
