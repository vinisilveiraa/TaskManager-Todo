using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Helpers;
using ToDoApi.Repositories;


namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ItemController : ControllerBase
    {
        private readonly ItemRepository _repository;
        public ItemController(ItemRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _repository.GetAllAsync();
            return Ok(items);
        }
        [HttpGet("me")]
        public async Task<IActionResult> GetMyItems(
            [FromQuery] string? filter,
            [FromQuery] string? sort
            )
        {
            var userId = User.GetUserId();
            var items = await _repository.GetByUserIdAsync(userId, filter, sort);
            return Ok(items);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
                return NotFound($"Item with ID {id} not found.");

            return Ok(item);
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var item = await _repository.GetByUserIdAsync(userId, null, null);
            if (item == null)
                return NotFound($"Item related to user ID {userId} not found.");

            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateItemRequestDto request)
        {
            if (request == null) return BadRequest("Item is null.");

            var userId = User.GetUserId();

            var item = new Item
            {
                Title = request.Title,
                Description = request.Description,
                IsCompleted = false,
                Created_At = DateTime.UtcNow,
                UserId = userId,
            };

            await _repository.AddAsync(item);

            var response = new CreateItemResponseDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                IsCompleted = item.IsCompleted,
                Created_At = item.Created_At
            };

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _repository.ExistsAsync(id);
            if (!item)
                return NotFound($"Item with ID {id} not found.");

            await _repository.DeleteAsync(id);
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchStatus(int id)
        {
            var exists = await _repository.ExistsAsync(id);
            if (!exists)
                return NotFound($"Item with ID {id} not found.");

            await _repository.PatchStatusAsync(id);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateItemRequestDto request)
        {
            var item = await _repository.GetByIdAsync(id);

            if (item == null)
                return NotFound($"Item with ID {id} not found.");

            item.Title = request.Title;
            item.Description = request.Description;

            await _repository.UpdateAsync(id, item);
            return Ok(item);
        }
    }
}