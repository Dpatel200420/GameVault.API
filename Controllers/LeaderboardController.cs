using Microsoft.AspNetCore.Mvc;
using GameVault.API.DTOs.Player;
using GameVault.API.Services.Interfaces;

namespace GameVault.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaderboardController : ControllerBase
    {
        private readonly ILeaderboardService _leaderboardService;

        public LeaderboardController(ILeaderboardService leaderboardService)
        {
            _leaderboardService = leaderboardService;
        }

        // GET /api/leaderboard
        // GET /api/leaderboard?count=10
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LeaderboardEntryDto>),
            StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LeaderboardEntryDto>>>
            GetLeaderboard([FromQuery] int count = 100)
        {
            if (count < 1 || count > 500)
                count = 100;

            var entries = await _leaderboardService.GetTopPlayersAsync(count);
            return Ok(entries);
        }
    }
}