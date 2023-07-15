using Model.bounderies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Boundaries
{
    public class GameSessionBoundary
    {
        public Guid SessionId { get; set; }
        public GameStateBoundary? GameState { get; set; }
        public PlayerBoundary? Player { get; set; }
        public DateTime StaringTime { get; set; }
        public TimeSpan GameDuration { get; set; }

    }
}
