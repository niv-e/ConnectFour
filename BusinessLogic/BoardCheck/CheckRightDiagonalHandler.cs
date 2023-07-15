using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.GameService;

namespace BusinessLogic.BoardCheckStrategies
{
    public class CheckRightDiagonalHandler : AbstractBoardCheckerHandler
    {
        public override IEnumerable<Tuple<int, int>> HandleCheckBoard(int[,] gameBoard)
        {
            for (int row = gameBoard.GetLength(0) - 1; row - 4 >= 0; row--)
            {
                for (int col = 0; col < gameBoard.GetLength(1) - 4; col++)
                {
                    Console.WriteLine($"{nameof(CheckRightDiagonalHandler)} : ({row},{col})");
                    IEnumerable<Tuple<int, int>> pawnSequence = GetRightDiagonalSequenceFromPosition(gameBoard, row, col);

                    if (pawnSequence.Count() == 4)
                    {
                        return pawnSequence;
                    }
                }
            }

            return base.HandleCheckBoard(gameBoard);
        }

        private IEnumerable<Tuple<int, int>> GetRightDiagonalSequenceFromPosition(int[,] gameBoard, int row, int col)
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
                if (currentValue == gameBoard[row - offset, col + offset])
                {
                    connectFour.Add(new(row - offset, col + offset));
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
