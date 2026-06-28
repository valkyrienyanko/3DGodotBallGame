using Godot;
using System;

namespace BallGame;

public partial class Player : RigidBody3D
{
    [Export] private float _speed = 15;
    [Export] private float _jumpForce = 3;
    [Export] private RayCast3D _jumpRayCast;

    public override void _PhysicsProcess(double delta)
    {
        // Player can move in all directions
        Vector2 inputDir = Input.GetVector(InputAction.Right, InputAction.Left, InputAction.Backwards, InputAction.Forwards);

        ApplyCentralForce(new Vector3(inputDir.X, 0, inputDir.Y) * _speed);

        // Player can only jump if they pressed jump key and are actually touching the floor
        if (Input.IsActionJustPressed(InputAction.Jump) && _jumpRayCast.IsColliding())
        {
            ApplyCentralImpulse(Vector3.Up * _jumpForce);
        }

        // Since the jump raycast is not a child of the rigidbody, we need to update its position every frame
        _jumpRayCast.GlobalPosition = GlobalPosition;
    }
}
