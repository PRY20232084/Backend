using System.ComponentModel.DataAnnotations;

namespace PRY20232084.Models.Entities
{
    public class RawMaterialMovementDetail
    {
        [Key]
        public int ID { get; set; }
        public int RawMaterial_ID { get; set; }
        public int Movement_ID { get; set; }
        public decimal Quantity { get; set; }
        public RawMaterial RawMaterialRef { get; set; } = null!;
        public Movement MovementRef { get; set; } = null!;
    }
}
