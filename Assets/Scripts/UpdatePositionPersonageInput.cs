using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UpdatePositionPersonageInput
{
    public Vector3Int OldPosition { get; set; }
    public Vector3Int NewPosition { get; set; }
    public TileBase TileBase { get; set; }
}

