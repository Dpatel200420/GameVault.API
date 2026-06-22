using GameVault.API.Models.Entities;

namespace GameVault.API.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        Task<Player?> GetByIdAsync(int id);
        Task<Player?> GetByUsernameAsync(string username);
        Task<Player?> GetByEmailAsync(string email);
        Task<IEnumerable<Player>> GetLeaderboardAsync(int count);
        Task AddAsync(Player player);
        Task UpdateAsync(Player player);
        Task SaveChangesAsync();
    }
}