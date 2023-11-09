using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PRY20232084.Models.Entities
{
    public class MeasurementUnit
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string CreatedBy { get; set; }

        public ApplicationUser CreatedByUser { get; set; }

        public List<RawMaterial> RawMaterials { get; set; }
    }
}
