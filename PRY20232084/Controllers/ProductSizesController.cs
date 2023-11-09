using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRY20232084.Data;
using PRY20232084.Models.Entities;
using PRY20232084.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRY20232084.Models.DTOs;

namespace PRY20232084.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSizesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductSizesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductSizeResponseDTO>>>
        GetProductSizes()
        {
            var sizes = await _context.Sizes
                .Select(s => new ProductSizeResponseDTO
                {
                    ID = s.ID,
                    Name = s.Name,
                    CreatedBy = s.CreatedBy
                })
                .ToListAsync();

            return sizes;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductSizeResponseDTO>> GetProductSize(int id)
        {
            var size = await _context.Sizes
                .Where(s => s.ID == id)
                .Select(s => new ProductSizeResponseDTO
                {
                    ID = s.ID,
                    Name = s.Name,
                    CreatedBy = s.CreatedBy
                })
                .SingleOrDefaultAsync();

            if (size == null)
            {
                return NotFound();
            }

            return size;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductSize(int id, ProductSizeRequestDTO sizeRequestDTO)
        {
            var size = await _context.Sizes.FindAsync(id);

            if (size == null)
            {
                return NotFound();
            }

            // Actualiza los campos del tamaño con los valores del DTO
            size.Name = sizeRequestDTO.Name;
            size.CreatedBy = sizeRequestDTO.CreatedBy;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductSizeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ProductSizeResponseDTO>> PostProductSize(ProductSizeRequestDTO sizeRequestDTO)
        {
            var size = new ProductSize
            {
                Name = sizeRequestDTO.Name,
                CreatedBy = sizeRequestDTO.CreatedBy
            };

            _context.Sizes.Add(size);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductSize", new { id = size.ID }, new ProductSizeResponseDTO
            {
                ID = size.ID,
                Name = size.Name,
                CreatedBy = size.CreatedBy
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductSize(int id)
        {
            var size = await _context.Sizes.FindAsync(id);

            if (size == null)
            {
                return NotFound();
            }

            _context.Sizes.Remove(size);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductSizeExists(int id)
        {
            return _context.Sizes.Any(e => e.ID == id);
        }
    }
}
