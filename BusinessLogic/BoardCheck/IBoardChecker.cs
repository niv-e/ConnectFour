namespace BusinessLogic.BoardCheck
{
    public interface IBoardChecker
    {
        IEnumerable<Tuple<int, int>> GetPawnSequenceIfExists(int[,] gameBoard);
    }
}