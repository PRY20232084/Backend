using System.ComponentModel.DataAnnotations;

namespace PRY20232084.Models.Entities
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Size_ID { get; set; }
        public int Style_ID { get; set; }
        public int Stock { get; set; }
        public string CreatedBy { get; set; }

        public ApplicationUser CreatedByUser { get; set; }
        public ProductSize SizeRef { get; set; }
        public ProductStyle StyleRef { get; set; }
        public List<ProductDetail> ProductDetails { get; set; }
        public List<ProductMovementDetail> ProductMovementDetails { get; set; }
    }
}
