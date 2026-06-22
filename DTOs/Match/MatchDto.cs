using GameVault.API.Models.Entities;

namespace GameVault.API.DTOs.Match
{
    public class MatchDto
    {
        public int Id { get; set; }
        public string GameMode { get; set; } = string.Empty;
        public DateTime PlayedAt { get; set; }
        public int DurationSeconds { get; set; }
        public List<MatchParticipantResultDto> Participants { get; set; } = new();
    }

    public class MatchParticipantResultDto
    {
        public int PlayerId { get; set; }
        public string Username { get; set; } = string.Empty;
        public int Score { get; set; }
        public MatchResult Result { get; set; }
    }
}