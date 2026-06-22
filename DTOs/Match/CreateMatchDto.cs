using System.ComponentModel.DataAnnotations;
using GameVault.API.Models.Entities;

namespace GameVault.API.DTOs.Match
{
    public class CreateMatchDto
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string GameMode { get; set; } = string.Empty;

        [Range(1, 86400)]
        public int DurationSeconds { get; set; }

        [Required]
        [MinLength(1)]
        public List<MatchParticipantInputDto> Participants { get; set; } = new();
    }

    public class MatchParticipantInputDto
    {
        [Required]
        public int PlayerId { get; set; }

        [Range(0, int.MaxValue)]
        public int Score { get; set; }

        [Required]
        public MatchResult Result { get; set; }
    }
}