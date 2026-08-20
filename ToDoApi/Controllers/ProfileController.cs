using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Helpers;
using ToDoApi.Repositories;
using ToDoApi.Services;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService _profileService;

        public ProfileController(ProfileService profileService)
        {
            _profileService = profileService;
        }

        // get 
        [HttpGet("me/stats")]
        public async Task<IActionResult> getStats()
        {
            var userId = User.GetUserId();
            var stats = await _profileService.GetStatsAsync(userId);

            return Ok(stats);
        }
    }
}
