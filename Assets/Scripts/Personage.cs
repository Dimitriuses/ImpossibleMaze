using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Personage
{
    public Vector2Int Position { get; set; }

    public Personage(Vector2Int position)
    {
        Position = position;
    }
}

