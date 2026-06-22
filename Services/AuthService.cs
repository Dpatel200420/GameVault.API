using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using GameVault.API.DTOs.Auth;
using GameVault.API.Models.Entities;
using GameVault.API.Repositories.Interfaces;
using GameVault.API.Services.Interfaces;

namespace GameVault.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IConfiguration _configuration;

        public AuthService(
            IPlayerRepository playerRepository,
            IConfiguration configuration)
        {
            _playerRepository = playerRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            // Check username is not already taken
            var existingByUsername = await _playerRepository
                .GetByUsernameAsync(request.Username);

            if (existingByUsername != null)
                throw new InvalidOperationException(
                    "Username is already taken.");

            // Check email is not already registered
            var existingByEmail = await _playerRepository
                .GetByEmailAsync(request.Email);

            if (existingByEmail != null)
                throw new InvalidOperationException(
                    "Email is already registered.");

            // Hash the password — never store plain text
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Create the new player
            var player = new Player
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash
            };

            await _playerRepository.AddAsync(player);
            await _playerRepository.SaveChangesAsync();

            return GenerateAuthResponse(player);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {   
            // Find player by username
            var player = await _playerRepository
                .GetByUsernameAsync(request.Username);

            // Use identical error message for both "not found" and "wrong password"
            // This prevents attackers from knowing which usernames exist
            if (player == null)
                throw new UnauthorizedAccessException("Invalid credentials.");

            // Verify password against stored hash
            var isPasswordValid = BCrypt.Net.BCrypt.Verify(
                request.Password,
                player.PasswordHash);

            if (!isPasswordValid)
                throw new UnauthorizedAccessException("Invalid credentials.");

            return GenerateAuthResponse(player);
        }

        private AuthResponseDto GenerateAuthResponse(Player player)
        {
            var expiry = DateTime.UtcNow.AddHours(
                _configuration.GetValue<int>("Jwt:ExpiryHours"));

            var token = GenerateJwtToken(player, expiry);

            return new AuthResponseDto
            {
                Token = token,
                Username = player.Username,
                ExpiresAt = expiry
            };
        }

        private string GenerateJwtToken(Player player, DateTime expiry)
        {
            var secret = _configuration["Jwt:Secret"]!;
            var issuer = _configuration["Jwt:Issuer"]!;
            var audience = _configuration["Jwt:Audience"]!;

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secret));

            var credentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            // Claims are data embedded in the token
            // Player ID lets protected endpoints know which player is calling
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, player.Id.ToString()),
                new Claim(ClaimTypes.Name, player.Username),
                new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiry,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}