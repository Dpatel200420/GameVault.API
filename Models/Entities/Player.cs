namespace GameVault.API.Models.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int Level { get; set; } = 1;
        public int TotalScore { get; set; } = 0;
        public int GamesPlayed { get; set; } = 0;
        public int GamesWon { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<MatchParticipant> MatchParticipants { get; set; }
           = new List<MatchParticipant>();
    }
}