using System.ComponentModel.DataAnnotations;

namespace PRY20232084.DTOs
{
    public class ProductCreateDTO
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int Size_ID { get; set; }

        [Required]
        public int Style_ID { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public string CreatedBy { get; set; }
    }
}
