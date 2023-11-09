using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PRY20232084.Models.Entities
{
    public class RawMaterial
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
        public string Color { get; set; }
        public decimal Stock { get; set; }
        public int MeasurementUnit_ID { get; set; }
        public string CreatedBy { get; set; }

        public ApplicationUser CreatedByUser { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
        public List<ProductDetail> ProductDetails { get; set; }
        public List<RawMaterialMovementDetail> RawMaterialMovementDetails { get; set; }
    }
}
