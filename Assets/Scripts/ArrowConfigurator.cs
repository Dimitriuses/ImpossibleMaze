using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum ArrowDirection
{
    Up,
    Down,
    Right,
    Left
}

public enum TurnDirection
{
    Right,
    Left
}
public class ArrowConfigurator
{
    public Dictionary<ArrowDirection, ArrowDirection> TurnLeft { get; }
    public Dictionary<ArrowDirection, ArrowDirection> TurnRight { get; }
    public Dictionary<ArrowDirection, float> DirectionToZRotation { get; }
    public Dictionary<int, ArrowDirection> IntToArrowDirection { get; }

    private Random Random;

    public ArrowConfigurator()
    {
        TurnLeft = new Dictionary<ArrowDirection, ArrowDirection>()
        {
            {ArrowDirection.Up, ArrowDirection.Left },
            {ArrowDirection.Down, ArrowDirection.Right },
            {ArrowDirection.Right, ArrowDirection.Up },
            {ArrowDirection.Left, ArrowDirection.Down }
        };
        TurnRight = new Dictionary<ArrowDirection, ArrowDirection>()
        {
            {ArrowDirection.Up, ArrowDirection.Right },
            {ArrowDirection.Down, ArrowDirection.Left },
            {ArrowDirection.Right, ArrowDirection.Down },
            {ArrowDirection.Left, ArrowDirection.Up }
        };
        DirectionToZRotation = new Dictionary<ArrowDirection, float>()
        {
            {ArrowDirection.Up, 90f },
            {ArrowDirection.Down, 270f},
            {ArrowDirection.Right, 0f},
            {ArrowDirection.Left, 180f}
        };
        IntToArrowDirection = new Dictionary<int, ArrowDirection>()
        {
            {0, ArrowDirection.Up},
            {1, ArrowDirection.Down },
            {2, ArrowDirection.Right},
            {3, ArrowDirection.Left}
        };
        Random = new Random();
    }

    public ArrowDirection RandomDirection()
    {
        return IntToArrowDirection[Random.Next(0, 3)];
    }
    
}