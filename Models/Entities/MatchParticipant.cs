namespace GameVault.API.Models.Entities
{
    public class MatchParticipant
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public int Score { get; set; }
        public MatchResult Result { get; set; }

        public Match Match { get; set; } = null!;
        public Player Player { get; set; } = null!;
    }

    public enum MatchResult
    {
        Win,
        Loss,
        Draw
    }
}