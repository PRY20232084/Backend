using System.ComponentModel.DataAnnotations;

namespace PRY20232084.Models.DTOs
{
    public class ProductStyleRequestDTO
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
    }
}
