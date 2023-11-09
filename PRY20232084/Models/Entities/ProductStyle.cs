using System.ComponentModel.DataAnnotations;

namespace PRY20232084.Models.Entities
{
    public class ProductStyle
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }

        public ApplicationUser CreatedByUser { get; set; }
        public List<Product> Products { get; set; }
    }
}
