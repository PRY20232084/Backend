using System.ComponentModel.DataAnnotations;

namespace PRY20232084.DTOs
{
    public class MovementRequestDTO
    {
        public string Description { get; set; }
        public DateTime BoughtDate { get; set; }
        public bool MovementType { get; set; }
        public bool RegisterType { get; set; }
        public string CreatedBy { get; set; }
    }
}