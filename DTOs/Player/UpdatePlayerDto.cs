using System.ComponentModel.DataAnnotations;

namespace GameVault.API.DTOs.Player
{
    public class UpdatePlayerDto
    {
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string? Email { get; set; }
    }
}