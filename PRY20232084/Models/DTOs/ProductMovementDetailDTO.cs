using System.ComponentModel.DataAnnotations;

namespace PRY20232084.DTOs
{
    public class ProductMovementDetailDTO
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int MovementID { get; set; }
        public int Quantity { get; set; }
    }
}
