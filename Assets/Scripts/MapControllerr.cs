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
    [SerializeField]
    public Sprite ArrowSprite;

    public List<Room> Rooms { get; set; }
    public List<Personage> Personages { get; set; }
    private ArrowConfigurator ArrowConfigurator;
    private TileMeneger TileMeneger;
    // Start is called before the first frame update
    void Start()
    {
        ArrowConfigurator = new ArrowConfigurator();
        TileMeneger = new TileMeneger();
        Rooms = TileMeneger.GetRooms(TilemapRooms, TilemapArrows, ArrowConfigurator, ArrowSprite);
        //Rooms.ForEach(r => r.ArrowDirection = ArrowConfigurator.RandomDirection());
        Personages = TileMeneger.GetPersonages(TilemapPersonages);
        //TileMeneger.TestTilemapRooms(Rooms, ref TilemapRooms);
        TileMeneger.UpdateTilemapRooms(Rooms, ref TilemapArrows, ArrowConfigurator, ArrowSprite);
        
    }
    public void TurnButtonOnClick(int turnDirection)
    {
        Dictionary<ArrowDirection, ArrowDirection> moveDirection = new Dictionary<ArrowDirection, ArrowDirection>();
        switch ((TurnDirection)turnDirection)
        {
            case TurnDirection.Right:
                moveDirection = ArrowConfigurator.TurnRight;
                break;
            case TurnDirection.Left:
                moveDirection = ArrowConfigurator.TurnLeft;
                break;
        }
        Rooms.ForEach(r => r.ArrowDirection = moveDirection[r.ArrowDirection]);
        TileMeneger.UpdateTilemapRooms(Rooms, ref TilemapArrows, ArrowConfigurator, ArrowSprite);
        //PersonageMove();
        StartCoroutine(PersonageMove());
    }

    private IEnumerator PersonageMove()
    {
        List<UpdatePositionPersonageInput> inputs = new List<UpdatePositionPersonageInput>();
        foreach (var personage in Personages)
        {
            Room room = Rooms.Find(r => r.Position == personage.Position);
            if(room != null)
            {
                Vector2Int newPosition = ArrowConfigurator.MoveToDirection(personage.Position, room.ArrowDirection);
                inputs.Add(new UpdatePositionPersonageInput
                {
                    OldPosition = new Vector3Int(personage.Position.x, personage.Position.y, 0),
                    NewPosition = new Vector3Int(newPosition.x, newPosition.y, 0)
                });
                personage.Position = newPosition;
            }
        }
        yield return new WaitForSeconds(0.3f);
        TileMeneger.UpdatePositionTilemapPersonage(inputs, ref TilemapPersonages);
    }
    private IEnumerator ArrowAnimate()
    {
        float status = 0;
        while (status >= 100f)
        {
            yield return new WaitForSeconds(0.3f);
            status += 0.1f;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
