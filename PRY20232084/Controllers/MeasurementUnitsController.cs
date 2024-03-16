using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRY20232084.Data;
using PRY20232084.DTOs;
using PRY20232084.Models;
using PRY20232084.Models.DTOs;
using PRY20232084.Models.Entities;

namespace PRY20232084.Controllers
{
    [EnableCors("InventoryManagement")]
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementUnitsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MeasurementUnitsController(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        // GET: api/MeasurementUnits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeasurementUnitRequestDTO>>> GetMeasurementUnits()
        {
            var measurementUnits = await _context.MeasurementUnits
                .Select(d => new MeasurementUnitRequestDTO
                {
                    Name = d.Name,
                    Abbreviation = d.Abbreviation,
                    CreatedBy = d.CreatedBy
                })
                .ToListAsync();

            // fetch username for each movement
            foreach (MeasurementUnitRequestDTO measurementUnit in measurementUnits)
            {
                var user = await _context.Users.FindAsync(measurementUnit.CreatedBy);
                measurementUnit.CreatedBy = user.Name;
            }

            return measurementUnits;
        }

        // GET: api/MeasurementUnits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MeasurementUnitRequestDTO>> GetMeasurementUnit(int id)
        {
            var measurementUnit = await _context.MeasurementUnits
                .Where(x => x.ID == id)
                .Select(d => new MeasurementUnitRequestDTO
                {
                    Name = d.Name,
                    Abbreviation = d.Abbreviation,
                    CreatedBy = d.CreatedBy
                })
                .SingleOrDefaultAsync();

            if (measurementUnit == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(measurementUnit.CreatedBy);
            measurementUnit.CreatedBy = user.Name;

            return measurementUnit;
        }

        // PUT: api/MeasurementUnits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeasurementUnit(int id, MeasurementUnitDTO measurementUnitDTO)
        {
            var measurementUnit = await _context.MeasurementUnits.FindAsync(id);

            if (measurementUnit == null)
            {
                return NotFound();
            }

            measurementUnit.Name = measurementUnitDTO.Name;
            measurementUnit.Abbreviation = measurementUnitDTO.Abbreviation;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Measurement unit updated successfully.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeasurementUnitExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/MeasurementUnits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MeasurementUnit>> PostMeasurementUnit(MeasurementUnitCreateDTO measurementUnitCreateDTO)
        {
            var measurementUnit = new MeasurementUnit
            {
                Name = measurementUnitCreateDTO.Name,
                Abbreviation = measurementUnitCreateDTO.Abbreviation,
                CreatedBy = measurementUnitCreateDTO.CreatedBy
            };

            _context.MeasurementUnits.Add(measurementUnit);
            await _context.SaveChangesAsync();

            var user = await _context.Users.FindAsync(measurementUnit.CreatedBy);

            measurementUnit.CreatedBy = user.Name;

            return CreatedAtAction("GetMeasurementUnit", new { id = measurementUnit.ID }, measurementUnit);
        }

        // DELETE: api/MeasurementUnits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeasurementUnit(int id)
        {
            if (_context.MeasurementUnits == null)
            {
                return NotFound();
            }
            var measurementUnit = await _context.MeasurementUnits.FindAsync(id);
            if (measurementUnit == null)
            {
                return NotFound();
            }

            _context.MeasurementUnits.Remove(measurementUnit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MeasurementUnitExists(int id)
        {
            return (_context.MeasurementUnits?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
