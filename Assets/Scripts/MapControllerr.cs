using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapControllerr : MonoBehaviour
{
    [SerializeField]
    public Tilemap TilemapArrows;
    [SerializeField]
    public Tilemap TilemapPersonages;
    [SerializeField]
    public Tilemap TilemapRooms;

    public List<Room> Rooms { get; set; }
    public List<Personage> Personages { get; set; }
    private ArrowConfigurator ArrowConfigurator;
    // Start is called before the first frame update
    void Start()
    {
        ArrowConfigurator = new ArrowConfigurator();
        Rooms = TileMeneger.GetAllRooms(TilemapArrows);
        Rooms.ForEach(r => r.ArrowDirection = ArrowConfigurator.RandomDirection());
        TileMeneger.TestTilemapRooms(Rooms, ref TilemapRooms);
        TileMeneger.UpdateTilemapRooms(Rooms, ref TilemapArrows, ArrowConfigurator);
        
    }

    public void TurnButtonOnClick(TurnDirection turnDirection)
    {

    }

    private void PersonageMove()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
