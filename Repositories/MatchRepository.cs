using Microsoft.EntityFrameworkCore;
using GameVault.API.Data;
using GameVault.API.Models.Entities;
using GameVault.API.Repositories.Interfaces;

namespace GameVault.API.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly GameVaultDbContext _context;

        public MatchRepository(GameVaultDbContext context)
        {
            _context = context;
        }

        public async Task<Match?> GetByIdAsync(int id)
            => await _context.Matches
                .Include(m => m.Participants)
                .ThenInclude(p => p.Player)
                .FirstOrDefaultAsync(m => m.Id == id);

        public async Task<IEnumerable<Match>> GetPlayerMatchHistoryAsync(
            int playerId, int count)
            => await _context.Matches
                .Include(m => m.Participants)
                .ThenInclude(p => p.Player)
                .Where(m => m.Participants
                    .Any(p => p.PlayerId == playerId))
                .OrderByDescending(m => m.PlayedAt)
                .Take(count)
                .ToListAsync();

        public async Task AddAsync(Match match)
            => await _context.Matches.AddAsync(match);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}