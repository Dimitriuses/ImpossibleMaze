using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;



public class Room
{
    public Vector2Int Position { get; }
    public ArrowDirection ArrowDirection { get; set; }
    public RoomWalls Walls { get; set; }


    public Room(Vector2Int position, ArrowDirection arrowDirection)
    {
        Position = position;
        ArrowDirection = arrowDirection;
    }
    public Room(Vector2Int position)
    {
        Position = position;
    }
}

