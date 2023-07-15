using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.GameService;

namespace BusinessLogic.BoardCheckStrategies
{
    public class CheckColumnHandler : AbstractBoardCheckerHandler
    {
        public override IEnumerable<Tuple<int, int>> HandleCheckBoard(int[,] gameBoard)
        {
            for (int col = 0; col < gameBoard.GetLength(1); col++)
            {
                for (int row = 0; row <= gameBoard.GetLength(0) - 4; )
                {
                    IEnumerable<Tuple<int, int>> pawnSequence =
                        GetColSequenceFromPosition(gameBoard, row, col);

                    if (pawnSequence.Count() == 4)
                    {
                        return pawnSequence;
                    }

                    row += pawnSequence.Count();

                }
            }
            Console.WriteLine($"{nameof(CheckColumnHandler)} : end successfuly");

            return base.HandleCheckBoard(gameBoard);
        }

        private IEnumerable<Tuple<int, int>> GetColSequenceFromPosition(int[,] gameBoard, int row, int col)
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
                if (currentValue == gameBoard[row + offset, col ])
                {
                    connectFour.Add(new(row + offset, col ));
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
