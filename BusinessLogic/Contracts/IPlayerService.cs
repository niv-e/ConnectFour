using Model.bounderies;
using Model.Dtos;
using Model.Entities;

namespace BusinessLogic.Contracts
{
    public interface IPlayerService
    {
        Task<Player> RegisterPlayer(PlayerBoundary playerBoundary);
        Task<Player?> GetPlayerById(int id);
    }
}