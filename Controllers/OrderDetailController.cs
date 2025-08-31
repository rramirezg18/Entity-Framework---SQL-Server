using Microsoft.AspNetCore.Mvc;
using HelloApi.Models.DTOs;
using HelloApi.Services;

namespace HelloApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailController(IOrderDetailService service) : ControllerBase
    {
        private readonly IOrderDetailService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var details = await _service.GetAllOrderDetailsAsync();
            return Ok(details);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var detail = await _service.GetOrderDetailByIdAsync(id);
            if (detail == null) return NotFound();
            return Ok(detail);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDetailCreateDto dto)
        {
            var created = await _service.CreateOrderDetailAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderDetailUpdateDto dto)
        {
            var updated = await _service.UpdateOrderDetailAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteOrderDetailAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
