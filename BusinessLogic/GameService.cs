
using AutoMapper;
using BusinessLogic.BoardCheck;
using BusinessLogic.Contracts;
using BusinessLogic.Model.Boundaries;
using BusinessLogic.Model.Dtos;
using BusinessLogic.Responses;
using DAL.Contracts;
using DAL.Entities;
using Microsoft.Extensions.Logging;

namespace BusinessLogic
{
    public partial class GameService : IGameService
    {
        private readonly IGameSessionRepository _gameSessionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GameService> _logger;
        private readonly IBoardChecker _boardChecker;


        public GameService(IGameSessionRepository gameSessionRepository, IMapper mapper, ILogger<GameService> logger,
            IBoardChecker boardChecker)
        {
            _mapper = mapper;
            _logger = logger;
            _boardChecker = boardChecker;
            _gameSessionRepository = gameSessionRepository;
        }
        public async Task<PlacePawnResponse> PlacePawn(Guid gameSessionId, int colIndex)
        {
            GameSessionDto gameSession = await GetSessionDto(gameSessionId);

            Tuple<int,int> placePosition = PlacePawnInColumn(
                gameSession.GameState?.GameBoard, colIndex,PawnType.Player);

            gameSession.GameState.IsPlayersTurn = false;

            UpdateSessionStatus(gameSession);

            await ConvertToEntityAndUpdateDatabase(gameSession);

            return new PlacePawnResponse
            {
                Position = placePosition,
                IsGameEndedWithWin = gameSession.GameState.HasWinner,
                LeftMoves = gameSession.GameState.LeftMoves,
                IsPlayerTurn = true
            };
        }


        public async Task<PlacePawnResponse> GetOpponentMove(Guid gameSessionId)
        {
            GameSessionDto gameSession = await GetSessionDto(gameSessionId);

            var randomColumnToInsert = Random
                .Shared
                .Next((int)gameSession.GameState?.GameBoard.GetLength(1));


            Tuple<int, int> placePosition = PlacePawnInColumn(
                gameSession.GameState?.GameBoard, randomColumnToInsert, PawnType.Server);

            gameSession.GameState.IsPlayersTurn = true;

            UpdateSessionStatus(gameSession);

            await ConvertToEntityAndUpdateDatabase(gameSession);

            return new PlacePawnResponse
            {
                Position = placePosition,
                IsGameEndedWithWin = gameSession.GameState.HasWinner,
                LeftMoves = gameSession.GameState.LeftMoves,
                IsPlayerTurn = false

            };

        }

        public async Task<GameSessionDto?> GetSessionById(Guid sessionId)
        {
            GameSession? gameSession = await _gameSessionRepository.GetSessionById(sessionId);

            return _mapper.Map<GameSessionDto>(gameSession);

        }

        public async Task<GameSessionDto> StartGame(PlayerBoundary playerBoundary)
        {
            var player = _mapper.Map<Player>(playerBoundary);

            GameSession session = CreateGameSession(player);

            await _gameSessionRepository.Insert(session);

            return _mapper.Map<GameSessionDto>(session);
        }
        public async Task<IEnumerable<Tuple<int, int>>> TryToGetWinnerSequence(Guid gameSessionId)
        {
            GameSessionDto gameSession = await GetSessionDto(gameSessionId);

            UpdateSessionStatus(gameSession);

            if (gameSession.GameState.HasWinner == false &&
                gameSession.GameState.LeftMoves == 0)
            {
                throw new ArgumentException("Game has no winner");
            }

            if (gameSession.GameState.HasWinner == false &&
                 gameSession.GameState.LeftMoves > 0)
            {
                throw new ArgumentException("Game not ended");
            }

            if(gameSession.GameState.HasWinner)
            {
                return gameSession.GameState.WinnerSequence;
            }

            throw new ArgumentException("Game not ended");

        }

        private void UpdateSessionStatus(GameSessionDto gameSession)
        {
            IEnumerable<Tuple<int, int>> pawnSequence =
                _boardChecker.GetPawnSequenceIfExists(gameSession.GameState.GameBoard);
            gameSession.GameState.LeftMoves = GetNumberOfLeftMoves(gameSession.GameState);

            if (pawnSequence.Count().Equals(4))
            {
                gameSession.GameState.IsGameEnded = true;
                gameSession.GameState.HasWinner = true;
                gameSession.GameState.WinnerSequence = pawnSequence;
                gameSession.GameDuration = DateTime.Now - gameSession.StaringTime;
            }
        }

        private int GetNumberOfLeftMoves(GameStateDto gameState)
        {
            int rowCount = gameState.GameBoard.GetLength(0);
            int colCount = gameState.GameBoard.GetLength(1);

            int leftMoves = rowCount * colCount;

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    leftMoves = gameState.GameBoard[i, j] == 0 ?
                        leftMoves :
                        leftMoves--;
                }
            }

            return leftMoves;

        }

        private async Task<GameSessionDto> GetSessionDto(Guid gameSessionId)
        {

            var gameSessionEntity = await _gameSessionRepository.GetSessionById(gameSessionId);

            GameSessionDto gameSession = _mapper.Map<GameSessionDto>(gameSessionEntity);

            return gameSession;

        }

        private async Task ConvertToEntityAndUpdateDatabase(GameSessionDto gameSessionDto)
        {
            var gameSessionEntity = await _gameSessionRepository.GetSessionById(gameSessionDto.SessionId);

            _mapper.Map(gameSessionDto, gameSessionEntity);

            await _gameSessionRepository.Update(gameSessionEntity);

        }

        private Tuple<int, int> PlacePawnInColumn(int[,]? gameBoard, int colIndex, PawnType pawnType)
        {
            for (var i = gameBoard.GetLength(0) - 1; i >= 0; i--)
            {
                if (gameBoard[i, colIndex] == 0)
                {
                    gameBoard[i, colIndex] = (int)pawnType;
                    return new(i, colIndex);
                }
            }

            throw new Exception("Invalid Pawn Position");
        }

        private GameSession CreateGameSession(Player player)
        {
            return new GameSession
            {
                Player = player,
                GameState = new()
                {
                    IsPlayersTurn = Random.Shared.Next(1) == 1 ? true : false,
                },
                StaringTime = DateTime.Now
            };
        }

        private async Task<GameSessionDto> InvokeMethodAndUpdateWrapper(Guid gameSessionId, Func<GameSessionDto, Task> method)
        {
            var gameSessionEntity = await _gameSessionRepository.GetSessionById(gameSessionId);
            var gameSession = _mapper.Map<GameSessionDto>(gameSessionEntity);

            await method(gameSession);

            _mapper.Map(gameSession, gameSessionEntity);
            await _gameSessionRepository.Update(gameSessionEntity);

            return gameSession;
        }

        public Task<bool> DeleteGameByGuid(Guid guid)
        {
            return _gameSessionRepository.DeleteGameById(guid);
        }
    }
}   