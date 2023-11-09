using System.ComponentModel.DataAnnotations;

namespace PRY20232084.Models.Entities
{
    public class Movement
    {
        [Key]
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime BoughtDate { get; set; }
        public bool MovementType { get; set; }
        public bool RegisterType { get; set; }
        public string CreatedBy { get; set; }

        public ApplicationUser CreatedByUser { get; set; }
        public List<ProductMovementDetail> ProductMovementDetails { get; set; }
        public List<RawMaterialMovementDetail> RawMaterialMovementDetails { get; set; }
    }
}
