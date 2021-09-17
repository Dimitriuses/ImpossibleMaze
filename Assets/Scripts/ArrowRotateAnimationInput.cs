using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ArrowRotateAnimationInput
{
    public TurnDirection TurnDirection { get; set; }
    public ArrowDirection OldDirection { get; set; }
    public ArrowDirection NewDirection { get; set; }
    public float Status { get; set; }
}
