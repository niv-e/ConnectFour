using AutoMapper;
using Model.Boundaries;
using Model.Dtos;
using Model.Entities;
using System.Text.Json;

namespace Model.Mappers
{
    internal class GameStateMapper : Profile
    {
        public GameStateMapper()
        {
            CreateMap<GameState, GameStateDto>()
                .ConvertUsing<GameStateToGameStateDtoConverter>();

            CreateMap<GameStateDto, GameState>()
                .ConvertUsing<GameStateDtoToGameStateConverter>();

            CreateMap<GameStateDto, GameStateBoundary>()
    .           ConvertUsing<GameStateDtoToGameStateBoundaryConverter>();

        }
    }

    internal class GameStateDtoToGameStateBoundaryConverter : ITypeConverter<GameStateDto, GameStateBoundary>
    {
        public GameStateBoundary Convert(GameStateDto source, GameStateBoundary destination, ResolutionContext context)
        {
            GameStateBoundary boundary = new()
            {
                GameStateId = source.GameStateId,
                IsPlayersTurn = source.IsPlayersTurn,
            };

            boundary.GameBoard = new int[source.GameBoard.GetLength(0)][];

            for (int row = 0; row < source.GameBoard.GetLength(0); row++)
            {
                boundary.GameBoard[row] = new int[source.GameBoard.GetLength(1)];

                for (int col = 0; col < source.GameBoard.GetLength(1); col++)
                {
                    boundary.GameBoard[row][col] = source.GameBoard[row, col];
                }
            }
            //Enumerable.Range(0, source.GameBoard.GetLength(0))
            //                .ToList()
            //                .ForEach(row =>
            //                    Enumerable.Range(0, source.GameBoard.GetLength(1))
            //                        .ToList()
            //                        .ForEach(col =>
            //                            boundary.GameBoard[row][col] = source.GameBoard[row, col]
            //                        )
            //                );

            return boundary;
        }
    }

    public class GameStateToGameStateDtoConverter : ITypeConverter<GameState, GameStateDto>
    {
        public GameStateDto Convert(GameState source, GameStateDto destination, ResolutionContext context)
        {
            var gameStateDto = new GameStateDto
            {
                GameStateId = source.GameStateId,
                IsPlayersTurn = source.IsPlayersTurn
            };

            if (String.IsNullOrEmpty(source.GameBoard))
            {
                gameStateDto.GameBoard = new int[6, 7];
                return gameStateDto;
            }

            int rowCount = gameStateDto.GameBoard.GetLength(0);
            int colCount = gameStateDto.GameBoard.GetLength(1);

            var flatten = JsonSerializer.Deserialize<int[]>(source.GameBoard);

            Enumerable.Range(0, rowCount)
            .ToList()
            .ForEach(row =>
                Enumerable.Range(0, colCount)
                    .ToList()
                    .ForEach(col =>
                        gameStateDto.GameBoard[row, col] = flatten.ElementAt(row + col)
                    )
            );


            return gameStateDto;
        }
    }

    public class GameStateDtoToGameStateConverter : ITypeConverter<GameStateDto, GameState>
    {
        public GameState Convert(GameStateDto source, GameState destination, ResolutionContext context)
        {
            var gameState = new GameState
            {
                GameStateId = source.GameStateId,
                IsPlayersTurn = source.IsPlayersTurn
            };

            if (source.GameBoard != null)
            {
                var flattenBoard = Enumerable.Range(0, source.GameBoard.GetLength(0))
                .Select(row => Enumerable.Range(0, source.GameBoard.GetLength(1))
                    .Select(col => source.GameBoard[row, col])
                    .ToArray())
                .ToArray()
                .SelectMany(x => x)
                .ToArray();

                destination.GameBoard = JsonSerializer.Serialize(flattenBoard);
            }

            return gameState;
        }
    }
}
