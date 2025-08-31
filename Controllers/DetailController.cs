using Microsoft.AspNetCore.Mvc;
using HelloApi.Models.DTOs;
using HelloApi.Services;

namespace HelloApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetailController(IDetailService service) : ControllerBase
    {
        private readonly IDetailService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var details = await _service.GetAllDetailsAsync();
            return Ok(details);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var detail = await _service.GetDetailByIdAsync(id);
            if (detail == null) return NotFound();
            return Ok(detail);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DetailCreateDto dto)
        {
            var created = await _service.CreateDetailAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] DetailUpdateDto dto)
        {
            var updated = await _service.UpdateDetailAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteDetailAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
