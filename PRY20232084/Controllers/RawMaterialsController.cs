using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRY20232084.Data;
using PRY20232084.Models.Entities;
using PRY20232084.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace PRY20232084.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RawMaterialsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RawMaterialsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RawMaterials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RawMaterialResponseDTO>>> GetRawMaterials()
        {
            var rawMaterials = await _context.RawMaterials
                .Select(r => new RawMaterialResponseDTO
                {
                    ID = r.ID,
                    Name = r.Name,
                    Description = r.Description,
                    BrandName = r.BrandName,
                    Color = r.Color,
                    Stock = r.Stock,
                    MeasurementUnit_ID = r.MeasurementUnit_ID,
                    CreatedBy = r.CreatedBy
                })
                .ToListAsync();

            return rawMaterials;
        }

        // GET: api/RawMaterials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RawMaterialResponseDTO>> GetRawMaterial(int id)
        {
            var rawMaterial = await _context.RawMaterials
                .Where(r => r.ID == id)
                .Select(r => new RawMaterialResponseDTO
                {
                    ID = r.ID,
                    Name = r.Name,
                    Description = r.Description,
                    BrandName = r.BrandName,
                    Color = r.Color,
                    Stock = r.Stock,
                    MeasurementUnit_ID = r.MeasurementUnit_ID,
                    CreatedBy = r.CreatedBy
                })
                .FirstOrDefaultAsync();

            if (rawMaterial == null)
            {
                return NotFound();
            }

            return rawMaterial;
        }

        // POST: api/RawMaterials
        [HttpPost]
        public async Task<IActionResult> PostRawMaterial(RawMaterialCreateDTO rawMaterialDTO)
        {
            var rawMaterial = new RawMaterial
            {
                Name = rawMaterialDTO.Name,
                Description = rawMaterialDTO.Description,
                BrandName = rawMaterialDTO.BrandName,
                Color = rawMaterialDTO.Color,
                Stock = rawMaterialDTO.Stock,
                MeasurementUnit_ID = rawMaterialDTO.MeasurementUnit_ID,
                CreatedBy = rawMaterialDTO.CreatedBy
            };

            _context.RawMaterials.Add(rawMaterial);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRawMaterial", new { id = rawMaterial.ID }, rawMaterial);
        }

        // PUT: api/RawMaterials/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRawMaterial(int id, RawMaterialCreateDTO rawMaterialDTO)
        {
            var rawMaterial = await _context.RawMaterials.FirstOrDefaultAsync(r => r.ID == id);

            if (rawMaterial == null)
            {
                return NotFound();
            }

            rawMaterial.Name = rawMaterialDTO.Name;
            rawMaterial.Description = rawMaterialDTO.Description;
            rawMaterial.BrandName = rawMaterialDTO.BrandName;
            rawMaterial.Color = rawMaterialDTO.Color;
            rawMaterial.Stock = rawMaterialDTO.Stock;
            rawMaterial.MeasurementUnit_ID = rawMaterialDTO.MeasurementUnit_ID;
            rawMaterial.CreatedBy = rawMaterialDTO.CreatedBy;

            _context.Entry(rawMaterial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RawMaterialExists(id))
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

        // DELETE: api/RawMaterials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRawMaterial(int id)
        {
            var rawMaterial = await _context.RawMaterials.FindAsync(id);
            if (rawMaterial == null)
            {
                return NotFound();
            }

            _context.RawMaterials.Remove(rawMaterial);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RawMaterialExists(int id)
        {
            return _context.RawMaterials.Any(e => e.ID == id);
        }
    }
}
