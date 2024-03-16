using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRY20232084.Data;
using PRY20232084.Models.Entities;
using PRY20232084.DTOs;
using Microsoft.AspNetCore.Cors;

namespace PRY20232084.Controllers
{
    [EnableCors("InventoryManagement")]
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

            // fetch username for each movement
            foreach (MovementResponseDTO movement in movements)
            {
                var user = await _context.Users.FindAsync(movement.CreatedBy);
                movement.CreatedBy = user.Name;
            }

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

            var user = await _context.Users.FindAsync(movement.CreatedBy);

            movement.CreatedBy = user.Name;

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

            var user = await _context.Users.FindAsync(movement.CreatedBy);

            var responseDTO = new MovementResponseDTO
            {
                ID = movement.ID,
                Description = movement.Description,
                CreatedAt = movement.CreatedAt,
                BoughtDate = movement.BoughtDate,
                MovementType = movement.MovementType,
                RegisterType = movement.RegisterType,
                CreatedBy = user.Name
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

            var movementType = movement.MovementType;
            var registerType = movement.RegisterType;

            if (registerType) //raw material
            {
                var rawMaterialDetail = movement.RawMaterialMovementDetails.FirstOrDefault();
                var rawMaterial = _context.RawMaterials.Where(x => x.ID == rawMaterialDetail.RawMaterial_ID).FirstOrDefault();
                
                if (movementType) //ingreso
                {
                    rawMaterial.Stock -= rawMaterialDetail.Quantity;
                }
                else //salida
                {
                    rawMaterial.Stock += rawMaterialDetail.Quantity;
                }
            }
            else //product
            {
                var productDetail = movement.ProductMovementDetails.FirstOrDefault();
                var product = _context.Products.Where(x => x.ID == productDetail.Product_ID).FirstOrDefault();

                if (movementType) //ingreso
                {
                    product.Stock -= productDetail.Quantity;
                }
                else //salida
                {
                    product.Stock += productDetail.Quantity;
                }
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
