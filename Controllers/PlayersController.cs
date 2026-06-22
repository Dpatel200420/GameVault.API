using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GameVault.API.DTOs.Player;
using GameVault.API.Services.Interfaces;

namespace GameVault.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        // GET /api/players/me
        // Returns the profile of the currently logged-in player
        [HttpGet("me")]
        [ProducesResponseType(typeof(PlayerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PlayerDto>> GetMyProfile()
        {
            var playerId = GetCurrentPlayerId();
            var player = await _playerService.GetMyProfileAsync(playerId);
            return Ok(player);
        }

        // GET /api/players/{id}
        // Returns any player's public profile
        // AllowAnonymous overrides the [Authorize] on the class
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PlayerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlayerDto>> GetPlayer(int id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);
            return Ok(player);
        }

        // PATCH /api/players/me
        // Updates the logged-in player's own profile
        [HttpPatch("me")]
        [ProducesResponseType(typeof(PlayerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlayerDto>> UpdateMyProfile(
            [FromBody] UpdatePlayerDto request)
        {
            var playerId = GetCurrentPlayerId();
            var player = await _playerService.UpdatePlayerAsync(
                playerId, request);
            return Ok(player);
        }

        // Private helper — extracts player ID from JWT claims
        // Reused by any endpoint that needs to know who is calling
        private int GetCurrentPlayerId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new UnauthorizedAccessException(
                    "Player identity not found in token.");

            return int.Parse(claim);
        }
    }
}