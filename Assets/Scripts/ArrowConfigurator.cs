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
    public Dictionary<ArrowDirection, float> DirectionToZRotation { get; }
    public Dictionary<float, ArrowDirection> ZRotationToArrowDirection { get; }

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
        DirectionToZRotation = new Dictionary<ArrowDirection, float>()
        {
            {ArrowDirection.Up, 90f },
            {ArrowDirection.Down, 270f},
            {ArrowDirection.Right, 0f},
            {ArrowDirection.Left, 180f}
        };
        ZRotationToArrowDirection = new Dictionary<float, ArrowDirection>()
        {
            {90f, ArrowDirection.Up},
            {270f, ArrowDirection.Down },
            {0f, ArrowDirection.Right},
            {180f, ArrowDirection.Left}
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