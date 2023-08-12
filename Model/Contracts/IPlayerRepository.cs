using DAL.Entities;

namespace DAL.Contracts
{
    public interface IPlayerRepository
    {
        Task DeletePlayer(int id);
        Task<Player?> GetPlayerById(int id);
        Task InsertPlayer(Player player);
        Task<bool> UpdatePlayer(Player player);
    }
}