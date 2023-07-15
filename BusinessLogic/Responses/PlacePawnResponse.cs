using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Responses
{
    public record PlacePawnResponse
    {
        public Tuple<int, int> Position { get; set; } = null!;
        public bool  IsGameEndedWithWin { get; set; }
        public int LeftMoves { get; set; }
        public bool IsPlayerTurn { get; set; }


    }
}
