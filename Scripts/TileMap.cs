using Godot;
using System;

public partial class TileMap : Godot.TileMap
{
	[Export] private NodePath _playerPath;
	private player _player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = GetNode<player>(_playerPath);
	}

	public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("Move"))
        {
            Vector2 clickPosition = GetGlobalMousePosition();
            Vector2I tilePosition = LocalToMap(clickPosition);
            HandleTileClick(tilePosition);
        }
    }

    private void HandleTileClick(Vector2I tilePosition)
    {
		TileData tileClicked = GetCellTileData(0, tilePosition);
		if (tileClicked != null && (bool)tileClicked.GetCustomData("isMineable"))
		{
			_player.SetIsMining(true);
		}
    }
}
