using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapControllerr : MonoBehaviour
{
    [SerializeField] private Tilemap TilemapArrows;
    [SerializeField] private Tilemap TilemapPersonages;
    [SerializeField] private Tilemap TilemapRooms;
    [SerializeField] private Tilemap TilemapWalls;
    [SerializeField] private Sprite ArrowSprite;

    [Header("WallAssets")]
    [SerializeField] private List<Wall> Walls;

    [Header("Animation settings")]
    [SerializeField] private float ArrowAnimatimationDuration;
    [SerializeField] private float PersonageAnimationDuration;
    [SerializeField] private AnimationCurve ArrowAnimation;
    [SerializeField] private AnimationCurve PersonageAnimation;


    public List<Room> Rooms { get; set; }
    public List<Personage> Personages { get; set; }
    private ArrowConfigurator ArrowConfigurator;
    private RotateMeneger RotateMeneger;
    private TileMeneger TileMeneger;
    // Start is called before the first frame update
    void Start()
    {
        ArrowConfigurator = new ArrowConfigurator();
        RotateMeneger = new RotateMeneger();
        TileMeneger = new TileMeneger(Walls, ArrowConfigurator, RotateMeneger);
        Rooms = TileMeneger.GetRooms(TilemapRooms, TilemapArrows, TilemapWalls, ArrowSprite);
        //Rooms.ForEach(r => r.ArrowDirection = ArrowConfigurator.RandomDirection());
        Personages = TileMeneger.GetPersonages(TilemapPersonages);
        //TileMeneger.TestTilemapRooms(Rooms, ref TilemapRooms);
        TileMeneger.UpdateTilemapRooms(Rooms, ref TilemapArrows);
        
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
        //Rooms.ForEach(r => r.ArrowDirection = moveDirection[r.ArrowDirection]);
        //TileMeneger.UpdateTilemapRooms(Rooms, ref TilemapArrows);
        //PersonageMove();
        StartCoroutine(ArrowAnimate((TurnDirection)turnDirection));
        //StartCoroutine(PersonageMove());
    }

    private void PersonageMove()
    {
        List<UpdatePositionPersonageInput> inputs = new List<UpdatePositionPersonageInput>();
        foreach (var personage in Personages)
        {
            Room room = Rooms.Find(r => r.Position == personage.Position);
            if(room != null)
            {
                Vector2Int newPosition = ArrowConfigurator.MoveToDirection(personage.Position, room.ArrowDirection);
                Room next = Rooms.Find(r => r.Position == newPosition);
                if(IsPosibleToPass(room, next))
                {
                    inputs.Add(new UpdatePositionPersonageInput
                    {
                        OldPosition = new Vector3Int(personage.Position.x, personage.Position.y, 0),
                        NewPosition = new Vector3Int(newPosition.x, newPosition.y, 0)
                    });
                    personage.Position = newPosition;
                }
                else
                {
                    inputs.Add(new UpdatePositionPersonageInput
                    {
                        OldPosition = new Vector3Int(personage.Position.x, personage.Position.y, 0),
                        NewPosition = new Vector3Int(personage.Position.x, personage.Position.y, 0)
                    });
                }
                
            }
            else
            {

            }
        }
        //yield return new WaitForSeconds(0.3f);
        //inputs = StopDublicate(inputs);
        //TileMeneger.UpdatePositionTilemapPersonage(inputs, ref TilemapPersonages);
        StartCoroutine(PersonageAnimate(inputs));
    }
    private IEnumerator ArrowAnimate(TurnDirection turnDirection)
    {
        float status = 0;
        while (status < 100f)
        {
            Rooms.ForEach(r =>
            {
                ArrowDirection curentDirection = r.ArrowDirection;
                ArrowDirection nextDirection = ArrowConfigurator.TurnDirection(curentDirection, turnDirection);
                float curentZRotate = RotateMeneger.DirectionToZRotation[ArrowConfigurator.ArrowToRotate[curentDirection]];
                float nextZRotate = curentZRotate;
                switch (turnDirection)
                {
                    case TurnDirection.Right:
                        nextZRotate -= 90f;
                        break;
                    case TurnDirection.Left:
                        nextZRotate += 90f;
                        break;
                    default:
                        break;
                }
                float diffZRotate = nextZRotate - curentZRotate;
                TileMeneger.RotateTile(
                    ref TilemapArrows,
                    new Vector3Int(r.Position.x, r.Position.y, 0),
                    curentZRotate + diffZRotate * ArrowAnimation.Evaluate(status/100f));
            });
            yield return new WaitForSeconds(0.01f);
            status += 1f/ArrowAnimatimationDuration;
        }
        Rooms.ForEach(r => r.ArrowDirection = ArrowConfigurator.TurnDirection(r.ArrowDirection, turnDirection));
        TileMeneger.UpdateTilemapRooms(Rooms, ref TilemapArrows);
        PersonageMove();
    }

    private IEnumerator PersonageAnimate(List<UpdatePositionPersonageInput> inputs)
    {
        float status = 0;
        while (status < 100f)
        {
            inputs.ForEach(i =>
            {
                Vector3 diff = i.NewPosition - i.OldPosition;
                TileMeneger.MoveTile(ref TilemapPersonages, i.OldPosition, diff * PersonageAnimation.Evaluate(status/100f));
            });
            yield return new WaitForSeconds(0.01f);
            status += 1f/PersonageAnimationDuration;
        }
        TileMeneger.UpdatePositionTilemapPersonage(inputs, ref TilemapPersonages);
    }

    private bool IsPosibleToPass(Room curentRoom, Room nextRoom)
    {
        ArrowDirection curentArrowDirection = curentRoom.ArrowDirection;
        ArrowDirection opositeArrowDirection = ArrowConfigurator.Opposite[curentArrowDirection];
        bool isNextExist = nextRoom != null;
        bool isCurentPass = curentRoom.Walls?.IsPosibleToPass(curentArrowDirection) ?? true;
        
        bool isNextPass = nextRoom?.Walls?.IsPosibleToPass(opositeArrowDirection) ?? true;
        //Debug.Log(isNextExist + " " + isCurentPass + " " + isNextPass);
        //Debug.Log(curentRoom.Walls?.FullStatus);
        //Debug.Log(curentArrowDirection + " " + opositeArrowDirection);
        return isCurentPass && isNextPass && isNextExist;
    }

    private List<UpdatePositionPersonageInput> StopDublicate(List<UpdatePositionPersonageInput> inputs)
    {
        var output = new List<UpdatePositionPersonageInput>();
        Debug.Log("input"+ output);
        output.AddRange(inputs);
        bool isEnd = false;
        while (!isEnd)
        {
            isEnd = true;
            output.ForEach(input =>
            {
                if(output.Any(i=> i.NewPosition == input.NewPosition))
                {
                    List<UpdatePositionPersonageInput> tmp = output.Where(i => i.NewPosition == input.NewPosition).ToList();
                    tmp.ForEach(i => i.Stop());
                    isEnd = false;
                }
            });
            
        }
        Debug.Log("output" + output);
        return output;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
