﻿using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMeneger
{
    private List<Wall> Walls;

    public TileMeneger( List<Wall> walls)
    {
        Walls = walls;
    }

    public List<Room> GetRooms(Tilemap roomTilemap, Tilemap arrowTilemap, Tilemap wallTilemap, ArrowConfigurator configurator, Sprite arrowSprite)
    {
        List<Room> returnRooms = new List<Room>();
        BoundsInt size = roomTilemap.cellBounds;
        for (int col = size.position.y; col <= size.size.y; col++)
        {
            for (int row = size.position.x; row <= size.size.x; row++)
            {
                Vector3Int position = new Vector3Int(row, col, 0);
                Vector2Int savePosition = new Vector2Int(row, col);
                TileBase currentRoomTile = roomTilemap.GetTile(position);
                TileBase curentArrowTile = arrowTilemap.GetTile(position);
                TileBase curentWallTile = wallTilemap.GetTile(position);
                if (currentRoomTile != null)
                {
                    Room room = null;
                    if (curentArrowTile == null)
                    {
                        Tile tile = CreateTile(arrowSprite);
                        arrowTilemap.SetTile(position, tile);
                        room = new Room(savePosition, configurator.RandomDirection());
                    }
                    else
                    {
                        Matrix4x4 tileTransform = arrowTilemap.GetTransformMatrix(position);
                        Vector3 tileZRotate = tileTransform.rotation.eulerAngles;
                        //Debug.Log(tileZRotate);
                        ArrowDirection direction = configurator.ZRotationToArrowDirection[tileZRotate.z];
                        room = new Room(savePosition, direction);
                    }
                    if(curentWallTile != null)
                    {
                        Sprite tmpSprite = wallTilemap.GetSprite(position);
                        Wall wall = GetWallBySprite(tmpSprite);
                        Matrix4x4 tileTransform = wallTilemap.GetTransformMatrix(position);
                        Vector3 tileZRotate = tileTransform.rotation.eulerAngles;
                        ArrowDirection direction = configurator.ZRotationToArrowDirection[tileZRotate.z];
                        room.Walls = wall.GetWalls(direction);
                    }
                    returnRooms.Add(room);
                }
            }
        }
        return returnRooms;
    }
    public List<Personage> GetPersonages(Tilemap tilemap)
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

    public void UpdateTilemapRooms(List<Room> rooms, ref Tilemap tilemap, ArrowConfigurator arrowConfigurator, Sprite arrowSprite)
    {
        foreach (var room in rooms)
        {
            Vector3Int position = new Vector3Int(room.Position.x, room.Position.y, 0);
            tilemap.SetTransformMatrix(
                position,
                Matrix4x4.TRS(
                    Vector3.zero,
                    Quaternion.Euler(0f, 0f, arrowConfigurator.DirectionToZRotation[room.ArrowDirection]),
                    Vector3.one));
        }
    }
    public void UpdatePositionTilemapPersonage(List<UpdatePositionPersonageInput> inputs, ref Tilemap tilemap)
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
    public void TestTilemapRooms(List<Room> rooms, ref Tilemap tilemap)
    {
        foreach (var room in rooms)
        {
            tilemap.SetColor(new Vector3Int(room.Position.x, room.Position.y, 0), Color.red);
        }
    }

    private Tile CreateTile(Sprite sprite)
    {
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = sprite;
        return tile;
    }

    private Wall GetWallBySprite(Sprite sprite)
    {
        return Walls.FirstOrDefault(w => w.UIIcon == sprite);
    }
}
