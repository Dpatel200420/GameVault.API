using GameVault.API.DTOs.Player;
using GameVault.API.Repositories.Interfaces;
using GameVault.API.Services.Interfaces;

namespace GameVault.API.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly IPlayerRepository _playerRepository;

        public LeaderboardService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<IEnumerable<LeaderboardEntryDto>> GetTopPlayersAsync(
            int count)
        {
            var players = await _playerRepository.GetLeaderboardAsync(count);

            // LINQ Select with index gives you the rank automatically
            return players.Select((player, index) => new LeaderboardEntryDto
            {
                Rank = index + 1,
                PlayerId = player.Id,
                Username = player.Username,
                TotalScore = player.TotalScore,
                GamesPlayed = player.GamesPlayed,
                GamesWon = player.GamesWon
            });
        }
    }
}