﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRY20232084.Data;
using PRY20232084.Models.Entities;
using PRY20232084.DTOs;

namespace PRY20232084.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MovementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovementResponseDTO>>> GetMovements()
        {
            var movements = await _context.Movements
                .Select(m => new MovementResponseDTO
                {
                    ID = m.ID,
                    Description = m.Description,
                    CreatedAt = m.CreatedAt,
                    BoughtDate = m.BoughtDate,
                    MovementType = m.MovementType,
                    RegisterType = m.RegisterType,
                    CreatedBy = m.CreatedBy
                })
                .ToListAsync();

            return movements;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovementResponseDTO>> GetMovement(int id)
        {
            var movement = await _context.Movements
                .Where(m => m.ID == id)
                .Select(m => new MovementResponseDTO
                {
                    ID = m.ID,
                    Description = m.Description,
                    CreatedAt = m.CreatedAt,
                    BoughtDate = m.BoughtDate,
                    MovementType = m.MovementType,
                    RegisterType = m.RegisterType,
                    CreatedBy = m.CreatedBy
                })
                .FirstOrDefaultAsync();

            if (movement == null)
            {
                return NotFound();
            }

            return movement;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovement(int id, Movement movement)
        {
            if (id != movement.ID)
            {
                return BadRequest();
            }

            _context.Entry(movement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovementExists(id))
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
        public async Task<ActionResult<MovementResponseDTO>> PostMovement(MovementRequestDTO movementRequestDTO)
        {
            var movement = new Movement
            {
                Description = movementRequestDTO.Description,
                CreatedAt = DateTime.UtcNow,
                BoughtDate = movementRequestDTO.BoughtDate,
                MovementType = movementRequestDTO.MovementType,
                RegisterType = movementRequestDTO.RegisterType,
                CreatedBy = movementRequestDTO.CreatedBy
            };

            _context.Movements.Add(movement);
            await _context.SaveChangesAsync();

            var responseDTO = new MovementResponseDTO
            {
                ID = movement.ID,
                Description = movement.Description,
                CreatedAt = movement.CreatedAt,
                BoughtDate = movement.BoughtDate,
                MovementType = movement.MovementType,
                RegisterType = movement.RegisterType,
                CreatedBy = movement.CreatedBy
            };

            return CreatedAtAction("GetMovement", new { id = movement.ID }, responseDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovement(int id)
        {
            var movement = await _context.Movements.FindAsync(id);
            if (movement == null)
            {
                return NotFound();
            }

            _context.Movements.Remove(movement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovementExists(int id)
        {
            return _context.Movements.Any(m => m.ID == id);
        }
    }
}