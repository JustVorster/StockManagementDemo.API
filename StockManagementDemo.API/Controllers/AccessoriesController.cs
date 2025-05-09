using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagementDemo.API.Data;
using StockManagementDemo.API.Models;

namespace StockManagementDemo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccessoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccessoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accessories = await _context.Accessories.Include(a => a.StockItem).ToListAsync();
            return Ok(accessories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var accessory = await _context.Accessories.FindAsync(id);
            if (accessory is null) return NotFound();
            return Ok(accessory);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Accessory accessory)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Accessories.Add(accessory);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = accessory.Id }, accessory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Accessory updated)
        {
            if (id != updated.Id) return BadRequest("ID mismatch");

            var existing = await _context.Accessories.FindAsync(id);
            if (existing is null) return NotFound();

            existing.Name = updated.Name;
            existing.Description = updated.Description;
            existing.StockItemId = updated.StockItemId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var accessory = await _context.Accessories.FindAsync(id);
            if (accessory is null) return NotFound();

            _context.Accessories.Remove(accessory);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}