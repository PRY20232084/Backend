namespace PRY20232084.Models.Entities
{
    public class ProductDetail
    {
        public int Product_ID { get; set; }
        public int RawMaterial_ID { get; set; }
        public decimal Quantity { get; set; }
        public Product ProductRef { get; set; }
        public RawMaterial RawMaterialRef { get; set; }
    }
}
