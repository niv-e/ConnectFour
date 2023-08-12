namespace BusinessLogic.Model.Boundaries
{
    public class GameStateBoundary
    {
        public Guid GameStateId { get; set; }
        public int[][]? GameBoard { get; set; }
        public bool IsPlayersTurn { get; set; }

    }
}
