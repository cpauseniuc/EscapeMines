using System;
using System.Collections.Generic;
using System.Text;

namespace Escape.Mines.Common.Enums
{
    public enum Status
    {
        Success,
        MineHit,
        StillInDanger
    }
    public enum MoveOperation
    {
        M,
        R,
        L
    }
    public enum CardinalDirection
    {
        N,
        E,
        S,
        W
    }
}
