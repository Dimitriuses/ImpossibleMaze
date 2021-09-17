using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public enum RotateDirection
    {
        Up,
        Down,
        Right,
        Left
    }
    public class RotateMeneger
    {
        public Dictionary<RotateDirection, RotateDirection> Opposite { get; }
        public Dictionary<RotateDirection, float> DirectionToZRotation { get; }
        public Dictionary<float, RotateDirection> ZRotationToDirection { get; }

        public RotateMeneger()
        {
            Opposite = new Dictionary<RotateDirection, RotateDirection>()
            {
                {RotateDirection.Up, RotateDirection.Down },
                {RotateDirection.Down, RotateDirection.Up },
                {RotateDirection.Right, RotateDirection.Left },
                {RotateDirection.Left, RotateDirection.Right }
            };
            DirectionToZRotation = new Dictionary<RotateDirection, float>()
            {
                {RotateDirection.Up, 0f },
                {RotateDirection.Left, 90f },
                {RotateDirection.Down, 180f },
                {RotateDirection.Right, 270f }
            };
            ZRotationToDirection = new Dictionary<float, RotateDirection>()
            {
                {0f, RotateDirection.Up},
                {90f, RotateDirection.Left},
                {180f, RotateDirection.Down},
                {270f, RotateDirection.Right}
            };
        }
    }
}
