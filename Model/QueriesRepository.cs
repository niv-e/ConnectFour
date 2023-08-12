using DAL.Contracts;
using DAL.Dtos;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class QueriesRepository : IQueriesRepository
    {
        private readonly ConnectFourDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public QueriesRepository(ConnectFourDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public IEnumerable<Player> GetAllPlayers(bool orderByName)
        {
            if(orderByName is false)
            {
                return _context.Players;
            };

            var players = _context.Players.ToList();
            var orderedPlayers =  players
                .OrderBy(p => p.FirstName, StringComparer.OrdinalIgnoreCase)
                .ToList();

            return orderedPlayers;
        }


        public IEnumerable<PlayerLastGameDto> GetAllPlayersLastGameDateDescending()
        {
            var queryResult =  _context.Players
                .Select(player => new PlayerLastGameDto
                {
                    PlayerNameAndId = $"{player.PlayerId}-{player.FirstName}",
                    LastGame = _context.GameSessions
                        .Where(session => session.Player.PlayerId == player.PlayerId)
                        .OrderByDescending(session => session.StaringTime)
                        .Select(session => session.StaringTime)
                        .FirstOrDefault()
                });

            return queryResult;
        }

        public IEnumerable<GameDetailsDto> GetAllGamesDetails()
        {
            var queryResult = _context.GameSessions
                .Select(session => new GameDetailsDto
                {
                    PlayerId = session.Player.PlayerId,
                    GameBoard = session.GameState.GameBoard,
                    StaringTime = session.StaringTime,
                    GameDuration = session.GameDuration
                })
                .AsEnumerable();

            return queryResult;
        }

        public IEnumerable<GameDetailsDto> GetAllGamesDetailsDistinct()
        {

            var queryResult = _context.GameSessions
                .Include(i => i.Player)
                .Include(i => i.GameState)
                .Select(session => new GameDetailsDto
                 {
                     PlayerId = session.Player.PlayerId,
                     GameBoard = session.GameState.GameBoard,
                     StaringTime = session.StaringTime,
                     GameDuration = session.GameDuration
                 })
                .AsEnumerable();

            return queryResult.DistinctBy(x => x.PlayerId);
        }

        public IEnumerable<GameDetailsDto> GetAllPlayerGames(int playerId)
        {
            var queryResult = _context.GameSessions
                .Where(session => session.Player.PlayerId == playerId)
                .Select(session => new GameDetailsDto
                {
                    PlayerId = session.Player.PlayerId,
                    GameBoard = session.GameState.GameBoard,
                    StaringTime = session.StaringTime,
                    GameDuration = session.GameDuration
                })
                .AsEnumerable();
            return queryResult;
        }

        public IEnumerable<PlayerGamesCountDto> GetAllPlayersNumberOfGames()
        {
            var groupingResult = _context.GameSessions
            .GroupBy(session => session.Player.PlayerId);

            var queryResult = groupingResult
            .Select(group => new PlayerGamesCountDto
             {
                 PlayerNameAndId = $"{group.Key.ToString()} {group.First().Player.FirstName}",
                 GamesCount = group.Count()
             });

            return queryResult;
        }

        public IEnumerable<PlayersPerGameCountDto> GetPlayersPerGameCount()
        {
            IEnumerable < PlayerGamesCountDto > playerGamesCount = GetAllPlayersNumberOfGames();

            var queryResult = playerGamesCount.GroupBy(pair => pair.GamesCount)
                .Select(group => new PlayersPerGameCountDto
                {
                    GamesCount = group.Key,
                    PlayerNameAndId = group.ToList()
                        .Select(x => x.PlayerNameAndId)
                });

            return queryResult;
        }

        public IEnumerable<PlayersPerCountryDto> GetPlayersPerCountry()
        {
            var queryResult = _context.Players
                .GroupBy(p => p.Country)
                .Select(group => new PlayersPerCountryDto
                { 
                    Country = group.Key,
                    PlayerNameAndId = group.ToList()
                        .Select(x => $"{x.PlayerId}-{x.FirstName}")
                });


            return queryResult;

        }


    }
}
