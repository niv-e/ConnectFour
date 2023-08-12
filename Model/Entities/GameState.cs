using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class GameState
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid GameStateId { get; set; }
        public string GameBoard { get; set; } = String.Empty;

        public bool IsPlayersTurn { get; set; }

    }
}
