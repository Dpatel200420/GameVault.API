using Microsoft.EntityFrameworkCore;
using GameVault.API.Data;
using GameVault.API.Models.Entities;
using GameVault.API.Repositories.Interfaces;

namespace GameVault.API.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly GameVaultDbContext _context;

        public PlayerRepository(GameVaultDbContext context)
        {
            _context = context;
        }

        public async Task<Player?> GetByIdAsync(int id)
            => await _context.Players.FindAsync(id);

        public async Task<Player?> GetByUsernameAsync(string username)
            => await _context.Players
                .FirstOrDefaultAsync(p => p.Username == username);

        public async Task<Player?> GetByEmailAsync(string email)
            => await _context.Players
                .FirstOrDefaultAsync(p => p.Email == email);

        public async Task<IEnumerable<Player>> GetLeaderboardAsync(int count)
            => await _context.Players
                .OrderByDescending(p => p.TotalScore)
                .Take(count)
                .ToListAsync();

        public async Task AddAsync(Player player)
            => await _context.Players.AddAsync(player);

        public async Task UpdateAsync(Player player)
        {
            _context.Players.Update(player);
            player.UpdatedAt = DateTime.UtcNow;
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}