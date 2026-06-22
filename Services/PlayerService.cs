using GameVault.API.DTOs.Player;
using GameVault.API.Models.Entities;
using GameVault.API.Repositories.Interfaces;
using GameVault.API.Services.Interfaces;

namespace GameVault.API.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<PlayerDto> GetPlayerByIdAsync(int id)
        {
            var player = await _playerRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException(
                    $"Player with ID {id} was not found.");

            return MapToDto(player);
        }

        public async Task<PlayerDto> GetMyProfileAsync(int playerId)
        {
            var player = await _playerRepository.GetByIdAsync(playerId)
                ?? throw new KeyNotFoundException("Player not found.");

            return MapToDto(player);
        }

        public async Task<PlayerDto> UpdatePlayerAsync(
            int playerId, UpdatePlayerDto request)
        {
            var player = await _playerRepository.GetByIdAsync(playerId)
                ?? throw new KeyNotFoundException("Player not found.");

            // Only update fields that were actually provided
            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                player.Email = request.Email;
            }

            await _playerRepository.UpdateAsync(player);
            await _playerRepository.SaveChangesAsync();

            return MapToDto(player);
        }

        // Private helper — converts entity to DTO
        // Never expose the entity directly (contains PasswordHash)
        private static PlayerDto MapToDto(Player player) => new()
        {
            Id = player.Id,
            Username = player.Username,
            Email = player.Email,
            Level = player.Level,
            TotalScore = player.TotalScore,
            GamesPlayed = player.GamesPlayed,
            GamesWon = player.GamesWon,
            CreatedAt = player.CreatedAt
        };
    }
}