using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Boundaries
{
    public class GameStateBoundary
    {
        public Guid GameStateId { get; set; }
        public int[][]? GameBoard { get; set; } 
        public bool IsPlayersTurn { get; set; }

    }
}
