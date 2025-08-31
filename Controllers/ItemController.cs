using Microsoft.AspNetCore.Mvc;
using HelloApi.Models.DTOs;
using HelloApi.Services;

namespace HelloApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController(IItemService service) : ControllerBase
    {
        private readonly IItemService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetItemByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ItemCreateDto dto)
        {
            var created = await _service.CreateItemAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ItemUpdateDto dto)
        {
            var updated = await _service.UpdateItemAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteItemAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
