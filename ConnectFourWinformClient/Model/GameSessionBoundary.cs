using Model.bounderies;

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
