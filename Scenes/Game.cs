using Godot;
using System;

namespace BallGame;

public partial class Game : Node
{
    // Exports are exposed to Godot editor inspector for quick editing
    [Export] private PackedScene _enemyScene;
    [Export] private Marker3D _enemySpawnMarker;
    [Export] private WorldEnvironment _worldEnvironment;

    // The initial spawn frequency of the enemies
    private const double InitialSpawnFrequency = 2;

    // Fields
    private double _spawnFrequency = InitialSpawnFrequency;
    private double _curSpawnTime; // current spawn time
    private ProceduralSkyMaterial _skyMat;
    private Godot.Environment _environment;

    public override void _Ready()
    {
        _environment = _worldEnvironment.Environment;
        _skyMat = (ProceduralSkyMaterial)_environment.Sky.SkyMaterial;;
    }

    public override void _Process(double delta)
    {
        SpawnEnemies(delta);
        IncreaseSpawnFrequencyOverTime(delta);
        ApplyVFX();
    }

    private void ApplyVFX()
    {
        const float lerpTime = 0.0005f;
        
        // Fade everything to black / gray
        _skyMat.SkyTopColor = _skyMat.SkyTopColor.Lerp(Colors.Black, lerpTime);
        _skyMat.GroundBottomColor = _skyMat.GroundBottomColor.Lerp(Colors.Black, lerpTime);
        _environment.AdjustmentSaturation = Mathf.Lerp(_environment.AdjustmentSaturation, 0, lerpTime);
    }

    // Increase enemy spawn rate over time
    private void IncreaseSpawnFrequencyOverTime(double delta)
    {
        const double increaseSpawnFrequencyFactor = 0.1;
        const double minSpawnFrequency = 0.2;

        _spawnFrequency -= delta * increaseSpawnFrequencyFactor;
        _spawnFrequency = Math.Max(minSpawnFrequency, _spawnFrequency);
    }
    
    // Spawn enemies every _spawnFrequency seconds
    private void SpawnEnemies(double delta)
    {
        _curSpawnTime += delta;

        if (_curSpawnTime >= _spawnFrequency)
        {
            _curSpawnTime = 0;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        const double offsetMaxRange = 3;

        // Spawn an enemy at a random horizontal offset
        float offset = (float)GD.RandRange(-offsetMaxRange, offsetMaxRange);

        Enemy enemy = _enemyScene.Instantiate<Enemy>();
        GetParent().AddChild(enemy); // Add the enemy to the world
        enemy.GlobalPosition = _enemySpawnMarker.GlobalPosition + new Vector3(offset, 0, 0);
    }
}
