using Godot;
using System;

namespace BallGame;

public partial class Enemy : RigidBody3D
{
    private const double LifeTime = 10;

    private double _time;

    public override void _Ready()
    {
        ApplyInitialForce();
    }

    public override void _Process(double delta)
    {
        _time += delta;

        if (_time >= LifeTime)
        {
            QueueFree(); // delete this enemy
        }
    }

    private void ApplyInitialForce()
    {
        // Throw the enemy at a downward angle with a random force
        const double minForce = 150;
        const double maxForce = 200;

        Vector3 direction = (Vector3.Forward + Vector3.Down).Normalized();

        float force = (float)GD.RandRange(minForce, maxForce);

        ApplyCentralImpulse(direction * force);
    }
}
