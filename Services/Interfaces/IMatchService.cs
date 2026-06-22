using GameVault.API.DTOs.Match;

namespace GameVault.API.Services.Interfaces
{
    public interface IMatchService
    {
        Task<MatchDto> CreateMatchAsync(CreateMatchDto request);
        Task<IEnumerable<MatchDto>> GetPlayerMatchHistoryAsync(
            int playerId, int count = 20);
    }
}