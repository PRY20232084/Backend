using Microsoft.AspNetCore.Identity;
using PRY20232084.Models.Entities;

namespace PRY20232084.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool UserType { get; set; }
        public string Email { get; set; }
        public string Enterprise { get; set; }
        public DateTime RegisterDate { get; set; }

        public List<Movement> Movements { get; set; }
        public List<RawMaterial> RawMaterials { get; set; }
        public List<Product> Products { get; set; }
        public List<MeasurementUnit> MeasurementUnits { get; set; }
        public List<ProductStyle> Styles { get; set; }
        public List<ProductSize> Sizes { get; set; }
    }
}
