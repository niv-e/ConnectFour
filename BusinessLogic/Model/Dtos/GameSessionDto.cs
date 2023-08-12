namespace BusinessLogic.Model.Dtos
{
    public class GameSessionDto
    {
        public Guid SessionId { get; set; }
        public GameStateDto? GameState { get; set; }
        public PlayerDto? Player { get; set; }
        public DateTime StaringTime { get; set; }
        public TimeSpan GameDuration { get; set; }

    }
}
