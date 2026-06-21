namespace GameVault.API.Models.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public string GameMode { get; set; } = string.Empty;
        public DateTime PlayedAt { get; set; } = DateTime.UtcNow;
        public int DurationSeconds { get; set; }

        public ICollection<MatchParticipant> Participants { get; set; }
            = new List<MatchParticipant>();
    }
}