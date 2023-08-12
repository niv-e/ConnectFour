using BusinessLogic.Model.Boundaries;
namespace BusinessLogic.Contracts
{
    public interface IPlayerService
    {
        Task<PlayerBoundary> RegisterPlayer(PlayerBoundary playerBoundary);
        Task<PlayerBoundary?> GetPlayerById(int id);
        Task<bool> DeletePlayerById(int id);
        Task<bool> UpdatePlayer(PlayerBoundary playerBoundary);
    }
}