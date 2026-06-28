using Godot;
using System;

namespace BallGame;

public partial class Score : Label
{
    private double _time = 0;

    public override void _Process(double delta)
    {
        const double scoreMultiplier = 15;

        _time += delta * scoreMultiplier;
        Text = _time.ToString("0");
    }
}
