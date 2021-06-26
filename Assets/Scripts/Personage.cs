using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Personage
{
    public Vector3Int Position { get; }

    public Personage(Vector3Int position)
    {
        Position = position;
    }
}

