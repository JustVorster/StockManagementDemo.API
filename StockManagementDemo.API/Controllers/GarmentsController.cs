using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagementDemo.API.DTOs;
using StockManagementDemo.API.Interfaces;
using StockManagementDemo.API.Models;

namespace StockManagementDemo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GarmentsController(IGarmentRepository repository) : ControllerBase
    {
        private readonly IGarmentRepository _repository = repository;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repository.GetAllAsync();
            var dtos = items.Select(item => new GarmentReadDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                RentalPrice = item.RentalPrice,
                ResalePrice = item.ResalePrice,
                IsResaleAvailable = item.IsResaleAvailable,
                Size = item.Size,
                Occasion = item.Occasion,
                LenderId = item.LenderId
            });

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item is null) return NotFound();

            var dto = new GarmentReadDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                RentalPrice = item.RentalPrice,
                ResalePrice = item.ResalePrice,
                IsResaleAvailable = item.IsResaleAvailable,
                Size = item.Size,
                Occasion = item.Occasion,
                LenderId = item.LenderId
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GarmentCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var garment = new Garment
            {
                Name = dto.Name,
                Description = dto.Description,
                RentalPrice = dto.RentalPrice,
                ResalePrice = dto.ResalePrice,
                IsResaleAvailable = dto.IsResaleAvailable,
                Size = dto.Size,
                Occasion = dto.Occasion,
                LenderId = dto.LenderId
            };

            await _repository.AddAsync(garment);
            return CreatedAtAction(nameof(GetById), new { id = garment.Id }, new { garment.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GarmentUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            var existing = await _repository.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.RentalPrice = dto.RentalPrice;
            existing.ResalePrice = dto.ResalePrice;
            existing.IsResaleAvailable = dto.IsResaleAvailable;
            existing.Size = dto.Size;
            existing.Occasion = dto.Occasion;

            await _repository.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var garment = await _repository.GetByIdAsync(id);
            if (garment is null)
                return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
