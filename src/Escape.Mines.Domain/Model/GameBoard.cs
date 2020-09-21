using System;
using System.Collections.Generic;
using System.Text;

namespace Escape.Mines.Domain.Model
{
    public class GameBoard
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Position> Mines { get; set; } = new List<Position>();
        public Position ExitPoint { get; set; }


    }
}
