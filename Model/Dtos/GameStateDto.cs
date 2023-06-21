using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Model.Dtos
{
    public class GameStateDto
    {
        public Guid GameStateId { get; set; }
        public int[,] GameBoard { get; set; } = new int[6,7];
        public bool IsPlayersTurn { get; set; }
    }
}
