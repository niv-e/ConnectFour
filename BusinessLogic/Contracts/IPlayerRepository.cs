using Model.Entities;

namespace BusinessLogic.Contracts
{
    public interface IPlayerRepository
    {
        Task<Player?> GetPlayerById(int id);
        Task InsertPlayer(Player player);
    }
}