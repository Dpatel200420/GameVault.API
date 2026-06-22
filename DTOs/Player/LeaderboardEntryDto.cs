namespace GameVault.API.DTOs.Player
{
    public class LeaderboardEntryDto
    {
        public int Rank { get; set; }
        public int PlayerId { get; set; }
        public string Username { get; set; } = string.Empty;
        public int TotalScore { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
    }
}