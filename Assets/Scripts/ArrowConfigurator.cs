using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
    public Dictionary<ArrowDirection, ArrowDirection> Opposite { get; }
    public Dictionary<ArrowDirection, RotateDirection> ArrowToRotate { get; }
    public Dictionary<RotateDirection, ArrowDirection> RotateToArrow { get; }

    private System.Random Random;

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
        Opposite = new Dictionary<ArrowDirection, ArrowDirection>()
        {
            {ArrowDirection.Up, ArrowDirection.Down },
            {ArrowDirection.Down, ArrowDirection.Up },
            {ArrowDirection.Right, ArrowDirection.Left },
            {ArrowDirection.Left, ArrowDirection.Right }
        };
        ArrowToRotate = new Dictionary<ArrowDirection, RotateDirection>()
        {
            {ArrowDirection.Up, RotateDirection.Left},
            {ArrowDirection.Down, RotateDirection.Right},
            {ArrowDirection.Right, RotateDirection.Up},
            {ArrowDirection.Left, RotateDirection.Down}
        };
        RotateToArrow = new Dictionary<RotateDirection, ArrowDirection>()
        {
            {RotateDirection.Left, ArrowDirection.Up},
            {RotateDirection.Right, ArrowDirection.Down},
            {RotateDirection.Up, ArrowDirection.Right},
            {RotateDirection.Down, ArrowDirection.Left}
        };
        Random = new System.Random();
    }

    public ArrowDirection RandomDirection()
    {
        return (ArrowDirection)Random.Next(0, 3);
    }

    public Vector2Int MoveToDirection(Vector2Int positionIn,ArrowDirection direction)
    {
        switch (direction)
        {
            case ArrowDirection.Up:
                return new Vector2Int(positionIn.x, positionIn.y + 1);
            case ArrowDirection.Down:
                return new Vector2Int(positionIn.x, positionIn.y - 1);
            case ArrowDirection.Right:
                return new Vector2Int(positionIn.x + 1, positionIn.y);
            case ArrowDirection.Left:
                return new Vector2Int(positionIn.x - 1, positionIn.y);
        }
        return positionIn;
    }
    
}