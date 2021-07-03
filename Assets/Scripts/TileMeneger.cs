using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMeneger
{
    public static List<Room> GetRooms(Tilemap tilemap)
    {
        List<Room> returnRooms = new List<Room>();
        BoundsInt size = tilemap.cellBounds;
        for (int col = size.position.y; col <= size.size.y; col++)
        {
            for (int row = size.position.x; row <= size.size.x; row++)
            {
                
                TileBase currentTile = tilemap.GetTile(new Vector3Int(row, col, 0));
                if (currentTile != null)
                {
                    returnRooms.Add(new Room(new Vector2Int(row, col), ArrowDirection.Right));
                }
            }
        }
        return returnRooms;
    }
    public static List<Personage> GetPersonages(Tilemap tilemap)
    {
        List<Personage> returnPersonage = new List<Personage>();
        BoundsInt size = tilemap.cellBounds;
        for (int col = size.position.y; col <= size.size.y; col++)
        {
            for (int row = size.position.x; row <= size.size.x; row++)
            {
                Vector3Int position = new Vector3Int(row, col, 0);
                TileBase currentTile = tilemap.GetTile(position);
                if (currentTile != null)
                {
                    returnPersonage.Add(new Personage(new Vector2Int(row, col)));
                }
            }
        }
        return returnPersonage;
    }

    public static void UpdateTilemapRooms(List<Room> rooms, ref Tilemap tilemap, ArrowConfigurator arrowConfigurator)
    {
        foreach (var room in rooms)
        {
            tilemap.SetTransformMatrix(
                new Vector3Int(room.Position.x, room.Position.y, 0),
                Matrix4x4.TRS(
                    Vector3.zero,
                    Quaternion.Euler(0f, 0f, arrowConfigurator.DirectionToZRotation[room.ArrowDirection]),
                    Vector3.one)
                );
        }
    }
    public static void UpdatePositionTilemapPersonage(List<UpdatePositionPersonageInput> inputs, ref Tilemap tilemap)
    {
        foreach (var input in inputs)
        {
            TileBase tile = tilemap.GetTile(input.OldPosition);
            if(tile != null)
            {
                input.TileBase = tile;
            }
        }
        tilemap.ClearAllTiles();
        foreach (var input in inputs)
        {
            if(input.TileBase != null)
            {
                tilemap.SetTile(input.NewPosition, input.TileBase);
            }
        }
    }
    public static void TestTilemapRooms(List<Room> rooms, ref Tilemap tilemap)
    {
        foreach (var room in rooms)
        {
            tilemap.SetColor(new Vector3Int(room.Position.x, room.Position.y, 0), Color.red);
        }
    }

}
