using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class RoomWalls
    {
        public bool Up { get; private set; }
        public bool Down { get; private set; }
        public bool Right { get; private set; }
        public bool Left { get; private set; }

        public RoomWalls()
        {
            Up = false;
            Down = false;
            Right = false;
            Left = false;
        }

        public RoomWalls(Wall wall)
        {
            Up = wall.Up;
            Down = wall.Down;
            Right = wall.Right;
            Left = wall.Left;
        }

        public RoomWalls (bool up, bool down, bool right, bool left)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
        }

        public bool IsPosibleToPass(ArrowDirection direction)
        {
            switch (direction)
            {
                case ArrowDirection.Up:
                    return !Up;
                case ArrowDirection.Down:
                    return !Down;
                case ArrowDirection.Right:
                    return !Right;
                case ArrowDirection.Left:
                    return !Left;
                default:
                    return false;
            }
        }
    }
}
