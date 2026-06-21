using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameVault.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        [HttpGet("status")]
        public ActionResult<string> GetStatus()
        {
            return Ok("GameVault API is running!");
        }
    }
}
