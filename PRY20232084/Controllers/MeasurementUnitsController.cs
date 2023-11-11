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
        public async Task<ActionResult<IEnumerable<MeasurementUnit>>> GetMeasurementUnits()
        {
            if (_context.MeasurementUnits == null)
            {
                return NotFound();
            }
            return await _context.MeasurementUnits.ToListAsync();
        }

        // GET: api/MeasurementUnits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MeasurementUnit>> GetMeasurementUnit(int id)
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

            return measurementUnit;
        }

        // PUT: api/MeasurementUnits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeasurementUnit(int id, MeasurementUnitDTO measurementUnitDTO)
        {
            if (id != id)
            {
                return BadRequest();
            }

            var measurementUnit = new MeasurementUnit
            {
                ID = id,
                Name = measurementUnitDTO.Name,
                Abbreviation = measurementUnitDTO.Abbreviation
            };

            _context.Entry(measurementUnit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            return NoContent();
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
