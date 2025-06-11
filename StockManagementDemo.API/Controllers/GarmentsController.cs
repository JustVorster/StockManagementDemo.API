using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagementDemo.API.DTOs;
using StockManagementDemo.API.Interfaces;
using StockManagementDemo.API.Models;
using StockManagementDemo.API.Services;

namespace StockManagementDemo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GarmentsController(IGarmentRepository repository, DbContext context) : ControllerBase
    {
        private readonly IGarmentRepository _repository = repository;
        private readonly DbContext _context = context; 

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
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id, [FromServices] AnalyticsLoggerService analyticsLogger)
        {
            var garment = await _repository.GetByIdAsync(id);
            if (garment == null)
                return NotFound();

            await analyticsLogger.LogAsync(id, "Viewed");

            var dto = new GarmentReadDto
            {
                Id = garment.Id,
                Name = garment.Name,
                Description = garment.Description,
                RentalPrice = garment.RentalPrice,
                ResalePrice = garment.ResalePrice,
                IsResaleAvailable = garment.IsResaleAvailable,
                Size = garment.Size,
                Occasion = garment.Occasion,
                LenderId = garment.LenderId
            };

            return Ok(dto);
        }


        [HttpGet("search")]
        [AllowAnonymous]

        public async Task<IActionResult> GetFiltered(
        [FromQuery] string? size,
        [FromQuery] decimal? priceMax,
        [FromQuery] string? occasion,
        [FromQuery] bool? resaleOnly,
        [FromServices] AnalyticsLoggerService analyticsLogger)
        {
            var query = _repository.GetQueryable();

            if (!string.IsNullOrWhiteSpace(size))
                query = query.Where(g => g.Size.ToLower() == size.ToLower());

            if (priceMax.HasValue)
                query = query.Where(g => g.RentalPrice <= priceMax.Value);

            if (!string.IsNullOrWhiteSpace(occasion))
                query = query.Where(g => g.Occasion.ToLower().Contains(occasion.ToLower()));

            if (resaleOnly == true)
                query = query.Where(g => g.IsResaleAvailable);

            var results = await query.ToListAsync();

            foreach (var garment in results)
            {
                await analyticsLogger.LogAsync(garment.Id, "Filtered");
            }

            var dtos = results.Select(g => new GarmentReadDto
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                RentalPrice = g.RentalPrice,
                ResalePrice = g.ResalePrice,
                IsResaleAvailable = g.IsResaleAvailable,
                Size = g.Size,
                Occasion = g.Occasion,
                LenderId = g.LenderId
            });

            return Ok(dtos);
        }


        [HttpGet("{id}/calendar")]
        [Authorize(Roles = "Admin,Lender")]

        public async Task<IActionResult> GetAvailability(int id, [FromServices] AnalyticsLoggerService analyticsLogger)
        {
            var garment = await _context.Set<Garment>()
                 .Include(g => g.Rentals)
                 .FirstOrDefaultAsync(g => g.Id == id);

            if (garment == null)
                return NotFound("Garment not found");

            await analyticsLogger.LogAsync(id, "Viewed calendar");

            var calendar = garment.Rentals
                .Where(r => r.Status == RentalStatus.Confirmed)
                .Select(r => new DetailedRentalCalendarDto
                {
                    RentalId = r.Id,
                    RenterId = r.RenterId,
                    RentFrom = r.RentFrom,
                    RentTo = r.RentTo,
                    BufferUntil = r.RentTo.AddDays(3),
                    Status = r.Status
                })
                .OrderBy(r => r.RentFrom)
                .ToList();

            return Ok(calendar);
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
