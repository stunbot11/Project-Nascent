using Godot;
using System;
using System.Collections.Generic;

public partial class gameManager : Node2D
{
    [Export] private PackedScene enemyScene;
	[Export] private Timer enemySpawnTimer;
    
	private RadialMenu radialMenu;
    private List<int> waveData = new();

    private int currentWavePosition;
	private int currentWaveIndex;
	private int waveCount;
	private Node enemiesParent;

    // Called when the node enters the scene tree for the first time.

    struct EnemyData //defines the characterisitcs of the enemeies
	{
		public Texture2D sprite;
		public int healthPoints;
		public int speed;
	}

	private List<EnemyData> enemyData = new() //finds the png's for the enemies
	{
		new EnemyData {sprite = GD.Load<Texture2D>("res://Assets/brackeys_platformer_assets/sprites/slime_green.png"), healthPoints = 5, speed = 3 },
		new EnemyData {sprite = GD.Load<Texture2D>("res://Assets/brackeys_platformer_assets/sprites/slime_purple.png"), healthPoints = 10, speed = 1 },
	};

	private List<(int, int)[]> waves = new() //defines what spawns on what wave
	{
		new (int, int)[] {(0,2), (1,2)},
		new (int, int)[] {(0,7), (1,2)},
		new (int, int)[] {(0,3), (1,3), (0,5)},
		new (int, int)[] {(1,3), (0, 10)},
		new (int, int)[] {(0,15), (1,10), (0,5)},
	};

	public override void _Ready()
	{
		enemiesParent = GetNode("Enemy path");
		StartWave(0);
		radialMenu = GetNode<RadialMenu>("RadialMenu");
		radialMenu.Hide();
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

	private void StartWave(int waveIndex)
	{
		currentWaveIndex = waveIndex;

		waveData.Clear();
		foreach ((int type, int count) in waves[waveIndex])
		{
			for (int i = 0; i < count; i++)
			{
				waveData.Add(type);
			}
		}
		currentWavePosition = 0;
		waveCount = waveData.Count;
		enemySpawnTimer.Start();
	}

	private void OnSpawnEnemy()
	{
		if (currentWavePosition >= waveData.Count) return;
		
            int enemyType = waveData[currentWavePosition];
            currentWavePosition++;

			Enemy enemy = enemyScene.Instantiate<Enemy>();
			enemiesParent.AddChild(enemy);
			EnemyData d = enemyData[enemyType];
			enemy.Initialize(d.sprite, d.healthPoints, d.speed);
        
	}

    public void EndEnemy(Node enemy)
    {
        enemy.QueueFree();
        waveCount--;

        if (waveCount == 0) enemySpawnTimer.Stop();
    }
}
