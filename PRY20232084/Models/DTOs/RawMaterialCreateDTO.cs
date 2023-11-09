using System.ComponentModel.DataAnnotations;

namespace PRY20232084.DTOs
{
    public class RawMaterialCreateDTO
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string BrandName { get; set; }

        public string Color { get; set; }

        [Required]
        public decimal Stock { get; set; }

        [Required]
        public int MeasurementUnit_ID { get; set; }

        [Required]
        public string CreatedBy { get; set; }
    }
}
