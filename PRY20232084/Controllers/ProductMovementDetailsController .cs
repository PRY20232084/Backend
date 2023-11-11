using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRY20232084.Data;
using PRY20232084.DTOs;
using PRY20232084.Models.DTOs;
using PRY20232084.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRY20232084.Controllers
{
    [EnableCors("InventoryManagement")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductMovementDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductMovementDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductMovementDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductMovementDetailDTO>>> GetProductMovementDetails()
        {
            var productMovementDetails = await _context.ProductMovementDetails
                .Select(pmd => new ProductMovementDetailDTO
                {
                    ID = pmd.ID,
                    ProductID = pmd.Product_ID,
                    MovementID = pmd.Movement_ID,
                    Quantity = pmd.Quantity
                })
                .ToListAsync();

            return productMovementDetails;
        }

        // GET: api/ProductMovementDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductMovementDetailDTO>> GetProductMovementDetail(int id)
        {
            var productMovementDetail = await _context.ProductMovementDetails.FindAsync(id);

            if (productMovementDetail == null)
            {
                return NotFound();
            }

            var responseDTO = new ProductMovementDetailDTO
            {
                ID = productMovementDetail.ID,
                ProductID = productMovementDetail.Product_ID,
                MovementID = productMovementDetail.Movement_ID,
                Quantity = productMovementDetail.Quantity
            };

            return responseDTO;
        }

        // POST: api/ProductMovementDetails
        [HttpPost]
        public async Task<ActionResult<ProductMovementDetailDTO>> CreateProductMovementDetail(CreateProductMovementDetailDTO createDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productMovementDetail = new ProductMovementDetail
            {
                Product_ID = createDTO.ProductID,
                Movement_ID = createDTO.MovementID,
                Quantity = createDTO.Quantity
            };

            _context.ProductMovementDetails.Add(productMovementDetail);
            await _context.SaveChangesAsync();

            var responseDTO = new ProductMovementDetailDTO
            {
                ID = productMovementDetail.ID,
                ProductID = productMovementDetail.Product_ID,
                MovementID = productMovementDetail.Movement_ID,
                Quantity = productMovementDetail.Quantity
            };

            return CreatedAtAction("GetProductMovementDetail", new { id = productMovementDetail.ID }, responseDTO);
        }

        // PUT: api/ProductMovementDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductMovementDetail(int id, UpdateProductMovementDetailDTO updateDTO)
        {
            var productMovementDetail = await _context.ProductMovementDetails.FindAsync(id);

            if (productMovementDetail == null)
            {
                return NotFound();
            }

            productMovementDetail.Quantity = updateDTO.Quantity;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/ProductMovementDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductMovementDetail(int id)
        {
            var productMovementDetail = await _context.ProductMovementDetails.FindAsync(id);

            if (productMovementDetail == null)
            {
                return NotFound();
            }

            _context.ProductMovementDetails.Remove(productMovementDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
