using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRY20232084.Data;
using PRY20232084.Models.Entities;
using PRY20232084.Models.DTOs;
using Microsoft.AspNetCore.Cors;

namespace PRY20232084.Controllers
{
    [EnableCors("InventoryManagement")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/ProductDetails
        [HttpPost]
        public async Task<ActionResult<ProductDetailResponseDTO>> PostProductDetail(ProductDetailDTO productDetailDTO)
        {
            var productDetail = new ProductDetail
            {
                Product_ID = productDetailDTO.Product_ID,
                RawMaterial_ID = productDetailDTO.RawMaterial_ID,
                Quantity = productDetailDTO.Quantity
            };

            _context.ProductDetails.Add(productDetail);
            await _context.SaveChangesAsync();

            var responseDTO = new ProductDetailResponseDTO
            {
                Product_ID = productDetail.Product_ID,
                RawMaterial_ID = productDetail.RawMaterial_ID,
                Quantity = productDetail.Quantity
            };

            return CreatedAtAction("GetProductDetail", new { product_ID = productDetail.Product_ID, rawMaterial_ID = productDetail.RawMaterial_ID }, responseDTO);
        }

        // GET: api/ProductDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDetailResponseDTO>>> GetProductDetails()
        {
            var productDetails = await _context.ProductDetails
                .Select(pd => new ProductDetailResponseDTO
                {
                    Product_ID = pd.Product_ID,
                    RawMaterial_ID = pd.RawMaterial_ID,
                    Quantity = pd.Quantity
                })
                .ToListAsync();

            return productDetails;
        }

        // GET: api/ProductDetails/5/1
        [HttpGet("{product_ID}/{rawMaterial_ID}")]
        public async Task<ActionResult<ProductDetailResponseDTO>> GetProductDetail(int product_ID, int rawMaterial_ID)
        {
            var productDetail = await _context.ProductDetails
                .Where(pd => pd.Product_ID == product_ID && pd.RawMaterial_ID == rawMaterial_ID)
                .FirstOrDefaultAsync();

            if (productDetail == null)
            {
                return NotFound();
            }

            var responseDTO = new ProductDetailResponseDTO
            {
                Product_ID = productDetail.Product_ID,
                RawMaterial_ID = productDetail.RawMaterial_ID,
                Quantity = productDetail.Quantity
            };

            return responseDTO;
        }

        // PUT: api/ProductDetails/5/1
        [HttpPut("{product_ID}/{rawMaterial_ID}")]
        public async Task<IActionResult> PutProductDetail(int product_ID, int rawMaterial_ID, ProductDetailDTO productDetailDTO)
        {
            if (product_ID != productDetailDTO.Product_ID || rawMaterial_ID != productDetailDTO.RawMaterial_ID)
            {
                return BadRequest();
            }

            var existingProductDetail = await _context.ProductDetails
                .Where(pd => pd.Product_ID == product_ID && pd.RawMaterial_ID == rawMaterial_ID)
                .FirstOrDefaultAsync();

            if (existingProductDetail == null)
            {
                return NotFound();
            }

            existingProductDetail.Product_ID = productDetailDTO.Product_ID;
            existingProductDetail.RawMaterial_ID = productDetailDTO.RawMaterial_ID;
            existingProductDetail.Quantity = productDetailDTO.Quantity;

            _context.Entry(existingProductDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductDetailExists(product_ID, rawMaterial_ID))
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

        // DELETE: api/ProductDetails/5/1
        [HttpDelete("{product_ID}/{rawMaterial_ID}")]
        public async Task<IActionResult> DeleteProductDetail(int product_ID, int rawMaterial_ID)
        {
            var productDetail = await _context.ProductDetails
                .Where(pd => pd.Product_ID == product_ID && pd.RawMaterial_ID == rawMaterial_ID)
                .FirstOrDefaultAsync();

            if (productDetail == null)
            {
                return NotFound();
            }

            _context.ProductDetails.Remove(productDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductDetailExists(int product_ID, int rawMaterial_ID)
        {
            return _context.ProductDetails.Any(pd => pd.Product_ID == product_ID && pd.RawMaterial_ID == rawMaterial_ID);
        }
    }
}
