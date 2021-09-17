using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public enum WallType
    {
        staticWalls
    }
    [CreateAssetMenu(menuName = "Asset Wall")]
    public class Wall: ScriptableObject
    {
        public Sprite UIIcon => _icon;
        public WallType Type;
        public bool Up => _up;
        public bool Down => _down;
        public bool Right => _right;
        public bool Left => _left;

        [SerializeField] private Sprite _icon;
        [Header("Walls Direction")]
        [SerializeField] private bool _up;
        [SerializeField] private bool _down;
        [SerializeField] private bool _right;
        [SerializeField] private bool _left;

        public RoomWalls GetWalls(RotateDirection rotateDirection)
        {
            bool[] tmp = new bool[] { Up, Right, Down, Left };
            switch (rotateDirection)
            {
                case RotateDirection.Up:
                    break;
                case RotateDirection.Down:
                    tmp = tmp.Skip(2).Concat(tmp.Take(2)).ToArray();
                    break;
                case RotateDirection.Right:
                    tmp = tmp.Skip(1).Concat(tmp.Take(1)).ToArray();
                    break;
                case RotateDirection.Left:
                    tmp = tmp.Skip(3).Concat(tmp.Take(3)).ToArray();
                    break;
                default:
                    break;
            }
            return new RoomWalls(tmp[0], tmp[2], tmp[1], tmp[3]);
        }
    }
}
