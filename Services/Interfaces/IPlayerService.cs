using GameVault.API.DTOs.Player;

namespace GameVault.API.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<PlayerDto> GetPlayerByIdAsync(int id);
        Task<PlayerDto> GetMyProfileAsync(int playerId);
        Task<PlayerDto> UpdatePlayerAsync(int playerId, UpdatePlayerDto request);
    }
}