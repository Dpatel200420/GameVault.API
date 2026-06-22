namespace GameVault.API.DTOs.Player
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Level { get; set; }
        public int TotalScore { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}