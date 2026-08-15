using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.DTOs.User;
using ToDoApi.Helpers;
using ToDoApi.Models;
using ToDoApi.Repositories;
using ToDoApi.Services;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _repository;
        private readonly FileService _fileService;
        public UserController(UserRepository repository, FileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var users = await _repository.GetAllAsync();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return Ok(user);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _repository.ExistsAsync(id);
            if (!user)
                return NotFound($"User with ID {id} not found.");

            await _repository.DeleteAsync(id);
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchRole(int id)
        {
            var exists = await _repository.ExistsAsync(id);
            if (!exists)
                return NotFound($"User with ID {id} not found.");

            await _repository.PatchRoleAsync(id);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, User user)
        {
            var exists = await _repository.ExistsAsync(id);
            if (!exists)
                return NotFound($"User with ID {id} not found.");

            await _repository.UpdateAsync(id, user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }
        [HttpPost("avatar")]
        public async Task<IActionResult> UploadAvatar(IFormFile file)
        {
            var userId = User.GetUserId();
            var user = await _repository.GetByIdAsync(userId);

            if (user == null)
                return NotFound($"User with ID {userId} not found.");

            try
            {
                var currentAvatar = user.AvatarUrl;

                var avatarUrl = await _fileService.SaveAvatarAsync(file);
                user.AvatarUrl = avatarUrl;

                await _repository.UpdateAsync(userId, user);

                if (currentAvatar != null)
                    await _fileService.DeleteAvatarAsync(currentAvatar);

                return Ok(new { AvatarUrl = avatarUrl });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.GetUserId();
            var user = await _repository.GetByIdAsync(userId);

            if (user == null)
                return NotFound($"User with ID {userId} not found.");

            var response = new CurrentUserResponseDTO
            {
                Id = userId,
                UserName = user.UserName,
                AvatarUrl = user.AvatarUrl,
                Role = user.Role,
                Created_At = user.Created_At
            };

            return Ok(response);
        }
    }
}
