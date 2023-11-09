using System.ComponentModel.DataAnnotations;

namespace PRY20232084.Models.DTOs
{
    public class ProductDetailDTO
    {
        [Required]
        public int Product_ID { get; set; }

        [Required]
        public int RawMaterial_ID { get; set; }

        [Required]
        public decimal Quantity { get; set; }
    }
}
