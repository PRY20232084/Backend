using PRY20232084.DTOs;

namespace PRY20232084.Models.DTOs
{
    public class UpdateProductMovementDTO
	{
		public string Description { get; set; }
		public DateTime BoughtDate { get; set; }
		public bool MovementType { get; set; }
		public bool RegisterType { get; set; }
		public string CreatedBy { get; set; }
		public CreateProductMovementDetailDTO productMovementDetailDTO { get; set; } //Send movement id as 0
	}
}
