using Escape.Mines.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Escape.Mines.Domain.Model
{
    public class GameResult
    {
        public List<MoveOperation> MoveOperations { get; set; }
        public Status Status { get; set; }
    }
}
