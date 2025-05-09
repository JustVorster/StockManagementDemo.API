using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagementDemo.API.Data;
using StockManagementDemo.API.DTOs;
using StockManagementDemo.API.Models;

namespace StockManagementDemo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var images = await _context.Images.Include(i => i.StockItem).ToListAsync();
            return Ok(images);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image is null) return NotFound();
            return Ok(image);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Image image)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Images.Add(image);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = image.Id }, image);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image is null) return NotFound();

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("No file uploaded.");

            var item = await _context.StockItems
                .Include(s => s.Images)
                .FirstOrDefaultAsync(s => s.Id == dto.StockItemId);

            if (item is null)
                return NotFound("Stock item not found.");

            if (item.Images.Count >= 3)
                return BadRequest("Maximum of 3 images allowed per stock item.");

            using var memoryStream = new MemoryStream();
            await dto.File.CopyToAsync(memoryStream);

            var image = new Image
            {
                Name = dto.File.FileName,
                Data = memoryStream.ToArray(),
                StockItemId = dto.StockItemId
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return Ok(new { image.Id, image.Name });
        }

    }
}
