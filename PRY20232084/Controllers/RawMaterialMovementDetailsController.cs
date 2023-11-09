using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRY20232084.Data;
using PRY20232084.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRY20232084.DTOs;

namespace PRY20232084.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RawMaterialMovementDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RawMaterialMovementDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RawMaterialMovementDetailResponseDTO>>>
        GetRawMaterialMovementDetails()
        {
            var details = await _context.RawMaterialMovementDetails
                .Select(d => new RawMaterialMovementDetailResponseDTO
                {
                    ID = d.ID,
                    RawMaterial_ID = d.RawMaterial_ID,
                    Movement_ID = d.Movement_ID,
                    Quantity = d.Quantity
                })
                .ToListAsync();

            return details;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RawMaterialMovementDetailResponseDTO>> GetRawMaterialMovementDetail(int id)
        {
            var detail = await _context.RawMaterialMovementDetails
                .Where(d => d.ID == id)
                .Select(d => new RawMaterialMovementDetailResponseDTO
                {
                    ID = d.ID,
                    RawMaterial_ID = d.RawMaterial_ID,
                    Movement_ID = d.Movement_ID,
                    Quantity = d.Quantity
                })
                .SingleOrDefaultAsync();

            if (detail == null)
            {
                return NotFound();
            }

            return detail;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRawMaterialMovementDetail(int id, RawMaterialMovementDetailRequestDTO detailRequestDTO)
        {
            var detail = await _context.RawMaterialMovementDetails.FindAsync(id);

            if (detail == null)
            {
                return NotFound();
            }

            // Actualiza los campos del detalle con los valores del DTO
            detail.RawMaterial_ID = detailRequestDTO.RawMaterial_ID;
            detail.Movement_ID = detailRequestDTO.Movement_ID;
            detail.Quantity = detailRequestDTO.Quantity;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RawMaterialMovementDetailExists(id))
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
        public async Task<ActionResult<RawMaterialMovementDetailResponseDTO>> PostRawMaterialMovementDetail(RawMaterialMovementDetailRequestDTO detailRequestDTO)
        {
            var detail = new RawMaterialMovementDetail
            {
                RawMaterial_ID = detailRequestDTO.RawMaterial_ID,
                Movement_ID = detailRequestDTO.Movement_ID,
                Quantity = detailRequestDTO.Quantity
            };

            _context.RawMaterialMovementDetails.Add(detail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRawMaterialMovementDetail", new { id = detail.ID }, new RawMaterialMovementDetailResponseDTO
            {
                ID = detail.ID,
                RawMaterial_ID = detail.RawMaterial_ID,
                Movement_ID = detail.Movement_ID,
                Quantity = detail.Quantity
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRawMaterialMovementDetail(int id)
        {
            var detail = await _context.RawMaterialMovementDetails.FindAsync(id);

            if (detail == null)
            {
                return NotFound();
            }

            _context.RawMaterialMovementDetails.Remove(detail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RawMaterialMovementDetailExists(int id)
        {
            return _context.RawMaterialMovementDetails.Any(e => e.ID == id);
        }
    }
}
