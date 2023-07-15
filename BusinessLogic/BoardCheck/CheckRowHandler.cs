using static BusinessLogic.GameService;

namespace BusinessLogic.BoardCheckStrategies
{

    public class CheckRowHandler : AbstractBoardCheckerHandler
    {
        public override IEnumerable<Tuple<int, int>> HandleCheckBoard(int[,] gameBoard)
        {

            for (int row = 0; row < gameBoard.GetLength(0); row++)
            {
                for (int col = 0; col < gameBoard.GetLength(1) - 4;)
                {
                    IEnumerable<Tuple<int, int>> pawnSequence = GetRowSequenceFromPosition(gameBoard, row, col);

                    if (pawnSequence.Count() == 4)
                    {
                        return pawnSequence;
                    }

                    col += pawnSequence.Count();

                }
            }
            Console.WriteLine($"{nameof(CheckRowHandler)} : end successfuly");

            return base.HandleCheckBoard(gameBoard);
        }

        private IEnumerable<Tuple<int, int>> GetRowSequenceFromPosition(int[,] gameBoard, int row, int col)
        {
            List<Tuple<int, int>> connectFour = new();

            int currentValue = gameBoard[row, col];

            connectFour.Add(new(row, col));

            if (currentValue == ((int)PawnType.free))
            {
                return connectFour;
            }

            for (int offset = 1; offset < 4; offset++)
            {
                if (currentValue == gameBoard[row, col + offset])
                {
                    connectFour.Add(new(row, col + offset));
                }
                else
                {
                    return connectFour;
                }
            }
            return connectFour;

        }
    }
}
