using System.ComponentModel.DataAnnotations;

namespace PRY20232084.Models.DTOs
{
    public class ProductSizeRequestDTO
    {
        [Required]
        public string Name { get; set; }
        public string CreatedBy { get; set; }
    }
}
