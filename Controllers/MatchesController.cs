using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GameVault.API.DTOs.Match;
using GameVault.API.Services.Interfaces;

namespace GameVault.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchesController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        // POST /api/matches
        // Records a completed match and updates all participant stats
        [HttpPost]
        [ProducesResponseType(typeof(MatchDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<MatchDto>> CreateMatch(
            [FromBody] CreateMatchDto request)
        {
            var match = await _matchService.CreateMatchAsync(request);
            return StatusCode(201, match);
        }

        // GET /api/matches/history/{playerId}
        // GET /api/matches/history/{playerId}?count=10
        [HttpGet("history/{playerId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<MatchDto>),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetMatchHistory(
            int playerId,
            [FromQuery] int count = 20)
        {
            if (count < 1 || count > 100)
                count = 20;

            var matches = await _matchService
                .GetPlayerMatchHistoryAsync(playerId, count);

            return Ok(matches);
        }
    }
}