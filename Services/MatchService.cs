using GameVault.API.DTOs.Match;
using GameVault.API.Models.Entities;
using GameVault.API.Repositories.Interfaces;
using GameVault.API.Services.Interfaces;

namespace GameVault.API.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerRepository _playerRepository;

        public MatchService(
            IMatchRepository matchRepository,
            IPlayerRepository playerRepository)
        {
            _matchRepository = matchRepository;
            _playerRepository = playerRepository;
        }

        public async Task<MatchDto> CreateMatchAsync(CreateMatchDto request)
        {
            // Build the Match entity
            var match = new Match
            {
                GameMode = request.GameMode,
                DurationSeconds = request.DurationSeconds,
                PlayedAt = DateTime.UtcNow,
                Participants = request.Participants.Select(p =>
                    new MatchParticipant
                    {
                        PlayerId = p.PlayerId,
                        Score = p.Score,
                        Result = p.Result
                    }).ToList()
            };

            // Save match first to get its ID
            await _matchRepository.AddAsync(match);
            await _matchRepository.SaveChangesAsync();

            // Update stats for every participant
            foreach (var participantDto in request.Participants)
            {
                var player = await _playerRepository
                    .GetByIdAsync(participantDto.PlayerId);

                if (player == null) continue;

                player.GamesPlayed++;
                player.TotalScore += participantDto.Score;

                if (participantDto.Result == MatchResult.Win)
                    player.GamesWon++;

                await _playerRepository.UpdateAsync(player);
            }

            // Save all player stat updates in one operation
            await _playerRepository.SaveChangesAsync();

            // Reload match with participant and player data for response
            var savedMatch = await _matchRepository.GetByIdAsync(match.Id)
                ?? throw new KeyNotFoundException("Match not found after save.");

            return MapToDto(savedMatch);
        }

        public async Task<IEnumerable<MatchDto>> GetPlayerMatchHistoryAsync(
            int playerId, int count = 20)
        {
            var matches = await _matchRepository
                .GetPlayerMatchHistoryAsync(playerId, count);

            return matches.Select(MapToDto);
        }

        private static MatchDto MapToDto(Match match) => new()
        {
            Id = match.Id,
            GameMode = match.GameMode,
            PlayedAt = match.PlayedAt,
            DurationSeconds = match.DurationSeconds,
            Participants = match.Participants.Select(p =>
                new MatchParticipantResultDto
                {
                    PlayerId = p.PlayerId,
                    Username = p.Player?.Username ?? "Unknown",
                    Score = p.Score,
                    Result = p.Result
                }).ToList()
        };
    }
}