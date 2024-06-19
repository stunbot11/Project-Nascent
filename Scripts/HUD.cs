using Godot;
using System;

public partial class HUD : CanvasLayer
{
	[Export]
	private NodePath CoalLabelPath;
	private Label CoalLabel;

    public override void _Ready()
    {
        CoalLabel = GetNode<Label>(CoalLabelPath);
    }
    public void UpdateCoal(int newAmount)
	{
		CoalLabel.Text = $"Coal: {newAmount}";
	}
}
