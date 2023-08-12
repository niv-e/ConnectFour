using DAL.Dtos;
using DAL.Entities;

namespace DAL.Contracts
{
    public interface IQueriesRepository
    {
        IEnumerable<GameDetailsDto> GetAllPlayerGames(int playerId);
        IEnumerable<GameDetailsDto> GetAllGamesDetails();
        IEnumerable<GameDetailsDto> GetAllGamesDetailsDistinct();
        IEnumerable<Player> GetAllPlayers(bool orderByName);
        IEnumerable<PlayerLastGameDto> GetAllPlayersLastGameDateDescending();
        IEnumerable<PlayerGamesCountDto> GetAllPlayersNumberOfGames();
        IEnumerable<PlayersPerGameCountDto> GetPlayersPerGameCount();
        IEnumerable<PlayersPerCountryDto> GetPlayersPerCountry();
    }
}