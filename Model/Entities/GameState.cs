using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class GameState
    {
        public int[,]? GameBoard { get; set; }

        public bool IsPlayersTurn { get; set; }
    }
}
