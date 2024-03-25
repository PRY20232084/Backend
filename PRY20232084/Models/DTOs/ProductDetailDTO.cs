using System.ComponentModel.DataAnnotations;

namespace PRY20232084.Models.DTOs
{
    public class ProductDetailDTO
    {
        public int Product_ID { get; set; }
        public int RawMaterial_ID { get; set; }
        public decimal Quantity { get; set; }
    }

	public class CreateProductDetailDTO
	{
		public int RawMaterial_ID { get; set; }
		public decimal Quantity { get; set; }
	}
}
