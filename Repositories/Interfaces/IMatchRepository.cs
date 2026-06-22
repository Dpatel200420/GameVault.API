using GameVault.API.Models.Entities;

namespace GameVault.API.Repositories.Interfaces
{
    public interface IMatchRepository
    {
        Task<Match?> GetByIdAsync(int id);
        Task<IEnumerable<Match>> GetPlayerMatchHistoryAsync(
            int playerId, int count);
        Task AddAsync(Match match);
        Task SaveChangesAsync();
    }
}