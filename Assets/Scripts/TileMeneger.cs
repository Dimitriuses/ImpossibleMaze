using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMeneger
{
    public static List<Room> GetAllRooms(Tilemap tilemap)
    {
        List<Room> returnRooms = new List<Room>();
        BoundsInt size = tilemap.cellBounds;
        int colNumbers = size.yMax - size.yMin;
        int rowNumbers = size.xMax - size.xMin;
        for (int col = colNumbers; col >= 0; col--)
        {
            for (int row = 0; row <= rowNumbers; row++)
            {
                
                TileBase currentTile = tilemap.GetTile(new Vector3Int(row, col, 0));
                if (currentTile != null)
                {
                    //Debug.Log("x:" + row + " y:" + col);
                    returnRooms.Add(new Room(new Vector2Int(row, col), ArrowDirection.Right));
                    //count++;
                }
                else
                {
                    //Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }
            }
        }
        return returnRooms;
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
    public static void TestTilemapRooms(List<Room> rooms, ref Tilemap tilemap)
    {
        foreach (var room in rooms)
        {
            tilemap.SetColor(new Vector3Int(room.Position.x, room.Position.y, 0), Color.red);
            Debug.Log(room.Position.ToString());
        }
    }
}
