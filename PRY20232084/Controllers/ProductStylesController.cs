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
    public class ProductStylesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductStylesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductStyleResponseDTO>>>
        GetProductStyles()
        {
            var styles = await _context.Styles
                .Select(s => new ProductStyleResponseDTO
                {
                    ID = s.ID,
                    Name = s.Name,
                    Description = s.Description,
                    CreatedBy = s.CreatedBy
                })
                .ToListAsync();

            return styles;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductStyleResponseDTO>> GetProductStyle(int id)
        {
            var style = await _context.Styles
                .Where(s => s.ID == id)
                .Select(s => new ProductStyleResponseDTO
                {
                    ID = s.ID,
                    Name = s.Name,
                    Description = s.Description,
                    CreatedBy = s.CreatedBy
                })
                .SingleOrDefaultAsync();

            if (style == null)
            {
                return NotFound();
            }

            return style;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductStyle(int id, ProductStyleRequestDTO styleRequestDTO)
        {
            var style = await _context.Styles.FindAsync(id);

            if (style == null)
            {
                return NotFound();
            }

            // Actualiza los campos del estilo con los valores del DTO
            style.Name = styleRequestDTO.Name;
            style.Description = styleRequestDTO.Description;
            style.CreatedBy = styleRequestDTO.CreatedBy;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductStyleExists(id))
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
        public async Task<ActionResult<ProductStyleResponseDTO>> PostProductStyle(ProductStyleRequestDTO styleRequestDTO)
        {
            var style = new ProductStyle
            {
                Name = styleRequestDTO.Name,
                Description = styleRequestDTO.Description,
                CreatedBy = styleRequestDTO.CreatedBy
            };

            _context.Styles.Add(style);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductStyle", new { id = style.ID }, new ProductStyleResponseDTO
            {
                ID = style.ID,
                Name = style.Name,
                Description = style.Description,
                CreatedBy = style.CreatedBy
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductStyle(int id)
        {
            var style = await _context.Styles.FindAsync(id);

            if (style == null)
            {
                return NotFound();
            }

            _context.Styles.Remove(style);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductStyleExists(int id)
        {
            return _context.Styles.Any(e => e.ID == id);
        }
    }
}
