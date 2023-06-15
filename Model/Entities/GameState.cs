using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class GameState
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        Guid GameStateId { get; set; }
        public int[,]? GameBoard { get; set; }

        public bool IsPlayersTurn { get; set; }
    }
}
