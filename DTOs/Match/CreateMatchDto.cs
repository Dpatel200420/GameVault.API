using System.ComponentModel.DataAnnotations;
using GameVault.API.Models.Entities;

namespace GameVault.API.DTOs.Match
{
    public class CreateMatchDto
    {
        [Required(ErrorMessage = "GameMode is required.")]
        [StringLength(50, MinimumLength = 1,
            ErrorMessage = "GameMode must be between 1 and 50 characters.")]
        public string GameMode { get; set; } = string.Empty;

        [Range(1, 86400,
            ErrorMessage = "Duration must be between 1 and 86400 seconds.")]
        public int DurationSeconds { get; set; }

        [Required(ErrorMessage = "Participants list is required.")]
        [MinLength(1, ErrorMessage = "At least one participant is required.")]
        [MaxLength(10, ErrorMessage = "Maximum 10 participants per match.")]
        public List<MatchParticipantInputDto> Participants { get; set; } = new();
    }

    public class MatchParticipantInputDto
    {
        [Required]
        [Range(1, int.MaxValue,
            ErrorMessage = "PlayerId must be a positive number.")]
        public int PlayerId { get; set; }

        [Range(0, 1000000,
            ErrorMessage = "Score must be between 0 and 1,000,000.")]
        public int Score { get; set; }

        [Required]
        [EnumDataType(typeof(MatchResult),
            ErrorMessage = "Result must be 0 (Win), 1 (Loss), or 2 (Draw).")]
        public MatchResult Result { get; set; }
    }
}