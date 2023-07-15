using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BoardCheckStrategies
{
    /// <summary>
    /// handle all the board checking strategies using 
    /// the chain of responsibility design pattern
    /// </summary>
    public interface IBoardCheckerHandler
    {
        IEnumerable<Tuple<int, int>> HandleCheckBoard(int[,] gameBoard);
        IBoardCheckerHandler SetNext(IBoardCheckerHandler handler);

    }

    public abstract class AbstractBoardCheckerHandler : IBoardCheckerHandler
    {
        private IBoardCheckerHandler? nextHandler;

        public virtual IEnumerable<Tuple<int, int>> HandleCheckBoard(int[,] gameBoard)
        {
            if (nextHandler != null)
            {
                return nextHandler.HandleCheckBoard(gameBoard);
            }
            else
            {
                return new List<Tuple<int, int>>();
            }

        }

        public IBoardCheckerHandler SetNext(IBoardCheckerHandler handler)
        {
            nextHandler = handler;

            return handler;
        }
    }
}
