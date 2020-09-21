using Escape.Mines.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Escape.Mines.Domain.Model
{
    public class Turtle
    {
        public CardinalDirection Direction { get; set; }
        public Position Position { get; set; }
        public List<List<MoveOperation>> MoveOperations { get; set; } = new List<List<MoveOperation>>();
    }
}
