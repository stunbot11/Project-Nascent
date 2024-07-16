using Godot;
using System;
using System.Collections.Generic;

public partial class GameManagerThreeD : Node3D
{
    [Export] private PackedScene enemyScene;
    [Export] private Timer enemySpawnTimer;
    [Export] private PackedScene targetPointPrefab;
	private Camera3D cam;
	private bool clickedThisFrame = false;
	private Vector2 clickMousePos;
	private PlayerThreeD tempPlayer;

    private List<int> waveData = new();
    private int currentWavePosition;
    private int currentWaveIndex;
    private int waveCount;
    private Node enemiesParent;
    private TowerSelectionMenu towerMenu;

    struct EnemyData //defines the characterisitcs of the enemeies
    {
        public Texture3D sprite;
        public int healthPoints;
        public int speed;
    }

    private List<EnemyData> enemyData = new() //finds the png's for the enemies
	{
        //new EnemyData {sprite = GD.Load<Texture3D>("res://Assets/brackeys_platformer_assets/sprites/slime_green.png"), healthPoints = 5, speed = 3 },
        //new EnemyData {sprite = GD.Load<Texture3D>("res://Assets/brackeys_platformer_assets/sprites/slime_purple.png"), healthPoints = 10, speed = 1 },
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
        //StartWave(0);
        cam = GetNode<Camera3D>("CameraAnchor/Camera3D");
		tempPlayer = GetNode<PlayerThreeD>("TempCharacter");
        towerMenu = GetNode<TowerSelectionMenu>("TowerSelectionMenu");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("Move Player") && @event is InputEventMouseButton eventMouseButton && !towerMenu.getIsMenuShowing()) {
			clickedThisFrame = true;
			clickMousePos = eventMouseButton.Position;
		} else {
			clickedThisFrame = false;
		}

        if(@event.IsActionPressed("Open Tower Menu")) {
            towerMenu.setIsMenuShowing(!towerMenu.getIsMenuShowing());
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
    /*
    private void OnSpawnEnemy()
    {
        if (currentWavePosition >= waveData.Count) return;

        int enemyType = waveData[currentWavePosition];
        currentWavePosition++;

        //neeed to change enemy to 3d enemy
        enemy_3d enemy = enemyScene.Instantiate<enemy_3d>();
        enemiesParent.AddChild(enemy);
        EnemyData d = enemyData[enemyType];
        enemy.Initialize(d.sprite, d.healthPoints, d.speed);

    }
    */
    public void EndEnemy(Node enemy)
    {
        enemy.QueueFree();
        waveCount--;

        if (waveCount == 0) enemySpawnTimer.Stop();
    }
}