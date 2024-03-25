using PRY20232084.Models.DTOs;

namespace PRY20232084.DTOs
{
    public class MovementResponseDTO
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime BoughtDate { get; set; }
        public bool MovementType { get; set; }
        public bool RegisterType { get; set; }
        public string CreatedBy { get; set; }
    }

	public class RawMaterialMovementDTO
	{
		public int ID { get; set; }
		public string Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime BoughtDate { get; set; }
		public bool MovementType { get; set; }
		public bool RegisterType { get; set; }
		public string CreatedBy { get; set; }
		public RawMaterialMovementDetailRequestDTO rawMaterialDetailDTO { get; set; }
	}

	public class ProductMovementDTO
	{
		public int ID { get; set; }
		public string Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime BoughtDate { get; set; }
		public bool MovementType { get; set; }
		public bool RegisterType { get; set; }
		public string CreatedBy { get; set; }
		public ProductMovementDetailDTO productDetailDTO { get; set; }
	}
}
