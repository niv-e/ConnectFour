using BusinessLogic.BoardCheckStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BoardCheck
{
    public class BoardChecker : IBoardChecker
    {
        AbstractBoardCheckerHandler rowCheckerHandler = new CheckRowHandler();
        AbstractBoardCheckerHandler colCheckerHandler = new CheckColumnHandler();
        AbstractBoardCheckerHandler rightDiagonalCheckerHandler = new CheckRightDiagonalHandler();
        AbstractBoardCheckerHandler leftDiagonalCheckerHandler = new CheckLeftDiagonalHandler();

        public BoardChecker()
        {
            rowCheckerHandler
                .SetNext(colCheckerHandler)
                .SetNext(leftDiagonalCheckerHandler)
                .SetNext(rightDiagonalCheckerHandler);
        }

        public IEnumerable<Tuple<int, int>> GetPawnSequenceIfExists(int[,] gameBoard)
            => rowCheckerHandler.HandleCheckBoard(gameBoard);

    }
}
