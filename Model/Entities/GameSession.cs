using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class GameSession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SessionId { get; set; }
        
        [ForeignKey("GameStateId")]
        public virtual GameState? GameState { get; set; }
        
        [ForeignKey("PlayerId")]
        public Player? Player { get; set; }

        public DateTime StaringTime { get; set; }
        public TimeSpan GameDuration { get; set; }

    }
}
