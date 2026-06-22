using System.ComponentModel.DataAnnotations;

namespace GameVault.API.DTOs.Player
{
    public class UpdatePlayerDto
    {
        [EmailAddress]
        public string? Email { get; set; }
    }
}