using GameVault.API.DTOs.Player;

namespace GameVault.API.Services.Interfaces
{
    public interface ILeaderboardService
    {
        Task<IEnumerable<LeaderboardEntryDto>> GetTopPlayersAsync(int count);
    }
}