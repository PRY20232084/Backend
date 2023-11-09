using System.ComponentModel.DataAnnotations;

namespace PRY20232084.Models.Entities
{
    public class MeasurementUnitCreateDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Abbreviation { get; set; }

        [Required]
        public string CreatedBy { get; set; }
    }
}
