using System.ComponentModel.DataAnnotations;

namespace PRY20232084.Models.Entities
{
    public class ProductMovementDetail
    {
        [Key]
        public int ID { get; set; }
        public int Product_ID { get; set; }
        public int Movement_ID { get; set; }
        public int Quantity { get; set; }
        public Product ProductRef { get; set; } = null!;
        public Movement MovementRef { get; set; } = null!;
    }
}
