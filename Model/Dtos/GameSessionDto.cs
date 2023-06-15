using Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dtos
{
    public class GameSessionDto
    {
        public GameState? GameState { get; set; }
        public Player? Player { get; set; }
    }
}
