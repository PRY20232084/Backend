using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRY20232084.Data;
using PRY20232084.Models.Entities;
using PRY20232084.DTOs;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using PRY20232084.Models.DTOs;

namespace PRY20232084.Controllers
{
    [EnableCors("InventoryManagement")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetProducts()
        {
            var products = await _context.Products
                .Select(p => new ProductResponseDTO
                {
                    ID = p.ID,
                    Name = p.Name,
                    Description = p.Description,
                    Size_ID = p.Size_ID,
                    Style_ID = p.Style_ID,
                    Stock = p.Stock,
                    CreatedBy = p.CreatedBy
                })
                .ToListAsync();

            foreach (ProductResponseDTO product in products)
            {
                var user = await _context.Users.FindAsync(product.CreatedBy);
                product.CreatedBy = user.Name;
            }

            foreach (ProductResponseDTO product in products)
            {
                var size = await _context.Sizes.FindAsync(product.Size_ID);
                product.sizeName = size.Name;
            }

            foreach (ProductResponseDTO product in products)
            {
                var style = await _context.Styles.FindAsync(product.Style_ID);
                product.styleName = style.Name;
            }

            return products;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDTO>> GetProduct(int id)
        {
            var product = await _context.Products
                .Where(p => p.ID == id)
                .Select(p => new ProductResponseDTO
                {
                    ID = p.ID,
                    Name = p.Name,
                    Description = p.Description,
                    Size_ID = p.Size_ID,
                    Style_ID = p.Style_ID,
                    Stock = p.Stock,
                    CreatedBy = p.CreatedBy
                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/Products
        [HttpPost("PostProduct")]
        public async Task<IActionResult> PostProduct(ProductCreateDTO productDTO)
        {
            var product = new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Size_ID = productDTO.Size_ID,
                Style_ID = productDTO.Style_ID,
                Stock = productDTO.Stock,
                CreatedBy = productDTO.CreatedBy
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            foreach (CreateProductDetailDTO detail in productDTO.productDetailDTOs)
            {
                var productDetail = new ProductDetail
                {
                    Product_ID = product.ID,
                    RawMaterial_ID = detail.RawMaterial_ID,
                    Quantity = detail.Quantity
                };
                _context.ProductDetails.Add(productDetail);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("OldPostProduct")]
        public async Task<IActionResult> OldPostProduct(ProductCreateDTO2 productDTO) //old version
        {
            var product = new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Size_ID = productDTO.Size_ID,
                Style_ID = productDTO.Style_ID,
                Stock = productDTO.Stock,
                CreatedBy = productDTO.CreatedBy
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ID }, product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductCreateDTO productDTO)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ID == id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            product.Size_ID = productDTO.Size_ID;
            product.Style_ID = productDTO.Style_ID;
            product.Stock = productDTO.Stock;
            product.CreatedBy = productDTO.CreatedBy;

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}