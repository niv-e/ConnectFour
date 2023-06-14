using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class GameSession
    {
        public Guid SessionId { get; set; }
        public GameState? GameState { get; set; }
        public Player? Player { get; set; }

        public DateTime StaringTime { get; set; }
        public TimeSpan GameDuration { get; set; }
    }
}
