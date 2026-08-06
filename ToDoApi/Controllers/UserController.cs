using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Repositories;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _repository;
        public UserController(UserRepository repository)
        {
            _repository = repository;
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
    }
}
