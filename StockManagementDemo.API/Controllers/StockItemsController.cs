using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagementDemo.API.DTOs;
using StockManagementDemo.API.Models;
using StockManagementDemo.API.Services;

namespace StockManagementDemo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class StockItemsController : ControllerBase
    {
        private readonly StockItemService _service;

        public StockItemsController(StockItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllAsync();
            var dtos = items.Select(item => new StockItemReadDto
            {
                Id = item.Id,
                RegNo = item.RegNo,
                Make = item.Make,
                Model = item.Model,
                ModelYear = item.ModelYear,
                KMS = item.KMS,
                Colour = item.Colour,
                VIN = item.VIN,
                RetailPrice = item.RetailPrice,
                CostPrice = item.CostPrice,
                DTCreated = item.DTCreated,
                DTUpdated = item.DTUpdated
            });

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item is null) return NotFound();

            var dto = new StockItemReadDto
            {
                Id = item.Id,
                RegNo = item.RegNo,
                Make = item.Make,
                Model = item.Model,
                ModelYear = item.ModelYear,
                KMS = item.KMS,
                Colour = item.Colour,
                VIN = item.VIN,
                RetailPrice = item.RetailPrice,
                CostPrice = item.CostPrice,
                DTCreated = item.DTCreated,
                DTUpdated = item.DTUpdated
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StockItemCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = new StockItem
            {
                RegNo = dto.RegNo,
                Make = dto.Make,
                Model = dto.Model,
                ModelYear = dto.ModelYear,
                KMS = dto.KMS,
                Colour = dto.Colour,
                VIN = dto.VIN,
                RetailPrice = dto.RetailPrice,
                CostPrice = dto.CostPrice,
                DTCreated = DateTime.UtcNow,
                DTUpdated = DateTime.UtcNow
            };

            await _service.AddAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, new { item.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StockItemUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            var existing = await _service.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            existing.RegNo = dto.RegNo;
            existing.Make = dto.Make;
            existing.Model = dto.Model;
            existing.ModelYear = dto.ModelYear;
            existing.KMS = dto.KMS;
            existing.Colour = dto.Colour;
            existing.VIN = dto.VIN;
            existing.RetailPrice = dto.RetailPrice;
            existing.CostPrice = dto.CostPrice;
            existing.DTUpdated = DateTime.UtcNow;

            await _service.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item is null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}